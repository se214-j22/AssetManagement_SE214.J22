using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CTTaiSanController : GWebsiteControllerBase
    {
        private readonly ICTTaiSanAppService cTTaiSanAppService;

        public CTTaiSanController(ICTTaiSanAppService cTTaiSanAppService)
        {
            this.cTTaiSanAppService = cTTaiSanAppService;
        }

        [HttpGet]
        public PagedResultDto<CTTaiSanDto> GetCTTaiSansByFilter(CTTaiSanFilter cTTaiSanFilter)
        {
            return cTTaiSanAppService.GetCTTaiSans(cTTaiSanFilter);
        }

        [HttpGet]
        public CTTaiSanInput GetCTTaiSanForEdit(int id)
        {
            return cTTaiSanAppService.GetCTTaiSanForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCTTaiSan([FromBody] CTTaiSanInput input)
        {
            cTTaiSanAppService.CreateOrEditCTTaiSan(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCTTaiSan(int id)
        {
            cTTaiSanAppService.DeleteCTTaiSan(id);
        }

        [HttpGet]
        public CTTaiSanForViewDto GetCTTaiSanForView(int id)
        {
            return cTTaiSanAppService.GetCTTaiSanForView(id);
        }
    }
}
