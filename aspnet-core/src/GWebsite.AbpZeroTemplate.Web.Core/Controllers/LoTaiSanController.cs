using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoTaiSanController : GWebsiteControllerBase
    {
        private readonly ILoTaiSanAppService loTaiSanAppservice;

        public LoTaiSanController(ILoTaiSanAppService loTaiSanAppservice)
        {
            this.loTaiSanAppservice = loTaiSanAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<LoTaiSanDto> GetLoTaiSansByFilter(LoTaiSanFilter LoTaiSanFilter)
        {
            return loTaiSanAppservice.GetLoTaiSans(LoTaiSanFilter);
        }
        [HttpPost]
        public void CreateOrEditLoTaiSan([FromBody] LoTaiSanInput input)
        {
            loTaiSanAppservice.CreateOrEditLoTaiSan(input);
        }

        [HttpGet]
        public LoTaiSanInput GetLoTaiSanForEdit(int id)
        {
            return loTaiSanAppservice.GetLoTaiSanForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteLoTaiSan(int id)
        {
            loTaiSanAppservice.DeleteLoTaiSan(id);
        }
        [HttpPost]
        public LoTaiSanForViewDto GetLoTaiSanForView(int id)
        {
            return loTaiSanAppservice.GetLoTaiSanForView(id);
        }
    }
}

