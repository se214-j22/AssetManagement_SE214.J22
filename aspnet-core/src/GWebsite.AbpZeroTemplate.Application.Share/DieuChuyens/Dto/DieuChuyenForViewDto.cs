using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto
{
    public class DieuChuyenForViewDto
    {
        public String TenDVDieuChuyen { get; set; }
        public int? SoLuong { get; set; }
        public int? MaTaiSan { get; set; }
        public DateTime NgayDieuChuyen { get; set; }
        public String TenDVDuocDieuChuyen { get; set; }
    }
}
