using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobLOTOTOMaster;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobLOTOTOMasterDAL : ICheckListJobLOTOTOMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobLOTOTOMasterDAL));
        public CheckListJobLOTOTOMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobLOTOTO(CheckListJobLOTOTOCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == data.checkListJobLOTOTOId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        var stepCount = commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(data.checkListJobMasterId, data.checkListJobGroupId, "LOTOTO", "Job");

                        CheckListJobLototomaster item = new CheckListJobLototomaster();
                        item.CheckListJobMasterId = data.checkListJobMasterId;
                        item.CheckListJobGroupId = data.checkListJobGroupId;
                        item.CheckListJobLockStepNumber = stepCount + 1;
                        item.PositionDescription = data.positionDescription;
                        item.IsLockOutRequired = data.isLockOutRequired;
                        item.IsTagOutRequired = data.isTagOutRequired;
                        item.IsTryOutRequired = data.isTryOutRequired;
                        item.Remarks = data.remarks;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = true;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobLototomaster.Add(item);
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
                        res.CheckListJobMasterId = data.checkListJobMasterId;
                        res.CheckListJobGroupId = data.checkListJobGroupId;
                        res.CheckListJobLockStepNumber = data.checkListJobLockStepNumber;
                        res.PositionDescription = data.positionDescription;
                        res.IsLockOutRequired = data.isLockOutRequired;
                        res.IsTagOutRequired = data.isTagOutRequired;
                        res.IsTryOutRequired = data.isTryOutRequired;
                        res.Remarks = data.remarks;
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
        public CommonResponse ViewMultipleCheckListJobLOTOTO()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobLototomaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListJobLOTOTOId = wf.CheckListJobLototoid,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobLockStepNumber = wf.CheckListJobLockStepNumber,
                                  positionDescription = wf.PositionDescription,
                                  isLockOutRequired = wf.IsLockOutRequired,
                                  isTagOutRequired = wf.IsTagOutRequired,
                                  isTryOutRequired = wf.IsTryOutRequired,
                                  remarks = wf.Remarks,
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
        /// View Check ListJob LOTOTO By Check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobLOTOTOByCheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobLototomaster
                              where wf.IsDeleted == false && wf.CheckListJobMasterId == checkListJobMasterId && wf.CheckListJobGroupId == checkListJobGroupId
                              select new
                              {
                                  checkListJobLOTOTOId = wf.CheckListJobLototoid,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobLockStepNumber = wf.CheckListJobLockStepNumber,
                                  positionDescription = wf.PositionDescription,
                                  isLockOutRequired = wf.IsLockOutRequired,
                                  isTagOutRequired = wf.IsTagOutRequired,
                                  isTryOutRequired = wf.IsTryOutRequired,
                                  remarks = wf.Remarks,
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
        /// <param name="checkListJobLOTOTOId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobLOTOTOById(int checkListJobLOTOTOId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobLototomaster
                              where wf.IsDeleted == false && wf.CheckListJobLototoid == checkListJobLOTOTOId
                              select new
                              {
                                  checkListJobLOTOTOId = wf.CheckListJobLototoid,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobLockStepNumber = wf.CheckListJobLockStepNumber,
                                  positionDescription = wf.PositionDescription,
                                  isLockOutRequired = wf.IsLockOutRequired,
                                  isTagOutRequired = wf.IsTagOutRequired,
                                  isTryOutRequired = wf.IsTryOutRequired,
                                  remarks = wf.Remarks,
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
        /// <param name="checkListJobLOTOTOId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public CommonResponse DeleteCheckListJobLOTOTO(int checkListJobLOTOTOId, long userId = 0)
        //{
        //    CommonResponse obj = new CommonResponse();
        //    CommonFunction commonFunction = new CommonFunction();
        //    try
        //    {
        //        var res = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == checkListJobLOTOTOId).FirstOrDefault();
        //        if (res != null)
        //        {
        //            res.IsDeleted = true;
        //            res.ModifiedOn = DateTime.Now;
        //            db.SaveChanges();

        //            commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListJobMasterId, res.CheckListJobGroupId, "LOTOTO", "Job");

        //            obj.response = ResourceResponse.DeletedSucessfully;
        //            obj.isStatus = true;
        //        }
        //        else
        //        {
        //            obj.response = ResourceResponse.FailureMessage;
        //            obj.isStatus = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //        obj.response = ResourceResponse.ExceptionMessage;
        //        obj.isStatus = false;
        //    }
        //    return obj;
        //}


          public CommonResponse DeleteCheckListJobLOTOTO(string checkListJobLOTOTOId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                string[] deletIds = checkListJobLOTOTOId.Split(',');
                List<string> listIds = new List<string>();
                listIds = deletIds.ToList();
                foreach (var i in listIds)
                {
                    int idds = Convert.ToInt32(i);
                    var res = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == idds).FirstOrDefault();
                    if (res != null)
                    {
                        res.IsDeleted = true;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();

                        commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListJobMasterId, res.CheckListJobGroupId, "LOTOTO", "Job");

                        obj.response = ResourceResponse.DeletedSucessfully;
                        obj.isStatus = true;
                    }
                }
                //else
                //{
                //    obj.response = ResourceResponse.FailureMessage;
                //    obj.isStatus = false;
                //}
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
        /// <param name="checkListJobLOTOTOId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobLOTOTO(int checkListJobLOTOTOId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == checkListJobLOTOTOId).FirstOrDefault();
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
        /// <param name="checkListJobLOTOTOId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListJobLOTOTO(int checkListJobLOTOTOId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == checkListJobLOTOTOId && m.IsDeleted == false).Count();
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
