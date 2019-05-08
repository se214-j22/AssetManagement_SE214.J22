using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public  class HoaDonVanHanhXeController : GWebsiteControllerBase
    {
        private readonly IHoaDonVanHanhXeAppService hoaDonVanHanhXeAppService;

        public HoaDonVanHanhXeController(IHoaDonVanHanhXeAppService hoaDonVanHanhXeAppService)
        {
            this.hoaDonVanHanhXeAppService = hoaDonVanHanhXeAppService;
        }

        [HttpGet]
        public PagedResultDto<HoaDonVanHanhXeDto> GetHoaDonVanHanhXesByFilter(HoaDonVanHanhXeFilter hoaDonVanHanhXeFilter)
        {
            return hoaDonVanHanhXeAppService.GetHoaDonVanHanhXes(hoaDonVanHanhXeFilter);
        }

        [HttpGet]
        public HoaDonVanHanhXeInput GetHoaDonVanHanhXeForEdit(string soHoaDon)
        {
            return hoaDonVanHanhXeAppService.GetHoaDonVanHanhXeForEdit(soHoaDon);
        }

        [HttpPost]
        public void CreateOrEditHoaDonVanHanhXe([FromBody] HoaDonVanHanhXeInput input)
        {
            hoaDonVanHanhXeAppService.CreateOrEditHoaDonVanHanhXe(input);
        }

        [HttpDelete("{id}")]
        public void DeleteHoaDonVanHanhXe(string soHoaDon)
        {
            hoaDonVanHanhXeAppService.DeleteHoaDonVanHanhXe(soHoaDon);
        }

        [HttpGet]
        public HoaDonVanHanhXeForViewDto GetHoaDonVanHanhXeForView(string soHoaDon)
        {
            return hoaDonVanHanhXeAppService.GetHoaDonVanHanhXeForView(soHoaDon);
        }
    }
}
