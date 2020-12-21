using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class UserDetails
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFullName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAdminApproved { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public long? RoleId { get; set; }
        public long? DesignationId { get; set; }
        public long? DepartmentId { get; set; }
        public string ReportingManagerName { get; set; }
        public long? ReportManagerId { get; set; }
        public string EmployeeUniqueCode { get; set; }
        public long? DocumentUploadedId { get; set; }
    }
}
