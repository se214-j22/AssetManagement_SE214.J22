using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="AssetLine"></model>
    /// </summary>
    public class AssetLineFilter : PagedAndSortedInputDto
    {
        public string Term { get; set; }
        public int AssetTypeId { get; set; }
        public int ManufacturerId { get; set; }
    }
}
