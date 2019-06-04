using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Projects.Dto
{ 
    public partial class GetProjectInput : PagedAndSortedInputDto , IShouldNormalize
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name";
            }
        }
    }


    public partial class ModelName 
    {
        
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public partial class ModelStatus
    {
        public int Status { get; set; }
        public int Id { get; set; }
    }
}
