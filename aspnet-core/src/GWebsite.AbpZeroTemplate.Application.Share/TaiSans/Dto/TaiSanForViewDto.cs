using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto
{

    /// <summary>
    /// <model cref="TaiSan"></model>
    /// </summary>
    public class TaiSanForViewDto
    {
        public string TenTaiSan { get; set; }
        public string LoaiTaiSan { get; set; }
        public string NguyenGia { get; set; }
        public int? TrangThai { get; set; }
        public int? SoLuong { get; set; }
    }
}
