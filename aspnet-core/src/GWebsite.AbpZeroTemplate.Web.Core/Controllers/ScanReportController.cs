using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ScanReportController : GWebsiteControllerBase
    {
        private readonly IScanReportAppService scanReportAppService;

        public ScanReportController(IScanReportAppService scanReportAppService)
        {
            this.scanReportAppService = scanReportAppService;
        }

        [HttpGet]
        public PagedResultDto<ScanReportDto> GetScanReportsByFilter(ScanReportFilter scanReportFilter)
        {
            return scanReportAppService.GetScanReports(scanReportFilter);
        }

        [HttpGet]
        public List<Dictionary<string, object>> ScanSoftware()
        {
            return scanReportAppService.ScanSoftware();
        }


        [HttpGet]
        public Dictionary<string, object> ScanHardware()
        {
            return scanReportAppService.ScanHardware();
        }

        [HttpGet]
        public ScanReportDto Scan()
        {
            return scanReportAppService.Scan();
        }

        [HttpGet]
        public ScanReportInput GetScanReportForEdit(int id)
        {
            return scanReportAppService.GetScanReportForEdit(id);
        }

        [HttpPost]
        public ScanReportDto CreateOrEditScanReport([FromBody] ScanReportInput input)
        {
            return scanReportAppService.CreateOrEditScanReport(input);
        }

        [HttpDelete("{id}")]
        public void DeleteScanReport(int id)
        {
            scanReportAppService.DeleteScanReport(id);
        }

        [HttpGet]
        public ScanReportForViewDto GetScanReportForView(int id)
        {
            return scanReportAppService.GetScanReportForView(id);
        }

    }
}
