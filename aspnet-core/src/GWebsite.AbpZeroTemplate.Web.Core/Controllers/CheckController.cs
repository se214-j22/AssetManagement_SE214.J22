using GWebsite.AbpZeroTemplate.Application.Share.Duyet;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
   public class CheckController : GWebsiteControllerBase
    {
        private readonly ICheckService checkService;
        public CheckController(ICheckService checkService)
        {
            this.checkService = checkService;
        }
        [HttpGet]
        public bool isDuyet()
        {
            return checkService.isDuyet();
        }

    }
}
