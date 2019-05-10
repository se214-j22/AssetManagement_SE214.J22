using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products
{
    public interface IProductAppService
    {
        Task CreateOrEdit(ProductInput productInput);
    }
}
