using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Application.Share.User.Dto
{
    class UserInput: Entity<int>
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
    }
}
