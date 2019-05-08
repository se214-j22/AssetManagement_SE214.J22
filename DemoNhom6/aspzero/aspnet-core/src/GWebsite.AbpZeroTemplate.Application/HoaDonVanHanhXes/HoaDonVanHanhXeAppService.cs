using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using Abp.Authorization;

namespace GWebsite.AbpZeroTemplate.Web.Core.HoaDonVanHanhXes
{
    public class HoaDonVanHanhXeAppService : GWebsiteAppServiceBase, IHoaDonVanHanhXeAppService
    {
        private readonly IRepository<HoaDonVanHanhXe> hoaDonVanHanhXeRepository;

        public HoaDonVanHanhXeAppService(IRepository<HoaDonVanHanhXe> hoaDonVanHanhXeRepository)
        {
            this.hoaDonVanHanhXeRepository = hoaDonVanHanhXeRepository;
        }

        public void CreateOrEditHoaDonVanHanhXe(HoaDonVanHanhXeInput input)
        {
            if (input.Id == 0)
            {
                Create(input);
            }
            else
                Update(input);
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(HoaDonVanHanhXeInput input)
        {
            var entity = ObjectMapper.Map<HoaDonVanHanhXe>(input);
            SetAuditInsert(entity);
           hoaDonVanHanhXeRepository.Insert(entity);
            CurrentUnitOfWork.SaveChanges();
        }

        private void Update(HoaDonVanHanhXeInput input)
        {
            var entity = hoaDonVanHanhXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == input.Id);
            if (entity == null)
            {
            }
            ObjectMapper.Map(input, entity);
            SetAuditEdit(entity);
            hoaDonVanHanhXeRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        public void DeleteHoaDonVanHanhXe(string soHoaDon)
        {
            var entity = hoaDonVanHanhXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.soHoaDon==soHoaDon);
           if(entity!=null)
            {
                entity.IsDelete = true;
                hoaDonVanHanhXeRepository.Update(entity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public HoaDonVanHanhXeInput GetHoaDonVanHanhXeForEdit(string soHoaDon)
        {
            var entity = hoaDonVanHanhXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.soHoaDon == soHoaDon);
            if(entity!=null)
            {
                return ObjectMapper.Map<HoaDonVanHanhXeInput>(entity);
            }
            else
                return null;
        }

        public HoaDonVanHanhXeForViewDto GetHoaDonVanHanhXeForView(string soHoaDon)
        {
            var entity = hoaDonVanHanhXeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.soHoaDon == soHoaDon);
            if (entity != null)
            {
                return ObjectMapper.Map<HoaDonVanHanhXeForViewDto>(entity);
            }
            else
                return null;
        }

        public PagedResultDto<HoaDonVanHanhXeDto> GetHoaDonVanHanhXes(HoaDonVanHanhXeFilter input)
        {
            var query =hoaDonVanHanhXeRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.soXe != null)
            {
                query = query.Where(x => x.soXe.ToLower().Equals(input.soXe));
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
            return new PagedResultDto<HoaDonVanHanhXeDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<HoaDonVanHanhXeDto>(item)).ToList());

        }
    }
}
