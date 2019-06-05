using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto
{

    /// <summary>
    /// <model cref="NhomTaiSan"></model>
    /// </summary>
    public class NhomTaiSanForViewDto
    {
        public string tenNhomTaiSan { get; set; }
        public string loaiTaiSan { get; set; }
        public int? soThangKhauHao { get; set; }
        public int? tyLeKhauHao { get; set; }
    }
}
