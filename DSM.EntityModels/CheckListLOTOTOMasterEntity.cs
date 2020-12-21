using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListLOTOTOMasterEntity
    {
        public class CheckListLOTOTOCustom
        {
            public long checkListLOTOTOId { get; set; }
            public long? checkListMasterId { get; set; }
            public long? checkListGroupId { get; set; }
            public long? checkListLockStepNumber { get; set; }
            public string positionDescription { get; set; }
            public bool? isLockOutRequired { get; set; }
            public bool? isTagOutRequired { get; set; }
            public bool? isTryOutRequired { get; set; }
            public string remarks { get; set; }
        }
    }
}
