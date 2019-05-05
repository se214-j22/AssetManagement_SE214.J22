using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Product.Dto
{
   public partial class TitleBiddingProduct
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public TitleSupplier Supplier { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }
}
