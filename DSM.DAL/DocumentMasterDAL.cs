using DSM.DAL.Helpers;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DocumentMasterEntity;

namespace DSM.DAL
{
    public class DocumentMasterDAL : IDocumentMaster
    {
        DSMContext db = new DSMContext();
        private readonly AppSettings appSettings;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentMasterDAL));
        public DocumentMasterDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditDocumentMaster(DocumentMasterCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.DocumentMaster.Where(m => m.DocumentMasterId == data.documentMasterId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        DocumentMaster item = new DocumentMaster();
                        item.Name = data.name;
                        item.Description = data.description;
                        item.IsActive = true;
                        item.IsDelete = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        item.RecordId = Guid.NewGuid().ToString();
                        db.DocumentMaster.Add(item);
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
                        res.Name = data.name;
                        res.Description = data.description;
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
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleDocumentMaster()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.DocumentMaster
                              where wf.IsDelete == false
                              select new
                              {
                                  documentMasterId = wf.DocumentMasterId,
                                  name = wf.Name,
                                  description = wf.Description,
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
        /// View Document Master by Id
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <returns></returns>
        public CommonResponse ViewDocumentMasterById(int documentMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.DocumentMaster
                              where wf.IsDelete == false && wf.DocumentMasterId == documentMasterId
                              select wf).FirstOrDefault();
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
        /// Delete Document Master
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteDocumentMaster(int documentMasterId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.DocumentMaster.Where(m => m.DocumentMasterId == documentMasterId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDelete = true;
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
        /// Archive Document Master
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveDocumentMaster(int documentMasterId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.DocumentMaster.Where(m => m.DocumentMasterId == documentMasterId).FirstOrDefault();
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
        /// Check Document Master
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <returns></returns>
        public CommonResponse CheckDocumentMaster(int documentMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.DocumentMaster.Where(m => m.DocumentMasterId == documentMasterId && m.IsDelete == false).Count();
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