using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.User.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.User
{
    interface IUserAppService
    {
        void CreateOrEditUser(UserInput userInput);
        UserInput GetCustomerForEdit(int id);
        void DeleteUser(int id);
        PagedResultDto<UserDto> GetUser(UserFilter input);
        UserForView GetUserForView(int id);
    }
}
