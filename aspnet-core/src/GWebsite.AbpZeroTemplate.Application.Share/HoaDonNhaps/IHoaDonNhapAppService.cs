using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps
{
    public interface IHoaDonNhapAppService
    {
        void CreateOrEditHoaDonNhap(HoaDonNhapInput hoaDonNhapInput);
        HoaDonNhapInput GetHoaDonNhapForEdit(int id);
        void DeleteHoaDonNhap(int id);
        PagedResultDto<HoaDonNhapOutput> GetHoaDonNhaps(HoaDonNhapFilter input);
        HoaDonNhapForViewDto GetHoaDonNhapForView(int id);
    }
}
