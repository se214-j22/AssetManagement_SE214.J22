using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto
{
    /// <summary>
    /// <model cref="Customer_QuanLyCongTrinhXayDung"></model>
    /// </summary>
    public class CustomerInput_QuanLyCongTrinhXayDung : Entity<int>
    {
        public string maBDS { get; set; }
        public string TenCongTrinh { get; set; }
        public string DiaChiCongTrinh { get; set; }
        public string DienTichCongTrinh { get; set; }

        public string LoaiThucHienCongTrinh { get; set; }
        public string ChiPhiDuToanBanDau { get; set; }
        public string ChiPhiThucHien { get; set; }
        public string ChiPhiPhatSinh { get; set; }
        public string MoTaCongTrinh { get; set; }
        public string ThoiGianDuKienHoanThanh { get; set; }
        public string ThoiGianHoanThanh { get; set; }
        public string TienDoCongTrinh { get; set; }


        public string SoToTrinh { get; set; }
        public string ChiPhiDuKien { get; set; }
        public string ChiPhiDuocDuyet { get; set; }

        public string MaGoiThau { get; set; }
        public string TenHangMucGoiThau { get; set; }
        public string DonViTrungThau { get; set; }
        public string GiaTriChaoThau { get; set; }
        public string NgayNopHoSoThau { get; set; }
        public string ThoiGianThiCong { get; set; }
        public string ThongTinDonViThamGiaThau { get; set; }

        public string TongGiaTriHopDong { get; set; }
        public string GiaTriDaThanhToanCuaHopDong { get; set; }
        public string TienDoThiCong { get; set; }
        public string ThoiGianThanhToan { get; set; }


    }
}
