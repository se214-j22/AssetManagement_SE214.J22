using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThongTinBaoDuongController : GWebsiteControllerBase
    {
        private readonly IThongTinBaoDuongAppService thongtinbaoduongAppService;

        public ThongTinBaoDuongController(IThongTinBaoDuongAppService thongtinbaoduongAppService)
        {
            this.thongtinbaoduongAppService = thongtinbaoduongAppService;
        }

        [HttpGet]
        public PagedResultDto<ThongTinBaoDuongDto> GetThongTinBaoDuongsByFilter(ThongTinBaoDuongFilter thongtinbaoduongFilter)
        {
            return thongtinbaoduongAppService.GetThongTinBaoDuongs(thongtinbaoduongFilter);
        }

        [HttpGet]
        public ThongTinBaoDuongInput GetThongTinBaoDuongForEdit(int id)
        {
            return thongtinbaoduongAppService.GetThongTinBaoDuongForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditThongTinBaoDuong([FromBody] ThongTinBaoDuongInput input)
        {
            thongtinbaoduongAppService.CreateOrEditThongTinBaoDuong(input);
        }

        [HttpDelete("{id}")]
        public void DeleteThongTinBaoDuong(int id)
        {
            thongtinbaoduongAppService.DeleteThongTinBaoDuong(id);
        }

        [HttpGet]
        public ThongTinBaoDuongForViewDto GetThongTinBaoDuongForView(int id)
        {
            return thongtinbaoduongAppService.GetThongTinBaoDuongForView(id);
        }
    }
}
