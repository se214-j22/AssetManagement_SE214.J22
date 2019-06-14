using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThanhLy:FullAuditModel
    {
        public string MaTS { get; set; }
        public string TenTS { get; set; }
        public int MaDonViMua { get; set; }
        public string DonViMua { get; set; }
        public string HinhThucThanhLy { get; set; }
        //Toàn phần or từng phần
        public long GiaTienThanhLy { get; set; }
        public string GhiChu { get; set; }
    }
}
