using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListJobWrtoperator
    {
        public long CheckListJobWrtoperatorId { get; set; }
        public long CheckListJobMasterId { get; set; }
        public long OperatorId { get; set; }
        public DateTime? CheckListJobStartTime { get; set; }
        public DateTime? CheckListJobEndTime { get; set; }
        public bool? CheckListJobIsCompleted { get; set; }
        public bool? CheckListJobIsPartialCompleted { get; set; }
        public bool? IsAdminApproved { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsJobClosed { get; set; }
        public long? CheckListJobGroupId { get; set; }
        public bool? IsJobRejected { get; set; }
        public DateTime? JobApprovedTime { get; set; }
        public string JobRejectedReason { get; set; }
    }
}
