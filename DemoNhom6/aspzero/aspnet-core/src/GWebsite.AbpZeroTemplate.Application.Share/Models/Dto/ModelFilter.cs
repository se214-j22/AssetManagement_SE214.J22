using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Models.Dto
{
   public class ModelFilter : PagedAndSortedInputDto
    {
        public string model { get; set; }
    }
}
