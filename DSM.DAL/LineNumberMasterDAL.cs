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
using static DSM.EntityModels.LineNumberMasterEntity;

namespace DSM.DAL
{
    public class LineNumberMasterDAL : ILineNumberMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LineNumberMasterDAL));
        private readonly AppSettings appSettings;

        public LineNumberMasterDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
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
        public CommonResponse AddAndEditLineNumber(LineNumberCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.LineNumberMaster.Where(m => m.LineNumberId == data.lineNumberId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        LineNumberMaster item = new LineNumberMaster();
                        item.LineNumberName = data.lineNumberName;
                        item.LineNumberDescription = data.lineNumberDescription;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.LineNumberMaster.Add(item);
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
                        res.LineNumberName = data.lineNumberName;
                        res.LineNumberDescription = data.lineNumberDescription;
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
        public CommonResponse ViewMultipleLineNumber()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                string imageUrl = appSettings.ImageUrl;
                var result = (from wf in db.LineNumberMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  lineNumberId = wf.LineNumberId,
                                  lineNumberName = wf.LineNumberName,
                                  lineNumberDescription = wf.LineNumberDescription,
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
        /// <param name="lineNumberId"></param>
        /// <returns></returns>
        public CommonResponse ViewLineNumberById(int lineNumberId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.LineNumberMaster
                              where wf.IsDeleted == false && wf.LineNumberId == lineNumberId
                              select new
                              {
                                  lineNumberId = wf.LineNumberId,
                                  lineNumberName = wf.LineNumberName,
                                  lineNumberDescription = wf.LineNumberDescription,
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
        /// <param name="lineNumberId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteLineNumber(int lineNumberId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.LineNumberMaster.Where(m => m.LineNumberId == lineNumberId).FirstOrDefault();
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
        /// <param name="lineNumberId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveLineNumber(int lineNumberId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.LineNumberMaster.Where(m => m.LineNumberId == lineNumberId).FirstOrDefault();
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
