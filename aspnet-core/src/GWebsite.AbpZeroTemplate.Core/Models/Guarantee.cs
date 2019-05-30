using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial  class Guarantee : Entity<int>
    {
        public int Type { get; set; }
        public int Amount { get; set; }
        public string BankName { get; set; }
        public float Percent { get; set; }
        public float Fee { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}
