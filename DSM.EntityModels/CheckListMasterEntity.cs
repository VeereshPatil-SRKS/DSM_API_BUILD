using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListActivityMasterEntity;
using static DSM.EntityModels.CheckListAdvanceMasterEntity;
using static DSM.EntityModels.CheckListLOTOTOMasterEntity;

namespace DSM.EntityModels
{
    public class CheckListMasterEntity
    {
        public class CheckListCustom
        {
            public long checkListId { get; set; }
            public string checkListName { get; set; }
            public string checkListVersion { get; set; }
            public string checkListDescription { get; set; }
            public long? checkListCategoryId { get; set; }
            public long? checkListTypeId { get; set; }
            public string checkListOwner { get; set; }
            public string checkListGroup { get; set; }
            public string checkListGroupId { get; set; }
            public dynamic checkListGroupList { get; set; }

            //Mani
            public long? estimatedTime { get; set; }
            //Mani


        }

        public class CheckListDetails
        {
            public long checkListId { get; set; }
            public string checkListName { get; set; }
            public string checkListVersion { get; set; }
            public string checkListCategoryName { get; set; }
            public string checkListTypeName { get; set; }
            public string checkListOwner { get; set; }
            public string checkListGroup { get; set; }
            public string checkListGroupId { get; set; }
            public dynamic checkListGroupList { get; set; }
            public List<CheckListAdvanceCustom> checkListAdvanceCustom { get; set; }
            public List<CheckListActivityBySubCategory> checkListActivityBySubCategory { get; set; }
            public List<CheckListLOTOTOCustom> checkListLOTOTOCustom { get; set; }
        }

        public class CheckListCustoms
        {
            public long checkListId { get; set; }
            public string checkListName { get; set; }
            public string checkListVersion { get; set; }
            public string checkListDescription { get; set; }
            public long? checkListCategoryId { get; set; }
            public long? checkListTypeId { get; set; }
            public string checkListOwner { get; set; }
            public string checkListCategoryName { get; set; }
            public string checkListTypeName { get; set; }
            public bool isActive { get; set; }
            public string checkListGroup { get; set; }
            public string checkListGroupId { get; set; }
            public dynamic checkListGroupList { get; set; }
            //Mani
            public long? estimatedTime { get; set; }
            //Mani
        }

    }
}
