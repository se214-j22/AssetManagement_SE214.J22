using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhiDuongBos
{
    public class PhiDuongBoDTO : Entity<int>
    {
        public string soXe { get; set; }
        public DateTime? ngayCapNhat { get; set; }
        public DateTime? ngayDongPhi { get; set; }
        public DateTime? ngayHetHanDongPhi { get; set; }
        public int? thoiGianSuDung { get; set; }
        public long? soTienThanhToan { get; set; }
        public string congTyThuPhi { get; set; }
        public string loaiPhi { get; set; }
        public string ghiChu { get; set; }
    }
}
