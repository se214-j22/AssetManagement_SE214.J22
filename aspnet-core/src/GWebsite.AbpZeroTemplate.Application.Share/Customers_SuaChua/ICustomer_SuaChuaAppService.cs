using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua
{
    public interface ICustomer_SuaChuaAppService
    {
        void CreateOrEditCustomer(CustomerInput_SuaChua customerInput);
        CustomerInput_SuaChua GetCustomerForEdit(int id);
        void DeleteCustomer(int id);
        PagedResultDto<CustomerDto_SuaChua> GetCustomers(CustomerFilter_SuaChua input);
        CustomerForViewDto_SuaChua GetCustomerForView(int id);
    }
}
