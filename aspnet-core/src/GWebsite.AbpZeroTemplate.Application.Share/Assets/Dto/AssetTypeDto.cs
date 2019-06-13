using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="AssetType"></model>
    /// </summary>
    public class AssetTypeDto : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Descriptions { get; set; }
    }
}
