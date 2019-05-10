using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CapPhatController : GWebsiteControllerBase
    {
        private readonly ICapPhatAppService capPhatAppservice;

        public CapPhatController(ICapPhatAppService capPhatAppservice)
        {
            this.capPhatAppservice = capPhatAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }
        [HttpGet]
        public PagedResultDto<CapPhatDto> GetCapPhatsByFilter(CapPhatFilter CapPhatFilter)
        {
            return capPhatAppservice.GetCapPhats(CapPhatFilter);
        }
        [HttpPost]
        public void CreateOrEditCapPhat([FromBody] CapPhatInput input)
        {
            capPhatAppservice.CreateOrEditCapPhat(input);
        }

        [HttpGet]
        public CapPhatInput GetCapPhatForEdit(int id)
        {
            return capPhatAppservice.GetCapPhatForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteCapPhat(int id)
        {
            capPhatAppservice.DeleteCapPhat(id);
        }
        [HttpGet]
        public CapPhatForViewDto GetCapPhatForView(int id)
        {
            return capPhatAppservice.GetCapPhatForView(id);
        }
    }
}

