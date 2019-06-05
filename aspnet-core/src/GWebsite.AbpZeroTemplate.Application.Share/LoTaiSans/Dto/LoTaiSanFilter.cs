using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto
{
    /// <summary>
    /// <model cref="LoTaiSan"></model>
    /// </summary>
    public class LoTaiSanFilter : PagedAndSortedInputDto
    {
        public int SoLuong { get; set; }
    }
}
