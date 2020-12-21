using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class RejectedJobHistory
    {
        public long RejectedId { get; set; }
        public long? CheckListJobId { get; set; }
        public long? PreviousJobIdWrtoperator { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public string Remark { get; set; }
    }
}
