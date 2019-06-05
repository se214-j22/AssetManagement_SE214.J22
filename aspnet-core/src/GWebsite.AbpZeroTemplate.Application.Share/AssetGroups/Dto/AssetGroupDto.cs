using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.AssetGroups.Dto
{
    /// <summary>
    /// <model cref="AssetGroup"></model>
    /// </summary>
    public class AssetGroupDto : Entity<int>
    {
        //Mã nhóm tài sản
        public string AssetGrouptId { get; set; }
        //Tên nhóm tài sản
        public string AssetGroupName { get; set; }
        //Loại tài sản
        public int AssetType { get; set; }
        //Nhóm tài sản cha
        public string AssetGroupParentId { get; set; }
    }
}