using System;
using System.Collections.Generic;
using System.Text;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class WarehouseStatus
    {
        public int AllNumber {set;get;}
        public int ParentNumber { set; get; }
        public int ChildrenNumber { set; get; }

    }
}
