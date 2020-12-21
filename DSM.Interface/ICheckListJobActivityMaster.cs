using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobActivityMaster;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobActivityMaster
    {
        CommonResponse AddAndEditCheckListJobActivity(CheckListJobActivityCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListJobActivity();
        CommonResponse ViewCheckListJobActivityBycheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId);
        CommonResponse ViewCheckListJobActivityById(int checkListJobActivityId);
        CommonResponse DeleteCheckListJobActivity(string checkListJobActivityId, long userId = 0);
        CommonResponse ArchiveCheckListJobActivity(int checkListJobActivityId, long userId = 0);
        CommonResponse CheckCheckListJobActivity(int checkListJobActivityId);
    }
}
