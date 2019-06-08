using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs.Dto
{
    /// <summary>
    /// <model cref="QuanLyVanHanh"></model>
    /// </summary>
    public class QuanLyVanHanhFilter : PagedAndSortedInputDto
    {
        public string SoXe { get; set; }
    }
}