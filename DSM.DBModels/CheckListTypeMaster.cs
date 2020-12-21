using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class CheckListTypeMaster
    {
        public long CheckListTypeId { get; set; }
        public string CheckListTypeName { get; set; }
        public string CheckListTypeDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
