using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class DocumentMasterEntity
    {
        public class DocumentMasterCustom
        {
            public int documentMasterId { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }
    }
}
