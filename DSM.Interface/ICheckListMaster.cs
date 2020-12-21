using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListMaster
    {
        CommonResponseWithIds AddAndEditCheckList(CheckListCustom data, long userId = 0);
        CommonResponse GetAutomaticCheckListVersionName(string chekcListName);
        CommonResponse GetAutoSuggestForCheckListName(string chekcListName);
        CommonResponse ViewMultipleCheckList();
        CommonResponse ViewMultipleCheckListWRTCategoryIdAndTypeId(int checkListCategoryId, int checkListTypeId, long userId);
        CommonResponse ViewCheckListDetailsByCheckListAndGroupId(int checkListMasterId = 0, int checkListGroupId = 0);
        CommonResponse ViewCheckListById(int checkListId);
        CommonResponse DeleteCheckList(int checkListId, long userId = 0);
        CommonResponse ArchiveCheckList(int checkListId, long userId = 0);
        CommonResponse CheckCheckList(int checkListId);
        CommonResponse CheckCheckListCompletlyCreatedOrNot(int checkListId);
    }
}
