using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListSupervisorApprovalEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListSupervisorApproval
    {
        CommonResponse CheckListForSupervisorApproval(int checkListJobId, int checkListJobGroupId, long userId);

        //Mani
        CommonResponse CheckListForLototo(int checkListJobId, int checkListJobGroupId, long userId);
        //Mani

        ////Mani
        CommonResponse AddAndEditCheckListForLOTOTO(CheckListJobLOTOTOCustom1 data, long userId = 0);
        ////Mani

        

        //veeresh
        CommonResponse AddAndEditCheckListJobLOTOTOAdmin(CheckListJobLOTOTOOperatorCustomNew data, long userId = 0);
        
        CommonResponse CheckListJobOverallsubmit(int checkListJobId, int checkListJobGroupId);


        CommonResponse CheckListJobLototoCheckEnable(int checkListJobMasterId, int checkListGroupId, int checkListJobOperatorId);

        //Mani
        CommonResponse ViewMultipleCheckListJobForApproval(long userId);
        CommonResponse ViewMultipleCheckListGroupByCheckListJobMasterId(int checkListJobMasterId);
        CommonResponse CloseJobByCheckListJobMasterId(int checkListJobMasterId);
        CommonResponse ViewMultipleCheckListGroupByCheckListJobMasterIdAfterJobCompletion(int checkListJobMasterId);
        CommonResponse ViewMultipleSupervisorForReassigningJob(int superVisorId);
        CommonResponse ReassigningJobToSupervisor(int checkListJobId, int superVisorId, long userId);
    }
}
