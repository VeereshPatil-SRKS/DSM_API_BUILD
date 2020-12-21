using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListAdvanceMaster
    {
        public long AdvanceCheckListId { get; set; }
        public long? CheckListMasterId { get; set; }
        public long? CheckListGroupId { get; set; }
        public long? CheckListStepNumber { get; set; }
        public string ActivityBeforeChangeOverDescription { get; set; }
        public string Remarks { get; set; }
        public bool? IsAdminApproved { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
