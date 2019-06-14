using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SanPhams;
using GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SanPhamController : GWebsiteControllerBase
    {
        private readonly ISanPhamAppService sanPhamAppService;

        public SanPhamController(ISanPhamAppService sanPhamAppService)
        {
            this.sanPhamAppService = sanPhamAppService;
        }

        [HttpGet]
        public PagedResultDto<SanPhamDto> GetSanPhamsByFilter(SanPhamFilter sanPhamFilter)
        {
            return sanPhamAppService.GetSanPhams(sanPhamFilter);
        }
        [HttpGet]
        public IEnumerable<SanPhamReports> GetAllSanPhams()
        {
            return sanPhamAppService.GetAllSanPhams();
        }

        [HttpGet]
        public SanPhamInput GetSanPhamForEdit(int id)
        {
            return sanPhamAppService.GetSanPhamForEdit(id);
        }
        [HttpGet]
        public SanPhamInput GetSanPhamForEditMaSP(string maSP)
        {
            return sanPhamAppService.GetSanPhamForEditMaSP(maSP);
        }
        [HttpPost]
        public void CreateOrEditSanPham([FromBody] SanPhamInput input)
        {
            sanPhamAppService.CreateOrEditSanPham(input);
        }

        [HttpDelete("{id}")]
        public void DeleteSanPham(int id)
        {
            sanPhamAppService.DeleteSanPham(id);
        }

        [HttpGet]
        public SanPhamForViewDto GetSanPhamForView(int id)
        {
            return sanPhamAppService.GetSanPhamForView(id);
        }
    }
}
