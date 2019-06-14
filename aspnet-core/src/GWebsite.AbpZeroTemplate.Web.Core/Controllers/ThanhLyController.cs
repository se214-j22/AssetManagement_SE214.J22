using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThanhLys;
using GWebsite.AbpZeroTemplate.Application.Share.ThanhLys.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThanhLyController : GWebsiteControllerBase
    {
        private readonly IThanhLyAppService thanhLyAppservice;

        public ThanhLyController(IThanhLyAppService thanhLyAppservice)
        {
            this.thanhLyAppservice = thanhLyAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<ThanhLyDto> GetThanhLyByFilter(ThanhLyFilter thanhLyFilter)
        {
            return thanhLyAppservice.GetThanhLys(thanhLyFilter);
        }
        [HttpPost]
        public void CreateOrEditThanhLy([FromBody] ThanhLyInput input)
        {
            thanhLyAppservice.CreateOrEditThanhLy(input);
        }

        [HttpGet]
        public ThanhLyInput GetThanhLyForEdit(int id)
        {
            return thanhLyAppservice.GetThanhLyForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteThanhLy(int id)
        {
            thanhLyAppservice.DeleteThanhLy(id);
        }
        [HttpGet]
        public ThanhLyForViewDto GetThanhLyForView(int id)
        {
            return thanhLyAppservice.GetThanhLyForView(id);
        }
        [HttpGet]
        public string[] GetArrTenDonVi()
        {
            return thanhLyAppservice.GetArrTenDonVi();
        }
    }
}

