using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap;
using GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.NhaCungCaps
{
    public class NhaCungCapAppService : GWebsiteAppServiceBase, INhaCungCapAppService
    {
        public readonly IRepository<NhaCungCap> nhaCungCapRepository;
        public NhaCungCapAppService(IRepository<NhaCungCap> nhaCungCapRepository)
        {
            this.nhaCungCapRepository = nhaCungCapRepository;
        }

        public void CreateOrEditNhaCungCap(NhaCungCapInput nhaCungCapInput)
        {
            if (nhaCungCapInput.Id == 0)
                Create(nhaCungCapInput);
            else
                Update(nhaCungCapInput);
        }

        private void Create(NhaCungCapInput nhaCungCapInput)
        {
            var nhaCungCapEntity = ObjectMapper.Map<NhaCungCap>(nhaCungCapInput);
            SetAuditInsert(nhaCungCapEntity);
            nhaCungCapRepository.Insert(nhaCungCapEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        private void Update(NhaCungCapInput nhaCungCapInput)
        {
            var nhaCungCapEntity = nhaCungCapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == nhaCungCapInput.Id);
            ObjectMapper.Map(nhaCungCapInput, nhaCungCapEntity);
            SetAuditEdit(nhaCungCapEntity);
            nhaCungCapRepository.Update(nhaCungCapEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        public void DeleteNhaCungCap(int id)
        {
            var nhaCungCapEntity = nhaCungCapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if(nhaCungCapEntity !=null)
            {
                nhaCungCapEntity.IsDelete = true;
                nhaCungCapRepository.Update(nhaCungCapEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public NhaCungCapInput GetNhaCungCapForEdit(int id)
        {
            var nhaCungCapEntity = nhaCungCapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhaCungCapEntity == null)
            {
                return null;
            }
            else
                return ObjectMapper.Map<NhaCungCapInput>(nhaCungCapEntity);
        }

        public NhaCungCapForViewDto GetNhaCungCapForView(int id)
        {
            var nhaCungCapEntity = nhaCungCapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhaCungCapEntity == null)
            {
                return null;
            }
            else return ObjectMapper.Map<NhaCungCapForViewDto>(nhaCungCapEntity);

        }

        public PagedResultDto<NhaCungCapDto> GetNhaCungCaps(NhaCungCapFilter filter)
        {
            var query = nhaCungCapRepository.GetAll().Where(x => !x.IsDelete);
            if(filter.maCongTyBaoHiem!=null)
            {
                query = query.Where(x => x.maCongTyBaoHiem == filter.maCongTyBaoHiem);

            }
            var total = query.Count();
            if (!string.IsNullOrWhiteSpace(filter.Sorting))
                query = query.OrderBy(filter.Sorting);

            var items = query.PageBy(filter).ToList();

            return new PagedResultDto<NhaCungCapDto>(total, items.Select(item => ObjectMapper.Map<NhaCungCapDto>(item)).ToList());
        }
    }
}
