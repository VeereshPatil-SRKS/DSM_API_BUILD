using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class DesignationMaster
    {
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string DesignationDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
