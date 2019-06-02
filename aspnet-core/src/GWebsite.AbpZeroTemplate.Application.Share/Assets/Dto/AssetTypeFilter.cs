using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="AssetType"></model>
    /// </summary>
    public class AssetTypeFilter : PagedAndSortedInputDto
    {
        public string Term { get; set; }
    }
}
