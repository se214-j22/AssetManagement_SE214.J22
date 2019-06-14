using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Customer_QuanLyCongTrinhXayDungController : GWebsiteControllerBase
    {
        private readonly ICustomer_QuanLyCongTrinhXayDungAppService customerAppService;

        public Customer_QuanLyCongTrinhXayDungController(ICustomer_QuanLyCongTrinhXayDungAppService customerAppService)
        {
            this.customerAppService = customerAppService;
        }

        [HttpGet]
        public PagedResultDto<CustomerDto_QuanLyCongTrinhXayDung> GetCustomersByFilter(CustomerFilter_QuanLyCongTrinhXayDung customerFilter)
        {
            return customerAppService.GetCustomers(customerFilter);
        }

        [HttpGet]
        public CustomerInput_QuanLyCongTrinhXayDung GetCustomerForEdit(int id)
        {
            return customerAppService.GetCustomerForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCustomer([FromBody] CustomerInput_QuanLyCongTrinhXayDung input)
        {
            customerAppService.CreateOrEditCustomer(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            customerAppService.DeleteCustomer(id);
        }

        [HttpGet]
        public CustomerForViewDto_QuanLyCongTrinhXayDung GetCustomerForView(int id)
        {
            return customerAppService.GetCustomerForView(id);
        }
    }
}
