using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto
{
    /// <summary>
    /// <model cref="Customer"></model>
    /// </summary>
    public class CustomerInput : Entity<int>
    {
        public string maBDS { get; set; }
        public string tenBDS { get; set; }
        public string loaiBDS { get; set; }
        public string nhomBDS { get; set; }

        public string hienTrang { get; set; }
        public string dienTich { get; set; }
        public string dai { get; set; }
        public string rong { get; set; }
        public string thoiHanSD { get; set; }
        public string tinhTrangSD { get; set; }
        public string ketCauNha { get; set; }
        public string ranhGioi { get; set; }
        public string tinhTrangPhapLy { get; set; }
    }
}
