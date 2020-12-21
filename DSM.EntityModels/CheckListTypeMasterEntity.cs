using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListTypeMasterEntity
    {
        public class CheckListTypeCustom
        {
            public long checkListTypeId { get; set; }
            public string checkListTypeName { get; set; }
            public string checkListTypeDescription { get; set; }
        }
    }
}
