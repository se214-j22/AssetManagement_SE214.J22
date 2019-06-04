using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Projects;
using GWebsite.AbpZeroTemplate.Application.Share.Projects.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Projects
{
  public  class ProjectAppService : GWebsiteAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Project, int> projectRepository;
        public ProjectAppService(IRepository<Project, int> projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<PagedResultDto<ProjectDto>> GetSupplierWithFilterAsync(GetProjectInput input)
        {
            IQueryable<Project> query = projectRepository.GetAll().Where(p => p.Name.Contains(input.Name) && p.Code.Contains(input.Code) && p.CreateDate.Equals(input.CreateDate));
          
            int totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                query = projectRepository.GetAll();
                totalCount = await query.CountAsync();
            }
            List<Project> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProjectDto>(
            totalCount,
            items.Select(item => this.ObjectMapper.Map<ProjectDto>(item)).ToList());
        }

        public async Task<ProjectDto> ChangeNameAsync(ModelName modelName)
        {
            Project query = await projectRepository.GetAllIncluding(p => p.BidProfiles).FirstOrDefaultAsync(item => item.Id == modelName.Id);
            query.Name = modelName.Name;
            query = await projectRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProjectDto>(query);
        }

        public async Task<ProjectDto> CloseProjectAsync(int id)
        {
            Project query = await projectRepository.GetAllIncluding(p => p.BidProfiles).FirstOrDefaultAsync(item => item.Id == id);
            query.Status = query.Status==2?3: query.Status;
            query = await projectRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProjectDto>(query);
        }

        public async Task<ProjectDto> ActiveProjectAsync(int id)
        {
            Project query = await projectRepository.GetAllIncluding(p => p.BidProfiles).FirstOrDefaultAsync(item => item.Id == id);
            query.Status = query.Status == 2 ? 1 : query.Status;
            query = await projectRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProjectDto>(query);
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectSavedDto projectSavedDto)
        {
            Project project = ObjectMapper.Map<Project>(projectSavedDto);
            project.ActiveDate = project.Status == 1 ? DateTime.Now : (DateTime?)null;
            await projectRepository.InsertAndGetIdAsync(project);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProjectDto>(project);
        }


    }
}
