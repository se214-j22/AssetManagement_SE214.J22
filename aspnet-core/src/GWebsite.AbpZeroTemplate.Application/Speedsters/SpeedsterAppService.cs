using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Speedsters;
using GWebsite.AbpZeroTemplate.Application.Share.Speedsters.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Speedsters
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Speedster)]
    public class SpeedsterAppService : GWebsiteAppServiceBase, ISpeedsterAppService
    {
        private readonly IRepository<Speedster> speedsterRepository;

        public SpeedsterAppService(IRepository<Speedster> speedsterRepository)
        {
            this.speedsterRepository = speedsterRepository;
        }

        #region Public Method

        public void CreateOrEditSpeedster(SpeedsterInput speedsterInput)
        {
            if (speedsterInput.Id == 0)
            {
                Create(speedsterInput);
            }
            else
            {
                Update(speedsterInput);
            }
        }

        public void DeleteSpeedster(int id)
        {
            var speedsterEntity = speedsterRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (speedsterEntity != null)
            {
                speedsterEntity.IsDelete = true;
                speedsterRepository.Update(speedsterEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public SpeedsterInput GetSpeedsterForEdit(int id)
        {
            var speedsterEntity = speedsterRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (speedsterEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SpeedsterInput>(speedsterEntity);
        }

        public SpeedsterForViewDto GetSpeedsterForView(int id)
        {
            var speedsterEntity = speedsterRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (speedsterEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SpeedsterForViewDto>(speedsterEntity);
        }

        public PagedResultDto<SpeedsterDto> GetSpeedsters(SpeedsterFilter input)
        {
            var query = speedsterRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.Name != null)
            {
                query = query.Where(x => x.Name.ToLower().Equals(input.Name));
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
            return new PagedResultDto<SpeedsterDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<SpeedsterDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Speedster_Create)]
        private void Create(SpeedsterInput speedsterInput)
        {
            var speedsterEntity = ObjectMapper.Map<Speedster>(speedsterInput);
            SetAuditInsert(speedsterEntity);
            speedsterRepository.Insert(speedsterEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Speedster_Edit)]
        private void Update(SpeedsterInput speedsterInput)
        {
            var speedsterEntity = speedsterRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == speedsterInput.Id);
            if (speedsterEntity == null)
            {
            }
            ObjectMapper.Map(speedsterInput, speedsterEntity);
            SetAuditEdit(speedsterEntity);
            speedsterRepository.Update(speedsterEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}