using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class LineNumberMaster
    {
        public long LineNumberId { get; set; }
        public string LineNumberName { get; set; }
        public string LineNumberDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
