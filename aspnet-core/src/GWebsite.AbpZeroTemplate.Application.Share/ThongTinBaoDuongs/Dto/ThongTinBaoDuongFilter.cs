using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs.Dto
{
    /// <summary>
    /// <model cref="ThongTinBaoDuong"></model>
    /// </summary>
    public class ThongTinBaoDuongFilter : PagedAndSortedInputDto
    {
        public string SoXe { get; set; }
    }
}