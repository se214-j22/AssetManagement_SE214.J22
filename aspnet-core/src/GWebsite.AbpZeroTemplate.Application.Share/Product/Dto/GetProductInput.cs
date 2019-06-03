using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Product.Dto
{
    public class GetProductInput : PagedAndSortedInputDto
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
     
    }
}
