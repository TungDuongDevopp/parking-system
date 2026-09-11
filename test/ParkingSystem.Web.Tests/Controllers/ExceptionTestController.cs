using System;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ParkingSystem.Web.Tests.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ExceptionTestController : AbpController
    {
        [HttpGet]
        [WrapResult]
        public string ThrowUnexpectedException()
        {
            throw new InvalidOperationException("Sensitive internal database connection string failed");
        }
    }
}
