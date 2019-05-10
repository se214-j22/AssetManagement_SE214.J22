using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto
{
    /// <summary>
    /// <model cref="Provider"></model>
    /// </summary>
    public class ProviderForViewDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Info { get; set; }
    }
}
