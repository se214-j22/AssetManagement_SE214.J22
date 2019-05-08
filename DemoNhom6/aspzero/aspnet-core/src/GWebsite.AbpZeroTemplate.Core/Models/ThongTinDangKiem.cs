using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThongTinDangKiem : FullAuditModel
    {
        /*1.	Số xe
2.	Ngày đăng kiểm
3.	Ngày hết hạn đăng kiểm 
4.	Thời hạn đăng kiểm
5.	Cơ quan đăng kiểm
6.	Trạng thái duyệt
7.	Ghi chú
*/
        public string soXe { get; set; }
        public DateTime ngayDangKiem { get; set; }
        public DateTime ngayHetHanDangKiem { get; set; }
        public DateTime thoiHanDangKiem { get; set; }
        public string coQuanDangKiem { get; set; }
        public string trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }
    }

}
