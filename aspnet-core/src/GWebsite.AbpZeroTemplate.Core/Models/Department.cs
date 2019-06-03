using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class Department : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Major { get; set; }
        //public ICollection<Purchase> Purchases { get; set; }
        
        public Department()
        {
            //Purchases = new Collection<Purchase>();
         
        }
    }
}
