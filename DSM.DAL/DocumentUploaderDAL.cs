using DSM.DAL.Helpers;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DocumentUploaderEntity;

namespace DSM.DAL
{
    public class DocumentUploaderDAL : IDocumentUploader
    {
        DSMContext db = new DSMContext();
        private readonly AppSettings appSettings;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentUploaderDAL));
        public DocumentUploaderDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        /// <summary>
        /// Add And Edit Document Uploader
        /// </summary>
        /// <param name="documentDetails"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponseWithIdsDoc AddAndEditDocumentUploader(DocumentUplodedMasterCustom documentDetails, long userId)
        {
            CommonResponseWithIdsDoc response = new CommonResponseWithIdsDoc();

            string fileName = "";

            if (documentDetails.Image != null)
            {
                try
                {
                    string extensionFile = documentDetails.Image.FileName;
                    string[] fileArray = extensionFile.Split('.');
                    try
                    {
                        extensionFile = fileArray[1];
                    }
                    catch
                    {
                        extensionFile = "jpeg";
                    }
                    fileName = Guid.NewGuid().ToString() + "." + extensionFile;


                    #region save file

                    var path = Path.Combine(appSettings.ImageUrlSave, fileName);
                    bool exists = System.IO.File.Exists(path);
                    // Getting Image
                    var image = documentDetails.Image;
                    
                    try
                    {

                        if (!exists)
                        {
                            if (image.Length > 0)
                            {
                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    image.CopyTo(fileStream);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                    }


                    #endregion

                    #region save file details in DB

                    var dbItem = db.DocumentUplodedMaster.Where(m => m.DocumentUploaderId == documentDetails.documentUploaderId && m.IsDeleted == false && m.IsActive == true).FirstOrDefault();
                    if (dbItem == null)
                    {
                        #region insert item into DB
                        DocumentUplodedMaster documentUplodedMaster = new DocumentUplodedMaster();
                        documentUplodedMaster.DocumentName = documentDetails.Image.FileName;
                        documentUplodedMaster.DocumentMasterId = documentDetails.documentMasterId;
                        documentUplodedMaster.FileName = fileName;
                        documentUplodedMaster.FilePath = path;
                        documentUplodedMaster.CreatedOn = DateTime.Now;
                        documentUplodedMaster.CreatedBy = 0;
                        documentUplodedMaster.IsDeleted = false;
                        documentUplodedMaster.IsActive = true;
                        documentUplodedMaster.DocumentUploadedFor = documentDetails.DocumentUploadedFor;
                        db.DocumentUplodedMaster.Add(documentUplodedMaster);
                        db.SaveChanges();
                        response.id = documentUplodedMaster.DocumentUploaderId;
                        var doc = appSettings.ImageUrl +
                                  (from wfd in db.DocumentUplodedMaster
                                   where wfd.IsDeleted == false && wfd.DocumentUploaderId == documentUplodedMaster.DocumentUploaderId
                                   select wfd.FileName).FirstOrDefault();
                        response.url = doc;

                        #endregion
                    }
                    else
                    {
                        #region delete old files
                        try
                        {
                            var deleteFileName = Path.Combine(appSettings.ImageUrlSave, dbItem.FileName);
                            if (deleteFileName != null || deleteFileName != string.Empty)
                            {
                                if ((System.IO.File.Exists(deleteFileName)))
                                {
                                    System.IO.File.Delete(deleteFileName);
                                }

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion

                        #region update into DB
                        dbItem.FileName = fileName;
                        dbItem.DocumentName = documentDetails.Image.FileName;
                        dbItem.DocumentUploadedFor = documentDetails.DocumentUploadedFor;
                        dbItem.FilePath = path;
                        dbItem.ModifiedOn = DateTime.Now;
                        dbItem.ModifiedBy = 0;
                        db.SaveChanges();
                        response.id = documentDetails.documentUploaderId;
                        var doc = appSettings.ImageUrl +
                                (from wfd in db.DocumentUplodedMaster
                                 where wfd.IsDeleted == false && wfd.DocumentUploaderId == dbItem.DocumentUploaderId
                                 select wfd.FileName).FirstOrDefault();
                        response.url = doc;
                        #endregion
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                    response.isStatus = false;
                    response.response = ResourceResponse.ExceptionMessage;
                }
                response.isStatus = true;
                response.response = ResourceResponse.FileUploaderSuccess;
            }
            else
            {
                response.isStatus = false;
                response.response = ResourceResponse.FileUploaderFailure;
            }
            return response;
        }

        /// <summary>
        /// Add And Edit Document Uploader Base64
        /// </summary>
        /// <param name="documentDetails"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponseWithIdsDoc AddAndEditDocumentUploaderBase64(DocumentUplodedMasterCustomBase64 documentDetails, long userId)
        {
            CommonResponseWithIdsDoc response = new CommonResponseWithIdsDoc();

            string fileName = "";

            if (documentDetails.base64Image != null)
            {
                try
                {
                    string extensionFile = documentDetails.uploadedFileName;
                    string[] fileArray = extensionFile.Split('.');
                    try
                    {
                        extensionFile = fileArray[1];
                    }
                    catch
                    {
                        extensionFile = "jpeg";
                    }
                    fileName = Guid.NewGuid().ToString() + "." + extensionFile;


                    #region save file

                    var path = Path.Combine(appSettings.ImageUrlSave, fileName);
                    bool exists = System.IO.File.Exists(path);
                    // Getting Image
                    var image = documentDetails.base64Image.Split(',');

                  

                    try
                    {

                        if (!exists)
                        {
                            if (image[1].Length > 0)
                            {
                                byte[] imgByteArray = Convert.FromBase64String(image[1]);
                                File.WriteAllBytes(path, imgByteArray);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                    }


                    #endregion

                    #region save file details in DB

                    var dbItem = db.DocumentUplodedMaster.Where(m => m.DocumentUploaderId == documentDetails.documentUploaderId && m.IsDeleted == false && m.IsActive == true).FirstOrDefault();
                    if (dbItem == null)
                    {
                        #region insert item into DB
                        DocumentUplodedMaster documentUplodedMaster = new DocumentUplodedMaster();
                        documentUplodedMaster.DocumentName = documentDetails.uploadedFileName;
                        documentUplodedMaster.DocumentMasterId = documentDetails.documentMasterId;
                        documentUplodedMaster.FileName = fileName;
                        documentUplodedMaster.FilePath = path;
                        documentUplodedMaster.CreatedOn = DateTime.Now;
                        documentUplodedMaster.CreatedBy = 0;
                        documentUplodedMaster.IsDeleted = false;
                        documentUplodedMaster.IsActive = true;
                        documentUplodedMaster.DocumentUploadedFor = documentDetails.DocumentUploadedFor;
                        db.DocumentUplodedMaster.Add(documentUplodedMaster);
                        db.SaveChanges();
                        response.id = documentUplodedMaster.DocumentUploaderId;
                        var doc = appSettings.ImageUrl +
                                 (from wfd in db.DocumentUplodedMaster
                                  where wfd.IsDeleted == false && wfd.DocumentUploaderId == documentUplodedMaster.DocumentUploaderId
                                  select wfd.FileName).FirstOrDefault();
                        response.url = doc;
                        #endregion
                    }
                    else
                    {
                        #region delete old files
                        try
                        {
                            var deleteFileName = Path.Combine(appSettings.ImageUrlSave, dbItem.FileName);
                            if (deleteFileName != null || deleteFileName != string.Empty)
                            {
                                if ((System.IO.File.Exists(deleteFileName)))
                                {
                                    System.IO.File.Delete(deleteFileName);
                                }

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion

                        #region update into DB
                        dbItem.FileName = fileName;
                        dbItem.DocumentName = documentDetails.uploadedFileName;
                        dbItem.DocumentUploadedFor = documentDetails.DocumentUploadedFor;
                        dbItem.FilePath = path;
                        dbItem.ModifiedOn = DateTime.Now;
                        dbItem.ModifiedBy = 0;
                        db.SaveChanges();
                        response.id = documentDetails.documentUploaderId;
                        var doc = appSettings.ImageUrl +
                                 (from wfd in db.DocumentUplodedMaster
                                  where wfd.IsDeleted == false && wfd.DocumentUploaderId == documentDetails.documentUploaderId
                                  select wfd.FileName).FirstOrDefault();
                        response.url = doc;
                        #endregion
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                    response.isStatus = false;
                    response.response = ResourceResponse.ExceptionMessage;
                }
                response.isStatus = true;
                response.response = ResourceResponse.FileUploaderSuccess;
            }
            else
            {
                response.isStatus = false;
                response.response = ResourceResponse.FileUploaderFailure;
            }
            return response;
        }
        
        /// <summary>
        /// View Document Uploaded By Id
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <returns></returns>
        public CommonResponse ViewDocumentUploadedById(long DocumentUploaderId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.DocumentUplodedMaster
                              where wf.IsDeleted == false && wf.DocumentUploaderId == DocumentUploaderId
                              select new
                              {
                                 documentUploaderId = wf.DocumentUploaderId,
                                 documentName = wf.DocumentName,
                                 documentMasterId = wf.DocumentMasterId,
                                 fileName = appSettings.ImageUrl+ wf.FileName,
                                 filePath = wf.FilePath,
                                 documentUploadedFor = wf.DocumentUploadedFor
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

    }
}
