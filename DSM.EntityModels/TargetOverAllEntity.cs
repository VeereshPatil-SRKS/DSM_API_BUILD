using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class TargetOverAllEntity
    {
        public class TargetOverAllCustom
        {
            public long targetId { get; set; }
            public string targetYearName { get; set; }
            public decimal targetValue { get; set; }
            public string targetStartTime { get; set; }
            public string targetEndTime { get; set; }
        }
    }
}
