using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class DesignationEntity
    {
        public class DesignationCustom
        {
            public long designationId { get; set; }
            public string designationName { get; set; }
            public string designationDescription { get; set; }
        }
    }
}
