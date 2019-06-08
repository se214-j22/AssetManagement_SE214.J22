using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems.Dto
{
    /// <summary>
    /// <model cref="ThongTinDangKiem"></model>
    /// </summary>
    public class ThongTinDangKiemInput : Entity<int>
    {
        public string soXe { get; set; }
        public DateTime ngayDangKiem { get; set; }
        public DateTime ngayHetHanDangKiem { get; set; }
        public int? thoiHanDangKiem { get; set; }
        public string coQuanDangKiem { get; set; }
        public string trangThaiDuyet { get; set; }
        public string ghiChu { get; set; }
    }
}
