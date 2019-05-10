using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products.Dto
{
    /// <summary>
    /// <model cref="ProductInput"></model>
    /// </summary>
    public class ProductInput : Entity<int>
    {
        public string Name { get; set; }
    }
}
