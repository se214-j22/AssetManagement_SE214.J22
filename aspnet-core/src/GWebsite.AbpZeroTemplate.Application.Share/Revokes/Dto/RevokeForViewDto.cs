using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Revokes.Dto
{
    /// <summary>
    /// <model cref="Revoke"></model>
    /// </summary>
    public class RevokeForViewDto
    {
        //Ngày thu hồi
        public string RevokeDate { get; set; }
        //Mã tài sản
        public string AssetId { get; set; }
        //Lý do
        public string Reason { get; set; }
        //Nội dung thu hồi
        public string RevokeContent { get; set; }
        //Tình trạng tài sản/ công cụ
        public string AssetStatus { get; set; }
        //Vị trí: vị trí hiện tại của tài sản
        public string CurrentLocationOfAssets { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}