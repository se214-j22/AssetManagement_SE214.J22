using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto
{
    public partial class ProductTitleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Summary { get; set; }
        public string AvailableAddress { get; set; }
        public DateTime AddedDate { get; set; }
        public ImageDto Image { get; set; }
    }
}
