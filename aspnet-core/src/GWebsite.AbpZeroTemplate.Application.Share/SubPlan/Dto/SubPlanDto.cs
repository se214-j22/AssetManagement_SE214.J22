using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto
{
   public class SubPlanDto :Entity<int>
    {
        public ProductDto Product { get; set; }
        public float Totalprice { get; set; }
        public string ScheduleMonth { get; set; }
        public int ImplementQantity { get; set; }
        public float ImplementPrice { get; set; }
        public float PesidualQuantity { get; set; }
        public float PesidualPrice { get; set; }
        public int Quantity { get; set; }
        public int PlanId { get; set; }

    }
}
