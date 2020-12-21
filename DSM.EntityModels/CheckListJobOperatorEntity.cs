using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobOperatorEntity
    {
        public class CheckListJobOperatorCustom
        {
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
        }

        public class CheckListJobOperatorBasedOnGroup
        {
            public int checkListJobId { get; set; }
            public int checkListJobGroupId { get; set; }
            public string rejectReason { get; set; }

        }
    }
}
