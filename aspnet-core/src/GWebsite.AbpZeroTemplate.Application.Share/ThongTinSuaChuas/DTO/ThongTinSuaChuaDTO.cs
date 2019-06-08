using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO
{
    public class ThongTinSuaChuaDTO : Entity<int>
    {
        public string soXe { get; set; }
        public DateTime? ngaySuaChua { get; set; }
        public DateTime? ngayDuKienSuaXong { get; set; }
        public double? chiPhiSuaChua { get; set; }
        public string noiDungSuaChuaThucTe { get; set; }
        public string trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }
    }
}
