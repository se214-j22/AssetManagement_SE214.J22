using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GSoft.AbpZeroTemplate.Organizations.Dto;

namespace GSoft.AbpZeroTemplate.Organizations
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        #region Organization Unit
        Task<OrganizationUnitDto> GetOrganizationUnit();

        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits();

        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);

        Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);

        Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);

        Task DeleteOrganizationUnit(EntityDto<long> input);
        #endregion
        #region User
        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);

        Task AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input);
        #endregion

        #region Asset
        Task<PagedResultDto<OrganizationUnitAssetListDto>> GetAssets(GetOrganizationUnitAssetsInput input);

        Task PlaceAssetsToOrganizationUnit(AssetsToOrganizationUnitInput input);

        Task<PagedResultDto<NameValueDto>> FindAssets(FindOrganizationUnitAssetsInput input);
        Task<WarehouseStatus> GetWarehouseStatus();
        #endregion
    }

}
