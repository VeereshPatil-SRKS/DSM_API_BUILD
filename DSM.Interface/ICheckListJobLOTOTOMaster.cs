using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobLOTOTOMaster;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobLOTOTOMaster
    {
        CommonResponse AddAndEditCheckListJobLOTOTO(CheckListJobLOTOTOCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListJobLOTOTO();
        CommonResponse ViewCheckListJobLOTOTOByCheckListJobMasterId(int checkListJobId, int checkListJobGroupId);
        CommonResponse ViewCheckListJobLOTOTOById(int checkListJobLOTOTOId);
        CommonResponse DeleteCheckListJobLOTOTO(string checkListJobLOTOTOId, long userId = 0);
        CommonResponse ArchiveCheckListJobLOTOTO(int checkListJobLOTOTOId, long userId = 0);
        CommonResponse CheckCheckListJobLOTOTO(int checkListJobLOTOTOId);
    }
}
