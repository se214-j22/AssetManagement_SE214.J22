using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductController : GWebsiteControllerBase
    {
        private readonly IProductAppService productAppService;

        public ProductController(IProductAppService productAppService)
        {
            this.productAppService = productAppService;
        }

        //[HttpPost]
        //public void Create([FromBody] ProductInput input)
        //{
        //    productAppService.Create(input);
        //}

        [HttpPut]
        public void CreateOrEdit([FromBody] ProductInput input)
        {
            productAppService.CreateOrEdit(input);
        }
    }
}
