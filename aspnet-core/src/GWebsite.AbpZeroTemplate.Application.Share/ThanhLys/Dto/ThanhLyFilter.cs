using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThanhLys.Dto
{

    /// <summary>
    /// <model cref="NhomTaiSan"></model>
    /// </summary>
    public class ThanhLyFilter : PagedAndSortedInputDto
    {
        public string TenTS { get; set; }

    }
}
