using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes.Dto
{
    public class ThongTinXeDto : Entity<int>
    {
        public string soXe { get; set; }
        public string model { get; set; }
        public string nuocSanXuat { get; set; }
        public string loaiNhienLieu { get; set; }
        public int? namSanXuat { get; set; }
        public string mauXe { get; set; }
        public string maTaiSan { get; set; }
        public string mucDichSuDung { get; set; }
        public DateTime? ngayDangKiBanDau { get; set; }
        public string soMay { get; set; }
        public string soSuon { get; set; }
        public string coLopSuDung { get; set; }
        public string kieuDongCo { get; set; }
        public string loaiHopSo { get; set; }
        public string theTichDongCo { get; set; }
        public string chieuDai { get; set; }
        public string chieuCao { get; set; }
        public string chieuNgang { get; set; }
        public string trangThaiDuyet { get; set; }
        public string donViSuDung { get; set; }
        public string tenChuPhuongTien { get; set; }
    }
}
