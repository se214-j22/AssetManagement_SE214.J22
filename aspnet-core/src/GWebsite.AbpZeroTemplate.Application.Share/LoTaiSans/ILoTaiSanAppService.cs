using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans
{
    public interface ILoTaiSanAppService
    {
        void CreateOrEditLoTaiSan(LoTaiSanInput loTaiSanInput);
        LoTaiSanInput GetLoTaiSanForEdit(int id);
        void DeleteLoTaiSan(int id);
        PagedResultDto<LoTaiSanDto> GetLoTaiSans(LoTaiSanFilter input);
        LoTaiSanForViewDto GetLoTaiSanForView(int id);
    }
}
