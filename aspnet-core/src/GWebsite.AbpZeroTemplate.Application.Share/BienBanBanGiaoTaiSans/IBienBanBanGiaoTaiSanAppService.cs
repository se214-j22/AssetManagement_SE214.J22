using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans
{
    public interface IBienBanBanGiaoTaiSanAppService
    {
        void CreateOrEditBienBanBanGiaoTaiSan(BienBanBanGiaoTaiSanInput bienBanBanGiaoTaiSanInput);
        BienBanBanGiaoTaiSanInput GetBienBanBanGiaoTaiSanForEdit(int id);
        void DeleteBienBanBanGiaoTaiSan(int id);
        PagedResultDto<BienBanBanGiaoTaiSanForViewDto> GetBienBanBanGiaoTaiSans(BienBanBanGiaoTaiSanFilter input);
        BienBanBanGiaoTaiSanDto GetBienBanBanGiaoTaiSanForView(int id);
    }
}
