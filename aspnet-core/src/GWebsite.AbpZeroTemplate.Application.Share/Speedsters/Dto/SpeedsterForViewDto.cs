using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Speedsters.Dto
{
    public class SpeedsterForViewDto : Entity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
    }
}
