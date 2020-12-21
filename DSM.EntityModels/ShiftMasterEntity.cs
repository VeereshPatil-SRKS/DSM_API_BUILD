using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class ShiftMasterEntity
    {
        public class ShiftCustom
        {
            public long shiftId { get; set; }
            public string shiftName { get; set; }
            public string shiftDescription { get; set; }
            public TimeSpan? shiftStartTimings { get; set; }
            public TimeSpan? shiftEndTimings { get; set; }
        }
    }
}
