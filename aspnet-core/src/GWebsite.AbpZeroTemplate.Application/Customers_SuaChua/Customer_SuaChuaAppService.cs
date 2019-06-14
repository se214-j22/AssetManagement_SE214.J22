using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Customers_SuaChua
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class Customer_SuaChuaAppService : GWebsiteAppServiceBase, ICustomer_SuaChuaAppService
    {
        private readonly IRepository<Customer_SuaChua> customerRepository;

        public Customer_SuaChuaAppService(IRepository<Customer_SuaChua> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        #region Public Method

        public void CreateOrEditCustomer(CustomerInput_SuaChua customerInput)
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

        public CustomerInput_SuaChua GetCustomerForEdit(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CustomerInput_SuaChua>(customerEntity);
        }

        public CustomerForViewDto_SuaChua GetCustomerForView(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CustomerForViewDto_SuaChua>(customerEntity);
        }

        public PagedResultDto<CustomerDto_SuaChua> GetCustomers(CustomerFilter_SuaChua input)
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
            return new PagedResultDto<CustomerDto_SuaChua>(
                totalCount,
                items.Select(item => ObjectMapper.Map<CustomerDto_SuaChua>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(CustomerInput_SuaChua customerInput)
        {
            var customerEntity = ObjectMapper.Map<Customer_SuaChua>(customerInput);
            SetAuditInsert(customerEntity);
            customerRepository.Insert(customerEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(CustomerInput_SuaChua customerInput)
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
