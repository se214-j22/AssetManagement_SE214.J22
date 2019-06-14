using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.UseAssets.Dto
{
    /// <summary>
    /// <model cref="UseAsset"></model>
    /// </summary>
    public class UseAssetFilter : PagedAndSortedInputDto
    {
        //Mã tài sản
        public string AssetId { get; set; }
    }
}