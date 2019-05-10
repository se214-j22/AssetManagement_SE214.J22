using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.AssetDetails.Dto
{
    /// <summary>
    /// <model cref="AssetDetail"></model>
    /// </summary>
    public class AssetDetailFilter : PagedAndSortedInputDto
    {
        public string Name { get; set; }
    }
}
