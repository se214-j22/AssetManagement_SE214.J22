using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.User.Dto
{
    class UserFilter: PagedAndSortedInputDto
    {
        public string UserId { get; set; }
    }
}
