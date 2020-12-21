using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobActivityMaster
    {
        public class CheckListJobActivityCustom
        {
            public long checkListJobActivityId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? activitySubCategoryId { get; set; }
            public long? checkListJobStepNumber { get; set; }
            public string activityDescription { get; set; }
            public string remarks { get; set; }
            public bool? isActivityManditory { get; set; }
            public bool? isPhotoManditory { get; set; }
            public bool? isBarCodeManditory { get; set; }
            public TimeSpan? expectedCompletionTime { get; set; }
            public long? assetId { get; set; }
        }

        public class CheckListJobActivityBySubCategory
        {
            public long? activitySubCategoryId { get; set; }
            public string activitySubCategoryName { get; set; }
            public List<CheckListJobActivityDetails> checkListActivityDetails { get; set; }
        }

        public class CheckListJobActivityDetails
        {
            public long checkListJobActivityId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobStepNumber { get; set; }
            public string activityDescription { get; set; }
            public string remarks { get; set; }
            public bool? isActivityManditory { get; set; }
            public bool? isPhotoManditory { get; set; }
            public bool? isBarCodeManditory { get; set; }
            public TimeSpan? expectedCompletionTime { get; set; }
            public long? assetId { get; set; }
        }
    }
}
