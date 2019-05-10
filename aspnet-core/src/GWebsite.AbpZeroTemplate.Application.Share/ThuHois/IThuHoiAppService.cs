using GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThuHois
{
    public interface IThuHoiAppService
    {
        void CreateOrEditThuHoi(ThuHoiInput thuHoiInput);
        ThuHoiInput GetThuHoiForEdit(int id);
        void DeleteThuHoi(int id);
        PagedResultDto<ThuHoiDto> GetThuHois(ThuHoiFilter input);
        ThuHoiForViewDto GetThuHoiForView(int id);
    }
}
