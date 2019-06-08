using System;
using System.Collections.Generic;
using System.Text;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class WarehouseStatus
    {
        public int AllNumber {set;get;}
        public int RestingNumber { set; get; }
        public int DamagedNumber { set; get; }
        public int UsingNumber { set; get; }

    }
}
