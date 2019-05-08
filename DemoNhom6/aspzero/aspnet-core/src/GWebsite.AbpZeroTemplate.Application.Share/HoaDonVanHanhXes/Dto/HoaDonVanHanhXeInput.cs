using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes.Dto
{
   public class HoaDonVanHanhXeInput : Entity<int>
    {
        public string soXe { get; set; }
        public string soHoaDon { get; set; }
        public DateTime? ngayXuat { get; set; }
        public long soTien { get; set; }
    }
}
