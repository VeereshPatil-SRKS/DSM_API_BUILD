using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.GradeMasterEntity;

namespace DSM.DAL
{
    public class GradeMasterDAL : IGradeMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(GradeMasterDAL));
        public GradeMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditGrade(GradeCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.GradeMaster.Where(m => m.GradeId == data.gradeId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        GradeMaster item = new GradeMaster();
                        item.GradeName = data.gradeName;
                        item.GradeCode = data.gradeCode;
                        item.GradeDescription = data.gradeDescription;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.GradeMaster.Add(item);
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
                        res.GradeName = data.gradeName;
                        res.GradeCode = data.gradeCode;
                        res.GradeDescription = data.gradeDescription;
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
        /// AddAndEditGradeExcel
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditGradeExcel(List<GradeCustom> datas, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                foreach (var data in datas)
                {
                    var res = db.GradeMaster.Where(m => m.GradeId == data.gradeId).FirstOrDefault();
                    if (res == null)
                    {
                        try
                        {
                            GradeMaster item = new GradeMaster();
                            item.GradeName = data.gradeName;
                            item.GradeCode = data.gradeCode;
                            item.GradeDescription = data.gradeDescription;
                            item.IsActive = true;
                            item.IsDeleted = false;
                            item.CreatedBy = userId;
                            item.CreatedOn = DateTime.Now;
                            db.GradeMaster.Add(item);
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
                            res.GradeName = data.gradeName;
                            res.GradeCode = data.gradeCode;
                            res.GradeDescription = data.gradeDescription;
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
        public CommonResponse ViewMultipleGrade()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.GradeMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  gradeId = wf.GradeId,
                                  gradeName = wf.GradeName,
                                  gradeCode =wf.GradeCode,
                                  gradeDescription = wf.GradeDescription,
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
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public CommonResponse ViewGradeById(int gradeId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.GradeMaster
                              where wf.IsDeleted == false && wf.GradeId == gradeId
                              select new
                              {
                                  gradeId = wf.GradeId,
                                  gradeName = wf.GradeName,
                                  gradeCode = wf.GradeCode,
                                  gradeDescription = wf.GradeDescription,
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
        /// <param name="gradeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteGrade(int gradeId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.GradeMaster.Where(m => m.GradeId == gradeId).FirstOrDefault();
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
        /// <param name="gradeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveGrade(int gradeId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.GradeMaster.Where(m => m.GradeId == gradeId).FirstOrDefault();
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
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public CommonResponse CheckGrade(int gradeId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.GradeMaster.Where(m => m.GradeId == gradeId && m.IsDeleted == false).Count();
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
