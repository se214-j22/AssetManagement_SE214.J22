using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails.Dto
{
    /// <summary>
    /// <model cref="LiquidationDetail"></model>
    /// </summary>
    public class LiquidationDetailFilter : PagedAndSortedInputDto
    {
        public string LiquidationID { get; set; }
    }
}
