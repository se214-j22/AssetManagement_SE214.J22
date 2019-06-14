using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Customer_QuanLyToaNhaController : GWebsiteControllerBase
    {
        private readonly ICustomer_QuanLyToaNhaAppService customerAppService;

        public Customer_QuanLyToaNhaController(ICustomer_QuanLyToaNhaAppService customerAppService)
        {
            this.customerAppService = customerAppService;
        }

        [HttpGet]
        public PagedResultDto<CustomerDto_QuanLyToaNha> GetCustomersByFilter(CustomerFilter_QuanLyToaNha customerFilter)
        {
            return customerAppService.GetCustomers(customerFilter);
        }

        [HttpGet]
        public CustomerInput_QuanLyToaNha GetCustomerForEdit(int id)
        {
            return customerAppService.GetCustomerForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCustomer([FromBody] CustomerInput_QuanLyToaNha input)
        {
            customerAppService.CreateOrEditCustomer(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            customerAppService.DeleteCustomer(id);
        }

        [HttpGet]
        public CustomerForViewDto_QuanLyToaNha GetCustomerForView(int id)
        {
            return customerAppService.GetCustomerForView(id);
        }
    }
}
