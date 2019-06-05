using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DonVis;
using GWebsite.AbpZeroTemplate.Application.Share.DonVis.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DonViController : GWebsiteControllerBase
    {
        private readonly IDonViAppService donViAppservice;

        public DonViController(IDonViAppService donViAppservice)
        {
            this.donViAppservice = donViAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        
        [HttpGet]
        public PagedResultDto<DonViDto> GetDonVisByFilter(DonViFilter DonViFilter)
        {
            return donViAppservice.GetDonVis(DonViFilter);
        }
        [HttpPost]
        public void CreateOrEditDonVi([FromBody] DonViInput input)
        {
            donViAppservice.CreateOrEditDonVi(input);
        }

        [HttpGet]
        public DonViInput GetDonViForEdit(int id)
        {
            return donViAppservice.GetDonViForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteDonVi(int id)
        {
            donViAppservice.DeleteDonVi(id);
        }
        [HttpPost]
        public DonViForViewDto GetDonViForView(int id)
        {
            return donViAppservice.GetDonViForView(id);
        }
    }
}

