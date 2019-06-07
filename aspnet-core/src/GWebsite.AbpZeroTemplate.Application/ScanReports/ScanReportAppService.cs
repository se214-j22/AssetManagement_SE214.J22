using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.ScanReports
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ScanReportAppService : GWebsiteAppServiceBase, IScanReportAppService
    {
        private readonly IRepository<ScanReport> scanReportRepository;

        public ScanReportAppService(IRepository<ScanReport> scanReportRepository)
        {
            this.scanReportRepository = scanReportRepository;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        public ScanReportDto CreateOrEditScanReport(ScanReportInput scanReportInput)
        {
            ScanReport scanReportEntity = null;
            if (scanReportInput.Id == 0)
            {
                // Insert
                scanReportEntity = ObjectMapper.Map<ScanReport>(scanReportInput);
                SetAuditInsert(scanReportEntity);
                scanReportRepository.Insert(scanReportEntity);
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                // Update
                scanReportEntity = scanReportRepository.GetAll().SingleOrDefault(x => x.Id == scanReportInput.Id);
                if (scanReportEntity == null)
                {
                    return null;
                }
                ObjectMapper.Map(scanReportInput, scanReportEntity);
                SetAuditEdit(scanReportEntity);
                scanReportRepository.Update(scanReportEntity);
                CurrentUnitOfWork.SaveChanges();
            }

            return ObjectMapper.Map<ScanReportDto>(scanReportEntity);
        }

        public void DeleteScanReport(int id)
        {
            var scanReportEntity = scanReportRepository.Get(id);
            if (scanReportEntity != null)
            {
                scanReportEntity.IsDelete = true;
                scanReportRepository.Update(scanReportEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ScanReportInput GetScanReportForEdit(int id)
        {
            var scanReportEntity = scanReportRepository.GetAll().SingleOrDefault(x => x.Id == id);
            if (scanReportEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ScanReportInput>(scanReportEntity);
        }

        public ScanReportForViewDto GetScanReportForView(int id)
        {
            var scanReportEntity = scanReportRepository.GetAll().SingleOrDefault(x => x.Id == id);
            if (scanReportEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ScanReportForViewDto>(scanReportEntity);
        }

        public PagedResultDto<ScanReportDto> GetScanReports(ScanReportFilter input)
        {
            var query = scanReportRepository.GetAll();

            // filter by value
            if (input.ScannedData != null)
            {
                query = query.Where(x => x.ScannedData.Contains(input.ScannedData));
            }

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<ScanReportDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ScanReportDto>(item)).ToList());
        }
    }
}
