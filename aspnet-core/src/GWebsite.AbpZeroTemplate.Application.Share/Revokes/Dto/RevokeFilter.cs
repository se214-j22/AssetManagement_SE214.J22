using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Revokes.Dto
{
    /// <summary>
    /// <model cref="Revoke"></model>
    /// </summary>
    public class RevokeFilter : PagedAndSortedInputDto
    {
        //Mã tài sản
        public string AssetId { get; set; }
    }
}
