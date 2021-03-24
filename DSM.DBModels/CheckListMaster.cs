using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListMaster
    {
        public long CheckListId { get; set; }
        public string CheckListName { get; set; }
        public string CheckListVersion { get; set; }
        public string CheckListDescription { get; set; }
        public long? CheckListCategoryId { get; set; }
        public long? CheckListTypeId { get; set; }
        public string CheckListOwner { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public string CheckListGroup { get; set; }
        public long? EstimatedEndTime { get; set; }
    }
}
