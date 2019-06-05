using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans
{
    public interface INhomTaiSanAppService
    {
        void CreateOrEditNhomTaiSan(NhomTaiSanInput TaiSanInput);
        NhomTaiSanInput GetNhomTaiSanForEdit(int id);
        void DeleteNhomTaiSan(int id);
        PagedResultDto<NhomTaiSanDto> GetNhomTaiSans(NhomTaiSanFilter input);
        NhomTaiSanForViewDto GetNhomTaiSanForView(int id);
    }
}
