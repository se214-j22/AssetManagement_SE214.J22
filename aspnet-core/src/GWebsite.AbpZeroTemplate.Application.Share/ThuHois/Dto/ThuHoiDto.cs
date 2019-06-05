using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto
{

    /// <summary>
    /// <model cref="ThuHoi"></model>
    /// </summary>
    public class ThuHoiDto:Entity<int>
    {
        public int MaDV { get; set; }
        public string TenDonVi { get; set; }
        public int MaTS { get; set; }
        public string TenTaiSan { get; set; }
        public int SoLuongTh { get; set; }
        public DateTime NgayThuHoi { get; set; }
        public string LyDo { get; set; }
        public bool TrangThaiDuyet { get; set; }
        public string NoiDungTh { get; set; }
    }
}
