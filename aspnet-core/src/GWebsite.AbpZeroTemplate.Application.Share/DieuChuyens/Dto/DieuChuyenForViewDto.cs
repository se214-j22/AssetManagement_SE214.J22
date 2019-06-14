using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto
{

    /// <summary>
    /// <model cref="DieuChuyen"></model>
    /// </summary>
    public class DieuChuyenForViewDto
    {

        public string MaTaiSan { get; set; }
        public string TenTaiSan { get; set; }
        public int MaDVDC { get; set; }
        public string TenDonViDC { get; set; }
        public int MaDVNhan { get; set; }
        public string TenDonViNhan { get; set; }
        public int MaNhanVienNhan { get; set; }
        public string TenNhanVienNhan { get; set; }
        public DateTime NgayDieuChuyen { get; set; }
        public string GhiChu { get; set; }
    }
}
