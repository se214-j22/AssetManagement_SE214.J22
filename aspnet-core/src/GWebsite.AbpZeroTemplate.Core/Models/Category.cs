using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity danh mục tài sản
    /// </summary>
    public class Category : FullAuditModel
    {
        //tên danh mục tài sản
        public string Name { get; set; }
        //ghi chú danh mục tài sản
        public string Note { get; set; }
    }
}
