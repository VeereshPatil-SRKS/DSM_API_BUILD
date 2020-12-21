using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobActivityOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobActivityOperator
    {
        CommonResponse AddAndEditCheckListJobActivityOperator(CheckListJobActivityOperatorCustom data, long userId = 0);
        CommonResponse CheckListJobActivityOperatorStartActivity(int checkListJobOperatorId, int checkListJobActivityId,string barcodeNumber, long userId = 0);
        CommonResponse ApproveCheckListJobActivityOperator(int checkListJobActivityOperatorId, long userId = 0);
        CommonResponse RejectCheckListJobActivityOperator(int checkListJobActivityOperatorId, string rejectReason, long userId = 0);
        CommonResponse DeleteCheckListJobActivityOperator(int checkListJobActivityOperatorId, long userId = 0);
        CommonResponse ArchiveCheckListJobActivityOperator(int checkListJobActivityOperatorId, long userId = 0);
    }
}
