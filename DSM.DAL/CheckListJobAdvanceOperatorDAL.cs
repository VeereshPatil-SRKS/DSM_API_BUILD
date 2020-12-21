using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobAdvanceOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobAdvanceOperatorDAL : ICheckListJobAdvanceOperator
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobAdvanceOperatorDAL));
        public CheckListJobAdvanceOperatorDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobAdvanceOperator(CheckListJobAdvanceOperatorCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobOperatorId == data.checkListJobOperatorId && m.CheckListJobAdvanceId==data.checkListJobAdvanceId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        CheckListJobAdvanceOperator item = new CheckListJobAdvanceOperator();
                        item.CheckListJobOperatorId = data.checkListJobOperatorId;
                        item.CheckListJobAdvanceId = data.checkListJobAdvanceId;
                        item.OperatorId = userId;
                        item.OperatorRemark = data.operatorRemark;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = false;
                        item.IsJobRejected = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobAdvanceOperator.Add(item);
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
                        res.CheckListJobOperatorId = data.checkListJobOperatorId;
                        res.CheckListJobAdvanceId = data.checkListJobAdvanceId;
                        res.OperatorId = userId;
                        res.OperatorRemark = data.operatorRemark;
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
        /// Approve Check List Job Advance Operator
        /// </summary>
        /// <param name="checkListJobAdvanceOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ApproveCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobAdvanceOperatorId == checkListJobAdvanceOperatorId).FirstOrDefault();
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
        /// Remove Check List Job Advance Operator
        /// </summary>
        /// <param name="checkListJobAdvanceOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse RejectCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, string rejectReason, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {

                var res = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobAdvanceOperatorId == checkListJobAdvanceOperatorId).FirstOrDefault();
               var checkListJobLOTOTOMaster = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobAdvanceId == res.CheckListJobAdvanceId).FirstOrDefault();
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
        /// <param name="checkListJobAdvanceOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobAdvanceOperatorId == checkListJobAdvanceOperatorId).FirstOrDefault();
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
        /// <param name="checkListJobAdvanceOperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobAdvanceOperatorId == checkListJobAdvanceOperatorId).FirstOrDefault();
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
