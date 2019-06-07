using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CapPhatController : GWebsiteControllerBase
    {
        private readonly ICapPhatAppService capPhatAppService;

        public CapPhatController(ICapPhatAppService capPhatAppService)
        {
            this.capPhatAppService = capPhatAppService;
        }

        [HttpGet]
        public PagedResultDto<CapPhatOutput> GetCapPhatsByFilter(CapPhatFilter capPhatFilter)
        {
            return capPhatAppService.GetCapPhats(capPhatFilter);
        }

        [HttpGet]
        public CapPhatInput GetCapPhatForEdit(int id)
        {
            return capPhatAppService.GetCapPhatForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditCapPhat([FromBody] CapPhatInput input)
        {
            capPhatAppService.CreateOrEditCapPhat(input);
        }

        [HttpDelete("{id}")]
        public void DeleteCapPhat(int id)
        {
            capPhatAppService.DeleteCapPhat(id);
        }

        [HttpGet]
        public CapPhatForViewDto GetCapPhatForView(int id)
        {
            return capPhatAppService.GetCapPhatForView(id);
        }
    }
}
