using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListCategoryMaster
    {
        public long CheckListCategoryId { get; set; }
        public string CheckListCategoryName { get; set; }
        public string CheckListCategoryDescription { get; set; }
        public string CheckListCategoryOwner { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
