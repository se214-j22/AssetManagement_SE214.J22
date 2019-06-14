using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Customers_QuanLyCongTrinhXayDung
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class Customer_QuanLyCongTrinhXayDungAppService : GWebsiteAppServiceBase, ICustomer_QuanLyCongTrinhXayDungAppService
    {
        private readonly IRepository<Customer_QuanLyCongTrinhXayDung> customerRepository;

        public Customer_QuanLyCongTrinhXayDungAppService(IRepository<Customer_QuanLyCongTrinhXayDung> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        #region Public Method

        public void CreateOrEditCustomer(CustomerInput_QuanLyCongTrinhXayDung customerInput)
        {
            if (customerInput.Id == 0)
            {
                Create(customerInput);
            }
            else
            {
                Update(customerInput);
            }
        }

        public void DeleteCustomer(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity != null)
            {
                customerEntity.IsDelete = true;
                customerRepository.Update(customerEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public CustomerInput_QuanLyCongTrinhXayDung GetCustomerForEdit(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CustomerInput_QuanLyCongTrinhXayDung>(customerEntity);
        }

        public CustomerForViewDto_QuanLyCongTrinhXayDung GetCustomerForView(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CustomerForViewDto_QuanLyCongTrinhXayDung>(customerEntity);
        }

        public PagedResultDto<CustomerDto_QuanLyCongTrinhXayDung> GetCustomers(CustomerFilter_QuanLyCongTrinhXayDung input)
        {
            var query = customerRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.maBDS != null)
            {
                query = query.Where(x => x.maBDS.ToLower().Equals(input.maBDS));
            }

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<CustomerDto_QuanLyCongTrinhXayDung>(
                totalCount,
                items.Select(item => ObjectMapper.Map<CustomerDto_QuanLyCongTrinhXayDung>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(CustomerInput_QuanLyCongTrinhXayDung customerInput)
        {
            var customerEntity = ObjectMapper.Map<Customer_QuanLyCongTrinhXayDung>(customerInput);
            SetAuditInsert(customerEntity);
            customerRepository.Insert(customerEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(CustomerInput_QuanLyCongTrinhXayDung customerInput)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == customerInput.Id);
            if (customerEntity == null)
            {
            }
            ObjectMapper.Map(customerInput, customerEntity);
            SetAuditEdit(customerEntity);
            customerRepository.Update(customerEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }

   
}
