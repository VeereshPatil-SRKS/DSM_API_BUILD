using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class GradeMasterEntity
    {
        public class GradeCustom
        {
            public long gradeId { get; set; }
            public string gradeName { get; set; }
            public string gradeCode { get; set; }
            public string gradeDescription { get; set; }
        }
    }
}
