using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="Manufacturer"></model>
    /// </summary>
    public class ManufacturerDto : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Logo { get; set; }
    }
}
