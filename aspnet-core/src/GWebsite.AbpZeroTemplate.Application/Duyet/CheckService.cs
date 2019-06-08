using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Organizations;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Duyet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Duyet
{
    public class CheckService : GWebsiteAppServiceBase, ICheckService
    {
        public readonly IRepository<OrganizationUnit, long> organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        public CheckService(
          IRepository<OrganizationUnit, long> organizationUnitRepository,
         IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository)
        {
           
            this.organizationUnitRepository = organizationUnitRepository;
            this._userOrganizationUnitRepository = userOrganizationUnitRepository;
        }
        public bool isDuyet()
        {
            var user = GetCurrentUser();

            var organizationUnitIds = _userOrganizationUnitRepository
                                        .GetAll()
                                        .Where(x => x.UserId == user.Id)
                                        .Select(x => x.OrganizationUnitId)
                                        .ToList();


            var organizationUnitOrUserCodes = organizationUnitRepository
                                                            .GetAll()
                                                            .Where(x => x.IsDeleted == false && organizationUnitIds.Contains(x.Id))
                                                            .Select(x => x.Code)
                                                            .ToList();
            string[] temp = organizationUnitOrUserCodes[0].ToString().Split(".");
            if (temp.Length == 2)
                return true;
            return false;
        }
    }
}
