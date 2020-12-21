using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.LoginEntity;

namespace DSM.Interface
{
    public interface ILogin
    {
        LoginDet ViewLoginDet(string username, string password);
    }
}
