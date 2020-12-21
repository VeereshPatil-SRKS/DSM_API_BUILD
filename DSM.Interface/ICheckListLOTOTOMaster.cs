using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListLOTOTOMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListLOTOTOMaster
    {
        CommonResponse AddAndEditCheckListLOTOTO(CheckListLOTOTOCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListLOTOTO();
        CommonResponse ViewCheckListLOTOTOByCheckListMasterId(int checkListId, int checkListGroupId);
        CommonResponse ViewCheckListLOTOTOById(int checkListLOTOTOId);
        CommonResponse DeleteCheckListLOTOTO(int checkListLOTOTOId, long userId = 0);
        CommonResponse ArchiveCheckListLOTOTO(int checkListLOTOTOId, long userId = 0);
        CommonResponse CheckCheckListLOTOTO(int checkListLOTOTOId);

    }
}
