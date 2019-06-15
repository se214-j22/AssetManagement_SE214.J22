using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products.Dto
{
    public partial class ProductDto :Entity<int>
    {
        public string Name { get; set; }
        public float Discount { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UnitPrice { get; set; }
        public string CalUnit { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        //public ImageDto Image { get; set; }
        //public ICollection<TitleBiddingProduct> Biddings { get; set; }
        public SupplierDtoTitle Supplier { get; set; }
        public SupplierTypeDtoTitle ProductType { get; set; }
    }
}
