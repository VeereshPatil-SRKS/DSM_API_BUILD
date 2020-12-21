using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class ColorMasterEntity
    {
        public class ColorCustom
        {
            public long colorId { get; set; }
            public string colorName { get; set; }
            public string colorCode { get; set; }
            public string colorDescription { get; set; }
        }
    }
}
