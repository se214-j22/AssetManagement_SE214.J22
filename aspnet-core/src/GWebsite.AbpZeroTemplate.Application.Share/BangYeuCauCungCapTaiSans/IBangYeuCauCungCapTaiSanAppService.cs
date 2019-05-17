using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans
{
    public interface IBangYeuCauCungCapTaiSanAppService
    {
        void CreateOrEditBangYeuCauCungCapTaiSan(BangYeuCauCungCapTaiSanInput BangYeuCauCungCapTaiSanInput);
        BangYeuCauCungCapTaiSanInput GetBangYeuCauCungCapTaiSanForEdit(int id);
        void DeleteBangYeuCauCungCapTaiSan(int id);
        PagedResultDto<BangYeuCauCungCapTaiSanOutput> GetBangYeuCauCungCapTaiSans(BangYeuCauCungCapTaiSanFilter input);
        BangYeuCauCungCapTaiSanForViewDto GetBangYeuCauCungCapTaiSanForView(int id);
    }
}
