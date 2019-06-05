using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans
{
    public interface IXuatTaiSanAppService
    {
        void CreateOrEditXuatTaiSan(XuatTaiSanInput xuatTaiSanInput);
        XuatTaiSanInput GetXuatTaiSanForEdit(int id);
        void DeleteXuatTaiSan(int id);
        PagedResultDto<XuatTaiSanDto> GetXuatTaiSans(XuatTaiSanFilter input);
        XuatTaiSanForViewDto GetXuatTaiSanForView(int id);
        string[] GetArrTenDonVi();
        string[] GetArrTenNhanVienTheoDV(string tenDV);
    }
}
