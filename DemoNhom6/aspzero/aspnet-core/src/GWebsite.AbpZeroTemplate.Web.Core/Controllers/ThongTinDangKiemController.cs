using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThongTinDangKiemController : GWebsiteControllerBase
    {
        private readonly IThongTinDangKiemAppService thongTinDangKiemAppService;

        public ThongTinDangKiemController(IThongTinDangKiemAppService thongTinDangKiemAppService)
        {
            this.thongTinDangKiemAppService = thongTinDangKiemAppService;
        }

        [HttpGet]
        public PagedResultDto<ThongTinDangKiemDto> GetThongTinDangKiemsByFilter(ThongTinDangKiemFilter thongTinDangKiemFilter)
        {
            return thongTinDangKiemAppService.GetThongTinDangKiems(thongTinDangKiemFilter);
        }

        [HttpGet]
        public ThongTinDangKiemInput GetThongTinDangKiemForEdit(int soXe)
        {
            return thongTinDangKiemAppService.GetThongTinDangKiemForEdit(soXe);
        }

        [HttpPost]
        public void CreateOrEditThongTinDangKiem([FromBody] ThongTinDangKiemInput input)
        {
            thongTinDangKiemAppService.CreateOrEditThongTinDangKiem(input);
        }

        [HttpDelete("{soXe}")]
        public void DeleteThongTinDangKiem(int soXe)
        {
            thongTinDangKiemAppService.DeleteThongTinDangKiem(soXe);
        }

        [HttpGet]
        public ThongTinDangKiemForViewDto GetThongTinDangKiemForView(int soXe)
        {
            return thongTinDangKiemAppService.GetThongTinDangKiemForView(soXe);
        }
    }
}