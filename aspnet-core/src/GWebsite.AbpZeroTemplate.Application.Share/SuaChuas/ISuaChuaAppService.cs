using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.SuaChuas
{
    public interface ISuaChuaAppService
    {
        void CreateOrEditSuaChua(SuaChuaInput suaChuaInput);
        SuaChuaInput GetSuaChuaForEdit(int id);
        void DeleteSuaChua(int id);
        PagedResultDto<SuaChuaDto> GetSuaChuas(SuaChuaFilter input);
        SuaChuaForViewDto GetSuaChuaForView(int id);
        string[] GetArrTenDVSC();
        string[] GetArrTenNVPT(string tenDV);
        string[] GetArrTenNVDX();
        string GetTenDVDX(string tenNV);
    }
}
