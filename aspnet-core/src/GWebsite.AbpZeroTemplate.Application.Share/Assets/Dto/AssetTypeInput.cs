using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="AssetType"></model>
    /// </summary>
    public class AssetTypeInput : Entity<int>
    {
        [StringLength(3, MinimumLength = 3)]
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
