using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HoaDonNhapController : GWebsiteControllerBase
    {
        private readonly IHoaDonNhapAppService hoaDonNhapAppService;

        public HoaDonNhapController(IHoaDonNhapAppService hoaDonNhapAppService)
        {
            this.hoaDonNhapAppService = hoaDonNhapAppService;
        }

        [HttpGet]
        public PagedResultDto<HoaDonNhapOutput> GetHoaDonNhapsByFilter(HoaDonNhapFilter hoaDonNhapFilter)
        {
            return hoaDonNhapAppService.GetHoaDonNhaps(hoaDonNhapFilter);
        }

        [HttpGet]
        public HoaDonNhapInput GetHoaDonNhapForEdit(int id)
        {
            return hoaDonNhapAppService.GetHoaDonNhapForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditHoaDonNhap([FromBody] HoaDonNhapInput input)
        {
            hoaDonNhapAppService.CreateOrEditHoaDonNhap(input);
        }

        [HttpDelete("{id}")]
        public void DeleteHoaDonNhap(int id)
        {
            hoaDonNhapAppService.DeleteHoaDonNhap(id);
        }

        [HttpGet]
        public HoaDonNhapForViewDto GetHoaDonNhapForView(int id)
        {
            return hoaDonNhapAppService.GetHoaDonNhapForView(id);
        }
    }
}