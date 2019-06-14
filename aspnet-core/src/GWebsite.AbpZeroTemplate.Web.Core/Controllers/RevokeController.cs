using System.Collections.Generic;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Revokes;
using GWebsite.AbpZeroTemplate.Application.Share.Revokes.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RevokeController : GWebsiteControllerBase
    {
        private readonly IRevokeAppService revokeAppService;

        public RevokeController(IRevokeAppService revokeAppService)
        {
            this.revokeAppService = revokeAppService;
        }

        [HttpGet]
        public PagedResultDto<RevokeDto> GetRevokesByFilter(RevokeFilter revokeFilter)
        {
            return revokeAppService.GetRevokes(revokeFilter);
        }

        [HttpGet]
        public RevokeInput GetRevokeForEdit(int id)
        {
            return revokeAppService.GetRevokeForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditRevoke([FromBody] RevokeInput input)
        {
            revokeAppService.CreateOrEditRevoke(input);
        }

        [HttpDelete("{id}")]
        public void DeleteRevoke(int id)
        {
            revokeAppService.DeleteRevoke(id);
        }

        [HttpGet]
        public RevokeForViewDto GetRevokeForView(int id)
        {
            return revokeAppService.GetRevokeForView(id);
        }

        [HttpGet("{id}")]
        public void ApproveRevoke(int id)
        {
            revokeAppService.ApproveRevoke(id);
        }

        [HttpGet]
        public List<RevokeDto> GetListRevokeNotApproved()
        {
            return revokeAppService.GetListRevokeNotApproved();
        }
    }
}