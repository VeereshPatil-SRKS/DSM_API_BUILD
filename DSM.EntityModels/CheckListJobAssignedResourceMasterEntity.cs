using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobAssignedResourceMasterEntity
    {
        public class CheckListJobAssignedResourceMasterCustom
        {
            public long checkListJobAssignedResourceMasterId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public string primaryResource { get; set; }
            public bool? primaryResourceToAllFlag { get; set; }
            public string secondaryResource { get; set; }
            public bool? secondaryResourceToAllFlag { get; set; }
        }
    }
}
