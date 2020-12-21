using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.RoleEntity;

namespace DSM.Interface
{
    public interface IRole
    {
        CommonResponse AddAndEditRole(RoleCustom data, long userId = 0);
        CommonResponse ViewMultipleRole(long userId);
        CommonResponse ViewRoleById(int roleId);
        CommonResponse DeleteRole(int roleId, long userId = 0);
        CommonResponse ArchiveRole(int roleId, long userId = 0);
        CommonResponse CheckRole(int roleId);
    }
}
