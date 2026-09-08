using Abp.AspNetCore.Mvc.Authorization;
using ParkingSystem.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ParkingSystem.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : ParkingSystemControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
