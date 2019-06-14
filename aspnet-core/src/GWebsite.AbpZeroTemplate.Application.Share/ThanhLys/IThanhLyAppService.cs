using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThanhLys.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThanhLys
{
    public interface IThanhLyAppService
    {
        void CreateOrEditThanhLy(ThanhLyInput thanhLyInput);
        ThanhLyInput GetThanhLyForEdit(int id);
        void DeleteThanhLy(int id);
        PagedResultDto<ThanhLyDto> GetThanhLys(ThanhLyFilter input);
        ThanhLyForViewDto GetThanhLyForView(int id);
        string[] GetArrTenDonVi();
    }
}
