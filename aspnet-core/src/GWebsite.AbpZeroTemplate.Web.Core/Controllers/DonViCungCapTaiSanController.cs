using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DonViCungCapTaiSanController : GWebsiteControllerBase
    {
        private readonly IDonViCungCapTaiSanAppService donViCungCapTaiSanAppService;

        public DonViCungCapTaiSanController(IDonViCungCapTaiSanAppService donViCungCapTaiSanAppService)
        {
            this.donViCungCapTaiSanAppService = donViCungCapTaiSanAppService;
        }

        [HttpGet]
        public PagedResultDto<DonViCungCapTaiSanDto> GetDonViCungCapTaiSansByFilter(DonViCungCapTaiSanFilter donViCungCapTaiSanFilter)
        {
            return donViCungCapTaiSanAppService.GetDonViCungCapTaiSans(donViCungCapTaiSanFilter);
        }

        [HttpGet]
        public DonViCungCapTaiSanInput GetDonViCungCapTaiSanForEdit(int id)
        {
            return donViCungCapTaiSanAppService.GetDonViCungCapTaiSanForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditDonViCungCapTaiSan([FromBody] DonViCungCapTaiSanInput input)
        {
            donViCungCapTaiSanAppService.CreateOrEditDonViCungCapTaiSan(input);
        }

        [HttpDelete("{id}")]
        public void DeleteDonViCungCapTaiSan(int id)
        {
            donViCungCapTaiSanAppService.DeleteDonViCungCapTaiSan(id);
        }

        [HttpGet]
        public DonViCungCapTaiSanForViewDto GetDonViCungCapTaiSanForView(int id)
        {
            return donViCungCapTaiSanAppService.GetDonViCungCapTaiSanForView(id);
        }
    }
}
