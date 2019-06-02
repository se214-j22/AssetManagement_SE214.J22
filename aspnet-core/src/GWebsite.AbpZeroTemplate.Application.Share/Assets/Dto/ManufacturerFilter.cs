using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="Manufacturer"></model>
    /// </summary>
    public class ManufacturerFilter : PagedAndSortedInputDto
    {
        public string Term { get; set; }
    }
}
