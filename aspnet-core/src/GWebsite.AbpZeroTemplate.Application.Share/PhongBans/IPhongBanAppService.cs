using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhongBan.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhongBan
{
    public interface IPhongBanAppService
    {
        void CreateOrEditPhongBan(PhongBanInput phongbanInput);
        PhongBanInput GetPhongBanForEdit(int id);
        void DeletePhongBan(int id);
        PagedResultDto<PhongBanDto> GetPhongBans(PhongBanFilter input);
        PhongBanForViewDto GetPhongBanForView(int id);
    }
}
