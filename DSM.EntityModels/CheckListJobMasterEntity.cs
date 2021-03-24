using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobActivityMaster;
using static DSM.EntityModels.CheckListJobAdvanceMasterEntity;
using static DSM.EntityModels.CheckListJobLOTOTOMaster;

namespace DSM.EntityModels
{
    public class CheckListJobMasterEntity
    {
        //public class CheckListJobCustom
        //{
        //    public long checkListJobId { get; set; }
        //    public long? checkListMasterId { get; set; }
        //    public string checkListJobName { get; set; }
        //    public string checkListJobDescription { get; set; }
        //    public long? checkListJobCategoryId { get; set; }
        //    public long? checkListJobTypeId { get; set; }
        //    public long? checkListJobSupervisorId { get; set; }
        //    public long? checkListJobLineNumber { get; set; }
        //    public long? checkListShiftNumber { get; set; }
        //    public string checkListStartTime { get; set; }
        //    public string checkListEndTime { get; set; }
        //    public long? previousGrade { get; set; }
        //    public long? currentGrade { get; set; }
        //    public long? previousColor { get; set; }
        //    public long? currentColor { get; set; }
        //    public string checkListJobGroup { get; set; }
        //    public string batchNumber { get; set; }
        //    public string processOrderNumber { get; set; }
        //    public long? estimatedTime { get; set; }

        //    //Mani
        //    public string previousGradeButton { get; set; }
        //    public string currentGradeButton { get; set; }
        //    //Mani




        //}


        public class CheckListJobCustom
        {
            public long checkListJobId { get; set; }
            public long? checkListMasterId { get; set; }
            public string checkListJobName { get; set; }
            public string checkListJobDescription { get; set; }
            public long? checkListJobCategoryId { get; set; }
            public long? checkListJobTypeId { get; set; }
            public long? checkListJobSupervisorId { get; set; }
            public long? checkListJobLineNumber { get; set; }
            public long? checkListShiftNumber { get; set; }
            public string checkListStartTime { get; set; }
            public string checkListEndTime { get; set; }
            public string previousGrade { get; set; }
            public string currentGrade { get; set; }
            public string previousColor { get; set; }
            public string currentColor { get; set; }
            public string checkListJobGroup { get; set; }
            public string batchNumber { get; set; }
            public string processOrderNumber { get; set; }
            public long? estimatedTime { get; set; }

            //Mani
          //  public string previousGradeButton { get; set; }
           // public string currentGradeButton { get; set; }
            //Mani




        }



        public class CheckListJobcustoms
        {
            public long checkListJobId { get; set; }
            public long? checkListMasterId { get; set; }
            public string checkListJobName { get; set; }
            public string checkListJobDescription { get; set; }
            public long? checkListJobCategoryId { get; set; }
            public long? checkListJobTypeId { get; set; }
            public long? checkListJobSupervisorId { get; set; }
            public long? checkListJobLineNumber { get; set; }
            public long? checkListShiftNumber { get; set; }
            public DateTime? checkListStartTime { get; set; }
            public DateTime? checkListEndTime { get; set; }
            public long? previousGrade { get; set; }
            public long? currentGrade { get; set; }
            public long? previousColor { get; set; }
            public long? currentColor { get; set; }
            public string checkListMasterName { get; set; }
            public string checkListJobSupervisorName { get; set; }
            public string checkListJobLineNumberName { get; set; }
            public string checkListShiftName { get; set; }
            public string checkListStartTimeAMPM { get; set; }
            public string checkListEndTimeAMPM { get; set; }
            public string previousGradeName { get; set; }
            public string currentGradeName { get; set; }
            public string previousColorName { get; set; }
            public string currentColorName { get; set; }
            public string checkListJobCategoryName { get; set; }
            public string checkListJobTypeName { get; set; }
            public string assignedOperators { get; set; }
            public long? checkListJobGroupId { get; set; }
            public string checkListJobGroupName { get; set; }
            public string checkListJobGroup { get; set; }
            public string checkListJobStatus { get; set; }
            public bool closeButtonEnable { get; set; }
            public string batchNumber { get; set; }
            public string processOrderNumber { get; set; }
            public long? estimatedTime { get; set; }

            public bool? isAllGroupsCompletedByOperator { get; set; }


        }

        public class Flag
        {
            public bool submitFlag { get; set; }
            public bool advanceFlag { get; set; }
            public bool activityFlag { get; set; }
            public bool lototoFlag { get; set; }
        }

        public class Flag1
        {
           
            public bool advanceFlag { get; set; }
            
            public bool lototoFlag { get; set; }
        }

    }
}
