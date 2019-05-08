using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes
{
    public interface IThongTinXeAppService
    {
        void CreateOrEditThongTinXe( ThongTinXeInput thongTinXeInput);
        ThongTinXeInput GetThongTinXeForEdit(string soXe);
        void DeleteThongTinXe(string soXe);
        PagedResultDto<ThongTinXeDto> GetThongTinXes(ThongTinXeFilter filter);
        ThongTinXeForViewDto GetThongTinXeForView(string soXe);


    }
}
