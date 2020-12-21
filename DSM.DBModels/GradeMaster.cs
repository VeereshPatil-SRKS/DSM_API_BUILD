using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class GradeMaster
    {
        public long GradeId { get; set; }
        public string GradeName { get; set; }
        public string GradeCode { get; set; }
        public string GradeDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
