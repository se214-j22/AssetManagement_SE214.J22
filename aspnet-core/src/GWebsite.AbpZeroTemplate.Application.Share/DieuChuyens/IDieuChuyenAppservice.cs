using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens
{
    public interface IDieuChuyenAppService
    {
        void CreateOrEditDieuChuyen(DieuChuyenInput dieuChuyenInput);
        DieuChuyenInput GetDieuChuyenForEdit(int id);
        void DeleteDieuChuyen(int id);
        PagedResultDto<DieuChuyenDto> GetDieuChuyens(DieuChuyenFilter input);
        DieuChuyenForViewDto GetDieuChuyenForView(int id);
        string[] GetArrTenDV();
        string[] GetArrTenNVNhan(string tenDV);
    }
}
