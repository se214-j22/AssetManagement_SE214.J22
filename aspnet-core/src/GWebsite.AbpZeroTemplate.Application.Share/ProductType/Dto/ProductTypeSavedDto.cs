using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto
{
  public  class ProductTypeSavedDto : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
