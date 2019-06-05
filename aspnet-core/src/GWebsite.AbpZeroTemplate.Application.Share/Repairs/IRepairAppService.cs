using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Repairs.Dto;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Share.Repairs
{
    public interface IRepairAppService
    {
        void CreateOrEditRepair(RepairInput repairInput);
        RepairInput GetRepairForEdit(int id);
        void DeleteRepair(int id);
        PagedResultDto<RepairDto> GetRepairs(RepairFilter input);
        RepairForViewDto GetRepairForView(int id);
        List<RepairForViewDto> GetListRepairByAssetId(string assetId);
        void ApproveRepair(int id);
    }
}