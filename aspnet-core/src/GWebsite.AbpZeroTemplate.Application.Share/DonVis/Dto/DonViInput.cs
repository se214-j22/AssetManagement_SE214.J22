using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.DonVis.Dto
{

    /// <summary>
    /// <model cref="DonVi"></model>
    /// </summary>
    public class DonViInput:Entity<int>
    {
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
    }
}

