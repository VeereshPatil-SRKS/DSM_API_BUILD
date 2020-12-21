using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface INotification
    {
        CommonResponse ViewNotification(int userId);
    }
}
