using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListGroupMasterEntity
    {
        public class CheckListGroupCustom
        {
            public long checkListGroupId { get; set; }
            public string checkListGroupName { get; set; }
            public string checkListGroupDescription { get; set; }
        }
    }
}
