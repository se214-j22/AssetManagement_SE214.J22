using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="Asset"></model>
    /// </summary>
    public class AssetDto : Entity<int>
    {
        //Mã tài sản
        public string AssetId { get; set; }
        //Loại tài sản
        public int AssetType { get; set; }
        //Tên tài sản
        public string AssetName { get; set; }
        //Mã nhóm tài sản
        public string AssetGrouptId { get; set; }
        //Ngày nhập
        public string DateAdded { get; set; }
        //Nhà cung cấp
        public string ProviderId { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}