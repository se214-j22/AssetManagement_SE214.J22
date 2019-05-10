using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products
{
    public interface IProductAppService
    {
        //void Create(ProductInput input);
        void CreateOrEdit(ProductInput productInput);
    }
}
