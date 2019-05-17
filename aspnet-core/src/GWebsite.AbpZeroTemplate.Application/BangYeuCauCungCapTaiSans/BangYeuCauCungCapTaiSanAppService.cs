using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;

namespace GWebsite.AbpZeroTemplate.Web.Core.BangYeuCauCungCapTaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_BangYeuCauCungCapTaiSan)]
    public class BangYeuCauCungCapTaiSanAppService : GWebsiteAppServiceBase, IBangYeuCauCungCapTaiSanAppService
    {
        private readonly IRepository<BangYeuCauCungCapTaiSan> bangYeuCauCungCapTaiSanRepository;
        private readonly IRepository<PhongBan> phongBanRepository;

        public BangYeuCauCungCapTaiSanAppService(IRepository<BangYeuCauCungCapTaiSan> bangYeuCauCungCapTaiSanRepository,
                                                IRepository<PhongBan> phongBanRepository)
        {
            this.bangYeuCauCungCapTaiSanRepository = bangYeuCauCungCapTaiSanRepository;
            this.phongBanRepository = phongBanRepository;
        }

        #region Public Method

        public void CreateOrEditBangYeuCauCungCapTaiSan(BangYeuCauCungCapTaiSanInput bangYeuCauCungCapTaiSanInput)
        {
            if (bangYeuCauCungCapTaiSanInput.Id == 0)
            {
                Create(bangYeuCauCungCapTaiSanInput);
            }
            else
            {
                Update(bangYeuCauCungCapTaiSanInput);
            }
        }

        public void DeleteBangYeuCauCungCapTaiSan(int id)
        {
            var bangYeuCauCungCapTaiSanEntity = bangYeuCauCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (bangYeuCauCungCapTaiSanEntity != null)
            {
                bangYeuCauCungCapTaiSanEntity.IsDelete = true;
                bangYeuCauCungCapTaiSanRepository.Update(bangYeuCauCungCapTaiSanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public BangYeuCauCungCapTaiSanInput GetBangYeuCauCungCapTaiSanForEdit(int id)
        {
            var bangYeuCauCungCapTaiSanEntity = bangYeuCauCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (bangYeuCauCungCapTaiSanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<BangYeuCauCungCapTaiSanInput>(bangYeuCauCungCapTaiSanEntity);
        }

        public BangYeuCauCungCapTaiSanForViewDto GetBangYeuCauCungCapTaiSanForView(int id)
        {
            var bangYeuCauCungCapTaiSanEntity = bangYeuCauCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (bangYeuCauCungCapTaiSanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<BangYeuCauCungCapTaiSanForViewDto>(bangYeuCauCungCapTaiSanEntity);
        }

        public PagedResultDto<BangYeuCauCungCapTaiSanOutput> GetBangYeuCauCungCapTaiSans(BangYeuCauCungCapTaiSanFilter input)
        {
            var query = bangYeuCauCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.PhongBanId != null)
            {
                query = query.Where(x => x.PhongBan.Id == input.PhongBanId);
            }

            IQueryable<BangYeuCauCungCapTaiSanDto> bangYeuCauCungCapTaiSanQuery = query.ProjectTo<BangYeuCauCungCapTaiSanDto>();
            IQueryable<PhongBan> phongBanQuery = phongBanRepository.GetAll().Where(x => !x.IsDelete);
            IQueryable<BangYeuCauCungCapTaiSanOutput> bangYeuCauCungCapTaiSanOutputQuery = (
                from bycccts in bangYeuCauCungCapTaiSanQuery
                join pb in phongBanQuery
                on bycccts.PhongBanId equals pb.Id
                select new BangYeuCauCungCapTaiSanOutput() {
                    BangYeuCauCungCapTaiSan = bycccts,
                    TenPhong = pb.TenPhong
                }
            );

            var totalCount = bangYeuCauCungCapTaiSanOutputQuery.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                bangYeuCauCungCapTaiSanOutputQuery = bangYeuCauCungCapTaiSanOutputQuery.OrderBy(input.Sorting);
            }

            // paging
            var items = bangYeuCauCungCapTaiSanOutputQuery.PageBy(input).ToList();

            // result
            return new PagedResultDto<BangYeuCauCungCapTaiSanOutput>(
                totalCount,
                //items.Select(item => ObjectMapper.Map<BangYeuCauCungCapTaiSanDto>(item)).ToList()
                items);
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_BangYeuCauCungCapTaiSan_Create)]
        private void Create(BangYeuCauCungCapTaiSanInput bangYeuCauCungCapTaiSanInput)
        {
            var bangYeuCauCungCapTaiSanEntity = ObjectMapper.Map<BangYeuCauCungCapTaiSan>(bangYeuCauCungCapTaiSanInput);
            SetAuditInsert(bangYeuCauCungCapTaiSanEntity);
            bangYeuCauCungCapTaiSanRepository.Insert(bangYeuCauCungCapTaiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_BangYeuCauCungCapTaiSan_Edit)]
        private void Update(BangYeuCauCungCapTaiSanInput bangYeuCauCungCapTaiSanInput)
        {
            var bangYeuCauCungCapTaiSanEntity = bangYeuCauCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == bangYeuCauCungCapTaiSanInput.Id);
            if (bangYeuCauCungCapTaiSanEntity == null)
            {
            }
            ObjectMapper.Map(bangYeuCauCungCapTaiSanInput, bangYeuCauCungCapTaiSanEntity);
            SetAuditEdit(bangYeuCauCungCapTaiSanEntity);
            bangYeuCauCungCapTaiSanRepository.Update(bangYeuCauCungCapTaiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
