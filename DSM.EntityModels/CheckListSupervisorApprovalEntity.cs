using System;
using System.Collections.Generic;
using static DSM.EntityModels.CheckListJobActivityMaster;

namespace DSM.EntityModels
{
    public class CheckListSupervisorApprovalEntity
    {
        public class CheckListSupervisorApprovalCustom
        {
            public CheckListJobDetails checkListJobDetails { get; set; }
        }

        public class CheckListJobDetails
        {
            public long checkListJobId { get; set; }
            public long? checkListMasterId { get; set; }
            public string checkListMasterName { get; set; }
            public string checkListJobName { get; set; }
            public string checkListJobDescription { get; set; }
            public long? checkListJobCategoryId { get; set; }
            public string checkListJobCategoryName { get; set; }
            public long? checkListJobTypeId { get; set; }
            public string checkListJobTypeName { get; set; }
            public long? checkListJobSupervisorId { get; set; }
            public string checkListJobSupervisorName { get; set; }
            public long? checkListJobLineNumber { get; set; }
            public string checkListJobLineNumberName { get; set; }
            public long? checkListShiftNumber { get; set; }
            public string checkListShiftName { get; set; }
            public DateTime? checkListStartTime { get; set; }
            public string checkListStartTimeAMPM { get; set; }
            public DateTime? checkListEndTime { get; set; }
            public string checkListEndTimeAMPM { get; set; }
            public long? previousGrade { get; set; }
            public string previousGradeName { get; set; }
            public long? currentGrade { get; set; }
            public string currentGradeName { get; set; }
            public long? previousColor { get; set; }
            public string previousColorName { get; set; }
            public long? currentColor { get; set; }
            public string currentColorName { get; set; }
            public string assignedOperators { get; set; }
            public operatorInArray[] assignedOperatorsInArray { get; set; }
            public string jobCreatedBy { get; set; }

            public string totalTimeTook { get; set; }
            public bool approveButton { get; set; }
            public bool rejectButton { get; set; }
            public bool? OverAllRejected { get; set; }
            public bool? OverAllApproved { get; set; }
            public bool? OverAllJobRejected { get; set; }
            public bool? OverAllJobCompleted { get; set; }
            public string batchNumber { get; set; }
            public string processOrderNumber { get; set; }
            public CheckListJobOperatorCustom checkListJobOperatorCustom { get; set; }
            public List<CheckListJobAdvanceCustom> checkListJobAdvanceCustom { get; set; }
            public List<CheckListJobActivityBySubCategory> checkListJobActivityBySubCategory { get; set; }
            public List<CheckListJobLOTOTOCustom> checkListJobLOTOTOCustom { get; set; }
           // public List<CheckListJobLOTOTOCustomAdmin> checkListJobLOTOTOCustomAdmin { get; set; }


        }


        public class operatorInArray
        {
            public long userId { get; set; }
           
            public string userName { get; set; }
           
        }



        //Mani
        public class CheckListJobDetailsLotato
        {
            public long checkListJobId { get; set; }
            public long? checkListMasterId { get; set; }
            public string checkListMasterName { get; set; }
            public string checkListJobName { get; set; }
            public string checkListJobDescription { get; set; }
            public List<CheckListJobLOTOTOCustom> checkListJobLOTOTOCustom{ get; set; }
        }


        public class CheckListJobDetailsLotatoAdmin
        {
            public long checkListJobId { get; set; }
            public long? checkListMasterId { get; set; }
            public string checkListMasterName { get; set; }
            public string checkListJobName { get; set; }
            public string checkListJobDescription { get; set; }
            public List<CheckListJobLOTOTOCustomAdmin> checkListJobLOTOTOCustom { get; set; }

            public bool? overAllSubmit { get; set; }

        }



        //public class CheckListJobLOTOTOCustom
        //{
        //    public long checkListJobLOTOTOId { get; set; }
        //    public long? checkListJobMasterId { get; set; }
        //    public long? checkListJobGroupId { get; set; }
        //    public long? checkListJobLockStepNumber { get; set; }
        //    public string positionDescription { get; set; }
        //    public bool? isLockOutRequired { get; set; }
        //    public bool? isTagOutRequired { get; set; }
        //    public bool? isTryOutRequired { get; set; }

        //   // public string remarks { get; set; }
        //    public CheckListJobLOTOTOOperatorCustom checkListJobLOTOTOOperatorCustom { get; set; }
        //}
        //Mani



        public class CheckListJobDetailsAll
        {
            public long checkListJobId { get; set; }
            public long? checkListMasterId { get; set; }
            public string checkListMasterName { get; set; }
            public string checkListJobName { get; set; }
            public string checkListJobDescription { get; set; }
            public long? checkListJobCategoryId { get; set; }
            public string checkListJobCategoryName { get; set; }
            public long? checkListJobTypeId { get; set; }
            public string checkListJobTypeName { get; set; }
            public long? checkListJobSupervisorId { get; set; }
            public string checkListJobSupervisorName { get; set; }
            public long? checkListJobLineNumber { get; set; }
            public string checkListJobLineNumberName { get; set; }
            public long? checkListShiftNumber { get; set; }
            public string checkListShiftName { get; set; }
            public DateTime? checkListStartTime { get; set; }
            public string checkListStartTimeAMPM { get; set; }
            public DateTime? checkListEndTime { get; set; }
            public string checkListEndTimeAMPM { get; set; }
            public long? previousGrade { get; set; }
            public string previousGradeName { get; set; }
            public long? currentGrade { get; set; }
            public string currentGradeName { get; set; }
            public long? previousColor { get; set; }
            public string previousColorName { get; set; }
            public long? currentColor { get; set; }
            public string currentColorName { get; set; }
            public string assignedOperators { get; set; }
            public string jobCreatedBy { get; set; }
            public string totalTimeTook { get; set; }
            public bool approveButton { get; set; }
            public bool rejectButton { get; set; }
            public string batchNumber { get; set; }
            public string processOrderNumber { get; set; }
            public List<GroupItem> groupItems { get; set; }
            
        }

        public class GroupItem
        {
            public long checkListJobGroupId { get; set; }
            public string checkListJobGroupName { get; set; }
            public string checkListStartTimeAMPM { get; set; }
            public string checkListEndTimeAMPM { get; set; }
            public string totalTimeTook { get; set; }
            public CheckListJobOperatorCustom checkListJobOperatorCustom { get; set; }
            public List<CheckListJobAdvanceCustom> checkListJobAdvanceCustom { get; set; }
            public List<CheckListJobActivityBySubCategory> checkListJobActivityBySubCategory { get; set; }
            public List<CheckListJobLOTOTOCustom> checkListJobLOTOTOCustom { get; set; }
        }

        public class CheckListJobOperatorCustom
        {
            public long? checkListJobMasterId { get; set; }
        }

        public class CheckListJobAdvanceCustom
        {
            public long checkListJobAdvanceId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobStepNumber { get; set; }
            public string activityBeforeChangeOverDescription { get; set; }
            public string remarks { get; set; }
            public CheckListJobAdvanceOperatorCustom checkListJobAdvanceOperatorCustom { get; set; }
        }

        public class CheckListJobAdvanceOperatorCustom
        {
            public long checkListJobAdvanceOperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public string operatorRemark { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public bool approveRejectFlag { get; set; }
            public bool? isJobRejected { get; set; }
            //Mani
            public string JobRejectedReason { get; set; }
            //Mani

            public bool isJobCompleted { get; set; }
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
            public string assetNumber { get; set; }
            public CheckListJobActivityOperatorCustom checkListJobActivityOperatorCustom { get; set; }
        }
        
        public class CheckListJobActivityOperatorCustom
        {
            public long? checkListJobActivityOperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public long? operatorId { get; set; }
            public string operatorScannedBarcodeNumber { get; set; }
            public long? barcodeAssetId { get; set; }
            public long? operatorUpoadedDocumentId { get; set; }
            public string operatorUpoadedDocumentURL { get; set; }
            public string operatorRemark { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public string totalTimeTook { get; set; }
            public bool approveRejectFlag { get; set; }
            public bool? isJobRejected { get; set; }
            //Mani
            public string JobRejectedReason { get; set; }
            //Mani

            public bool isJobCompleted { get; set; }
        }

        //Mani
        
            public class CheckListJobLOTOTOCustom1
            {
                public long checkListJobLOTOTOId { get; set; }
                public long? checkListJobMasterId { get; set; }
                public long? checkListJobGroupId { get; set; }
                public long? checkListJobLockStepNumber { get; set; }
                public string positionDescription { get; set; }
                public bool? isLockOutRequired { get; set; }
                public bool? isTagOutRequired { get; set; }
                public bool? isTryOutRequired { get; set; }


               // public string remarks { get; set; }
            }
        
        //Mani

        public class CheckListJobLOTOTOCustom
        {
            public long checkListJobLOTOTOId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobLockStepNumber { get; set; }
            public string positionDescription { get; set; }
            public bool? isLockOutRequired { get; set; }
            public bool? isTagOutRequired { get; set; }
            public bool? isTryOutRequired { get; set; }
            public string remarks { get; set; }
            public CheckListJobLOTOTOOperatorCustom checkListJobLOTOTOOperatorCustom { get; set; }


            public CheckListJobLOTOTOOperatorCustomAdmin checkListJobLOTOTOCustomAdmin { get; set; }



        }

        public class CheckListJobLOTOTOCustomAdmin
        {
            public long checkListJobLOTOTOId { get; set; }
            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobLockStepNumber { get; set; }
            public string positionDescription { get; set; }
            public bool? isLockOutRequired { get; set; }
            public bool? isTagOutRequired { get; set; }
            public bool? isTryOutRequired { get; set; }
            public string remarks { get; set; }
            public CheckListJobLOTOTOOperatorCustomAdmin checkListJobLOTOTOOperatorCustom { get; set; }
        }

        public class CheckListJobLOTOTOOperatorCustom
        {
            public long checkListJobLototooperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public long? operatorId { get; set; }
            public string overAllRemark { get; set; }
            public long? lockOutDoneByOperator { get; set; }
            public string lockOutDoneByOperatorName { get; set; }
            public string lockOutRemark { get; set; }
            public long? tagOutDoneByOperator { get; set; }
            public string tagOutDoneByOperatorName { get; set; }
            public string tagOutRemark { get; set; }
            public long? tryOutDoneByOperator { get; set; }
            public string tryOutDoneByOperatorName { get; set; }
            public string tryOutRemark { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public bool approveRejectFlag { get; set; }
            public bool? isJobRejected { get; set; }

            public string JobRejectedReason { get; set; }

            public bool isJobCompleted { get; set; }

        }

        public class CheckListJobLOTOTOOperatorCustomAdmin
        {
            public long checkListJobLototooperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public long? supervisorId { get; set; }

            public string  supervisorName { get; set; }
          //  public string overAllRemark { get; set; }
            public long? lockOutDoneBy { get; set; }
            public string lockOutDoneByName { get; set; }
            public bool? lockOut { get; set; }
            public long? tagOutDoneBy { get; set; }
            public string tagOutDoneByName { get; set; }
            public bool? tagOut { get; set; }
            public long? tryOutDoneBy { get; set; }
            public string tryOutDoneByName { get; set; }
            public bool? tryOut { get; set; }

            public bool? isSubmit { get; set; }


            public long? tryOutDoneByOperatorId { get; set; }
            public string tryOutDoneByOperatorName { get; set; }
            public string tryOutRemarks { get; set; }
            public string overAllRemarks { get; set; }


            // public string startTime { get; set; }
            //  public string endTime { get; set; }
            //  public bool approveRejectFlag { get; set; }
            //  public bool? isJobRejected { get; set; }

            //  public string JobRejectedReason { get; set; }
        }




        //Mani
        //public class CheckListJobLOTOTOOperatorCustomNew
        //{
        //    public long checkListJobLototooperatorId { get; set; }
        //    public long? checkListJobOperatorId { get; set; }
        //    public long? operatorId { get; set; }
        //    public string overAllRemark { get; set; }
        //    public long? lockOutDoneByOperator { get; set; }
        //    public string lockOutRemark { get; set; }
        //    public long? tagOutDoneByOperator { get; set; }
        //    public string tagOutRemark { get; set; }
        //    public long? tryOutDoneByOperator { get; set; }
        //    public long? checkListJobLOTOTOId { get; set; }
        //    public string tryOutRemark { get; set; }
        //}
        //Mani






        public class CheckListJobLOTOTOOperatorCustomNew
        {
            public long id { get; set; }
           // public long? checkListJobOperatorId { get; set; }

            public long? checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }

            public long? adminId { get; set; }

           // public string overAllRemark { get; set; }
            public long? lockOutDoneBy { get; set; }
            public bool lockOut { get; set; } 

           

            // public bool lockOut { get; set; } = false;
            public long? tagOutDoneBy { get; set; }
            public bool tagOut { get; set; } 
            public long? tryOutDoneBy { get; set; }

            public bool tryOut { get; set; } 

            public long? checkListJobLOTOTOId { get; set; }

           


        }


        public class CheckListJobLOTOTOByadmin
        {
            public long checkListJobMasterId { get; set; }
            public long? checkListJobGroupId { get; set; }
            public long? checkListJobLototoId { get; set; }
            public long? checkListJobLototoLockNo { get; set; }
            public long? SupervisorId { get; set; }
            public bool lockOut { get; set; }
            public bool tryOut { get; set; }
            public bool tagOut { get; set; }
            
        }


        public class remainigGroup
        {
            public long checkListJobMasterId { get; set; }
            public long groupId { get; set; }
            public string groupName { get; set; }
            public bool isThisGroupAllJobsCompleted { get; set; }
          

        }





    }
}
