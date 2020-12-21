using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class LoginEntity
    {
        public class LoginDet
        {
           public long userId { get; set; }
            public string userName { get; set; }
            public string userFirstName { get; set; }
            public string userLastName { get; set; }
            public string userFullName { get; set; }
            public long roleId { get; set; }
            public string roleName { get; set; }
            public long designationId { get; set; }
            public string designationName { get; set; }
            public long departmentId { get; set; }
            public string departmentName { get; set; }
            public long reportingManagerId { get; set; }
            public string reportingManagerName { get; set; }
            public string emailId { get; set; }
            public string phoneNumber { get; set; }
            public string employeeUniqueCode { get; set; }
            public string profilePic { get; set; }
            public bool isStatus { get; set; }
        }
    }
}
