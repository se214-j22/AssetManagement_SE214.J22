using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// chi tiết tài sản
    /// </summary>
    public class AssetDetail : FullAuditModel
    {
        //tên chi tiết tài sản
        public string Name { get; set; }
        //mã tài sản
        public string AssetID { get; set; }
        //giá trị tài sản
        public string Price { get; set; }
        //đặc điểm kĩ thuật
        public string Specification { get; set; }
    }
}
