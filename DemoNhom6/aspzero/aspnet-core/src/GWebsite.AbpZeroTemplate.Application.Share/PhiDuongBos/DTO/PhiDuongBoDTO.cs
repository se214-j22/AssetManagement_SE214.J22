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
        public string ngayCapNhat { get; set; }
        public string ngayDongPhi { get; set; }
        public string ngayHetHanDongPhi { get; set; }
        public string thoiGianSuDung { get; set; }
        public string soTienThanhToan { get; set; }
        public string congTyThuPhi { get; set; }
        public string loaiPhi { get; set; }
        public string ghiChu { get; set; }
    }
}
