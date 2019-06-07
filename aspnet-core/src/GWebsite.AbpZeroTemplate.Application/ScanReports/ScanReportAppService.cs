using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports;
using GWebsite.AbpZeroTemplate.Application.Share.ScanReports.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using GWebsite.AbpZeroTemplate.Core.Helper;
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
        public Dictionary<string, object> ScanHardware()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("cpuManufacture", HardwareInfo.GetCPUManufacturer());

            result.Add("biosMaker", HardwareInfo.GetBIOSmaker());

            result.Add("boardMaker", HardwareInfo.GetBoardMaker());

            result.Add("ramSlots", HardwareInfo.GetNoRamSlots());

            result.Add("memory", HardwareInfo.GetPhysicalMemory());

            result.Add("cdROM", HardwareInfo.GetCdRomDrive());

            result.Add("computerName", HardwareInfo.GetComputerName());

            result.Add("osInformation", HardwareInfo.GetOSInformation());

            result.Add("macAddress", HardwareInfo.GetMACAddress());

            result.Add("accountName", HardwareInfo.GetAccountName());

            result.Add("drives", System.IO.DriveInfo.GetDrives()); 

            return result;
        }

        public List<Dictionary<string, object>> ScanSoftware()
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            var displayName = sk.GetValue("DisplayName");
                            var size = sk.GetValue("EstimatedSize");
                            var version = sk.GetValue("Version");
                            var installDate = sk.GetValue("InstallDate");
                            var publisher = sk.GetValue("Publisher");
                            var installLocation = sk.GetValue("InstallLocation");

                            Dictionary<string, object> hash = new Dictionary<string, object>();

                            hash.Add("displayName", displayName);
                            hash.Add("size", size);
                            hash.Add("version", version);
                            hash.Add("installDate", installDate);
                            hash.Add("publisher", publisher);
                            hash.Add("installLocation", installLocation);

                            result.Add(hash);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
               
            }

            return result;
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
                scanReportRepository.Delete(id);
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
