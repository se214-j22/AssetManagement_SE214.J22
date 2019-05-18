using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto
{
    public class LoaiTaiSanDto : Entity<int>
    {
        public string TenNhomTaiSan { get; set; }
        public float TiLeHaoMon { get; set; }
        public float ThoiHanSuDung { get; set; }
    }
}