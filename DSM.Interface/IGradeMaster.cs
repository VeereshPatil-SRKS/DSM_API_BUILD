using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.GradeMasterEntity;

namespace DSM.Interface
{
    public interface IGradeMaster
    {
        CommonResponse AddAndEditGrade(GradeCustom data, long userId = 0);
        CommonResponse AddAndEditGradeExcel(List<GradeCustom> datas, long userId = 0);
        CommonResponse ViewMultipleGrade();
        CommonResponse ViewGradeById(int gradeId);
        CommonResponse DeleteGrade(int gradeId, long userId = 0);
        CommonResponse ArchiveGrade(int gradeId, long userId = 0);
        CommonResponse CheckGrade(int gradeId);
    }
}
