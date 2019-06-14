using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung
{
    public interface ICustomer_QuanLyCongTrinhXayDungAppService
    {
        void CreateOrEditCustomer(CustomerInput_QuanLyCongTrinhXayDung customerInput);
        CustomerInput_QuanLyCongTrinhXayDung GetCustomerForEdit(int id);
        void DeleteCustomer(int id);
        PagedResultDto<CustomerDto_QuanLyCongTrinhXayDung> GetCustomers(CustomerFilter_QuanLyCongTrinhXayDung input);
        CustomerForViewDto_QuanLyCongTrinhXayDung GetCustomerForView(int id);
    }
}
