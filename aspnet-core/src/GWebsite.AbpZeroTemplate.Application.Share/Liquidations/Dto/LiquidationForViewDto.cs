using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto
{
    /// <summary>
    /// <model cref="Liquidation"></model>
    /// </summary>
    public class LiquidationForViewDto
    {
        // tên nhà thanh lý
        public string LiquidatorName { get; set; }
        // số hợp đồng
        public string ContractCode { get; set; }
        // ngày thanh lý
        public DateTime Date { get; set; }
        // tổng tài sản thanh lý
        public string TotalAsset { get; set; }
        // tổng giá tiền thanh lý
        public string TotalPrice { get; set; }
    }
}
