using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class QuanLyVanHanh : FullAuditModel
    {
        public  string SoXe { get; set; }
        public  DateTime? NgayCapNhat { get; set; }
        public  int? SoKM { get; set; }
        public  int? XangTieuThu { get; set; }
        public  string  TrangThaiDaDuyet { get; set; }
        public  string GhiChu { get; set; }
        public float? kmMoi { get; set; }
        public float? kmCu { get; set; }

    }
}
