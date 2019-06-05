using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Repairs.Dto
{
    /// <summary>
    /// <model cref="Repair"></model>
    /// </summary>
    public class RepairFilter : PagedAndSortedInputDto
    {
        //Mã tài sản
        public string AssetId { get; set; }
    }
}
