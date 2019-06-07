using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto
{
    /// <summary>
    /// <model cref="DemoModel"></model>
    /// </summary>
    public class ScanReportDto : Entity<int>
    {
        public string ScannedData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
