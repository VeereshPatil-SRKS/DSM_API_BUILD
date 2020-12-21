using DSM.DBModels;
using DSM.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.LoginEntity;
using Microsoft.Extensions.Options;
using DSM.DAL.Helpers;

namespace DSM.DAL
{
    public class LoginDAL : ILogin
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LoginDAL));
        private readonly AppSettings appSettings;
        public LoginDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        /// <summary>
        /// View Login Details
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginDet ViewLoginDet(string userName, string password)
        {
            LoginDet obj = new LoginDet();
            try
            {
                var check = (from wf in db.UserDetails
                             where wf.IsDeleted == false && wf.IsActive == true && wf.IsAdminApproved == true && wf.UserName == userName && wf.Password == password
                             select new
                             {
                                 userId = wf.UserId,
                                 userName = wf.UserName,
                                 userFirstName = wf.UserFirstName,
                                 userLastName = wf.UserLastName,
                                 userFullName = wf.UserFullName,
                                 roleId = wf.RoleId,
                                 roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleName).FirstOrDefault(),
                                 emailId = wf.EmailId,
                                 phoneNumber = wf.PhoneNumber,
                                 designationId = wf.DesignationId,
                                 designationName = db.DesignationMaster.Where(m => m.IsDeleted == false && m.DesignationId == wf.DesignationId).Select(m => m.DesignationName).FirstOrDefault(),
                                 departmentId = wf.DepartmentId,
                                 departmentName = db.DepartmentMaster.Where(m => m.IsDeleted == false && m.DepartmentId == wf.DepartmentId).Select(m => m.DepartmentName).FirstOrDefault(),
                                 reportingManagerName = wf.ReportingManagerName,
                                 reportingManagerId = wf.ReportManagerId,
                                 employeeUniqueCode = wf.EmployeeUniqueCode,
                                 profilePic = (from wfs in db.DocumentUplodedMaster
                                               where wfs.IsDeleted == false && wfs.DocumentUploaderId == wf.DocumentUploadedId
                                               select new
                                               {
                                                   documentUploaderId = wfs.DocumentUploaderId,
                                                   documentName = wfs.DocumentName,
                                                   documentMasterId = wfs.DocumentMasterId,
                                                   fileName = appSettings.ImageUrl + wfs.FileName,
                                                   filePath = wfs.FilePath,
                                                   documentUploadedFor = wfs.DocumentUploadedFor
                                               }).FirstOrDefault()
                             }).FirstOrDefault();
                if (check != null)
                {
                    obj.userName = check.userName;
                    obj.userId = check.userId;
                    obj.userName = check.userName;
                    obj.userFirstName = check.userFirstName;
                    obj.userLastName = check.userLastName;
                    obj.userFullName = check.userFullName;
                    obj.roleId = Convert.ToInt32(check.roleId);
                    obj.roleName = check.roleName;
                    obj.departmentId = Convert.ToInt32(check.departmentId);
                    obj.departmentName = check.departmentName;
                    obj.designationId = Convert.ToInt32(check.designationId);
                    obj.designationName = check.designationName;
                    obj.reportingManagerId = Convert.ToInt32(check.reportingManagerId);
                    obj.reportingManagerName = check.reportingManagerName;
                    obj.employeeUniqueCode = check.employeeUniqueCode;
                    obj.emailId = check.emailId;
                    obj.phoneNumber = check.phoneNumber;
                    if (check.profilePic != null)
                    {
                        obj.profilePic = check.profilePic.fileName;
                    }
                    obj.isStatus = true;
                }
                else
                {
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.isStatus = false;
            }

            return obj;
        }
    }
}
