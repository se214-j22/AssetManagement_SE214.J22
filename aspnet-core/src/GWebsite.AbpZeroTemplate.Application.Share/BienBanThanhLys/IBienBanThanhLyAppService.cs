using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys
{
    public interface IBienBanThanhLyAppService
    {
        void CreateOrEditBienBanThanhLy(BienBanThanhLyInput bienBanThanhLyInput);
        BienBanThanhLyInput GetBienBanThanhLyForEdit(int id);
        void DeleteBienBanThanhLy(int id);
        PagedResultDto<BienBanThanhLyForViewDto> GetBienBanThanhLys(BienBanThanhLyFilter input);
        BienBanThanhLyDto GetBienBanThanhLyForView(int id);
    }
}
