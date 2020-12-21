using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class HelpContentEntity
    {
        public class HelpContentCustom
        {
            public long helpContentId { get; set; }
            public string helpContentShortName { get; set; }
            public string helpContentDescription { get; set; }
            public string instructionLink { get; set; }
            public string visbleToRoleId { get; set; }
        }

        public class HelpView
        {
            public long helpContentId { get; set; }
            public string helpContentShortName { get; set; }
            public string helpContentDescription { get; set; }
            public string instructionLink { get; set; }
            public string visbleToRoleId { get; set; }
            public dynamic visbleToRole { get; set; }
            public bool? isActive { get; set; } 
        }
    }
}
