using Abp.Application.Services.Dto;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GSoft.AbpZeroTemplate.Authorization.Users.Dto;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.User
{
    class UserAppService : GWebsiteAppServiceBase, IUserAppService
    {
        Task IUserAppService.CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            throw new NotImplementedException();
        }

        Task IUserAppService.DeleteUser(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        Task<GetUserForEditOutput> IUserAppService.GetUserForEdit(NullableIdDto<long> input)
        {
            throw new NotImplementedException();
        }

        Task<GetUserPermissionsForEditOutput> IUserAppService.GetUserPermissionsForEdit(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        Task<PagedResultDto<UserListDto>> IUserAppService.GetUsers(GetUsersInput input)
        {
            throw new NotImplementedException();
        }

        Task<FileDto> IUserAppService.GetUsersToExcel()
        {
            throw new NotImplementedException();
        }

        Task IUserAppService.ResetUserSpecificPermissions(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        Task IUserAppService.UnlockUser(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        Task IUserAppService.UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            throw new NotImplementedException();
        }
    }
}
