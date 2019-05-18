using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.AssetTypes
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset)]
    public class AssetTypeAppService : GWebsiteAppServiceBase, IAssetTypeAppService
    {
        private readonly IRepository<AssetType> assetTypeRepository;

        public AssetTypeAppService(IRepository<AssetType> assetTypeRepository)
        {
            this.assetTypeRepository = assetTypeRepository;
        }

        public async Task<AssetTypeDto> GetAsyncForView(int id)
        {
                var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefault(x => x.Id == id);
                if (assetTypeEntity == null)
                {
                    return null;
                }
            return ObjectMapper.Map<AssetTypeDto>(assetTypeEntity);
        }

        public async Task<AssetTypeDto> GetAsyncForView(string code)
        {
            var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefault(x => x.Code == code);
            if (assetTypeEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetTypeDto>(assetTypeEntity);
        }
        public async Task<AssetType> GetAsyncForEdit(string code)
        {
            var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Code == code);
            if (assetTypeEntity == null)
            {
                return null;
            }
            return assetTypeEntity;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Create_Edit)]
        public async Task CreateOrEdit(AssetTypeInput assetTypeInput)
        {
            if (assetTypeInput.Id == 0)
            {
                await CreateAsync(assetTypeInput);
            }
            else
            {
                await UpdateAsync(assetTypeInput);
            }
        }
        private async Task CreateAsync(AssetTypeInput assetTypeInput)
        {
            var assetTypeEntity = ObjectMapper.Map<AssetType>(assetTypeInput);
            SetAuditInsert(assetTypeEntity);
            await assetTypeRepository.InsertAsync(assetTypeEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task UpdateAsync(AssetTypeInput assetTypeInput)
        {
            var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetTypeInput.Id);
            if (assetTypeEntity == null)
            {
            }
            ObjectMapper.Map(assetTypeInput, assetTypeEntity);
            SetAuditEdit(assetTypeEntity);
            await assetTypeRepository.UpdateAsync(assetTypeEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}