using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abp.UI;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ManufacturerController : GWebsiteControllerBase
    {
        private readonly IManufacturerAppService manufacturerAppService;

        public ManufacturerController(IManufacturerAppService manufacturerAppService)
        {
            this.manufacturerAppService = manufacturerAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<ManufacturerDto>> GetByFilter(ManufacturerFilter filter)
        {
            return await manufacturerAppService.GetsForView(filter);
        }

        [HttpGet]
        public async Task<ManufacturerDto> GetById(int id)
        {
            return await manufacturerAppService.GetAsyncForView(id);
        }

        [HttpGet]
        public async Task<ManufacturerDto> GetByCode(string code)
        {
            return await manufacturerAppService.GetAsyncForView(code);
        }

        [HttpGet]
        public async Task<ManufacturerInput> GetForEdit(int id)
        {
            return await manufacturerAppService.GetAsyncForEdit(id);
        }

        [HttpPost]
        public async Task CreateOrEdit([FromBody] ManufacturerInput input)
        {
            await manufacturerAppService.CreateOrEdit(input);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (await manufacturerAppService.HasAnyRecordsPointTo(id))
                throw new UserFriendlyException("Ooppps! Your request was bad!", "This record has any records point to! Please destruct them!");
            await manufacturerAppService.DeleteAsync(id);
        }
    }
}
