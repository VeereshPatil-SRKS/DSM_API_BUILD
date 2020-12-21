using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobMaster
    {
        CommonResponseWithIds AddAndEditCheckListJob(CheckListJobCustom data, long userId = 0);
        CommonResponse GetCheckListJobType(int checkListId);
        CommonResponse UpdateCheckListEndTimeOfJobByCheckListJobId(int checkListJobId, string checkListEndTime, long userId = 0);
        CommonResponse MoveCheckListDataToCheckListJob(int checkListJobId, int checkListId, int userId = 0);
        CommonResponse ViewMultipleCheckListJob();
        CommonResponse ViewMultipleCheckListJobWRTUser(int userId);
        CommonResponse ViewCheckListJobDetailsByCheckListJobAndGroupId(int checkListJobMasterId = 0, int checkListJobGroupId = 0, int checkListJobOperatorId = 0);
        CommonResponseResource ViewCheckListAssignedJobByCheckListJobIdAndCheckListJobOperatorId(int checkListJobMasterId, int checkListJobGroupId);
        CommonResponse CheckListJobEnableDisable(int checkListJobMasterId = 0, int checkListJobGroupId = 0, int checkListJobOperatorId = 0);
        CommonResponse ViewCheckListJobById(int checkListJobId);
        CommonResponse DeleteCheckListJob(int checkListJobId, long userId = 0);
        CommonResponse ArchiveCheckListJob(int checkListJobId, long userId = 0);
        CommonResponse CheckCheckListJob(int checkListJobId);

    }
}
