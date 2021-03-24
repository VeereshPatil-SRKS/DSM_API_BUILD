using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class ReAssignedcheckListJobResourcesOperator
    {
        public long ReAssignedResourceId { get; set; }
        public long? CheckListJobMasterId { get; set; }
        public long? CheckListJobGroupId { get; set; }
        public string PrimaryResource { get; set; }
        public bool? PrimaryResourceToAllFlag { get; set; }
        public string SecondaryResource { get; set; }
        public bool? SecondaryResourceToAllFlag { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public int? IsReassigned { get; set; }
        public string ChecklistJobFirstOperatorIds { get; set; }
    }
}
