using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListAdvanceMasterEntity
    {
        public class CheckListAdvanceCustom
        {
            public long checkListAdvanceId { get; set; }
            public long? checkListMasterId { get; set; }
            public long? checkListGroupId { get; set; }
            public long? checkListStepNumber { get; set; }
            public string activityBeforeChangeOverDescription { get; set; }
            public string remarks { get; set; }
        }
    }
}
