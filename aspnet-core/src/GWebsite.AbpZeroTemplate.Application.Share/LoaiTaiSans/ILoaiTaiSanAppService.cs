using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans
{
    public interface ILoaiTaiSanAppService
    {
        void CreateOrEditLoaiTaiSan(LoaiTaiSanInput loaiTaiSanInput);
        LoaiTaiSanInput GetLoaiTaiSanForEdit(int id);
        void DeleteLoaiTaiSan(int id);
        PagedResultDto<LoaiTaiSanDto> GetLoaiTaiSans(LoaiTaiSanFilter input);
        LoaiTaiSanForViewDto GetLoaiTaiSanForView(int id);
    }
}
