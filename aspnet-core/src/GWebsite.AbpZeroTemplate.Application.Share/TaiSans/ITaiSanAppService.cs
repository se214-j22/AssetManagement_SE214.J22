using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans
{
  public  interface ITaiSanAppService
    {
        void CreateOrEditTaiSan(TaiSanInput taiSanInput);
        TaiSanInput GetTaiSanForEdit(string maTaiSan);
        void DeleteTaiSan(int id);
        PagedResultDto<TaiSanDto> GetTaiSans(TaiSanFilter filter);
        TaiSanForViewDto GetTaiSanForView(string maTaiSan);
    }
}
