using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Organizations;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.ThongTinXes
{
    public class ThongTinXeAppService : GWebsiteAppServiceBase, IThongTinXeAppService
    {
        public readonly IRepository<ThongTinXe,int> thongTinXeRepository;
        public readonly IRepository<OrganizationUnit, long> organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        public ThongTinXeAppService(IRepository<ThongTinXe , int> thongTinXeRepository,
             IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository)
        {
            this.thongTinXeRepository = thongTinXeRepository;
            this.organizationUnitRepository = organizationUnitRepository;
            this._userOrganizationUnitRepository = userOrganizationUnitRepository;
        }

        public void CreateOrEditThongTinXe(ThongTinXeInput thongTinXeInput)
        {
            if(thongTinXeInput.Id==0)
            {
                Create(thongTinXeInput);
            }
            else
            {
                Update(thongTinXeInput);
            }
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThongTinXeInput thongTinXeInput)
        {
          var thongTinXeEntity =   ObjectMapper.Map<ThongTinXe>(thongTinXeInput);
            SetAuditInsert(thongTinXeEntity);
            thongTinXeRepository.Insert(thongTinXeEntity);
            CurrentUnitOfWork.SaveChanges();

        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThongTinXeInput thongTinXeInput)
        {
            var thongTinXeEntity = thongTinXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thongTinXeInput.Id);
            if(thongTinXeEntity!=null)
            {
                ObjectMapper.Map(thongTinXeInput, thongTinXeEntity);
                SetAuditEdit(thongTinXeEntity);
                thongTinXeRepository.Update(thongTinXeEntity);
                CurrentUnitOfWork.SaveChanges();

            }
        }

        public void DeleteThongTinXe(string soXe)
        {
            var thongTinXeEntity = thongTinXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.soXe == soXe);
            if(thongTinXeEntity!=null)
            {
                thongTinXeEntity.IsDelete = true;
                thongTinXeRepository.Update(thongTinXeEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public PagedResultDto<ThongTinXeDto> GetThongTinXes(ThongTinXeFilter filter)
        {
            var query  = thongTinXeRepository.GetAll().Where(x => !x.IsDelete);
            if (filter.model != null)
            {
                query = query.Where(x => x.model.ToLower() == filter.model.ToLower());
            }
            if (filter.muchDichSuDung != null)
            {
                query = query.Where(x => x.mucDichSuDung.ToLower().Equals(filter.muchDichSuDung.ToLower()));
            }
            if (filter.namSanXuat != null)
            {
                query = query.Where(x => x.namSanXuat == filter.namSanXuat);
            }
            if (filter.soXe != null)
            {
                query = query.Where(x => x.soXe.ToLower().Equals(filter.soXe.ToLower()));
            }
            if (filter.trangThaiDuyet != null)
            {
                query = query.Where(x => x.trangThaiDuyet.ToLower().Equals(filter.trangThaiDuyet.ToLower()));
            }


            //if (filter.model != null || filter.muchDichSuDung != null || filter.namSanXuat != null || filter.soXe != null || filter.trangThaiDuyet != null)
            //{
            //  query =   query.Where(x => x.model.ToLower().Equals(filter.model) || x.mucDichSuDung.ToLower().Equals(filter.muchDichSuDung) || x.namSanXuat == filter.namSanXuat || x.trangThaiDuyet.ToLower().Equals(filter.trangThaiDuyet) || x.soXe.ToLower().Equals(filter.soXe));
            //}
            var user = GetCurrentUser();

            var organizationUnitIds = _userOrganizationUnitRepository
                                        .GetAll()
                                        .Where(x => x.UserId == user.Id)
                                        .Select(x => x.OrganizationUnitId)
                                        .ToList();


            var organizationUnitOrUserCodes = organizationUnitRepository
                                                            .GetAll()
                                                            .Where(x => x.IsDeleted == false && organizationUnitIds.Contains(x.Id))
                                                            .Select(x => x.Code)
                                                            .ToList();

            List<long> unitIds = new List<long>();

            foreach (var code in organizationUnitOrUserCodes)
            {
                unitIds.AddRange(organizationUnitRepository.GetAll().Where(x => x.Code.StartsWith(code)).Select(x => x.Id).ToList());
            }
            if(unitIds.Count!=0)
                query = query.Where(x => unitIds.Contains(x.organizationUnitId));

            var total = query.Count();
            if(!string.IsNullOrWhiteSpace(filter.Sorting))
            {
                query = query.OrderBy(filter.Sorting);

            }
            var items = query.PageBy(filter).ToList();

            return new PagedResultDto<ThongTinXeDto>(total, items.Select(item => ObjectMapper.Map<ThongTinXeDto>(item)).ToList());
        }

        public ThongTinXeInput GetThongTinXeForEdit(string soXe)
        {
            var thongTinXeEntity = thongTinXeRepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x=>x.soXe.Equals(soXe));
            if(thongTinXeEntity!=null)
            {
                return ObjectMapper.Map<ThongTinXeInput>(thongTinXeEntity);
            }
            return null;
        }

        public ThongTinXeForViewDto GetThongTinXeForView(string soXe)
        {
            var thongTinXeEntity = thongTinXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.soXe == soXe);
            if(thongTinXeEntity!=null)
            {
                return ObjectMapper.Map<ThongTinXeForViewDto>(thongTinXeEntity);
            }
            return null;
        }

        public bool IsDuyet()
        {
            var user = GetCurrentUser();

            var organizationUnitIds = _userOrganizationUnitRepository
                                        .GetAll()
                                        .Where(x => x.UserId == user.Id)
                                        .Select(x => x.OrganizationUnitId)
                                        .ToList();


            var organizationUnitOrUserCodes = organizationUnitRepository
                                                            .GetAll()
                                                            .Where(x => x.IsDeleted == false && organizationUnitIds.Contains(x.Id))
                                                            .Select(x => x.Code)
                                                            .ToList();
            string []temp = organizationUnitOrUserCodes[0].ToString().Split(".");
            if (temp.Length <= 2)
                return true;
            return false;


        }
    }
}
