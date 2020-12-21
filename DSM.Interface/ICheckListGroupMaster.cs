using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListGroupMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListGroupMaster
    {
        CommonResponse AddAndEditCheckListGroup(CheckListGroupCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListGroup();
        CommonResponse ViewMultipleCheckListGroupByCheckListMasterId(int checkListMasterId=0);
        CommonResponse ViewMultipleCheckListGroupByCheckListJobMasterId(int checkListJobMasterId=0);
        CommonResponse ViewCheckListGroupById(int checkListGroupId);
        CommonResponse DeleteCheckListGroup(int checkListGroupId, long userId = 0);
        CommonResponse ArchiveCheckListGroup(int checkListGroupId, long userId = 0);
        CommonResponse CheckCheckListGroup(int checkListGroupId);
    }
}
