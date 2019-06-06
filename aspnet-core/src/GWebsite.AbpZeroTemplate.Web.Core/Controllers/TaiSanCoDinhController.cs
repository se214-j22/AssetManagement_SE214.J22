using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TaiSanCoDinhController : GWebsiteControllerBase
    {
        private readonly ITaiSanCoDinhAppService taiSanCoDinhAppService;

        public TaiSanCoDinhController(ITaiSanCoDinhAppService taiSanCoDinhAppService)
        {
            this.taiSanCoDinhAppService = taiSanCoDinhAppService;
        }

        [HttpGet]
        public PagedResultDto<TaiSanCoDinhForViewDto> GetTaiSanCoDinhsByFilter(TaiSanCoDinhFilter taiSanCoDinhFilter)
        {
            return taiSanCoDinhAppService.GetTaiSanCoDinhs(taiSanCoDinhFilter);
        }

        [HttpGet]
        public TaiSanCoDinhInput GetTaiSanCoDinhForEdit(int id)
        {
            return taiSanCoDinhAppService.GetTaiSanCoDinhForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditTaiSanCoDinh([FromBody] TaiSanCoDinhInput input)
        {
            taiSanCoDinhAppService.CreateOrEditTaiSanCoDinh(input);
        }

        [HttpDelete("{id}")]
        public void DeleteTaiSanCoDinh(int id)
        {
            taiSanCoDinhAppService.DeleteTaiSanCoDinh(id);
        }

        [HttpGet]
        public TaiSanCoDinhDto GetTaiSanCoDinhForView(int id)
        {
            return taiSanCoDinhAppService.GetTaiSanCoDinhForView(id);
        }
    }
}
