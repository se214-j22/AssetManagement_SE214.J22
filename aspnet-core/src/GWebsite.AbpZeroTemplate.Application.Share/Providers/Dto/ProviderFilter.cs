using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto
{
    /// <summary>
    /// <model cref="Provider"></model>
    /// </summary>
    public class ProviderFilter : PagedAndSortedInputDto
    {
        public string Name { get; set; }
    }
}
