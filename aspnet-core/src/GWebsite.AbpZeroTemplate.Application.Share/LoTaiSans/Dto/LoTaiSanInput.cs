using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto
{
    /// <summary>
    /// <model cref="LoTaiSan"></model>
    /// </summary>
    public class LoTaiSanInput : Entity<int>
    { 
        public int SoLuong { get; set; }
        public int TongGiaTri { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
