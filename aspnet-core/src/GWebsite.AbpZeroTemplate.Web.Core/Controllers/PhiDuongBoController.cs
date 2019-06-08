using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhiDuongBos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhiDuongBoController : GWebsiteControllerBase
    {
            private readonly IPhiDuongBoAppService phiDuongBoAppService;

            public PhiDuongBoController(IPhiDuongBoAppService phiDuongBoAppService)
            {
                this.phiDuongBoAppService = phiDuongBoAppService;
            }

            [HttpGet]
            public PagedResultDto<PhiDuongBoDTO> GetPhiDuongBosByFilter(PhiDuongBoFilter phiDuongBoFilter)
            {
                return phiDuongBoAppService.GetPhiDuongBos(phiDuongBoFilter);
            }

            [HttpGet]
            public PhiDuongBoInput GetPhiDuongBoForEdit(int id)
            {
                return phiDuongBoAppService.GetPhiDuongBoForEdit(id);
            }

            [HttpPost]
            public void CreateOrEditPhiDuongBo([FromBody] PhiDuongBoInput input)
            {
                 phiDuongBoAppService.CreateOrEditPhiDuongBo(input);
            }

            [HttpDelete("{id}")]
            public void DeletePhiDuongBo(int id)
            {
                 phiDuongBoAppService.DeletePhiDuongBo(id);
            }

            [HttpGet]
        public PhiDuongBoForViewDTO GetPhiDuongBoForView(int id)
            {
                return phiDuongBoAppService.GetPhiDuongBoForView(id);
            }
        }
    
}
