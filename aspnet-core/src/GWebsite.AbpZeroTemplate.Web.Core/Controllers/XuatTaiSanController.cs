using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class XuatTaiSanController : GWebsiteControllerBase
    {
        private readonly IXuatTaiSanAppService xuatTaiSanAppservice;

        public XuatTaiSanController(IXuatTaiSanAppService xuatTaiSanAppservice)
        {
            this.xuatTaiSanAppservice = xuatTaiSanAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<XuatTaiSanDto> GetXuatTaiSanByFilter(XuatTaiSanFilter xuatTaiSanFilter)
        {
            return xuatTaiSanAppservice.GetXuatTaiSans(xuatTaiSanFilter);
        }
        [HttpPost]
        public void CreateOrEditXuatTaiSan([FromBody] XuatTaiSanInput input)
        {
            xuatTaiSanAppservice.CreateOrEditXuatTaiSan(input);
        }

        [HttpGet]
        public XuatTaiSanInput GetXuatTaiSanForEdit(int id)
        {
            return xuatTaiSanAppservice.GetXuatTaiSanForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteXuatTaiSan(int id)
        {
            xuatTaiSanAppservice.DeleteXuatTaiSan(id);
        }
        [HttpGet]
        public XuatTaiSanForViewDto GetXuatTaiSanForView(int id)
        {
            return xuatTaiSanAppservice.GetXuatTaiSanForView(id);
        }
    }
}

