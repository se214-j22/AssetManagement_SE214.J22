using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs
{
    public interface IChiNhanhAppService
    {
        void CreateOrEditChiNhanh(ChiNhanhInput chiNhanhInput);
        ChiNhanhInput GetChiNhanhForEdit(int id);
        void DeleteChiNhanh(int id);
        PagedResultDto<ChiNhanhDto> GetChiNhanhs(ChiNhanhFilter input);
        ChiNhanhForViewDto GetChiNhanhForView(int id);
    }
}
