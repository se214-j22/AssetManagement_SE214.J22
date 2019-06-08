using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Models;
using GWebsite.AbpZeroTemplate.Application.Share.Models.Dto;
using GWebsite.AbpZeroTemplate.Web.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public  class ModelController: GWebsiteControllerBase
    {
        private readonly IModelAppService  modelAppService;

        public ModelController(IModelAppService modelAppService)
        {
            this.modelAppService = modelAppService;
        }

        [HttpGet]
        public PagedResultDto<ModelDto> GetModelByFilter(ModelFilter modelFilter)
        {
            return modelAppService.GetModels(modelFilter);
        }

        [HttpGet]
        public ModelInput GetModelForEdit(string model)
        {
            return modelAppService.GetModelForEdit(model);
        }

        [HttpPost]
        public void CreateOrEditModel([FromBody] ModelInput input)
        {
            modelAppService.CreateOrEditModel(input);
        }

        [HttpDelete("{id}")]
        public void DeleteModel(int id)
        {
            modelAppService.DeleteModel(id);
        }

        [HttpGet]
        public ModelForViewDto GetModelForView(string model)
        {
            return modelAppService.GetModelForView(model);
        }
    }
}
