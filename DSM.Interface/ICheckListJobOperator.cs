using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobOperator
    {
        CommonResponseWithIds AddAndEditCheckListJobOperator(CheckListJobOperatorCustom data, long userId = 0);
        CommonResponse ApproveCheckListJobOperator(int checkListJobOperatorId, long userId = 0);
        CommonResponse RejectCheckListJobOperator(int checkListJobOperatorId, string rejectReason, long userId = 0);
        CommonResponse OverAllSubmitCheckListJobOperator(int checkListJobOperatorId, long userId = 0);
        CommonResponse DeleteCheckListJobOperator(int checkListJobOperatorId, long userId = 0);
        CommonResponse ArchiveCheckListJobOperator(int checkListJobOperatorId, long userId = 0);
        CommonResponse ApproveCheckListJobOperatorBasedOnGroup(int checkListJobId, int checkListJobGroupId, long userId = 0);
        CommonResponse RejectCheckListJobOperatorBasedOnGroup(CheckListJobOperatorBasedOnGroup data, long userId = 0);
    }
}
