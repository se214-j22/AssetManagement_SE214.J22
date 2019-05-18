using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="AssetLine"></model>
    /// </summary>
    public class AssetLineInput : Entity<int>
    {
        [StringLength(3,MinimumLength =3)]
        public string Code { get; set; }
        public string Name { get; set; }
        public int AssetTypeID { get; set; }
        public int ManufacturerID { get; set; }
    }
}
