using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto
{

    /// <summary>
    /// <model cref="TaiSan"></model>
    /// </summary>
    public class TaiSanFilter: PagedAndSortedInputDto
    {
        public string TenTs { get; set; }
        public string MaTS { get; set; }
        public string LoaiTS { get; set; }
        public string TenNhomTS { get; set; }
        public string SoSeri { get; set; }
        public string TenDV { get; set; }
    }
}
