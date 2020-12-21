using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.ShiftMasterEntity;

namespace DSM.Interface
{
    public interface IShiftMaster
    {
        CommonResponse AddAndEditShift(ShiftCustom data, long userId = 0);
        CommonResponse ViewMultipleShift();
        CommonResponse ViewShiftById(int shiftId);
        CommonResponse DeleteShift(int shiftId, long userId = 0);
        CommonResponse ArchiveShift(int shiftId, long userId = 0);
    }
}
