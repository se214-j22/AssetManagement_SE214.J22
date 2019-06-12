using System;
using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto
{
    public class OnlyIdDto: Entity<int>
    {
        public string Id { get; set; }
        public DateTime createdDate { get; set; }
    }
}
