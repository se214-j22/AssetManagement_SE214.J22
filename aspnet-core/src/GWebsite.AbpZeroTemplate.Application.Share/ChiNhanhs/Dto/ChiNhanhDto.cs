using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs.Dto
{
    /// <summary>
    /// <model cref="ChiNhanh"></model>
    /// </summary>
    public class ChiNhanhDto : Entity<int>
    {
        public string TenChiNhanh { get; set; }
        public int MaDonVi { get; set; }
        public string DiaChi { get; set; }
        public string TenDonVi { get; set; }
    }
}
