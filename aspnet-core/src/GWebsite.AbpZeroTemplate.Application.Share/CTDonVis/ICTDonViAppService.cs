using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.CTDonVis
{
    public interface ICTDonViAppService
    {
        void CreateOrEditCTDonVi(CTDonViInput cTDonViInput);
        CTDonViInput GetCTDonViForEdit(int id);
        void DeleteCTDonVi(int id);
        PagedResultDto<CTDonViDto> GetCTDonVis(CTDonViFilter input);
        CTDonViForViewDto GetCTDonViForView(int id);
    }
}
