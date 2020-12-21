using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobAdvanceOperatorEntity
    {
        public class CheckListJobAdvanceOperatorCustom
        {
            public long checkListJobAdvanceOperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public long? checkListJobAdvanceId { get; set; }
            public string operatorRemark { get; set; }
        }
    }
}
