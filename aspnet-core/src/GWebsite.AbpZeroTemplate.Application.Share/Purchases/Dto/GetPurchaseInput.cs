using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace GWebsite.AbpZeroTemplate.Application.Share.Purchases.Dto
{
    public class GetPurchaseInput : Pagination
    {
        public string Name { get; set; }
    }
}
