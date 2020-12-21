using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobAdvanceOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobAdvanceOperator
    {
        CommonResponse AddAndEditCheckListJobAdvanceOperator(CheckListJobAdvanceOperatorCustom data, long userId = 0);
        CommonResponse ApproveCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, long userId = 0);
        CommonResponse RejectCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, string rejectReason, long userId = 0);
        CommonResponse DeleteCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, long userId = 0);
        CommonResponse ArchiveCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, long userId = 0);
    }
}
