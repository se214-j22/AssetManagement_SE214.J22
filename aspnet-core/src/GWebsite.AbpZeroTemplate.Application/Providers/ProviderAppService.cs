using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Providers;
using GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Providers
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ProviderAppService : GWebsiteAppServiceBase, IProviderAppService
    {
        private readonly IRepository<Provider> providerRepository;

        public ProviderAppService(IRepository<Provider> providerRepository)
        {
            this.providerRepository = providerRepository;
        }

        #region Public Method

        public void CreateOrEditProvider(ProviderInput providerInput)
        {
            if (providerInput.Id == 0)
            {
                Create(providerInput);
            }
            else
            {
                Update(providerInput);
            }
        }

        public void DeleteProvider(int id)
        {
            var providerEntity = providerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (providerEntity != null)
            {
                providerEntity.IsDelete = true;
                providerRepository.Update(providerEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ProviderInput GetProviderForEdit(int id)
        {
            var providerEntity = providerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (providerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ProviderInput>(providerEntity);
        }

        public ProviderForViewDto GetProviderForView(int id)
        {
            var providerEntity = providerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (providerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ProviderForViewDto>(providerEntity);
        }

        public PagedResultDto<ProviderDto> GetProviders(ProviderFilter input)
        {
            var query = providerRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.Name != null)
            {
                query = query.Where(x => x.Name.ToLower().Equals(input.Name));
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
            return new PagedResultDto<ProviderDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ProviderDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ProviderInput providerInput)
        {
            var providerEntity = ObjectMapper.Map<Provider>(providerInput);
            SetAuditInsert(providerEntity);
            providerRepository.Insert(providerEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ProviderInput providerInput)
        {
            var providerEntity = providerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == providerInput.Id);
            if (providerEntity == null)
            {
            }
            ObjectMapper.Map(providerInput, providerEntity);
            SetAuditEdit(providerEntity);
            providerRepository.Update(providerEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}