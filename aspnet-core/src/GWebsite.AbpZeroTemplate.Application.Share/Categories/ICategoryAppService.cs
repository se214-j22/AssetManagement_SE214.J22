using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Categories.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Categories
{
    public interface ICategoryAppService
    {
        void CreateOrEditCategory(CategoryInput categoryInput);
        CategoryInput GetCategoryForEdit(int id);
        void DeleteCategory(int id);
        PagedResultDto<CategoryDto> GetCategories(CategoryFilter input);
        CategoryForViewDto GetCategoryForView(int id);
    }
}
