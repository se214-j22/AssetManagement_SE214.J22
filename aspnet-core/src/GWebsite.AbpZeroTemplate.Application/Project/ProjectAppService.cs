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
    }
}
