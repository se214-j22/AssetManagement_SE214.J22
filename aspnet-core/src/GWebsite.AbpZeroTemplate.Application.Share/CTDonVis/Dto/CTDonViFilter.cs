using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto
{
    /// <summary>
    /// <model cref="CTDonVi"></model>
    /// </summary>
    public class CTDonViFilter : PagedAndSortedInputDto
    {
        public string TenDonVi { get; set; }
    }
}
