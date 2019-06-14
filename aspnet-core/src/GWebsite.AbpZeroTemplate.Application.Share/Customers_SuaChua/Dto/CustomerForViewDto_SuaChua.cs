 using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua.Dto
{
    /// <summary>
    /// <model cref="Customer_SuaChua"></model>
    /// </summary>
    public class CustomerForViewDto_SuaChua
    {

        public string ngayXuat { get; set; }
        public string ngaySuaXong { get; set; }
        public string maBDS { get; set; }
        public string tenBDS { get; set; }

        public string nguoiDeXuat { get; set; }
        public string nvPhuTrach { get; set; }
        public string trangThaiDuyet { get; set; }
    }
}
