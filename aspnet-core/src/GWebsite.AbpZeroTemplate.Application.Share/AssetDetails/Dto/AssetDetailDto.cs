using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.AssetDetails.Dto
{
    /// <summary>
    /// <model cref="AssetDetail"></model>
    /// </summary>
    public class AssetDetailDto : Entity<int>
    {
        public string Name { get; set; }
        public string AssetID { get; set; }
        public string Price { get; set; }
        public string Specification { get; set; }
    }
}
