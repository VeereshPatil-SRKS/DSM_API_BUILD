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
using static DSM.EntityModels.ShiftMasterEntity;

namespace DSM.DAL
{
    public class ShiftMasterDAL : IShiftMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ShiftMasterDAL));
        private readonly AppSettings appSettings;

        public ShiftMasterDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
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
        public CommonResponse AddAndEditShift(ShiftCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.ShiftMaster.Where(m => m.ShiftId == data.shiftId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        ShiftMaster item = new ShiftMaster();
                        item.ShiftName = data.shiftName;
                        item.ShiftDescription = data.shiftDescription;
                        item.ShiftStartTimings = data.shiftStartTimings;
                        item.ShiftEndTimings = data.shiftEndTimings;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.ShiftMaster.Add(item);
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
                        res.ShiftName = data.shiftName;
                        res.ShiftDescription = data.shiftDescription;
                        res.ShiftStartTimings = data.shiftStartTimings;
                        res.ShiftEndTimings = data.shiftEndTimings;
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
        public CommonResponse ViewMultipleShift()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                string imageUrl = appSettings.ImageUrl;
                var result = (from wf in db.ShiftMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  shiftId = wf.ShiftId,
                                  shiftName = wf.ShiftName,
                                  shiftDescription = wf.ShiftDescription,
                                  shiftStartTimings = wf.ShiftStartTimings,
                                  shiftEndTimings = wf.ShiftEndTimings,
                                  isActive = wf.IsActive,
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
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public CommonResponse ViewShiftById(int shiftId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.ShiftMaster
                              where wf.IsDeleted == false && wf.ShiftId == shiftId
                              select new
                              {
                                  shiftId = wf.ShiftId,
                                  shiftName = wf.ShiftName,
                                  shiftDescription = wf.ShiftDescription,
                                  shiftStartTimings = wf.ShiftStartTimings,
                                  shiftEndTimings = wf.ShiftEndTimings,
                                  isActive = wf.IsActive,
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
        /// <param name="shiftId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteShift(int shiftId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.ShiftMaster.Where(m => m.ShiftId == shiftId).FirstOrDefault();
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
        /// <param name="shiftId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveShift(int shiftId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.ShiftMaster.Where(m => m.ShiftId == shiftId).FirstOrDefault();
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
        
    }
}
