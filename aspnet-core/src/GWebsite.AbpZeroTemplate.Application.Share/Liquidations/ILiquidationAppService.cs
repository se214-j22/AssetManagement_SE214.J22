using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Liquidations
{
    public interface ILiquidationAppService
    {
        void CreateOrEditLiquidation(LiquidationInput customerInput);
        LiquidationInput GetLiquidationForEdit(int id);
        void DeleteLiquidation(int id);
        PagedResultDto<LiquidationDto> GetLiquidations(LiquidationFilter input);
        LiquidationForViewDto GetLiquidationForView(int id);
    }
}
