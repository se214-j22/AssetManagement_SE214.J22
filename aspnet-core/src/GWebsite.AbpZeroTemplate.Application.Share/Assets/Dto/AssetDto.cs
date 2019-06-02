using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="Asset"></model>
    /// </summary>
    public class AssetDto : Entity<int>
    {
        public string Code { get; set; }
        public bool IsDamaged { get; set; }
        public AssetLine AssetLine { get; set; }
    }
}
