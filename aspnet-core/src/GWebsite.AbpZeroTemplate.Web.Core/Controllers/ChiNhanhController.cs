using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs;
using GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ChiNhanhController : GWebsiteControllerBase
    {
        private readonly IChiNhanhAppService customerAppService;

        public ChiNhanhController(IChiNhanhAppService customerAppService)
        {
            this.customerAppService = customerAppService;
        }

        [HttpGet]
        public PagedResultDto<ChiNhanhDto> GetChiNhanhsByFilter(ChiNhanhFilter chiNhanhFilter)
        {
            return customerAppService.GetChiNhanhs(chiNhanhFilter);
        }

        [HttpGet]
        public ChiNhanhInput GetChiNhanhForEdit(int id)
        {
            return customerAppService.GetChiNhanhForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditChiNhanh([FromBody] ChiNhanhInput input)
        {
            customerAppService.CreateOrEditChiNhanh(input);
        }

        [HttpDelete("{id}")]
        public void DeleteChiNhanh(int id)
        {
            customerAppService.DeleteChiNhanh(id);
        }

        [HttpGet]
        public ChiNhanhForViewDto GetChiNhanhForView(int id)
        {
            return customerAppService.GetChiNhanhForView(id);
        }
    }
}
