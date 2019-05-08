using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs.Dto
{
    /// <summary>
    /// <model cref="ThongTinBaoDuong"></model>
    /// </summary>
    public class ThongTinBaoDuongInput : Entity<int>
    {
        public string SoXe { get; set; }
        public DateTime? NgayBaoDuong { get; set; }
        public DateTime? NgayBaoDuongTiepTheo { get; set; }
        public int? SoTienThanhToan { get; set; }
        public string HangMucBaoDuong { get; set; }
        public string DonViBaoDuong { get; set; }
        public int? TrangThaiDuyet { get; set; }
    }
}