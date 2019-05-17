using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoaiTaiSanController : GWebsiteControllerBase
    {
        private readonly ILoaiTaiSanAppService loaiTaiSanAppService;

        public LoaiTaiSanController(ILoaiTaiSanAppService loaiTaiSanAppService)
        {
            this.loaiTaiSanAppService = loaiTaiSanAppService;
        }

        [HttpGet]
        public PagedResultDto<LoaiTaiSanDto> GetLoaiTaiSansByFilter(LoaiTaiSanFilter loaiTaiSanFilter)
        {
            return loaiTaiSanAppService.GetLoaiTaiSans(loaiTaiSanFilter);
        }

        [HttpGet]
        public LoaiTaiSanInput GetLoaiTaiSanForEdit(int id)
        {
            return loaiTaiSanAppService.GetLoaiTaiSanForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditLoaiTaiSan([FromBody] LoaiTaiSanInput input)
        {
            loaiTaiSanAppService.CreateOrEditLoaiTaiSan(input);
        }

        [HttpDelete("{id}")]
        public void DeleteLoaiTaiSan(int id)
        {
            loaiTaiSanAppService.DeleteLoaiTaiSan(id);
        }

        [HttpGet]
        public LoaiTaiSanForViewDto GetLoaiTaiSanForView(int id)
        {
            return loaiTaiSanAppService.GetLoaiTaiSanForView(id);
        }
    }
}
