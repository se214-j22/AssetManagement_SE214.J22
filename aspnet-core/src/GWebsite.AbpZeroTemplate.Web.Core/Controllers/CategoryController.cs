using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Categories;
using GWebsite.AbpZeroTemplate.Application.Share.Categories.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : GWebsiteControllerBase
    {
        private readonly ICategoryAppService categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            this.categoryAppService = categoryAppService;
        }

        [HttpGet]
        public PagedResultDto<CategoryDto> GetCategoriesByFilter(CategoryFilter categoryFilter)
        {
            return categoryAppService.GetCategories(categoryFilter);
        }

        [HttpGet]
        public CategoryInput GetCategoryForEdit(int id)
        {
            return categoryAppService.GetCategoryForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCategory([FromBody] CategoryInput input)
        {
            categoryAppService.CreateOrEditCategory(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
            categoryAppService.DeleteCategory(id);
        }

        [HttpGet]
        public CategoryForViewDto GetCategoryForView(int id)
        {
            return categoryAppService.GetCategoryForView(id);
        }
    }
}