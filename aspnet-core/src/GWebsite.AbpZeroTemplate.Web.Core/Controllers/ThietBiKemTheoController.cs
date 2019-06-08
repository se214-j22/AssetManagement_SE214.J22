using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos;
using GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThietBiKemTheoController : GWebsiteControllerBase
    {
        private readonly IThietBiKemTheoAppService tbktAppService;

        public ThietBiKemTheoController(IThietBiKemTheoAppService tbktAppService)
        {
            this.tbktAppService = tbktAppService;
        }

        [HttpGet]
        public PagedResultDto<ThietBiKemTheoDto> GetThietBiKemTheosByFilter(ThietBiKemTheoFilter tbktFilter)
        {
            return tbktAppService.GetThietBiKemTheos(tbktFilter);
        }

        [HttpGet]
        public ThietBiKemTheoInput GetThietBiKemTheoForEdit(int id)
        {
            return tbktAppService.GetThietBiKemTheoForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditThietBiKemTheo([FromBody] ThietBiKemTheoInput input)
        {
           tbktAppService.CreateOrEditThietBiKemTheo(input);
        }

        [HttpDelete("{id}")]
        public void DeleteThietBiKemTheo(int id)
        {
            tbktAppService.DeleteThietBiKemTheo(id);
        }

        [HttpGet]
        public ThietBiKemTheoForViewDto GetThietBiKemTheoForView(int id)
        {
            return tbktAppService.GetThietBiKemTheoForView(id);
        }
    }
}
