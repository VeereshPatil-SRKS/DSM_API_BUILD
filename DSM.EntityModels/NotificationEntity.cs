using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class NotificationEntity
    {
        public class NotificationCustom
        {
            public string count { get; set; }
            public List<string> detailed { get; set; }
        }
    }
}
