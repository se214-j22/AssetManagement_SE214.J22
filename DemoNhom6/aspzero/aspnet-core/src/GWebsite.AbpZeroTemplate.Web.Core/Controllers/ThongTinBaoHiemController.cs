using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThongTinBaoHiemController : GWebsiteControllerBase
    {
        private readonly IThongTinBaoHiemAppService thongTinBaoHiemAppService;

        public ThongTinBaoHiemController(IThongTinBaoHiemAppService thongTinBaoHiemAppService)
        {
            this.thongTinBaoHiemAppService = thongTinBaoHiemAppService;
        }

        [HttpGet]
        public PagedResultDto<ThongTinBaoHiemDto> GetThongTinBaoHiemsByFilter(ThongTinBaoHiemFilter thongTinBaoHiemFilter)
        {
            return thongTinBaoHiemAppService.GetThongTinBaoHiems(thongTinBaoHiemFilter);
        }

        [HttpGet]
        public ThongTinBaoHiemInput GetThongTinBaoHiemForEdit(int soXe)
        {
            return thongTinBaoHiemAppService.GetThongTinBaoHiemForEdit(soXe);
        }

        [HttpPost]
        public void CreateOrEditThongTinBaoHiem([FromBody] ThongTinBaoHiemInput input)
        {
            thongTinBaoHiemAppService.CreateOrEditThongTinBaoHiem(input);
        }

        [HttpDelete("{soXe}")]
        public void DeleteThongTinBaoHiem(int soXe)
        {
            thongTinBaoHiemAppService.DeleteThongTinBaoHiem(soXe);
        }

        [HttpGet]
        public ThongTinBaoHiemForViewDto GetThongTinBaoHiemForView(int soXe)
        {
            return thongTinBaoHiemAppService.GetThongTinBaoHiemForView(soXe);
        }
    }
}