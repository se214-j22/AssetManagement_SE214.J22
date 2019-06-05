using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans.Dto
{

    /// <summary>
    /// <model cref="XuatTaiSan"></model>
    /// </summary>
    public class XuatTaiSanFilter : PagedAndSortedInputDto
    {
        public string TenTaiSan { get; set; }
    }
}
