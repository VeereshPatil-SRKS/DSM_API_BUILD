using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class ColorMaster
    {
        public long ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public string ColorDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
