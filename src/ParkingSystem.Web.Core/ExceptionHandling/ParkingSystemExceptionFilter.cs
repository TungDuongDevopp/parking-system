using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Web.Configuration;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using ParkingSystem.Exceptions;

namespace ParkingSystem.Web.ExceptionHandling
{
    /// <summary>
    /// Exception filter kế thừa từ AbpExceptionFilter để bảo toàn toàn bộ pipeline xử lý ngoại lệ,
    /// logging, validation, authorization, event bus và AjaxResponse của ABP, đồng thời mở rộng
    /// để hỗ trợ trả về đúng mã HTTP (như 409 Conflict, 404 NotFound) khi exception implement IHasHttpStatusCode.
    /// </summary>
    public class ParkingSystemExceptionFilter : AbpExceptionFilter
    {
        public ParkingSystemExceptionFilter(
            IErrorInfoBuilder errorInfoBuilder,
            IAbpAspNetCoreConfiguration configuration,
            IAbpWebCommonModuleConfiguration abpWebCommonModuleConfiguration)
            : base(errorInfoBuilder, configuration, abpWebCommonModuleConfiguration)
        {
        }

        protected override int GetStatusCode(ExceptionContext context, bool wrapOnError)
        {
            if (context.Exception is IHasHttpStatusCode hasStatusCode)
            {
                return (int)hasStatusCode.StatusCode;
            }

            return base.GetStatusCode(context, wrapOnError);
        }
    }
}
