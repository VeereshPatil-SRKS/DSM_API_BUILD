using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListJobMasterHistory
    {
        public long CheckListHisJobId { get; set; }
        public long? CheckListJobId { get; set; }
        public long? CheckListMasterId { get; set; }
        public string CheckListJobName { get; set; }
        public string CheckListJobDescription { get; set; }
        public long? CheckListJobCategoryId { get; set; }
        public long? CheckListJobTypeId { get; set; }
        public long? CheckListJobSupervisorId { get; set; }
        public long? CheckListJobLineNumber { get; set; }
        public long? CheckListShiftNumber { get; set; }
        public DateTime? CheckListStartTime { get; set; }
        public DateTime? CheckListEndTime { get; set; }
        public long? PreviousGrade { get; set; }
        public long? CurrentGrade { get; set; }
        public long? PreviousColor { get; set; }
        public long? CurrentColor { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public string CheckListGroup { get; set; }
        public string BatchNumber { get; set; }
        public string ProcessOrderNumber { get; set; }
        public long? EstimatedEndTime { get; set; }
        public bool? OverAllRejected { get; set; }
        public bool? OverAllApproved { get; set; }
        public bool? OverAllJobCompleted { get; set; }
        public DateTime? ReassignedDate { get; set; }
        public long? ReassignedBy { get; set; }
    }
}
