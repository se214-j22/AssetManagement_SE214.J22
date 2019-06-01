using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto
{
    public class ProductTypeListInputDto : GetMenuClientInput
    {
        public string Code { get; set; }
        public int Status { get; set; }
    }
}
