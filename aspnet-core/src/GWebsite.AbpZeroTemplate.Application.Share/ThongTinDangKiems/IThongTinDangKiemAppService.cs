using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems
{
    public interface IThongTinDangKiemAppService
    {
        void CreateOrEditThongTinDangKiem(ThongTinDangKiemInput thongTinDangKiemInput);
        ThongTinDangKiemInput GetThongTinDangKiemForEdit(int soXe);
        void DeleteThongTinDangKiem(int soXe);
        PagedResultDto<ThongTinDangKiemDto> GetThongTinDangKiems(ThongTinDangKiemFilter input);
        ThongTinDangKiemForViewDto GetThongTinDangKiemForView(int id);
    }
}
