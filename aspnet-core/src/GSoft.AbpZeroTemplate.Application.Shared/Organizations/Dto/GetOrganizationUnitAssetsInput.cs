using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class GetOrganizationUnitAssetsInput : PagedAndSortedInputDto, IShouldNormalize
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "code";
            }
            else if (Sorting.Contains("addedTime"))
            {
                Sorting = Sorting.Replace("addedTime", "aou.creationTime");
            }
        }
    }
}