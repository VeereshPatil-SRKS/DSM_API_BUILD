using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobLOTOTOMaster
    {
        public class CheckListJobLOTOTOCustom
        {
            public long checkListJobLOTOTOId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobLockStepNumber { get; set; }
            public string positionDescription { get; set; }
            public bool? isLockOutRequired { get; set; }
            public bool? isTagOutRequired { get; set; }
            public bool? isTryOutRequired { get; set; }
            public string remarks { get; set; }
        }
    }
}
