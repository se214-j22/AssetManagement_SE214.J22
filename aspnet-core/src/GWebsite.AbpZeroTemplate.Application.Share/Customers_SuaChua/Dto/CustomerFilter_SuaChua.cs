using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua.Dto
{
    /// <summary>
    /// <model cref="Customer_SuaChua"></model>
    /// </summary>
    public class CustomerFilter_SuaChua : PagedAndSortedInputDto
    {
        public string maBDS { get; set; }
    }
}
