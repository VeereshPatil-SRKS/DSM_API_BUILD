using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListActivityMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListActivityMaster
    {
        CommonResponse AddAndEditCheckListActivity(CheckListActivityCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListActivity();
        CommonResponse ViewCheckListActivityBycheckListMasterId(int checkListMasterId, int checkListGroupId);
        CommonResponse ViewCheckListActivityById(int checkListActivityId);
        CommonResponse DeleteCheckListActivity(int checkListActivityId, long userId = 0);
        CommonResponse ArchiveCheckListActivity(int checkListActivityId, long userId = 0);
        CommonResponse CheckCheckListActivity(int checkListActivityId);
    }
}
