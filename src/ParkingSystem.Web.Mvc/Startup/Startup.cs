using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Microsoft.OpenApi.Models;
using ParkingSystem.Authentication.JwtBearer;
using ParkingSystem.Configuration;
using ParkingSystem.Identity;
using ParkingSystem.Web.ExceptionHandling;
using ParkingSystem.Web.Resources;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;


namespace ParkingSystem.Web.Startup;

public class Startup
{
    private const string _apiVersion = "v1";

    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IConfigurationRoot _appConfiguration;

    public Startup(IWebHostEnvironment env)
    {
        _hostingEnvironment = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // MVC
        services.AddControllersWithViews(
                options =>
                {
                    // NOTE: Chỉ dùng AbpAutoValidateAntiforgeryTokenAttribute.
                    // AutoValidateAntiforgeryTokenAttribute của ASP.NET Core sẽ chặn TẤT CẢ POST
                    // không có antiforgery token (bao gồm Swagger/JWT API calls) → trả HTTP 400
                    // trước khi request đến controller.
                    // AbpAutoValidateAntiforgery đã tích hợp logic tương tự nhưng bỏ qua API routes.
                    options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                }
            );

        services.PostConfigure<MvcOptions>(options =>
        {
            var abpFilter = options.Filters.FirstOrDefault(x =>
                x is ServiceFilterAttribute sf && sf.ServiceType == typeof(AbpExceptionFilter));

            if (abpFilter != null)
            {
                var index = options.Filters.IndexOf(abpFilter);
                options.Filters.RemoveAt(index);
                options.Filters.Insert(index, new ServiceFilterAttribute(typeof(ParkingSystemExceptionFilter)));
            }
        });

        services.AddTransient<ParkingSystemExceptionFilter>();

        IdentityRegistrar.Register(services);
        AuthConfigurer.Configure(services, _appConfiguration);

        services.Configure<WebEncoderOptions>(options =>
        {
            options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
        });

        services.AddScoped<IWebResourceManager, WebResourceManager>();

        services.AddSignalR();

        // Swagger
        ConfigureSwagger(services);

        // Configure Abp and Dependency Injection
        services.AddAbpWithoutCreatingServiceProvider<ParkingSystemWebMvcModule>(
            // Configure Log4Net logging
            options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(
                    _hostingEnvironment.IsDevelopment()
                        ? "log4net.config"
                        : "log4net.Production.config"
                    )
            )
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseAbp(); // Initializes ABP framework.

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseJwtTokenMiddleware();

        app.UseAuthorization();

        // Enable middleware to serve generated Swagger as a JSON endpoint
        app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

        // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"ParkingSystem API {_apiVersion}");
            options.DisplayRequestDuration();
        }); // URL: /swagger

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<AbpCommonHub>("/signalr");
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
        });
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(_apiVersion, new OpenApiInfo
            {
                Version = _apiVersion,
                Title = "ParkingSystem API",
                Description = "ParkingSystem Web API Documentation",
            });

            // Include only actions that have an explicit HTTP method to avoid ambiguous MVC view actions
            // without a verb (like Error403) from being included in the Swagger spec.
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                // apiDesc.HttpMethod is a string like "GET" or "POST"; if null, skip the action.
                return !string.IsNullOrEmpty(apiDesc.HttpMethod);
            });

            options.CustomSchemaIds(type => type.FullName);

            // Define the BearerAuth scheme
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            // Add Security Requirement for Swagger UI Authorize button
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Add XML summaries to swagger
            bool canShowSummaries = _appConfiguration.GetValue<bool>("Swagger:ShowSummaries");
            if (canShowSummaries)
            {
                var hostXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var hostXmlPath = Path.Combine(AppContext.BaseDirectory, hostXmlFile);
                if (File.Exists(hostXmlPath))
                {
                    options.IncludeXmlComments(hostXmlPath);
                }

                var applicationXml = "ParkingSystem.Application.xml";
                var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXml);
                if (File.Exists(applicationXmlPath))
                {
                    options.IncludeXmlComments(applicationXmlPath);
                }

                var webCoreXmlFile = "ParkingSystem.Web.Core.xml";
                var webCoreXmlPath = Path.Combine(AppContext.BaseDirectory, webCoreXmlFile);
                if (File.Exists(webCoreXmlPath))
                {
                    options.IncludeXmlComments(webCoreXmlPath);
                }
            }
        });
    }
}
