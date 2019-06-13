using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Transfers.Dto
{
    /// <summary>
    /// <model cref="Transfer"></model>
    /// </summary>
    public class TransferFilter : PagedAndSortedInputDto
    {
        //Mã tài sản
        public string AssetId { get; set; }
    }
}