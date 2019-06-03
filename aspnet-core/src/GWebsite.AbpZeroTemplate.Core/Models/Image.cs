using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class Image : Entity<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        //public int ProductId { get; set; }
        //public Product Product { get; set; }
    }
}
