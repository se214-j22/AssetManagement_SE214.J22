using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs
{
    public interface IPhieuBaoDuongAppService
    {
        void CreateOrEditPhieuBaoDuong(PhieuBaoDuongInput phieuBaoDuongInput);
        PhieuBaoDuongInput GetPhieuBaoDuongForEdit(int id);
        void DeletePhieuBaoDuong(int id);
        PagedResultDto<PhieuBaoDuongForViewDto> GetPhieuBaoDuongs(PhieuBaoDuongFilter input);
        PhieuBaoDuongDto GetPhieuBaoDuongForView(int id);
    }
}
