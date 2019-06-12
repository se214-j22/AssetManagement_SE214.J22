using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhanViens.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhanViens
{
    public interface INhanVienAppService
    {
        void CreateOrEditNhanVien(NhanVienInput nhanVienInput);
        NhanVienInput GetNhanVienForEdit(int id);
        void DeleteNhanVien(int id);
        PagedResultDto<NhanVienDto> GetNhanViens(NhanVienFilter input);
        NhanVienForViewDto GetNhanVienForView(int id);
        string[] GetArrTenDonVi();
    }
}
