using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class TargetOverall
    {
        public long TargetId { get; set; }
        public string TargetYearName { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime? TargetStartTime { get; set; }
        public DateTime? TargetEndTime { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
