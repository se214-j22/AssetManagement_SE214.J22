using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap.Dto
{
  public  class NhaCungCapForViewDto
    {
        public string maCongTyBaoHiem { get; set; }
        public string tenCongTyBaoHiem { get; set; }
        public string diaChi { get; set; }
        public string email { get; set; }
        public string maSoThue { get; set; }
        public string soDienThoai { get; set; }
        public string nguoiLienHe { get; set; }
        public bool hoatDong { get; set; }
        public string ghiChu { get; set; }
    }
}
