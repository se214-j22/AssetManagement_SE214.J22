using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems.Dto
{
    /// <summary>
    /// <model cref="ThongTinBaoHiem"></model>
    /// </summary>
    public class ThongTinBaoHiemForViewDto
    {
        public string soXe { get; set; }
        public DateTime ngayMuaBaoHiem { get; set; }
        public DateTime ngayHetHanBaoHiem { get; set; }
        public DateTime thoiHanBaoHiem { get; set; }
        public string congTyBaoHiem { get; set; }
        public string loaiBaoHiem { get; set; }
        public int soTienThanhToan { get; set; }
        public string trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }
    }
}
