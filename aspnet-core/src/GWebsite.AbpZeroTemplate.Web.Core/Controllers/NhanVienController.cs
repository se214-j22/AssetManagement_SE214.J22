using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhanViens;
using GWebsite.AbpZeroTemplate.Application.Share.NhanViens.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NhanVienController : GWebsiteControllerBase
    {
        private readonly INhanVienAppService nhanVienAppservice;

        public NhanVienController(INhanVienAppService nhanVienAppservice)
        {
            this.nhanVienAppservice = nhanVienAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<NhanVienDto> GetNhanVienByFilter(NhanVienFilter nhanVienFilter)
        {
            return nhanVienAppservice.GetNhanViens(nhanVienFilter);
        }
        [HttpPost]
        public void CreateOrEditNhanVien([FromBody] NhanVienInput input)
        {
            nhanVienAppservice.CreateOrEditNhanVien(input);
        }

        [HttpGet]
        public NhanVienInput GetNhanVienForEdit(int id)
        {
            return nhanVienAppservice.GetNhanVienForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteNhanVien(int id)
        {
            nhanVienAppservice.DeleteNhanVien(id);
        }
        [HttpGet]
        public NhanVienForViewDto GetNhanVienForView(int id)
        {
            return nhanVienAppservice.GetNhanVienForView(id);
        }
    }
}

