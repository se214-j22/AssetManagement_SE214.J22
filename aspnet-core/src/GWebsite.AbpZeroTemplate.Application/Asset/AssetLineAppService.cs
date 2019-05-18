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

namespace GWebsite.AbpZeroTemplate.Web.Core.Assets
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset)]
    public class AssetLineAppService : GWebsiteAppServiceBase, IAssetLineAppService
    {
        private readonly IRepository<AssetLine> assetLineRepository;

        public AssetLineAppService(IRepository<AssetLine> assetLineRepository)
        {
            this.assetLineRepository = assetLineRepository;
        }

        public async Task<AssetLineDto> GetAsyncForView(int id)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
                .Include(b => b.AssetType).Include(b => b.Manufacturer).SingleOrDefault(x => x.Id == id)
                ;
            if (assetLineEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetLineDto>(assetLineEntity);
        }

        public async Task<AssetLineDto> GetAsyncForView(string code)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefault(x => x.Code == code);
            if (assetLineEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetLineDto>(assetLineEntity);
        }
        public async Task<AssetLine> GetAsyncForEdit(string code)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Code == code);
            if (assetLineEntity == null)
            {
                return null;
            }
            return assetLineEntity;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Create_Edit)]
        public async Task CreateOrEdit(AssetLineInput assetLineInput)
        {
            if (assetLineInput.Id == 0)
            {
                await CreateAsync(assetLineInput);
            }
            else
            {
                await UpdateAsync(assetLineInput);
            }
        }
        private async Task CreateAsync(AssetLineInput assetLineInput)
        {
            var assetLineEntity = ObjectMapper.Map<AssetLine>(assetLineInput);
            SetAuditInsert(assetLineEntity);
            await assetLineRepository.InsertAsync(assetLineEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task UpdateAsync(AssetLineInput assetLineInput)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetLineInput.Id);
            if (assetLineEntity == null)
            {
            }
            ObjectMapper.Map(assetLineInput, assetLineEntity);
            SetAuditEdit(assetLineEntity);
            await assetLineRepository.UpdateAsync(assetLineEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}