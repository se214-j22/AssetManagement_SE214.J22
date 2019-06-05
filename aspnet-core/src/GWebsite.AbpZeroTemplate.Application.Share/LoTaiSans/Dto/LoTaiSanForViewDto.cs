using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto
{
    /// <summary>
    /// <model cref="Customer"></model>
    /// </summary>
    public class LoTaiSanForViewDto
    {
        public int SoLuong { get; set; }
        public int TongGiaTri { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
