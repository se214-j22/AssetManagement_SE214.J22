using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SanPhams
{
    public interface ISanPhamAppService
    {
        void CreateOrEditSanPham(SanPhamInput sanPhamInput);
        SanPhamInput GetSanPhamForEdit(int id);
        SanPhamInput GetSanPhamForEditMaSP(string maSP);
        void DeleteSanPham(int id);
        PagedResultDto<SanPhamDto> GetSanPhams(SanPhamFilter input);
        IEnumerable<SanPhamReports> GetAllSanPhams();
        SanPhamForViewDto GetSanPhamForView(int id);
    }
}
