using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems
{
    public interface IThongTinBaoHiemAppService
    {
        void CreateOrEditThongTinBaoHiem(ThongTinBaoHiemInput thongTinBaoHiemInput);
        ThongTinBaoHiemInput GetThongTinBaoHiemForEdit(int soXe);
        void DeleteThongTinBaoHiem(int soXe);
        PagedResultDto<ThongTinBaoHiemDto> GetThongTinBaoHiems(ThongTinBaoHiemFilter input);
        ThongTinBaoHiemForViewDto GetThongTinBaoHiemForView(int soXe);
    }
}


