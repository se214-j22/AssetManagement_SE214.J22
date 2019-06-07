using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ScanReports
{
    public interface IScanReportAppService
    {
        ScanReportDto CreateOrEditScanReport(ScanReportInput scanReportInput);
        ScanReportInput GetScanReportForEdit(int id);
        void DeleteScanReport(int id);
        PagedResultDto<ScanReportDto> GetScanReports(ScanReportFilter input);
        ScanReportForViewDto GetScanReportForView(int id);
        List<Dictionary<string, object>> ScanSoftware();

        Dictionary<string, object> ScanHardware();
    }
}
