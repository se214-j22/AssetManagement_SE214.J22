using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto
{

    /// <summary>
    /// <model cref="TaiSan"></model>
    /// </summary>
    public class CTTaiSanDto:Entity<int>
    {
        public int MaTS { get; set; }
        public int MaLo { get; set; }
        public string SoSeri { get; set; }
        public int MaXuatTS { get; set; }
        public int MADC { get; set; }
        public int MATH { get; set; }
        public int MaSC { get; set; }
        public int MaTL { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; }
    }
}
