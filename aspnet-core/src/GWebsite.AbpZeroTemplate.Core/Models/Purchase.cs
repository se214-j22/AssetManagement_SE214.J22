using Abp.Domain.Entities;
using GSoft.AbpZeroTemplate.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class Purchase : Entity<int>
    {
        public User User { get; set; }
        public int? UserId { get; set; }
        public int Level { get; set; }
        public ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? RaisedDate { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ReceivedDate { get; set; }

        public Purchase()
        {
            RaisedDate = DateTime.Now;
            PurchaseProducts = new Collection<PurchaseProduct>();
        }
    }
}
