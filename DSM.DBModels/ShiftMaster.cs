using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class ShiftMaster
    {
        public long ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftDescription { get; set; }
        public TimeSpan? ShiftStartTimings { get; set; }
        public TimeSpan? ShiftEndTimings { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
