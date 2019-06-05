using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CTDonViController : GWebsiteControllerBase
    {
        private readonly ICTDonViAppService cTDonViAppService;

        public CTDonViController(ICTDonViAppService cTDonViAppService)
        {
            this.cTDonViAppService = cTDonViAppService;
        }

        [HttpGet]
        public PagedResultDto<CTDonViDto> GetCTDonVisByFilter(CTDonViFilter cTDonViFilter)
        {
            return cTDonViAppService.GetCTDonVis(cTDonViFilter);
        }

        [HttpGet]
        public CTDonViInput GetCTDonViForEdit(int id)
        {
            return cTDonViAppService.GetCTDonViForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCTDonVi([FromBody] CTDonViInput input)
        {
            cTDonViAppService.CreateOrEditCTDonVi(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCTDonVi(int id)
        {
            cTDonViAppService.DeleteCTDonVi(id);
        }

        [HttpGet]
        public CTDonViForViewDto GetCTDonViForView(int id)
        {
            return cTDonViAppService.GetCTDonViForView(id);
        }
    }
}
