using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SuaChuaController : GWebsiteControllerBase
    {
        private readonly ISuaChuaAppService suaChuaAppservice;

        public SuaChuaController(ISuaChuaAppService suaChuaAppservice)
        {
            this.suaChuaAppservice = suaChuaAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<SuaChuaDto> GetSuaChuaByFilter(SuaChuaFilter suaChuaFilter)
        {
            return suaChuaAppservice.GetSuaChuas(suaChuaFilter);
        }
        [HttpPost]
        public void CreateOrEditSuaChua([FromBody] SuaChuaInput input)
        {
            suaChuaAppservice.CreateOrEditSuaChua(input);
        }

        [HttpGet]
        public SuaChuaInput GetSuaChuaForEdit(int id)
        {
            return suaChuaAppservice.GetSuaChuaForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteSuaChua(int id)
        {
            suaChuaAppservice.DeleteSuaChua(id);
        }
        [HttpGet]
        public SuaChuaForViewDto GetSuaChuaForView(int id)
        {
            return suaChuaAppservice.GetSuaChuaForView(id);
        }
        [HttpGet]
        public string[] GetArrTenDVSC()
        {
            return suaChuaAppservice.GetArrTenDVSC();
        }
        [HttpGet]
        public string[] GetArrTenNVPT(string tenDV)
        {
            return suaChuaAppservice.GetArrTenNVPT(tenDV);
        }
        [HttpGet]
        public string[] GetArrTenNVDX()
        {
            return suaChuaAppservice.GetArrTenNVDX();
        }
        [HttpGet]
        public string GetTenDVDX(string tenNV)
        {
            return suaChuaAppservice.GetTenDVDX(tenNV);
        }
    }
}

