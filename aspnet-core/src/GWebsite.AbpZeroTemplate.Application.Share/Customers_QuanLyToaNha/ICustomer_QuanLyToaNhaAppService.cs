using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha
{
    public interface ICustomer_QuanLyToaNhaAppService
    {
        void CreateOrEditCustomer(CustomerInput_QuanLyToaNha customerInput);
        CustomerInput_QuanLyToaNha GetCustomerForEdit(int id);
        void DeleteCustomer(int id);
        PagedResultDto<CustomerDto_QuanLyToaNha> GetCustomers(CustomerFilter_QuanLyToaNha input);
        CustomerForViewDto_QuanLyToaNha GetCustomerForView(int id);
    }
}
