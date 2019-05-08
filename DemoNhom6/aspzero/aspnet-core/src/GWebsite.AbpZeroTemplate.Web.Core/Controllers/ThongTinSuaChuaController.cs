using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO;
using Microsoft.AspNetCore.Mvc;


namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThongTinSuaChuaController : GWebsiteControllerBase
    {
        private readonly IThongTinSuaChuaAppService thongTinSuaChuaAppService;

        public ThongTinSuaChuaController(IThongTinSuaChuaAppService thongTinSuaChuaAppService)
        {
            this.thongTinSuaChuaAppService = thongTinSuaChuaAppService;
        }

        [HttpGet]
        public PagedResultDto<ThongTinSuaChuaDTO> GetThongTinSuaChuasByFilter(ThongTinSuaChuaFilter thongTinSuaChuaFilter)
        {
            return thongTinSuaChuaAppService.GetThongTinSuaChuas(thongTinSuaChuaFilter);
        }

        [HttpGet]
        public ThongTinSuaChuaInput GetThongTinSuaChuaForEdit(int id)
        {
            return thongTinSuaChuaAppService.GetThongTinSuaChuaForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditThongTinSuaChua([FromBody] ThongTinSuaChuaInput input)
        {
            thongTinSuaChuaAppService.CreateOrEditThongTinSuaChua(input);
        }

        [HttpDelete("{id}")]
        public void DeleteThongTinSuaChua(int id)
        {
            thongTinSuaChuaAppService.DeleteThongTinSuaChua(id);
        }

        [HttpGet]
        public ThongTinSuaChuaForViewDTO GetThongTinSuaChuaForView(int id)
        {
            return thongTinSuaChuaAppService.GetThongTinSuaChuaForView(id);
        }
    }
}
