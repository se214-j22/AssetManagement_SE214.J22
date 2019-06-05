using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans
{
    public interface ICTTaiSanAppService
    {
        void CreateOrEditCTTaiSan(CTTaiSanInput cTTaiSanInput);
        CTTaiSanInput GetCTTaiSanForEdit(int id);
        void DeleteCTTaiSan(int id);
        PagedResultDto<CTTaiSanDto> GetCTTaiSans(CTTaiSanFilter input);
        CTTaiSanForViewDto GetCTTaiSanForView(int id);
    }
}
