using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Purchases.Dto
{
    public partial class PurchaseSave :  Entity<int>
    {
        public ICollection<ProductSaved> PurchaseProducts { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public int DepartmentId { get; set; }
        public DateTime RaisedDate { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ReceivedDate { get; set; }

        public PurchaseSave()
        {
            RaisedDate = DateTime.Now;
            AuthorizedDate = DateTime.Now;
            PurchaseProducts = new Collection<ProductSaved>();
        }
    }
}
