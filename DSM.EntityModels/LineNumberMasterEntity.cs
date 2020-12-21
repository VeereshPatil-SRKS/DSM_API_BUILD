using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class LineNumberMasterEntity
    {
        public class LineNumberCustom
        {
            public long lineNumberId { get; set; }
            public string lineNumberName { get; set; }
            public string lineNumberDescription { get; set; }
        }
    }
}
