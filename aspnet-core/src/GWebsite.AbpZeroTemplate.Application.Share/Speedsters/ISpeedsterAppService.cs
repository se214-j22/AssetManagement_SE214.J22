using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Speedsters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Speedsters
{
    public interface ISpeedsterAppService
    {
        void CreateOrEditSpeedster(SpeedsterInput speedsterInput);
        SpeedsterInput GetSpeedsterForEdit(int id);
        void DeleteSpeedster(int id);
        PagedResultDto<SpeedsterDto> GetSpeedsters(SpeedsterFilter input);
        SpeedsterForViewDto GetSpeedsterForView(int id);
    }
}
