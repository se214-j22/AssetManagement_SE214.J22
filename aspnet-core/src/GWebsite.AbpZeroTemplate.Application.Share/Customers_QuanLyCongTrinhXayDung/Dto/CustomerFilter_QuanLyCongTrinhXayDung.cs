using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto
{
    /// <summary>
    /// <model cref="Customer_QuanLyCongTrinhXayDung"></model>
    /// </summary>
    public class CustomerFilter_QuanLyCongTrinhXayDung : PagedAndSortedInputDto
    {
        public string maBDS { get; set; }
    }
}
