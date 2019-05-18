using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BangYeuCauCungCapTaiSanController : GWebsiteControllerBase
    {
        private readonly IBangYeuCauCungCapTaiSanAppService bangYeuCauCungCapTaiSanAppService;

        public BangYeuCauCungCapTaiSanController(IBangYeuCauCungCapTaiSanAppService bangYeuCauCungCapTaiSanAppService)
        {
            this.bangYeuCauCungCapTaiSanAppService = bangYeuCauCungCapTaiSanAppService;
        }

        [HttpGet]
        public PagedResultDto<BangYeuCauCungCapTaiSanOutput> GetBangYeuCauCungCapTaiSansByFilter(BangYeuCauCungCapTaiSanFilter bangYeuCauCungCapTaiSanFilter)
        {
            return bangYeuCauCungCapTaiSanAppService.GetBangYeuCauCungCapTaiSans(bangYeuCauCungCapTaiSanFilter);
        }

        [HttpGet]
        public BangYeuCauCungCapTaiSanInput GetBangYeuCauCungCapTaiSanForEdit(int id)
        {
            return bangYeuCauCungCapTaiSanAppService.GetBangYeuCauCungCapTaiSanForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditBangYeuCauCungCapTaiSan([FromBody] BangYeuCauCungCapTaiSanInput input)
        {
            bangYeuCauCungCapTaiSanAppService.CreateOrEditBangYeuCauCungCapTaiSan(input);
        }

        [HttpDelete("{id}")]
        public void DeleteBangYeuCauCungCapTaiSan(int id)
        {
            bangYeuCauCungCapTaiSanAppService.DeleteBangYeuCauCungCapTaiSan(id);
        }

        [HttpGet]
        public BangYeuCauCungCapTaiSanForViewDto GetBangYeuCauCungCapTaiSanForView(int id)
        {
            return bangYeuCauCungCapTaiSanAppService.GetBangYeuCauCungCapTaiSanForView(id);
        }
    }
}