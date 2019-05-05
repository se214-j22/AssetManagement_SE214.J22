using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class Supplier : Entity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }
        public ICollection<Bidding> Biddings { get; set; }
        public Supplier()
        {
            Biddings = new Collection<Bidding>();
        }
    }
}

