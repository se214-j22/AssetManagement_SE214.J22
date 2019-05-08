using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Models;
using GWebsite.AbpZeroTemplate.Application.Share.Models.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using Abp.Authorization;

namespace GWebsite.AbpZeroTemplate.Web.Core.Models
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ModelAppService : GWebsiteAppServiceBase,IModelAppService
    {

        private readonly IRepository<Model> modelRepository;

        public ModelAppService (IRepository<Model> modelRepository)
        {
            this.modelRepository = modelRepository;
        }
        public void CreateOrEditModel(ModelInput modelInput)
        {
            if (modelInput.Id == 0)
                Create(modelInput);
            else
                Edit(modelInput);
            
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Edit(ModelInput modelInput)
        {
            var modelEntity = modelRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == modelInput.Id);
            ObjectMapper.Map(modelInput,modelEntity);
            SetAuditEdit(modelEntity);
            modelRepository.Update(modelEntity);
            CurrentUnitOfWork.SaveChanges();
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ModelInput modelInput)
        {
            var modelEntity = ObjectMapper.Map<Model>(modelInput);
            SetAuditInsert(modelEntity);
            modelRepository.Insert(modelEntity);
            CurrentUnitOfWork.SaveChanges();
          
        }

        public void DeleteModel(int id)
        {
            var modelEntity = modelRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if(modelEntity!=null)
            {
                modelEntity.IsDelete = true;

                modelRepository.Update(modelEntity);
                CurrentUnitOfWork.SaveChanges();

            }
        }

        public ModelInput GetModelForEdit(string model)
        {
            var modelEntity = modelRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.model == model);
            if (modelEntity != null)
            {
                return ObjectMapper.Map<ModelInput>(modelEntity);
            }
            else
                return null;
        }

        public ModelForViewDto GetModelForView(string model)
        {
            var modelEntity = modelRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.model == model);
            if (modelEntity == null)
                return null;
            else
                return ObjectMapper.Map<ModelForViewDto>(modelEntity);
        }

        public PagedResultDto<ModelDto> GetModels(ModelFilter input)
        {
            var query = modelRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.model != null)
            {
                query = query.Where(x => x.model.ToLower().Equals(input.model));
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
            return new PagedResultDto<ModelDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ModelDto>(item)).ToList());
        }
    }
}
