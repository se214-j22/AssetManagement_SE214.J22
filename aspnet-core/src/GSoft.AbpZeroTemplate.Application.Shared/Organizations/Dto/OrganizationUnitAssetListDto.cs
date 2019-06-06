using System;
using Abp.Application.Services.Dto;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class OrganizationUnitAssetListDto : EntityDto<int>
    {
        public string Code { get; set; }

        public string Name { get; set; }

    }
}