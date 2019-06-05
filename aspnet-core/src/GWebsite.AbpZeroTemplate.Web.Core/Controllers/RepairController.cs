using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Repairs;
using GWebsite.AbpZeroTemplate.Application.Share.Repairs.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RepairController : GWebsiteControllerBase
    {
        private readonly IRepairAppService repairAppService;

        public RepairController(IRepairAppService repairAppService)
        {
            this.repairAppService = repairAppService;
        }

        [HttpGet]
        public PagedResultDto<RepairDto> GetRepairsByFilter(RepairFilter repairFilter)
        {
            return repairAppService.GetRepairs(repairFilter);
        }

        [HttpGet]
        public RepairInput GetRepairForEdit(int id)
        {
            return repairAppService.GetRepairForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditRepair([FromBody] RepairInput input)
        {
            repairAppService.CreateOrEditRepair(input);
        }

        [HttpDelete("{id}")]
        public void DeleteRepair(int id)
        {
            repairAppService.DeleteRepair(id);
        }

        [HttpGet]
        public RepairForViewDto GetRepairForView(int id)
        {
            return repairAppService.GetRepairForView(id);
        }

        [HttpGet]
        public List<RepairForViewDto> GetListRepairByAssetId(string assetId)
        {
            return repairAppService.GetListRepairByAssetId(assetId);
        }

        [HttpGet("{id}")]
        public void ApproveRepair(int id)
        {
            repairAppService.ApproveRepair(id);
        }
    }
}