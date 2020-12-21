using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class UserManagementEntity
    {
        public class UserDetailsCustom
        {
            public long userId { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public string userFirstName { get; set; }
            public string userLastName { get; set; }
            public string userFullName { get; set; }
            public string emailId { get; set; }
            public string phoneNumber { get; set; }
            public long? roleId { get; set; }
            public long? designationId { get; set; }
            public long? departmentId { get; set; }
            public string reportingManagerName { get; set; }
            public long? reportingManagerId { get; set; }
            public string employeeUniqueCode { get; set; }
            public long documentUploadedId { get; set; }
            
        }
    }
}
