using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Customers_QuanLyToaNha
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class Customer_QuanLyToaNhaAppService : GWebsiteAppServiceBase, ICustomer_QuanLyToaNhaAppService
    {
        private readonly IRepository<Customer_QuanLyToaNha> customerRepository;

        public Customer_QuanLyToaNhaAppService(IRepository<Customer_QuanLyToaNha> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        #region Public Method

        public void CreateOrEditCustomer(CustomerInput_QuanLyToaNha customerInput)
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

        public CustomerInput_QuanLyToaNha GetCustomerForEdit(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CustomerInput_QuanLyToaNha>(customerEntity);
        }

        public CustomerForViewDto_QuanLyToaNha GetCustomerForView(int id)
        {
            var customerEntity = customerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (customerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CustomerForViewDto_QuanLyToaNha>(customerEntity);
        }

        public PagedResultDto<CustomerDto_QuanLyToaNha> GetCustomers(CustomerFilter_QuanLyToaNha input)
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
            return new PagedResultDto<CustomerDto_QuanLyToaNha>(
                totalCount,
                items.Select(item => ObjectMapper.Map<CustomerDto_QuanLyToaNha>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(CustomerInput_QuanLyToaNha customerInput)
        {
            var customerEntity = ObjectMapper.Map<Customer_QuanLyToaNha>(customerInput);
            SetAuditInsert(customerEntity);
            customerRepository.Insert(customerEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(CustomerInput_QuanLyToaNha customerInput)
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
