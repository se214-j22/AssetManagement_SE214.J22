using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto
{
    /// <summary>
    /// <model cref="Provider"></model>
    /// </summary>
    public class ProviderDto : Entity<int>
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
