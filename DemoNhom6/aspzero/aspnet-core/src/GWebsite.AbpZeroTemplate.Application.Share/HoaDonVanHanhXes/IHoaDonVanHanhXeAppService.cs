using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes
{
  public  interface IHoaDonVanHanhXeAppService
    {
        void CreateOrEditHoaDonVanHanhXe(HoaDonVanHanhXeInput input);
        HoaDonVanHanhXeInput GetHoaDonVanHanhXeForEdit(string soHoaDon);
        void DeleteHoaDonVanHanhXe(string soHoaDon);
        PagedResultDto<HoaDonVanHanhXeDto> GetHoaDonVanHanhXes(HoaDonVanHanhXeFilter input);
        HoaDonVanHanhXeForViewDto GetHoaDonVanHanhXeForView(string soHoaDon);
    }
}
