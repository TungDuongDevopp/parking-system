using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Authorization;
using ParkingSystem.Controllers;
using ParkingSystem.Customers;
using ParkingSystem.Web.Models.Customers;
using System.Threading.Tasks;

namespace ParkingSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerController : ParkingSystemControllerBase
    {
        private readonly ICustomerAppService _customerService;
        public CustomerController(ICustomerAppService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index()

        {
            return View();
        }
        public async Task<ActionResult> EditModal(long customerId)
        {
            var customer = await _customerService.GetAsync(new EntityDto<long>(customerId));
            var model = new EditCustomerViewModel
            {
                Customer = customer
            };
            return PartialView("_EditModal", model);
        }
    }
}
