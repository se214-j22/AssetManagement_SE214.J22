using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
  public  class ThongTinXeController : GWebsiteControllerBase
    {
        private readonly IThongTinXeAppService thongTinXeAppService;

        public ThongTinXeController(IThongTinXeAppService thongTinXeAppService)
        {
            this.thongTinXeAppService = thongTinXeAppService;
        }
        [HttpGet]
        public PagedResultDto<ThongTinXeDto> GetThongTinXeByFilter(ThongTinXeFilter filter)
        {
            return thongTinXeAppService.GetThongTinXes(filter);
        }
        [HttpGet]
        public    ThongTinXeForViewDto GetThongTinXeForView(string soXe)
        {
            return thongTinXeAppService.GetThongTinXeForView(soXe);
        }
        [HttpGet]
        public ThongTinXeInput GetThongTinSeForEdit(string soXe)
        {
            return thongTinXeAppService.GetThongTinXeForEdit(soXe);
        }
        [HttpPost]
        public   void CreateOrEditThongTinXe([FromBody]ThongTinXeInput input)
        {
             thongTinXeAppService.CreateOrEditThongTinXe(input);
        }

        [HttpDelete("{soxe}")]
        public void DeleteThongTinXe(string soxe)
        {
            thongTinXeAppService.DeleteThongTinXe(soxe);
        }

    }
}
