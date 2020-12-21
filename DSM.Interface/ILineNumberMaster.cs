using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.LineNumberMasterEntity;

namespace DSM.Interface
{
    public interface ILineNumberMaster
    {
        CommonResponse AddAndEditLineNumber(LineNumberCustom data, long userId = 0);
        CommonResponse ViewMultipleLineNumber();
        CommonResponse ViewLineNumberById(int lineNumberId);
        CommonResponse DeleteLineNumber(int lineNumberId, long userId = 0);
        CommonResponse ArchiveLineNumber(int lineNumberId, long userId = 0);
    }
}
