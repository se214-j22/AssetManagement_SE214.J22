using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NhomTaiSanController : GWebsiteControllerBase
    {
        private readonly INhomTaiSanAppService nhomTaiSanAppservice;

        public NhomTaiSanController(INhomTaiSanAppService nhomTaiSanAppservice)
        {
            this.nhomTaiSanAppservice = nhomTaiSanAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<NhomTaiSanDto> GetNhomTaiSanByFilter(NhomTaiSanFilter nhomTaiSanFilter)
        {
            return nhomTaiSanAppservice.GetNhomTaiSans(nhomTaiSanFilter);
        }
        [HttpPost]
        public void CreateOrEditNhomTaiSan([FromBody] NhomTaiSanInput input)
        {
            nhomTaiSanAppservice.CreateOrEditNhomTaiSan(input);
        }

        [HttpGet]
        public NhomTaiSanInput GetNhomTaiSanForEdit(int id)
        {
            return nhomTaiSanAppservice.GetNhomTaiSanForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteNhomTaiSan(int id)
        {
            nhomTaiSanAppservice.DeleteNhomTaiSan(id);
        }
        [HttpGet]
        public NhomTaiSanForViewDto GetNhomTaiSanForView(int id)
        {
            return nhomTaiSanAppservice.GetNhomTaiSanForView(id);
        }
    }
}

