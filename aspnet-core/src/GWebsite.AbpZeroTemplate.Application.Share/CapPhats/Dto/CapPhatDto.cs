using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto
{
    // <summary>
    /// <model cref="CapPhat"></model>
    /// </summary>
    public class CapPhatDto:Entity<int>
    {
        public string TenDonVi { get; set; }
        public int? SoLuong { get; set; }
        public int? MaTaiSan { get; set; }
        public DateTime NgayCapPhat { get; set; }
    }
}
