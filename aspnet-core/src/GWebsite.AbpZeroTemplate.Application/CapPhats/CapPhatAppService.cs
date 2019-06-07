using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Linq.Extensions;
using System;
using AutoMapper.QueryableExtensions;

namespace GWebsite.AbpZeroTemplate.Web.Core.CapPhats
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_CapPhat)]
    public class CapPhatAppService : GWebsiteAppServiceBase, ICapPhatAppService
    {
        private readonly IRepository<CapPhat> capPhatRepository;
        private readonly IRepository<PhongBan> phongBanRepository;
        private readonly IRepository<SanPham> sanPhamRepository;

        public CapPhatAppService(IRepository<CapPhat> capPhatRepository,
                                                IRepository<PhongBan> phongBanRepository,
                                                IRepository<SanPham> sanPhamRepository)
        {
            this.capPhatRepository = capPhatRepository;
            this.phongBanRepository = phongBanRepository;
            this.sanPhamRepository = sanPhamRepository;
        }

        #region Public Method

        public void CreateOrEditCapPhat(CapPhatInput capPhatInput)
        {
            if (capPhatInput.Id == 0)
            {
                Create(capPhatInput);
            }
            else
            {
                Update(capPhatInput);
            }
        }

        public void DeleteCapPhat(int id)
        {
            var capPhatEntity = capPhatRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (capPhatEntity != null)
            {
                capPhatEntity.IsDelete = true;
                capPhatRepository.Update(capPhatEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public CapPhatInput GetCapPhatForEdit(int id)
        {
            var capPhatEntity = capPhatRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (capPhatEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CapPhatInput>(capPhatEntity);
        }

        public CapPhatForViewDto GetCapPhatForView(int id)
        {
            var capPhatEntity = capPhatRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (capPhatEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CapPhatForViewDto>(capPhatEntity);
        }

        public PagedResultDto<CapPhatOutput> GetCapPhats(CapPhatFilter input)
        {
            var query = capPhatRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.PhongBanId != null)
            {
                query = query.Where(x => x.PhongBan.Id == input.PhongBanId);
            }

            IQueryable<CapPhatDto> capPhatQuery = query.ProjectTo<CapPhatDto>();
            IQueryable<PhongBan> phongBanQuery = phongBanRepository.GetAll().Where(x => !x.IsDelete);
            IQueryable<SanPham> sanPhamQuery = sanPhamRepository.GetAll().Where(x => !x.IsDelete);
            IQueryable<CapPhatOutput> capPhatOutputQuery = (
                from bycccts in capPhatQuery
                join pb in phongBanQuery on bycccts.PhongBanId equals pb.Id
                join sp in sanPhamQuery on bycccts.SanPhamId equals sp.Id
                select new CapPhatOutput()
                {
                    CapPhat = bycccts,
                    TenPhong = pb.TenPhong,
                    TenSanPham = sp.TenSP
                }
            );

            var totalCount = capPhatOutputQuery.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                capPhatOutputQuery = capPhatOutputQuery.OrderBy(input.Sorting);
            }

            // paging
           var items = capPhatOutputQuery.PageBy(input).ToList();

            // result
            return new PagedResultDto<CapPhatOutput>(
                totalCount,
                //items.Select(item => ObjectMapper.Map<BangYeuCauCungCapTaiSanDto>(item)).ToList()
                items);
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_CapPhat_Create)]
        private void Create(CapPhatInput capPhatInput)
        {
            var capPhatEntity = ObjectMapper.Map<CapPhat>(capPhatInput);
            SetAuditInsert(capPhatEntity);
            capPhatRepository.Insert(capPhatEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_CapPhat_Edit)]
        private void Update(CapPhatInput capPhatInput)
        {
            var capPhatEntity = capPhatRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == capPhatInput.Id);
            if (capPhatEntity == null)
            {
            }
            ObjectMapper.Map(capPhatInput, capPhatEntity);
            SetAuditEdit(capPhatEntity);
            capPhatRepository.Update(capPhatEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
