using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs.Dto
{
    /// <summary>
    /// <model cref="QuanLyVanHanh"></model>
    /// </summary>
    public class QuanLyVanHanhInput : Entity<int>
    {
        public string SoXe { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? SoKM { get; set; }
        public int? XangTieuThu { get; set; }
        public string TrangThaiDaDuyet { get; set; }
        public string GhiChu { get; set; }
        public float? kmMoi { get; set; }
        public float? kmCu { get; set; }
    }
}