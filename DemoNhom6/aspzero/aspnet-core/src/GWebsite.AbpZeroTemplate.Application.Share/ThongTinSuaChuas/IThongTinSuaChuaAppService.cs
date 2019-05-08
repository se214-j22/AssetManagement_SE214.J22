using GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO;
using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas
{
    public interface IThongTinSuaChuaAppService
    {
        void CreateOrEditThongTinSuaChua(ThongTinSuaChuaInput thongTinSuaChuaInput);
        ThongTinSuaChuaInput GetThongTinSuaChuaForEdit(int id);
        void DeleteThongTinSuaChua(int id);
        PagedResultDto<ThongTinSuaChuaDTO> GetThongTinSuaChuas(ThongTinSuaChuaFilter input);
        ThongTinSuaChuaForViewDTO GetThongTinSuaChuaForView(int id);

    }
}
