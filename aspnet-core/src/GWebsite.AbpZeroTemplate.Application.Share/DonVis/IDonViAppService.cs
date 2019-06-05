using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DonVis.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.DonVis
{
    public interface IDonViAppService
    {
        void CreateOrEditDonVi(DonViInput donViInput);
        DonViInput GetDonViForEdit(int id);
        void DeleteDonVi(int id);
        PagedResultDto<DonViDto> GetDonVis(DonViFilter input);
        DonViForViewDto GetDonViForView(int id);
    }
}
