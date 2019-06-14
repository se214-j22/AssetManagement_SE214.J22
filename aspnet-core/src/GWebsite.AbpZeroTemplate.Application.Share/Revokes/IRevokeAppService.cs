using System.Collections.Generic;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Revokes.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Revokes
{
    public interface IRevokeAppService
    {
        void CreateOrEditRevoke(RevokeInput revokeInput);
        RevokeInput GetRevokeForEdit(int id);
        void DeleteRevoke(int id);
        PagedResultDto<RevokeDto> GetRevokes(RevokeFilter input);
        RevokeForViewDto GetRevokeForView(int id);
        List<RevokeDto> GetListRevokeNotApproved();
        void ApproveRevoke(int id);
    }
}
