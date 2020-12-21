using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class RoleEntity
    {
        public class RoleCustom
        {
            public long roleId { get; set; }
            public string roleName { get; set; }
            public string roleDescription { get; set; }
        }
    }
}
