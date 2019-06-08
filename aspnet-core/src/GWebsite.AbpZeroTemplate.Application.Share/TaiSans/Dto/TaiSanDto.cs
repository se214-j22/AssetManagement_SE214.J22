using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto
{
   public class TaiSanDto : Entity<int>
    {
        public string maTaiSan { get; set; }
        public string tenTaiSan { get; set; }
        public string nhomTaiSan { get; set; }
        public string loaiTaiSan { get; set; }
        public string thongTinMoTa { get; set; }
        public long? nguyenGiaTaiSan { get; set; }
        public string donViSuDung { get; set; }
        public string tinhTrangTaiSan { get; set; }
        public string nguoiSuDung { get; set; }
        public string tinhTrangKhauHao { get; set; }
    }
}
