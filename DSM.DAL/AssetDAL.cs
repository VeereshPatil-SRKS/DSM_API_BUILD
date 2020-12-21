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
using static DSM.EntityModels.AssetEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class AssetDAL : IAsset
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AssetDAL));
        private readonly AppSettings appSettings;

        public AssetDAL(DSMContext _db,IOptions<AppSettings> _appSettings)
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
        public CommonResponseWithIds AddAndEditAsset(AssetCustom data, long userId = 0)
        {
            CommonResponseWithIds obj = new CommonResponseWithIds();
            try
            {
                var res = db.AssetMaster.Where(m => m.AssetId == data.assetId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        AssetMaster item = new AssetMaster();
                        item.AssetName = data.assetName;
                        item.AssetDescription = data.assetDescription;
                        item.AssetDocumentUploadedId = data.assetDocumentUploadedId ;
                        item.BarcodeAllocatedNumber = data.barcodeAllocatedNumber ;
                        item.LineNumber = data.lineNumber ;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.AssetMaster.Add(item);
                        db.SaveChanges();
                        obj.response = ResourceResponse.AddedSucessfully;
                        obj.isStatus = true;
                        obj.id = item.AssetId;
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
                        res.AssetName = data.assetName;
                        res.AssetDescription = data.assetDescription;
                        res.AssetDocumentUploadedId = data.assetDocumentUploadedId;
                        res.BarcodeAllocatedNumber = data.barcodeAllocatedNumber;
                        res.LineNumber = data.lineNumber;
                        res.ModifiedBy = userId;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        obj.id = res.AssetId;
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
        public CommonResponse ViewMultipleAsset()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                string imageUrl = appSettings.ImageUrl;
                var result = (from wf in db.AssetMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  assetId = wf.AssetId,
                                  assetDisplayName = wf.AssetName + "-"+wf.BarcodeAllocatedNumber,
                                  assetName = wf.AssetName,
                                  assetDescription = wf.AssetDescription,
                                  assetDocumentUploadedId = wf.AssetDocumentUploadedId,
                                  barcodeAllocatedNumber = wf.BarcodeAllocatedNumber,
                                  lineNumber = wf.LineNumber,
                                  lineNumberName =db.LineNumberMaster.Where(m=>m.LineNumberId == wf.LineNumber).Select(m=>m.LineNumberName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  assetDocumentPic = (from wfs in db.DocumentUplodedMaster
                                                where wfs.IsDeleted == false && wfs.DocumentUploaderId == wf.AssetDocumentUploadedId
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
        /// <param name="assetId"></param>
        /// <returns></returns>
        public CommonResponse ViewAssetById(int assetId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.AssetMaster
                              where wf.IsDeleted == false && wf.AssetId == assetId
                              select new
                              {
                                  assetId = wf.AssetId,
                                  assetName = wf.AssetName,
                                  assetDisplayName = wf.AssetName + "-" + wf.BarcodeAllocatedNumber,
                                  assetDescription = wf.AssetDescription,
                                  assetDocumentUploadedId = wf.AssetDocumentUploadedId,
                                  barcodeAllocatedNumber = wf.BarcodeAllocatedNumber,
                                  lineNumber = wf.LineNumber,
                                  lineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.LineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  assetDocumentPic = (from wfs in db.DocumentUplodedMaster
                                                      where wfs.IsDeleted == false && wfs.DocumentUploaderId == wf.AssetDocumentUploadedId
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
        /// Delete Document 
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteAsset(int assetId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.AssetMaster.Where(m => m.AssetId == assetId).FirstOrDefault();
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
        /// <param name="assetId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveAsset(int assetId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.AssetMaster.Where(m => m.AssetId == assetId).FirstOrDefault();
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
        /// <param name="assetId"></param>
        /// <returns></returns>
        public CommonResponse CheckAsset(int assetId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.AssetMaster.Where(m => m.AssetId == assetId && m.IsDeleted == false).Count();
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
        /// GetAssetBarCodeNumber
        /// </summary>
        /// <returns></returns>
        public CommonResponse GetAssetBarCodeNumber()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                CommonFunction generator = new CommonFunction();
                var barcode = generator.RandomNumber();
                obj.response = barcode;
                obj.isStatus = true;
                
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
        /// Check Manual Bar Code Number
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public CommonResponse CheckManualBarCodeNumber(string barCode)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                CommonFunction generator = new CommonFunction();

                var dbCheck = (from wf in db.AssetMaster
                               where wf.IsDeleted == false && wf.BarcodeAllocatedNumber == barCode
                               select wf).ToList();


                if (dbCheck.Count > 0)
                {
                    obj.response = ResourceResponse.BarCodeAlreadyExists;
                    obj.isStatus = false;
                }
                else
                {
                    obj.response = barCode;
                    obj.isStatus = true;
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
        /// Download Bar Code For Asset By Asset Id
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        public CommonResponse DownloadBarCodeForAssetByAssetId(int assetId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var check = db.AssetMaster.Where(m => m.AssetId == assetId).FirstOrDefault();
                if (check != null)
                {
                    BarCodePrinter barCodePrinter = new BarCodePrinter();
                    barCodePrinter.assetName = check.AssetName;
                    barCodePrinter.assetNumber = check.BarcodeAllocatedNumber;
                    barCodePrinter.lineNumber = db.LineNumberMaster.Where(m=>m.LineNumberId==check.LineNumber).Select(m=>m.LineNumberName).FirstOrDefault();
                    CommonFunction commonFunction = new CommonFunction();
                    //obj = commonFunction.ReadXML(barCodePrinter, appSettings);
                    obj = commonFunction.DownloadAllQrCode(false,barCodePrinter, appSettings);
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
        /// Download Bar Code For Asset By Asset All
        /// </summary>
        /// <returns></returns>
        public CommonResponse DownloadBarCodeForAssetByAssetAll()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var check = db.QrCode.FirstOrDefault();
                if (check != null)
                {
                    BarCodePrinter barCodePrinter = new BarCodePrinter();
                    CommonFunction commonFunction = new CommonFunction();
                    obj = commonFunction.DownloadAllQrCode(true,barCodePrinter, appSettings);
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
