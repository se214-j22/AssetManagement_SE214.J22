using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhongBan;
using GWebsite.AbpZeroTemplate.Application.Share.PhongBan.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhongBanController : GWebsiteControllerBase
    {
        private readonly IPhongBanAppService phongBanAppService;

        public PhongBanController(IPhongBanAppService phongBanAppService)
        {
            this.phongBanAppService = phongBanAppService;
        }

        [HttpGet]
        public PagedResultDto<PhongBanDto> GetPhongBansByFilter(PhongBanFilter phongBanFilter)
        {
            return phongBanAppService.GetPhongBans(phongBanFilter);
        }

        [HttpGet]
        public PhongBanInput GetPhongBanForEdit(int id)
        {
            return phongBanAppService.GetPhongBanForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditPhongBan([FromBody] PhongBanInput input)
        {
            phongBanAppService.CreateOrEditPhongBan(input);
        }

        [HttpDelete("{id}")]
        public void DeletePhongBan(int id)
        {
            phongBanAppService.DeletePhongBan(id);
        }

        [HttpGet]
        public PhongBanForViewDto GetPhongBanForView(int id)
        {
            return phongBanAppService.GetPhongBanForView(id);
        }
    }
}