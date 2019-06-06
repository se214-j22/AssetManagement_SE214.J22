using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs
{
    public interface ITaiSanCoDinhAppService
    {
        void CreateOrEditTaiSanCoDinh(TaiSanCoDinhInput taiSanCoDinhInput);
        TaiSanCoDinhInput GetTaiSanCoDinhForEdit(int id);
        void DeleteTaiSanCoDinh(int id);
        PagedResultDto<TaiSanCoDinhForViewDto> GetTaiSanCoDinhs(TaiSanCoDinhFilter input);
        TaiSanCoDinhDto GetTaiSanCoDinhForView(int id);
    }
}
