using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Const
{
    public class Const
    {
        public enum AssetStatus
        {
            IN_STOCK = 0,
            USING = 1,
            REPARING = 2,
            LIQUIDATED =3
        }

        public enum ApprovedStatus
        {
            NOT_APPROVE_YET = 0,
            APPROVE = 1
        }
    }
    
}
