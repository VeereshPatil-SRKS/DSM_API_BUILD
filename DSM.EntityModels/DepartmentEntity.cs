using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class DepartmentEntity
    {
        public class DepartmentCustom
        {
            public long departmentId { get; set; }
            public string departmentName { get; set; }
            public string departmentDescription { get; set; }
        }
    }
}
