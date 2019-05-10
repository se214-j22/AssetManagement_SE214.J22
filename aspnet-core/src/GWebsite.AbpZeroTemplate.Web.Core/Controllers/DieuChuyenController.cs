using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DieuChuyenController : GWebsiteControllerBase
    {
        private readonly IDieuChuyenAppService dieuChuyenAppservice;

        public DieuChuyenController(IDieuChuyenAppService dieuChuyenAppservice)
        {
            this.dieuChuyenAppservice = dieuChuyenAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }

        [HttpGet]
        public PagedResultDto<DieuChuyenDto> GetDieuChuyensByFilter(DieuChuyenFilter DieuChuyenFilter)
        {
            return dieuChuyenAppservice.GetDieuChuyens(DieuChuyenFilter);
        }
        [HttpPost]
        public void CreateOrEditDieuChuyen([FromBody] DieuChuyenInput input)
        {
            dieuChuyenAppservice.CreateOrEditDieuChuyen(input);
        }

        [HttpGet]
        public DieuChuyenInput GetDieuChuyenForEdit(int id)
        {
            return dieuChuyenAppservice.GetDieuChuyenForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteDieuChuyen(int id)
        {
            dieuChuyenAppservice.DeleteDieuChuyen(id);
        }
        [HttpGet]
        public DieuChuyenForViewDto GetDieuChuyenForView(int id)
        {
            return dieuChuyenAppservice.GetDieuChuyenForView(id);
        }
    }
}

