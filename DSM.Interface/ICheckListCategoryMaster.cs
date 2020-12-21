using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListCategoryMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListCategoryMaster
    {
        CommonResponse AddAndEditCheckListCategory(CheckListCategoryCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListCategory();
        CommonResponse ViewCheckListCategoryById(int checkListCategoryId);
        CommonResponse DeleteCheckListCategory(int checkListCategoryId, long userId = 0);
        CommonResponse ArchiveCheckListCategory(int checkListCategoryId, long userId = 0);
        CommonResponse CheckCheckListCategory(int checkListCategoryId);
    }
}
