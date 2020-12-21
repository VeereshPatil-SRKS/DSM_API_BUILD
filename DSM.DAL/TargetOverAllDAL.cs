using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.TargetOverAllEntity;

namespace DSM.DAL
{
    public class TargetOverAllDAL : ITargetOverAll
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(TargetOverAllDAL));
        public TargetOverAllDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditTargetOverAll(TargetOverAllCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                DateTime st = DateTime.Now;
                DateTime et = DateTime.Now;

                #region ST and ET
                try
                {
                    st = Convert.ToDateTime(data.targetStartTime);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    et = Convert.ToDateTime(data.targetEndTime);
                }
                catch (Exception ex)
                {

                }
                #endregion


                var res = db.TargetOverall.Where(m => m.TargetId == data.targetId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        TargetOverall item = new TargetOverall();
                        item.TargetYearName = data.targetYearName;
                        item.TargetValue = data.targetValue;
                        item.TargetStartTime = st;
                        item.TargetEndTime = et;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        db.TargetOverall.Add(item);
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
                        res.TargetYearName = data.targetYearName;
                        res.TargetValue = data.targetValue;
                        res.TargetStartTime = st;
                        res.TargetEndTime = et;
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
        public CommonResponse ViewMultipleTargetOverAll(long userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.TargetOverall
                              where wf.IsDeleted == false
                              select new
                              {
                                  targetOverAllId = wf.TargetId,
                                  targetYearName = wf.TargetYearName,
                                  targetValue = wf.TargetValue,
                                  targetStartTime = wf.TargetStartTime,
                                  targetEndTime = wf.TargetEndTime,
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
        /// <param name="TargetOverAllId"></param>
        /// <returns></returns>
        public CommonResponse ViewTargetOverAllById(int targetOverAllId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.TargetOverall
                              where wf.IsDeleted == false
                              select new
                              {
                                  targetOverAllId = wf.TargetId,
                                  targetYearName = wf.TargetYearName,
                                  targetValue = wf.TargetValue,
                                  targetStartTime = wf.TargetStartTime,
                                  targetEndTime = wf.TargetEndTime,
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
        /// <param name="TargetOverAllId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteTargetOverAll(int targetOverAllId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.TargetOverall.Where(m => m.TargetId == targetOverAllId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDeleted = true;
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
        /// <param name="TargetOverAllId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveTargetOverAll(int targetOverAllId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.TargetOverall.Where(m => m.TargetId == targetOverAllId).FirstOrDefault();
                if (result != null)
                {
                    result.IsActive = false;
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
        

    }
}
