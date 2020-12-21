using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListTypeMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListTypeMaster
    {
        CommonResponse AddAndEditCheckListType(CheckListTypeCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListType();
        CommonResponse ViewCheckListTypeById(int checkListTypeId);
        CommonResponse DeleteCheckListType(int checkListTypeId, long userId = 0);
        CommonResponse ArchiveCheckListType(int checkListTypeId, long userId = 0);
        CommonResponse CheckCheckListType(int checkListTypeId);
    }
}
