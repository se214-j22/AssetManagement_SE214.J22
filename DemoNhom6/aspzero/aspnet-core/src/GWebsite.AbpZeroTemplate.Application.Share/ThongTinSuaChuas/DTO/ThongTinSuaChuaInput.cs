using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO
{
    public class ThongTinSuaChuaInput : Entity<int>
    {
        public string soXe { get; set; }
        public string ngaySuaChua { get; set; }
        public string ngayDuKienSuaXong { get; set; }
        public string chiPhiSuaChua { get; set; }
        public string noiDungSuaChuaThucTe { get; set; }
        public bool trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }
    }
}
