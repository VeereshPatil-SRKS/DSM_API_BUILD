using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.UserManagementEntity;

namespace DSM.Interface
{
    public interface IUserManagement
    {
        CommonResponse AddAndEditUser(UserDetailsCustom data, long usersId = 0);
        CommonResponse ViewMultipleUser(long usersId = 0);
        CommonResponse ViewMultipleUserForOperator();
        CommonResponse ViewMultipleUserForAdmin();
        CommonResponse ViewMultipleUserForSuperAdmin();
        CommonResponse ViewUserById(int userId);
        CommonResponse UpdateUserPassword(int userId,string password, long usersId = 0);
        CommonResponse DeleteUser(int userId, long usersId = 0);
        CommonResponse ArchiveUser(int userId, long usersId = 0);
        CommonResponse CheckUser(int userId);
        CommonResponse ViewMultipleUserDropDown(long usersId = 0);
        CommonResponse CheckUserName(string userName ,long usersId);
    }
}
