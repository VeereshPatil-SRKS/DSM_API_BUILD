using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListAdvanceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListAdvanceMaster
    {
        CommonResponse AddAndEditCheckListAdvance(CheckListAdvanceCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListAdvance();
        CommonResponse ViewCheckListAdvanceByCheckListMasterId(int checkListMasterId, int checkListGroupId);
        CommonResponse ViewCheckListAdvanceById(int checkListAdvanceId);
        CommonResponse DeleteCheckListAdvance(int checkListAdvanceId, long userId = 0);
        CommonResponse ArchiveCheckListAdvance(int checkListAdvanceId, long userId = 0);
        CommonResponse CheckCheckListAdvance(int checkListAdvanceId);
    }
}
