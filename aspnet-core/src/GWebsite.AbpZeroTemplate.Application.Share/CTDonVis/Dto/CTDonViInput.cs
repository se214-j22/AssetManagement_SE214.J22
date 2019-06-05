using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto
{
    /// <summary>
    /// <model cref="CTDonVi"></model>
    /// </summary>
    public class CTDonViInput : Entity<int>
    {
        public int MaDV { get; set; }
        public int MaTS { get; set; }
        public int SoLuong { get; set; }
        public string TenDonVi { get; set; }
        public string TenTaiSan { get; set; }
    }
}
