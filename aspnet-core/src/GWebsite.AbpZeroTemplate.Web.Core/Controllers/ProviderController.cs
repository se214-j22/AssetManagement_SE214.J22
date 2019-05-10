using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Providers;
using GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProviderController : GWebsiteControllerBase
    {
        private readonly IProviderAppService providerAppService;

        public ProviderController(IProviderAppService providerAppService)
        {
            this.providerAppService = providerAppService;
        }

        [HttpGet]
        public PagedResultDto<ProviderDto> GetProvidersByFilter(ProviderFilter providerFilter)
        {
            return providerAppService.GetProviders(providerFilter);
        }

        [HttpGet]
        public ProviderInput GetProviderForEdit(int id)
        {
            return providerAppService.GetProviderForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditProvider([FromBody] ProviderInput input)
        {
            providerAppService.CreateOrEditProvider(input);
        }

        [HttpDelete("{id}")]
        public void DeleteProvider(int id)
        {
            providerAppService.DeleteProvider(id);
        }

        [HttpGet]
        public ProviderForViewDto GetProviderForView(int id)
        {
            return providerAppService.GetProviderForView(id);
        }
    }
}
