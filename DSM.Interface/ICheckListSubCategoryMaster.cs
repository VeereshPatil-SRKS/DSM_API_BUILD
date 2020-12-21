using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListSubCategoryMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListSubCategoryMaster
    {
        CommonResponse AddAndEditCheckListSubCategory(CheckListSubCategoryCustom data, long userId = 0);
        CommonResponse ViewMultipleCheckListSubCategory();
        CommonResponse ViewCheckListSubCategoryById(int checkListSubCategoryId);
        CommonResponse DeleteCheckListSubCategory(int checkListSubCategoryId, long userId = 0);
        CommonResponse ArchiveCheckListSubCategory(int checkListSubCategoryId, long userId = 0);
        CommonResponse CheckCheckListSubCategory(int checkListSubCategoryId);
    }
}
