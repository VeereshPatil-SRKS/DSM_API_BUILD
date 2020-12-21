using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobActivityOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobActivityOperatorDAL : ICheckListJobActivityOperator
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobActivityOperatorDAL));
        public CheckListJobActivityOperatorDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobActivityOperator(CheckListJobActivityOperatorCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobActivityOperator.Where(m => m.CheckListJobOperatorId == data.checkListJobOperatorId && m.CheckListJobActivityId == data.checkListJobActivityId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        CheckListJobActivityOperator item = new CheckListJobActivityOperator();
                        item.CheckListJobOperatorId = data.checkListJobOperatorId;
                        item.OperatorId = userId ;
                        item.OperatorScannedBarcodeNumber = data.operatorScannedBarcodeNumber;
                        item.BarcodeAssetId = data.barcodeAssetId ;
                        item.OperatorUpoadedDocumentId = data.operatorUpoadedDocumentId ;
                        item.OperatorRemark = data.operatorRemark ;
                        item.CheckListJobActivityId = data.checkListJobActivityId;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsJobRejected = false;
                        item.IsAdminApproved = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobActivityOperator.Add(item);
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
                        res.OperatorId = userId;
                        res.OperatorScannedBarcodeNumber = data.operatorScannedBarcodeNumber;
                        res.BarcodeAssetId = data.barcodeAssetId;
                        res.OperatorUpoadedDocumentId = data.operatorUpoadedDocumentId;
                        res.OperatorRemark = data.operatorRemark;
                        res.CheckListJobActivityId = data.checkListJobActivityId;
                        res.ActivityEndTime = DateTime.Now;
                        res.IsAdminApproved = false;
                        res.IsJobRejected = false;
                        res.JobRejectedReason = "";
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
        /// CheckList Job Activity Operator Start Activity
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <param name="checkListJobActivityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckListJobActivityOperatorStartActivity(int checkListJobOperatorId,int checkListJobActivityId,string barcodeNumber, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var barcode = db.AssetMaster.Where(m => m.BarcodeAllocatedNumber == barcodeNumber && m.IsDeleted == false).FirstOrDefault();
                if (barcode != null)
                {
                    var checkListActivity = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == checkListJobActivityId && m.AssetId == barcode.AssetId).FirstOrDefault();
                    if (checkListActivity != null)
                    {
                        var res = db.CheckListJobActivityOperator.Where(m => m.CheckListJobOperatorId == checkListJobOperatorId && m.CheckListJobActivityId == checkListJobActivityId).FirstOrDefault();
                        if (res == null)
                        {
                            try
                            {
                                CheckListJobActivityOperator item = new CheckListJobActivityOperator();
                                item.CheckListJobOperatorId = checkListJobOperatorId;
                                item.OperatorScannedBarcodeNumber = barcodeNumber;
                                item.OperatorId = userId;
                                item.CheckListJobActivityId = checkListJobActivityId;
                                item.ActivityStartTime = DateTime.Now;
                                item.IsActive = true;
                                item.IsDeleted = false;
                                item.IsAdminApproved = false;
                                item.CreatedBy = userId;
                                item.CreatedOn = DateTime.Now;
                                db.CheckListJobActivityOperator.Add(item);
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
                                res.OperatorId = userId;
                                res.OperatorScannedBarcodeNumber = barcodeNumber;
                                res.ActivityStartTime = DateTime.Now;
                                res.IsAdminApproved = false;
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
                    else
                    {
                        obj.response = ResourceResponse.BarcodeNotAssignedForThisActivity;
                        obj.isStatus = false;
                    }
                }
                else
                {
                    obj.response = ResourceResponse.BarcodeNotFoundInAssetLibray;
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
        /// Approve Check List Job Activity Operator
        /// </summary>
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ApproveCheckListJobActivityOperator(int checkListJobActivityOperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobActivityOperator.Where(m => m.CheckListJobActivityOperatorId == checkListJobActivityOperatorId).FirstOrDefault();
                if (res != null)
                {
                    res.IsAdminApproved = true;
                    res.IsJobRejected = false;
                    res.ModifiedOn = DateTime.Now;
                    res.ModifiedBy = userId;
                    db.SaveChanges();
                    obj.response = ResourceResponse.ApprovedSucessfully;
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
        /// Remove Check List Job Activity Operator
        /// </summary>
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse RejectCheckListJobActivityOperator(int checkListJobActivityOperatorId, string rejectReason, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobActivityOperator.Where(m => m.CheckListJobActivityOperatorId == checkListJobActivityOperatorId).FirstOrDefault();
                var checkListJobLOTOTOMaster = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == res.CheckListJobActivityId).FirstOrDefault();
                int checkListJobMasterId = Convert.ToInt32(checkListJobLOTOTOMaster.CheckListJobMasterId);
                int checkListJobGroupId = Convert.ToInt32(checkListJobLOTOTOMaster.CheckListJobGroupId);
                var ress = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == checkListJobMasterId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                var checkListJob = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobMasterId).FirstOrDefault();
                if (checkListJob != null)
                {
                    checkListJob.OverAllApproved = false;
                    checkListJob.OverAllRejected = true;
                    db.SaveChanges();
                }
                if (ress != null)
                {
                    ress.IsAdminApproved = false;
                    ress.IsJobRejected = true;
                    ress.IsJobClosed = false;
                    ress.CheckListJobIsCompleted = false;
                    ress.CheckListJobIsPartialCompleted = true;
               
                    ress.JobRejectedReason = rejectReason;
                    ress.ModifiedOn = DateTime.Now;
                    ress.ModifiedBy = userId;
                    db.SaveChanges();
                }
                if (res != null)
                {
                    res.IsAdminApproved = false;
                    res.IsJobRejected = true;
                    res.JobRejectedReason = rejectReason;
                    res.ModifiedOn = DateTime.Now;
                    res.ModifiedBy = userId;
                    db.SaveChanges();
                    obj.response = ResourceResponse.RejectedSucessfully;
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
        /// Delete Document 
        /// </summary>
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListJobActivityOperator(int checkListJobActivityOperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobActivityOperator.Where(m => m.CheckListJobActivityOperatorId == checkListJobActivityOperatorId).FirstOrDefault();
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
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobActivityOperator(int checkListJobActivityOperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobActivityOperator.Where(m => m.CheckListJobActivityOperatorId == checkListJobActivityOperatorId).FirstOrDefault();
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
