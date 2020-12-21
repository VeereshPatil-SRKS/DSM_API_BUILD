using DSM.DAL.App_Start;
using DSM.DAL.Helpers;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.UserManagementEntity;

namespace DSM.DAL
{
    public class UserManagementDAL : IUserManagement
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserManagementDAL));
        private readonly AppSettings appSettings;

        public UserManagementDAL(DSMContext _db,IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditUser(UserDetailsCustom data, long usersId = 0)
        {
            CommonResponse obj = new CommonResponse();
            Security security = new Security();
            string passwordEncrypt = security.Encrypt(data.password);
            try
            {
                var res = db.UserDetails.Where(m => m.UserId == data.userId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        UserDetails item = new UserDetails();
                        item.UserName = data.userName;
                        item.Password = passwordEncrypt;
                        item.UserFirstName = data.userFirstName;
                        item.UserLastName = data.userLastName;
                        item.UserFullName = data.userFullName;
                        item.EmailId = data.emailId;
                        item.PhoneNumber = data.phoneNumber;
                        item.RoleId = data.roleId;
                        item.DesignationId = data.designationId;
                        item.DepartmentId = data.departmentId;
                        item.ReportingManagerName = data.reportingManagerName;
                        item.ReportManagerId = data.reportingManagerId;
                        item.EmployeeUniqueCode = data.employeeUniqueCode;
                        item.DocumentUploadedId = data.documentUploadedId;
                        item.IsActive = true;
                        item.IsAdminApproved = true;
                        item.IsDeleted = false;
                        item.CreatedBy = usersId;
                        item.CreatedOn = DateTime.Now;
                        db.UserDetails.Add(item);
                        db.SaveChanges();
                        obj.response = ResourceResponse.AddedSucessfully;
                        obj.isStatus = true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                        obj.response = ResourceResponse.ExceptionMessage;
                        obj.isStatus = false;
                    }
                }
                else
                {
                    try
                    {
                        res.UserName = data.userName;
                        //res.Password = passwordEncrypt;
                        res.UserFirstName = data.userFirstName;
                        res.UserLastName = data.userLastName;
                        res.UserFullName = data.userFullName;
                        res.EmailId = data.emailId;
                        res.PhoneNumber = data.phoneNumber;
                        res.RoleId = data.roleId;
                        res.DesignationId = data.designationId;
                        res.DepartmentId = data.departmentId;
                        res.ReportingManagerName = data.reportingManagerName;
                        res.ReportManagerId = data.reportingManagerId;
                        res.EmployeeUniqueCode = data.employeeUniqueCode;
                        res.DocumentUploadedId = data.documentUploadedId;
                        res.ModifiedBy = usersId;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        obj.response = ResourceResponse.UpdatedSucessfully;
                        obj.isStatus = true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                        obj.response = ResourceResponse.ExceptionMessage;
                        obj.isStatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Multiple Document 
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleUser(long usersId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var item = db.UserDetails.Where(m => m.UserId == usersId).FirstOrDefault();
                int roleId = 0;

                roleId = Convert.ToInt32(item.RoleId);


                
                var result = (from wf in db.UserDetails
                              where wf.IsDeleted == false
                              select new
                              {
                                  userId = wf.UserId,
                                  userName = wf.UserName,
                                  userFirstName = wf.UserFirstName,
                                  userLastName = wf.UserLastName,
                                  userFullName = wf.UserFullName,
                                  emailId = wf.EmailId,
                                  phoneNumber = wf.PhoneNumber,
                                  roleId = wf.RoleId,
                                  roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleName).FirstOrDefault(),
                                  designationId = wf.DesignationId,
                                  designationName = db.DesignationMaster.Where(m => m.IsDeleted == false && m.DesignationId == wf.DesignationId).Select(m => m.DesignationName).FirstOrDefault(),
                                  departmentId = wf.DepartmentId,
                                  departmentName = db.DepartmentMaster.Where(m=> m.IsDeleted == false && m.DepartmentId == wf.DepartmentId).Select(m=> m.DepartmentName).FirstOrDefault(),
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
                              }).ToList();

                if (roleId == 2)
                {
                    result = result.Where(m => m.roleId == 2 || m.roleId == 3).ToList();
                }
               
                if (result.Count() != 0)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Multiple User Drop Down
        /// </summary>
        /// <param name="usersId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleUserDropDown(long usersId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.UserDetails
                              where wf.IsDeleted == false && (wf.RoleId==2 || wf.RoleId ==3)
                              select new
                              {
                                  userId = wf.UserId,
                                  userName = wf.UserName,
                                  userFirstName = wf.UserFirstName,
                                  userLastName = wf.UserLastName,
                                  userFullName = wf.UserFullName,
                                  emailId = wf.EmailId,
                                  phoneNumber = wf.PhoneNumber,
                                  roleId = wf.RoleId,
                                  roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleName).FirstOrDefault(),
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
                              }).ToList();

              
                if (result.Count() != 0)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Multiple User For Operator
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleUserForOperator()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.UserDetails
                              where wf.IsDeleted == false && wf.RoleId==3
                              select new
                              {
                                  userId = wf.UserId,
                                  userName = wf.UserName,
                                  userFirstName = wf.UserFirstName,
                                  userLastName = wf.UserLastName,
                                  userFullName = wf.UserFullName,
                                  emailId = wf.EmailId,
                                  phoneNumber = wf.PhoneNumber,
                                  roleId = wf.RoleId,
                                  roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleName).FirstOrDefault(),
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
                              }).ToList();
                if (result.Count() != 0)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Multiple User For Operator
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleUserForAdmin()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.UserDetails
                              where wf.IsDeleted == false && wf.RoleId == 2 
                              select new
                              {
                                  userId = wf.UserId,
                                  userName = wf.UserName,
                                  userFirstName = wf.UserFirstName,
                                  userLastName = wf.UserLastName,
                                  userFullName = wf.UserFullName,
                                  emailId = wf.EmailId,
                                  phoneNumber = wf.PhoneNumber,
                                  roleId = wf.RoleId,
                                  roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleName).FirstOrDefault(),
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
                              }).ToList();
                if (result.Count() != 0)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Multiple User For SuperAdmin
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleUserForSuperAdmin()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.UserDetails
                              where wf.IsDeleted == false && wf.RoleId == 1
                              select new
                              {
                                  userId = wf.UserId,
                                  userName = wf.UserName,
                                  userFirstName = wf.UserFirstName,
                                  userLastName = wf.UserLastName,
                                  userFullName = wf.UserFullName,
                                  emailId = wf.EmailId,
                                  phoneNumber = wf.PhoneNumber,
                                  roleId = wf.RoleId,
                                  roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleName).FirstOrDefault(),
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
                              }).ToList();
                if (result.Count() != 0)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Document  by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ViewUserById(int userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.UserDetails
                              where wf.IsDeleted == false && wf.UserId == userId
                              select new
                              {
                                  userId = wf.UserId,
                                  userName = wf.UserName,
                                  userFirstName = wf.UserFirstName,
                                  userLastName = wf.UserLastName,
                                  userFullName = wf.UserFullName,
                                  emailId = wf.EmailId,
                                  phoneNumber = wf.PhoneNumber,
                                  roleId = wf.RoleId,
                                  roleName = db.RoleMaster.Where(m => m.IsDeleted == false && m.RoleId == wf.RoleId).Select(m => m.RoleId).FirstOrDefault(),
                                  designationId = wf.DesignationId,
                                  designationName = db.DesignationMaster.Where(m => m.IsDeleted == false && m.DesignationId == wf.DesignationId).Select(m => m.DesignationName).FirstOrDefault(),
                                  departmentId = wf.DepartmentId,
                                  departmentName = db.DepartmentMaster.Where(m => m.IsDeleted == false && m.DepartmentId == wf.DepartmentId).Select(m => m.DepartmentName).FirstOrDefault(),
                                  reportingManagerName = wf.ReportingManagerName,
                                  reportManagerId = wf.ReportManagerId,
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
                if (result != null)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary> 
        /// Update User Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="usersId"></param>
        /// <returns></returns>
        public CommonResponse UpdateUserPassword(int userId,string password,long usersId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.UserDetails.Where(m=>m.UserId == userId).FirstOrDefault();
                if (result != null)
                {
                    Security security = new Security();
                    string passwordEncrypt = security.Encrypt(password);
                    result.Password = passwordEncrypt;
                    result.ModifiedOn = DateTime.Now;
                    result.ModifiedBy = usersId;
                    db.SaveChanges();
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Delete Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteUser(int userId, long usersId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDeleted = true;
                    res.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    obj.response = ResourceResponse.DeletedSucessfully;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.FailureMessage;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Archive Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveUser(int userId, long usersId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
                if (result != null)
                {
                    result.IsActive = false;
                    result.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    obj.response = ResourceResponse.DeletedSucessfully;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.FailureMessage;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Check Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckUser(int userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.UserDetails.Where(m => m.UserId == userId && m.IsDeleted == false).Count();
                if (result > 0)
                {
                    obj.isStatus = true;
                    obj.response = "Are You sure you want to Delete this Record?";
                }
                else
                {
                    obj.response = "This Record is associated with other data and cannot be deleted and can be Archieved";
                }

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Check User Name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckUserName(string userName,long usersId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.UserDetails.Where(m => m.UserName == userName && m.IsDeleted == false).Count();
                if (usersId != 0)
                {
                    result = db.UserDetails.Where(m => m.UserName == userName && m.UserId!=usersId && m.IsDeleted == false).Count();
                }
                if (result == 0)
                {
                    obj.isStatus = true;
                    obj.response = ResourceResponse.UserDoesNotExists;
                }
                else
                {
                    obj.isStatus = false;
                    obj.response = ResourceResponse.UserAlreadyExists;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

    }
}
