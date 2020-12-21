using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DesignationEntity;

namespace DSM.Interface
{
    public interface IDesignation
    {
        CommonResponse AddAndEditDesignation(DesignationCustom data, long userId = 0);
        CommonResponse ViewMultipleDesignation();
        CommonResponse ViewDesignationById(int designationId);
        CommonResponse DeleteDesignation(int designationId, long userId = 0);
        CommonResponse ArchiveDesignation(int designationId, long userId = 0);
        CommonResponse CheckDesignation(int designationId);
    }
}
