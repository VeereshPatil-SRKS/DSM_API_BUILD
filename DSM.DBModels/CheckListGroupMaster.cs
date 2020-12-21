using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListGroupMaster
    {
        public long CheckListGroupId { get; set; }
        public string CheckListGroupName { get; set; }
        public string CheckListGroupDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
