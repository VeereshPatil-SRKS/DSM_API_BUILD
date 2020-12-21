using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DepartmentEntity;

namespace DSM.DAL
{
    public class DepartmentDAL : IDepartment
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DepartmentDAL));
        public DepartmentDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditDepartment(DepartmentCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.DepartmentMaster.Where(m => m.DepartmentId == data.departmentId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        DepartmentMaster item = new DepartmentMaster();
                        item.DepartmentName = data.departmentName;
                        item.DepartmentDescription = data.departmentDescription;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.DepartmentMaster.Add(item);
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
                        res.DepartmentName = data.departmentName;
                        res.DepartmentDescription = data.departmentDescription;
                        res.ModifiedBy = userId;
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
        public CommonResponse ViewMultipleDepartment()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.DepartmentMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  departmentId = wf.DepartmentId,
                                  departmentName = wf.DepartmentName,
                                  departmentDescription = wf.DepartmentDescription,
                                  isActive = wf.IsActive
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
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public CommonResponse ViewDepartmentById(int departmentId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.DepartmentMaster
                              where wf.IsDeleted == false && wf.DepartmentId == departmentId
                              select new
                              {
                                  departmentId = wf.DepartmentId,
                                  departmentName = wf.DepartmentName,
                                  departmentDescription = wf.DepartmentDescription,
                                  isActive = wf.IsActive
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
        /// Delete Document 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteDepartment(int departmentId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.DepartmentMaster.Where(m => m.DepartmentId == departmentId).FirstOrDefault();
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
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveDepartment(int departmentId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.DepartmentMaster.Where(m => m.DepartmentId == departmentId).FirstOrDefault();
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
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public CommonResponse CheckDepartment(int departmentId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.DepartmentMaster.Where(m => m.DepartmentId == departmentId && m.IsDeleted == false).Count();
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

    }
}
