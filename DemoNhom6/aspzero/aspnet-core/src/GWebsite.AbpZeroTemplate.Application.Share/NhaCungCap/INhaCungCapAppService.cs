using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap
{
  public  interface INhaCungCapAppService 
    {
        void CreateOrEditNhaCungCap(NhaCungCapInput nhaCungCapInput);
        NhaCungCapInput GetNhaCungCapForEdit(int id);
        void DeleteNhaCungCap(int id);
        PagedResultDto<NhaCungCapDto> GetNhaCungCaps(NhaCungCapFilter filter);
        NhaCungCapForViewDto GetNhaCungCapForView(int id);


    }
}
