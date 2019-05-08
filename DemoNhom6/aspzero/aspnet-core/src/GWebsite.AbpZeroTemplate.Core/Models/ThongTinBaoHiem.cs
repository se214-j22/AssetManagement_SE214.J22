using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThongTinBaoHiem : FullAuditModel
    {
        /*1.	Số xe
2.	Ngày mua bảo hiểm
3.	Ngày hết hạn bảo hiểm
4.	Thời hạn bảo hiểm
5.	Công ty bảo hiểm
6.	Loại bảo hiểm
7.	Số tiền thanh toán
8.	Trạng thái duyệt
9.	Ghi chú
*/
        public string soXe { get; set; }
        public DateTime ngayMuaBaoHiem { get; set; }
        public DateTime ngayHetHanBaoHiem { get; set; }
        public DateTime thoiHanBaoHiem { get; set; }
        public string congTyBaoHiem { get; set; }
        public string loaiBaoHiem { get; set; }
        public int? soTienThanhToan { get; set; }
        public string trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }

    }
}
