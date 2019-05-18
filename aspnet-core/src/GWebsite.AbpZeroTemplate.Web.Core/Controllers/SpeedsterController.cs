using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Speedsters;
using GWebsite.AbpZeroTemplate.Application.Share.Speedsters.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SpeedsterController : GWebsiteControllerBase
    {
        private readonly ISpeedsterAppService speedsterAppService;

        public SpeedsterController(ISpeedsterAppService speedsterAppService)
        {
            this.speedsterAppService = speedsterAppService;
        }

        [HttpGet]
        public PagedResultDto<SpeedsterDto> GetSpeedstersByFilter(SpeedsterFilter speedsterFilter)
        {
            return speedsterAppService.GetSpeedsters(speedsterFilter);
        }

        [HttpGet]
        public SpeedsterInput GetSpeedsterForEdit(int id)
        {
            return speedsterAppService.GetSpeedsterForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditSpeedster([FromBody] SpeedsterInput input)
        {
            speedsterAppService.CreateOrEditSpeedster(input);
        }

        [HttpDelete("{id}")]
        public void DeleteSpeedster(int id)
        {
            speedsterAppService.DeleteSpeedster(id);
        }

        [HttpGet]
        public SpeedsterForViewDto GetSpeedsterForView(int id)
        {
            return speedsterAppService.GetSpeedsterForView(id);
        }
    }
}
