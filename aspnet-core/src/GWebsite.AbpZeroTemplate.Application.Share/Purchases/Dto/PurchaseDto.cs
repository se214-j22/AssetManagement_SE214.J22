using GSoft.AbpZeroTemplate.Authorization.Users;
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
    public partial class PurchaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public DepartmentDto Department { get; set; }
        public DateTime RaisedDate { get; set; }
        public DateTime AuthorizedDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public PurchaseDto()
        {
            PurchaseProducts = new Collection<PurchaseProduct>();
        }
    }
}
