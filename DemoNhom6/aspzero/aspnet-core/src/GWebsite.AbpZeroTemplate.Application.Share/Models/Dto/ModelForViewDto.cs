using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Models.Dto
{
   public class ModelForViewDto
    {
        public string model { get; set; }
        public string tenModel { get; set; }
        public string loaiXe { get; set; }
        public string hangSanXuat { get; set; }
        public float? dinhMucNhienLieu { get; set; }
        public string ghiChu { get; set; }
    }
}
