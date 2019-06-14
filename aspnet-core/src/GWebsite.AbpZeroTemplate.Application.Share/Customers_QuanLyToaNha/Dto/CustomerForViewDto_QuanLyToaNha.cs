using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha.Dto
{
    /// <summary>
    /// <model cref="Customer_QuanLyToaNha"></model>
    /// </summary>
    public class CustomerForViewDto_QuanLyToaNha
    {

        public string maBDS { get; set; }

        public string ThongTinKhachThue { get; set; }
        public string PhiDichVu { get; set; }
        public string NgayVao { get; set; }
        public string NgayRa { get; set; }
        public string GiaHan { get; set; }
        public string KhuVucThue { get; set; }
        public string LichSuThueSanPham { get; set; }
        public string DanhSachSanPham { get; set; }
    }
}
