using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto
{
    // <summary>
    /// <model cref="ThuHoi"></model>
    /// </summary>
    public class ThuHoiDto:Entity<int>
    {
        public string TenDonViThuHoi { get; set; }
        public int? MaTaiSan { get; set; }
        public DateTime NgayThuHoi { get; set; }
        public int? SoLuong { get; set; }
    }
}
