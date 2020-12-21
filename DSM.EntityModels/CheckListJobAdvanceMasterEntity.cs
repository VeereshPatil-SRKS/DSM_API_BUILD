using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobAdvanceMasterEntity
    {
        public class CheckListJobAdvanceCustom
        {
            public long checkListJobAdvanceId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobStepNumber { get; set; }
            public string activityBeforeChangeOverDescription { get; set; }
            public string remarks { get; set; }
        }
    }
}
