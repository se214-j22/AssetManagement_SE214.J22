using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Categories.Dto
{
    /// <summary>
    /// <model cref="Category"></model>
    /// </summary>
    public class CategoryFilter : PagedAndSortedInputDto
    {
        public string Name { get; set; }
    }
}
