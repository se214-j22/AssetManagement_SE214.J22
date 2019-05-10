using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products.Dto
{
    /// <summary>
    /// <model cref="Product"></model>
    /// </summary>
    public class ProductDto : Entity<int>
    {
        public string Name { get; set; }
    }
}
