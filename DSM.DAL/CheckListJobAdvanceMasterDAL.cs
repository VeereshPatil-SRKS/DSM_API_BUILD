using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobAdvanceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobAdvanceMasterDAL : ICheckListJobAdvanceMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobAdvanceMasterDAL));
        public CheckListJobAdvanceMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobAdvance(CheckListJobAdvanceCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobAdvanceId == data.checkListJobAdvanceId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        var stepCount = commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(data.checkListJobMasterId, data.checkListJobGroupId, "Advance", "Job");

                        CheckListJobAdvanceMaster item = new CheckListJobAdvanceMaster();
                        item.CheckListJobMasterId = data.checkListJobMasterId;
                        item.CheckListJobGroupId = data.checkListJobGroupId;
                        item.CheckListJobStepNumber = stepCount + 1;
                        item.ActivityBeforeChangeOverDescription = data.activityBeforeChangeOverDescription;
                        item.Remarks = data.remarks;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = true;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobAdvanceMaster.Add(item);
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
                        res.CheckListJobStepNumber = data.checkListJobStepNumber;
                        res.ActivityBeforeChangeOverDescription = data.activityBeforeChangeOverDescription;
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
        public CommonResponse ViewMultipleCheckListJobAdvance()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobAdvanceMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListJobAdvanceId = wf.CheckListJobAdvanceId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobStepNumber = wf.CheckListJobStepNumber,
                                  activityBeforeChangeOverDescription = wf.ActivityBeforeChangeOverDescription,
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
        /// View Check ListJob Advance By check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobAdvanceByCheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobAdvanceMaster
                              where wf.IsDeleted == false && wf.CheckListJobMasterId == checkListJobMasterId && wf.CheckListJobGroupId == checkListJobGroupId
                              select new
                              {
                                  checkListJobAdvanceId = wf.CheckListJobAdvanceId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobStepNumber = wf.CheckListJobStepNumber,
                                  activityBeforeChangeOverDescription = wf.ActivityBeforeChangeOverDescription,
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
        /// <param name="checkListJobAdvanceId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobAdvanceById(int checkListJobAdvanceId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobAdvanceMaster
                              where wf.IsDeleted == false && wf.CheckListJobAdvanceId == checkListJobAdvanceId
                              select new
                              {
                                  checkListJobAdvanceId = wf.CheckListJobAdvanceId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobStepNumber = wf.CheckListJobStepNumber,
                                  activityBeforeChangeOverDescription = wf.ActivityBeforeChangeOverDescription,
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
        /// <param name="checkListJobAdvanceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// 

        //public CommonResponse DeleteCheckListJobAdvance(int checkListJobAdvanceId, long userId = 0)
        //{
        //    CommonResponse obj = new CommonResponse();
        //    CommonFunction commonFunction = new CommonFunction();
        //    try
        //    {
        //        var res = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobAdvanceId == checkListJobAdvanceId).FirstOrDefault();
        //        if (res != null)
        //        {
        //            res.IsDeleted = true;
        //            res.ModifiedOn = DateTime.Now;
        //            db.SaveChanges();
        //            commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListJobMasterId, res.CheckListJobGroupId, "Advance", "Job");
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


        public CommonResponse DeleteCheckListJobAdvance(string checkListJobAdvanceId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                string[] deletIds = checkListJobAdvanceId.Split(',');
                List<string> listIds = new List<string>();
                listIds = deletIds.ToList();
                foreach (var i in listIds)
                {
                    int idds = Convert.ToInt32(i);

                    var res = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobAdvanceId == idds).FirstOrDefault();
                    if (res != null)
                    {
                        res.IsDeleted = true;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListJobMasterId, res.CheckListJobGroupId, "Advance", "Job");
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
        /// <param name="checkListJobAdvanceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobAdvance(int checkListJobAdvanceId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobAdvanceId == checkListJobAdvanceId).FirstOrDefault();
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
        /// <param name="checkListJobAdvanceId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListJobAdvance(int checkListJobAdvanceId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobAdvanceId == checkListJobAdvanceId && m.IsDeleted == false).Count();
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
