using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Projects;
using GWebsite.AbpZeroTemplate.Application.Share.Projects.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProjectController : GWebsiteControllerBase
    {
        private readonly IProjectAppService _ProjectAppService;

        public ProjectController(IProjectAppService ProjectService)
        {
            this._ProjectAppService = ProjectService;
        }


        [HttpGet]
        public async Task<PagedResultDto<ProjectDto>> GetProjects(GetProjectInput input)
        {
            return await this._ProjectAppService.GetSupplierWithFilterAsync(input);
        }

        [HttpPut]
        public async Task<ProjectDto> ChangeNameAsync(ModelName modelName)
        {
            return await this._ProjectAppService.ChangeNameAsync(modelName);
        }

        [HttpPut]
        public async Task<ProjectDto> CloseProjectAsync(int id)
        {
            return await this._ProjectAppService.CloseProjectAsync(id);
        }

        [HttpPut]
        public async Task<ProjectDto> ActiveProjectAsync(int id)
        {
            return await this._ProjectAppService.ActiveProjectAsync(id);
        }

        [HttpPost]
        public async Task<ProjectDto> CreateProjectAsync([FromBody] ProjectSavedDto dto)
        {
            return await this._ProjectAppService.CreateProjectAsync(dto);
        }
    }
}
