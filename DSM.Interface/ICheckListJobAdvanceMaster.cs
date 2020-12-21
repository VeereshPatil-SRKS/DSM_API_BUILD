using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobAdvanceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobAdvanceMaster
    {
        CommonResponse AddAndEditCheckListJobAdvance(CheckListJobAdvanceCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListJobAdvance();
        CommonResponse ViewCheckListJobAdvanceByCheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId);
        CommonResponse ViewCheckListJobAdvanceById(int checkListJobAdvanceId);
        CommonResponse DeleteCheckListJobAdvance(string checkListJobAdvanceId, long userId = 0);
        CommonResponse ArchiveCheckListJobAdvance(int checkListJobAdvanceId, long userId = 0);
        CommonResponse CheckCheckListJobAdvance(int checkListJobAdvanceId);
    }
}
