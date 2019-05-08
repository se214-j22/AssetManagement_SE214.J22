using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap;
using GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
   public class NhaCungCapController : GWebsiteControllerBase
    {
        private readonly INhaCungCapAppService nhaCungCapAppService;

        public NhaCungCapController(INhaCungCapAppService nhaCungCapAppService)
        {
            this.nhaCungCapAppService = nhaCungCapAppService;
        }

        [HttpGet]
        public PagedResultDto<NhaCungCapDto> GetNhaCungCapsByFilter(NhaCungCapFilter filter)
        {
            return nhaCungCapAppService.GetNhaCungCaps(filter);

        }
        [HttpGet]
        public  NhaCungCapInput GetNhaCungCapForEdit(int id)
        {
            return  nhaCungCapAppService.GetNhaCungCapForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditNhaCungCap([FromBody] NhaCungCapInput input)  
        {
            nhaCungCapAppService.CreateOrEditNhaCungCap(input);
        }

        [HttpDelete("{id}")]
        public void DeleteNhaCUngCap(int id)
        {
            nhaCungCapAppService.DeleteNhaCungCap(id);
        }
        [HttpGet]
        public NhaCungCapForViewDto GetNhaCungCapForView(int id)
        {
            return nhaCungCapAppService.GetNhaCungCapForView(id);
        }
    }
}
