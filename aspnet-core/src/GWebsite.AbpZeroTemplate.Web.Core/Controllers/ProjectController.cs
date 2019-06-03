using GWebsite.AbpZeroTemplate.Application.Share.Projects;
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



    }
}
