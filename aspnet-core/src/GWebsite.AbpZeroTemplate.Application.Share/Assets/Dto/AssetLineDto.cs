using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="AssetLine"></model>
    /// </summary>
    public class AssetLineDto : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AssetTypeDto AssetType { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
    }
}
