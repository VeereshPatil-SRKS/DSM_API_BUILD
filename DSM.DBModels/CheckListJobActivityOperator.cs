using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListJobActivityOperator
    {
        public long CheckListJobActivityOperatorId { get; set; }
        public long? CheckListJobOperatorId { get; set; }
        public long? OperatorId { get; set; }
        public string OperatorScannedBarcodeNumber { get; set; }
        public long? BarcodeAssetId { get; set; }
        public long? OperatorUpoadedDocumentId { get; set; }
        public string OperatorRemark { get; set; }
        public bool? IsAdminApproved { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CheckListJobActivityId { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public bool? IsJobRejected { get; set; }
        public string JobRejectedReason { get; set; }
    }
}
