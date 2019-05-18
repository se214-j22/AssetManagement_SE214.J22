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
    public class ManufacturerAppService : GWebsiteAppServiceBase, IManufacturerAppService
    {
        private readonly IRepository<Manufacturer> manufacturerRepository;

        public ManufacturerAppService(IRepository<Manufacturer> manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task<ManufacturerDto> GetAsyncForView(int id)
        {
                var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefault(x => x.Id == id);
                if (manufacturerEntity == null)
                {
                    return null;
                }
            return ObjectMapper.Map<ManufacturerDto>(manufacturerEntity);
        }

        public async Task<ManufacturerDto> GetAsyncForView(string code)
        {
            var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefault(x => x.Code == code);
            if (manufacturerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ManufacturerDto>(manufacturerEntity);
        }
        public async Task<Manufacturer> GetAsyncForEdit(string code)
        {
            var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Code == code);
            if (manufacturerEntity == null)
            {
                return null;
            }
            return manufacturerEntity;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Create_Edit)]
        public async Task CreateOrEdit(ManufacturerInput manufacturerInput)
        {
            if (manufacturerInput.Id == 0)
            {
                await CreateAsync(manufacturerInput);
            }
            else
            {
                await UpdateAsync(manufacturerInput);
            }
        }

        private async Task CreateAsync(ManufacturerInput manufacturerInput)
        {
            var manufacturerEntity = ObjectMapper.Map<Manufacturer>(manufacturerInput);
            SetAuditInsert(manufacturerEntity);
            await manufacturerRepository.InsertAsync(manufacturerEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task UpdateAsync(ManufacturerInput manufacturerInput)
        {
            var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == manufacturerInput.Id);
            if (manufacturerEntity == null)
            {
            }
            ObjectMapper.Map(manufacturerInput, manufacturerEntity);
            SetAuditEdit(manufacturerEntity);
            await manufacturerRepository.UpdateAsync(manufacturerEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}