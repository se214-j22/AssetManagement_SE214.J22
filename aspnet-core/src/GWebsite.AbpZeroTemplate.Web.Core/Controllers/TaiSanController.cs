using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TaiSanController : GWebsiteControllerBase
    {
        private readonly ITaiSanAppService taiSanAppservice;

        public TaiSanController(ITaiSanAppService taiSanAppservice)
        {
            this.taiSanAppservice = taiSanAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<TaiSanDto> GetTaiSansByFilter(TaiSanFilter TaiSanFilter)
        {
            return taiSanAppservice.GetTaiSans(TaiSanFilter);
        }
        [HttpPost]
        public void CreateOrEditTaiSan([FromBody] TaiSanInput input)
        {
            taiSanAppservice.CreateOrEditTaiSan(input);
        }

        [HttpGet]
        public TaiSanInput GetTaiSanForEdit(int id)
        {
            return taiSanAppservice.GetTaiSanForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteTaiSan(int id)
        {
            taiSanAppservice.DeleteTaiSan(id);
        }
        [HttpGet]
        public TaiSanForViewDto GetTaiSanForView(int id)
        {
            return taiSanAppservice.GetTaiSanForView(id);
        }
    }
}

