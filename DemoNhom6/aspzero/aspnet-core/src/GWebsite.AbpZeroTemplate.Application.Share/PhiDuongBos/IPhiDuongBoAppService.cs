using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhiDuongBos
{
    public interface IPhiDuongBoAppService
    {
        void CreateOrEditPhiDuongBo(PhiDuongBoInput phiDuongBoInput);
        PhiDuongBoInput GetPhiDuongBoForEdit(int id);
        void DeletePhiDuongBo(int id);
        PagedResultDto<PhiDuongBoDTO> GetPhiDuongBos(PhiDuongBoFilter input);
        PhiDuongBoForViewDTO GetPhiDuongBoForView(int id);
    }
}
