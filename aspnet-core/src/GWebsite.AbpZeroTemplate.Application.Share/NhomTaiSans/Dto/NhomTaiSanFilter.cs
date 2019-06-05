using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto
{

    /// <summary>
    /// <model cref="NhomTaiSan"></model>
    /// </summary>
    public class NhomTaiSanFilter: PagedAndSortedInputDto
    {
        public string tenNhomTaiSan { get; set; }
    }
}
