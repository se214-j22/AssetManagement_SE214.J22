using GSoft.AbpZeroTemplate.Dto;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class FindOrganizationUnitAssetsInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
