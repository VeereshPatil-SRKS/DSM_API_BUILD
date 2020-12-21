using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListSubCategoryMaster
    {
        public long CheckListSubCategoryId { get; set; }
        public string CheckListSubCategoryName { get; set; }
        public string CheckListSubCategoryDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
