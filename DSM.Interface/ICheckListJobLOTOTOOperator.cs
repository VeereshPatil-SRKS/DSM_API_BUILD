using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobLOTOTOOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobLOTOTOOperator
    {
        CommonResponse AddAndEditCheckListJobLOTOTOOperator(CheckListJobLOTOTOOperatorCustom data, long userId = 0);
        CommonResponse ApproveCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId, long userId = 0);
        CommonResponse RejectCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId, string rejectReason, long userId = 0);
        CommonResponse DeleteCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId, long userId = 0);
        CommonResponse ArchiveCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId, long userId = 0);
    }
}
