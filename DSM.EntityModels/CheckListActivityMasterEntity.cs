using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListActivityMasterEntity
    {
        public class CheckListActivityCustom
        {
            public long checkListActivityId { get; set; }
            public long? checkListMasterId { get; set; }
            public long? checkListGroupId { get; set; }
            public long? activitySubCategoryId { get; set; }
            public long? checkListStepNumber { get; set; }
            public string activityDescription { get; set; }
            public string remarks { get; set; }
            public bool? isActivityManditory { get; set; }
            public bool? isPhotoManditory { get; set; }
            public bool? isBarCodeManditory { get; set; }
            public TimeSpan? expectedCompletionTime { get; set; }
            public long? assetId { get; set; }
        }

        public class CheckListActivityBySubCategory
        {
            public long? activitySubCategoryId { get; set; }
            public string activitySubCategoryName { get; set; }
            public List<CheckListActivityDetails> checkListActivityDetails { get; set; }
        }

        public class CheckListActivityDetails
        {
            public long checkListActivityId { get; set; }
            public long? checkListMasterId { get; set; }
            public long? checkListGroupId { get; set; }
            public long? checkListStepNumber { get; set; }
            public string activityDescription { get; set; }
            public string remarks { get; set; }
            public bool? isActivityManditory { get; set; }
            public bool? isPhotoManditory { get; set; }
            public bool? isBarCodeManditory { get; set; }
            public TimeSpan? expectedCompletionTime { get; set; }
            public long? assetId { get; set; }
            public string assetNumber { get; set; }
        }
    }
}
