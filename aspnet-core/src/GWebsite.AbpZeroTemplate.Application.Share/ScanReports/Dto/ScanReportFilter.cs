using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto
{
    /// <summary>
    /// <model cref="DemoModel"></model>
    /// </summary>
    public class ScanReportFilter : PagedAndSortedInputDto
    {
        public string ScannedData { get; set; }
    }
}
