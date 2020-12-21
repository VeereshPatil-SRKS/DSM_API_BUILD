using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListLototomaster
    {
        public long LototoCheckListId { get; set; }
        public long? CheckListMasterId { get; set; }
        public long? CheckListGroupId { get; set; }
        public long? CheckListLockStepNumber { get; set; }
        public string PositionDescription { get; set; }
        public bool? IsLockOutRequired { get; set; }
        public bool? IsTagOutRequired { get; set; }
        public bool? IsTryOutRequired { get; set; }
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
