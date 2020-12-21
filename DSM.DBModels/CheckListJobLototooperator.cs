using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListJobLototooperator
    {
        public long CheckListJobLototooperatorId { get; set; }
        public long? CheckListJobOperatorId { get; set; }
        public long? OperatorId { get; set; }
        public string OverAllRemark { get; set; }
        public long? LockOutDoneByOperator { get; set; }
        public string LockOutRemark { get; set; }
        public long? TagOutDoneByOperator { get; set; }
        public string TagOutRemark { get; set; }
        public long? TryOutDoneByOperator { get; set; }
        public string TryOutRemark { get; set; }
        public bool? IsAdminApproved { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CheckListJobLototoid { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public bool? IsJobRejected { get; set; }
        public string JobRejectedReason { get; set; }
    }
}
