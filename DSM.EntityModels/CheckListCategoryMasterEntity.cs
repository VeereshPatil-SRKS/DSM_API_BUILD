using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListCategoryMasterEntity
    {
        public class CheckListCategoryCustom
        {
            public long checkListCategoryId { get; set; }
            public string checkListCategoryName { get; set; }
            public string checkListCategoryDescription { get; set; }
            public string checkListCategoryOwner { get; set; }
        }
    }
}
