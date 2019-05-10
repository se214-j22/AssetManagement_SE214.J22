using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails
{
    public interface ILiquidationDetailAppService
    {
        void CreateOrEditLiquidationDetail(LiquidationDetailInput customerInput);
        LiquidationDetailInput GetLiquidationDetailForEdit(int id);
        void DeleteLiquidationDetail(int id);
        PagedResultDto<LiquidationDetailDto> GetLiquidationDetails(LiquidationDetailFilter input);
        LiquidationDetailForViewDto GetLiquidationDetailForView(int id);
    }
}
