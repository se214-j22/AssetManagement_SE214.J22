using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class DieuChuyen :FullAuditModel
    {
        public int? MaNhanVienDC { get; set; }
        public int? MaNhanVienNhan { get; set; }
        public int? MaDonViDuocDieuChuyen { get; set; }
        public int? SoLuongDC { get; set; }
        public int? MaTaiSan { get; set; }
        public DateTime NgayDieuChuyen { get; set; }
      
    }
}
