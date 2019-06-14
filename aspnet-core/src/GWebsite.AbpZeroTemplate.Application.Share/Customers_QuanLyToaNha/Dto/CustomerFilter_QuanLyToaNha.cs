using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha.Dto
{
    /// <summary>
    /// <model cref="Customer_QuanLyToaNha"></model>
    /// </summary>
    public class CustomerFilter_QuanLyToaNha : PagedAndSortedInputDto
    {
        public string maBDS { get; set; }
    }
}
