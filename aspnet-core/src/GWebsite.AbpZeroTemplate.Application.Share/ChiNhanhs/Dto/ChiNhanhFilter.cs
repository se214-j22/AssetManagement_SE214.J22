using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs.Dto
{
    /// <summary>
    /// <model cref="ChiNhanh"></model>
    /// </summary>
    public class ChiNhanhFilter : PagedAndSortedInputDto
    {
        public string TenChiNhanh { get; set; }
        
    }
}
