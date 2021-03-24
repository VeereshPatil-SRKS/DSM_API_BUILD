using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CheckListJobAssignedResourceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface ICheckListJobAssignedResourceMaster
    {
        CommonResponse AddAndEditCheckListJobAssignedResource(CheckListJobAssignedResourceMasterCustom data, long userId = 0);
        CommonResponse AddAndEditCheckListJobAssignedResourceAll(int checkListJobMasterId, long userId = 0);
        CommonResponse ViewMultipleCheckListJobAssignedResource();
        CommonResponse ViewCheckListJobAssignedResourceBycheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId);
        CommonResponse ViewCheckListJobAssignedResourceById(int checkListJobAssignedResourceId);
        CommonResponse DeleteCheckListJobAssignedResource(int checkListJobAssignedResourceId, long userId = 0);
        CommonResponse ArchiveCheckListJobAssignedResource(int checkListJobAssignedResourceId, long userId = 0);
        CommonResponse CheckCheckListJobAssignedResource(int checkListJobAssignedResourceId);


        CommonResponse AddAndEditReAssignedCheckListJobResources(CheckListJobAssignedResourceMasterCustom data, long userId = 0);



    }
}
