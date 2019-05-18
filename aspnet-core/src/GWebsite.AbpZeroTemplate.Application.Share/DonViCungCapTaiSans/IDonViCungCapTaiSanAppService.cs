using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans
{
    public interface IDonViCungCapTaiSanAppService
    {
        void CreateOrEditDonViCungCapTaiSan(DonViCungCapTaiSanInput donViCungCapTaiSanInput);
        DonViCungCapTaiSanInput GetDonViCungCapTaiSanForEdit(int id);
        void DeleteDonViCungCapTaiSan(int id);
        PagedResultDto<DonViCungCapTaiSanDto> GetDonViCungCapTaiSans(DonViCungCapTaiSanFilter input);
        DonViCungCapTaiSanForViewDto GetDonViCungCapTaiSanForView(int id);
    }
}
