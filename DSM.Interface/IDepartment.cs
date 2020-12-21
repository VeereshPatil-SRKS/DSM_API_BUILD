using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DepartmentEntity;

namespace DSM.Interface
{
    public interface IDepartment
    {
        CommonResponse AddAndEditDepartment(DepartmentCustom data, long userId = 0);
        CommonResponse ViewMultipleDepartment();
        CommonResponse ViewDepartmentById(int departmentId);
        CommonResponse DeleteDepartment(int departmentId, long userId = 0);
        CommonResponse ArchiveDepartment(int departmentId, long userId = 0);
        CommonResponse CheckDepartment(int departmentId);
    }
}
