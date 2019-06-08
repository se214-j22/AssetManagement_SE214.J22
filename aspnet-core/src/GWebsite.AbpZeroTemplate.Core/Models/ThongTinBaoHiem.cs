using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThongTinBaoHiem :FullAuditModel
    {
        public string soXe { get; set; }
        public DateTime? ngayMuaBaoHiem { get; set; }
        public DateTime? ngayHetHanBaoHiem { get; set; }
        public int? thoiHanBaoHiem { get; set; }
        public string congTyBaoHiem { get; set; }
        public string loaiBaoHiem { get; set; }
        public double? soTienThanhToan { get; set; }
        public string trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }
    }
}
