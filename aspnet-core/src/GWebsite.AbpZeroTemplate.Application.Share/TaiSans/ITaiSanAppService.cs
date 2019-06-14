using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans
{
    public interface ITaiSanAppService
    {
        void CreateOrEditTaiSan(TaiSanInput TaiSanInput);
        TaiSanInput GetTaiSanForEdit(int id);
        void DeleteTaiSan(int id);
        PagedResultDto<TaiSanDto> GetTaiSans(TaiSanFilter input);
        TaiSanForViewDto GetTaiSanForView(int id);
        string[] GetArrTenNhomTaiSan(string loaiTS);
        TaiSanInput getSoLuongTonTaiSan(int id);
        PagedResultDto<TaiSanDto> GetTaiSansXuat(TaiSanFilter input);
        PagedResultDto<TaiSanDto> GetTaiSansTon(TaiSanFilter input);
        NhomTaiSanInput GetKhauHao(string tenNhomTS);
    }
}
