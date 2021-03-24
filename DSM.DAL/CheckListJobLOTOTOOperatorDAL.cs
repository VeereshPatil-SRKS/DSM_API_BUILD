using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobLOTOTOOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobLOTOTOOperatorDAL : ICheckListJobLOTOTOOperator
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobLOTOTOOperatorDAL));
        public CheckListJobLOTOTOOperatorDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobLOTOTOOperator(CheckListJobLOTOTOOperatorCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {


                var checklWRT = db.CheckListJobWrtoperator.Where(m => m.CheckListJobWrtoperatorId == data.checkListJobOperatorId).FirstOrDefault();



                var res = db.CheckListJobLototooperator.Where(m => m.checkListJobId == checklWRT.CheckListJobMasterId && m.checkListJobGroupId== checklWRT.CheckListJobGroupId   && m.CheckListJobLototoid == data.checkListJobLOTOTOId).FirstOrDefault();

               


                if (res == null)
                {
                    try
                    {
                        CheckListJobLototooperator item = new CheckListJobLototooperator();
                        item.CheckListJobOperatorId = data.checkListJobOperatorId;
                        item.OperatorId = userId;
                        item.OverAllRemark = data.overAllRemark;
                      //  item.LockOutDoneByOperator = data.lockOutDoneByOperator;
                      //  item.LockOutRemark = data.lockOutRemark;
                      //  item.TagOutDoneByOperator = data.tagOutDoneByOperator;
                       // item.TagOutRemark = data.tagOutRemark;
                        item.TryOutDoneByOperator = data.tryOutDoneByOperator;
                        item.CheckListJobLototoid = data.checkListJobLOTOTOId;
                        item.TryOutRemark = data.tryOutRemark;
                        item.IsJobRejected = false;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobLototooperator.Add(item);
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
                        res.OperatorId = userId;
                        res.OverAllRemark = data.overAllRemark;
                        //res.LockOutDoneByOperator = data.lockOutDoneByOperator;
                        //res.LockOutRemark = data.lockOutRemark;
                        //res.TagOutDoneByOperator = data.tagOutDoneByOperator;
                        //res.TagOutRemark = data.tagOutRemark;
                        res.TryOutDoneByOperator = data.tryOutDoneByOperator;
                        res.CheckListJobLototoid = data.checkListJobLOTOTOId;
                        res.TryOutRemark = data.tryOutRemark;
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
        /// Approve Check List Job LOTOTO Operator
        /// </summary>
        /// <param name="CheckListJobLototooperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ApproveCheckListJobLOTOTOOperator(int CheckListJobLototooperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototooperatorId == CheckListJobLototooperatorId).FirstOrDefault();
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
        /// Remove Check List Job LOTOTO Operator
        /// </summary>
        /// <param name="CheckListJobLototooperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse RejectCheckListJobLOTOTOOperator(int CheckListJobLototooperatorId, string rejectReason, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototooperatorId == CheckListJobLototooperatorId).FirstOrDefault();
                var checkListJobLOTOTOMaster = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == res.CheckListJobLototoid).FirstOrDefault();
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
        /// <param name="CheckListJobLototooperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListJobLOTOTOOperator(int CheckListJobLototooperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototooperatorId == CheckListJobLototooperatorId).FirstOrDefault();
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
        /// <param name="CheckListJobLototooperatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobLOTOTOOperator(int CheckListJobLototooperatorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototooperatorId == CheckListJobLototooperatorId).FirstOrDefault();
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
