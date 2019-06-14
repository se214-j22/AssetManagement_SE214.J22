using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto
{
    /// <summary>
    /// <model cref="Customer"></model>
    /// </summary>
    public class CustomerDto : Entity<int>
    {
        public string maBDS { get; set; }
        public string tenBDS { get; set; }
        public string loaiBDS { get; set; }
        public string nhomBDS { get; set; }
    }
}
