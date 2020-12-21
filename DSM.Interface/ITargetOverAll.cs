using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.TargetOverAllEntity;

namespace DSM.Interface
{
    public interface ITargetOverAll
    {
        CommonResponse AddAndEditTargetOverAll(TargetOverAllCustom data, long userId = 0);
        CommonResponse ViewMultipleTargetOverAll(long userId);
        CommonResponse ViewTargetOverAllById(int targetOverAllId);
        CommonResponse DeleteTargetOverAll(int targetOverAllId, long userId = 0);
        CommonResponse ArchiveTargetOverAll(int targetOverAllId, long userId = 0);
    }
}
