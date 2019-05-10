using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails.Dto
{
    /// <summary>
    /// <model cref="LiquidationDetail"></model>
    /// </summary>
    public class LiquidationDetailDto : Entity<int>
    {
        public string LiquidationID { get; set; }
        public string AssetID { get; set; }
        public string LiquidationPrice { get; set; }
        public string Note { get; set; }
    }
}
