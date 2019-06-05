using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.DonVis.Dto
{

    /// <summary>
    /// <model cref="DonVi"></model>
    /// </summary>
    public class DonViFilter: PagedAndSortedInputDto
    {
        public string TenDonVi { get; set; }
    }
}
