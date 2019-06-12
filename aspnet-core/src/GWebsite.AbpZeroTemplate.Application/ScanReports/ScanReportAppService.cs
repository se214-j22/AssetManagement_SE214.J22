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
using Newtonsoft.Json;
using System.IO;

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

        public ScanReportAppService()
        {
        }

        public ScanReportDto Scan()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("hardwareScan", this.ScanHardware());
            result.Add("softwareScan", this.ScanSoftware());

            ScanReportInput input = new ScanReportInput();
            input.ScannedData = JsonConvert.SerializeObject(result);
            input.Id = 0;

            return this.CreateOrEditScanReport(input);
        }
        public Dictionary<string, object> ScanHardware()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("cpuManufacture", HardwareInfo.GetCPUManufacturer().ToString());

            result.Add("cpuCores", HardwareInfo.GetCpuSpeedInGHz().ToString());

            result.Add("biosMaker", HardwareInfo.GetBIOSmaker().ToString());

            result.Add("boardMaker", HardwareInfo.GetBoardMaker().ToString());

            result.Add("ramSlots", HardwareInfo.GetNoRamSlots().ToString());

            result.Add("memory", HardwareInfo.GetPhysicalMemory().ToString());

            result.Add("cdROM", HardwareInfo.GetCdRomDrive().ToString());

            result.Add("computerName", HardwareInfo.GetComputerName().ToString());

            result.Add("osInformation", HardwareInfo.GetOSInformation().ToString());

            result.Add("macAddress", HardwareInfo.GetMACAddress().ToString());

            result.Add("accountName", HardwareInfo.GetAccountName().ToString());

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<Dictionary<string, object>> driveList = new List<Dictionary<string, object>>();

            foreach (DriveInfo d in allDrives)
            {
                Dictionary<string, object> driveInfo = new Dictionary<string, object>();

                driveInfo.Add("driveName", d.Name);
                driveInfo.Add("driveType", d.DriveType);
                driveInfo.Add("volumeLabel", d.VolumeLabel);
                driveInfo.Add("driveFormat", d.DriveFormat);
                driveInfo.Add("availableFreeSpace", d.AvailableFreeSpace);
                driveInfo.Add("totalFreeSpace", d.TotalFreeSpace);
                driveInfo.Add("totalSize", d.TotalSize);

                driveList.Add(driveInfo);
         
            }

            result.Add("drives", driveList); 

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

            query = query.OrderByDescending(item => item.CreatedDate);

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<ScanReportDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ScanReportDto>(item)).ToList());
        }

        public List<OnlyIdDto> GetAllScanReport()
        {
            var query = scanReportRepository.GetAll();
          
            query = query.OrderByDescending(item => item.CreatedDate);
           
            return query.ToList().Select(item => ObjectMapper.Map<OnlyIdDto>(item)).ToList();
        }
    }
}
