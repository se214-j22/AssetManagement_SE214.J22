using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto
{
    /// <summary>
    /// <model cref="Customer_QuanLyCongTrinhXayDung"></model>
    /// </summary>
    public class CustomerDto_QuanLyCongTrinhXayDung : Entity<int>
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
    }
}