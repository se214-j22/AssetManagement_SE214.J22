using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Customer_SuaChuaController : GWebsiteControllerBase
    {
        private readonly ICustomer_SuaChuaAppService customerAppService;

        public Customer_SuaChuaController(ICustomer_SuaChuaAppService customerAppService)
        {
            this.customerAppService = customerAppService;
        }

        [HttpGet]
        public PagedResultDto<CustomerDto_SuaChua> GetCustomersByFilter(CustomerFilter_SuaChua customerFilter)
        {
            return customerAppService.GetCustomers(customerFilter);
        }

        [HttpGet]
        public CustomerInput_SuaChua GetCustomerForEdit(int id)
        {
            return customerAppService.GetCustomerForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCustomer([FromBody] CustomerInput_SuaChua input)
        {
            customerAppService.CreateOrEditCustomer(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            customerAppService.DeleteCustomer(id);
        }

        [HttpGet]
        public CustomerForViewDto_SuaChua GetCustomerForView(int id)
        {
            return customerAppService.GetCustomerForView(id);
        }
    }
}
