using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Contract.Dto
{
    public partial class BiddingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public float Price { get; set; }
        public int BiddingType { get; set; }
    }
}
