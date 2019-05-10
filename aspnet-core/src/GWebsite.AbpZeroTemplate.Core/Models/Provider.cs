using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity nhà cung cấp
    /// </summary>
    public class Provider : FullAuditModel
    {
        //tên nhà cung cấp
        public string Name { get; set; }
        //số điện thoại nhà cung cấp
        public string Phone { get; set; }
        //email nhà cung cấp
        public string Email { get; set; }
        //địa chỉ nhà cung cấp
        public string Address { get; set; }
        //mã số thuế nhà cung cấp
        public string TaxCode { get; set; }
        //thông tin thêm về nhà cung cấp
        public string Info { get; set; }
    }
}

