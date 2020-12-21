using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class HelpContentMaster
    {
        public long HelpContentId { get; set; }
        public string HelpContentShortName { get; set; }
        public string HelpContentDescription { get; set; }
        public string InstructionLink { get; set; }
        public string VisbleToRoleId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
