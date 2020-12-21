using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListAdvanceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListAdvanceMasterDAL : ICheckListAdvanceMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListAdvanceMasterDAL));
        public CheckListAdvanceMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListAdvance(CheckListAdvanceCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListAdvanceMaster.Where(m => m.AdvanceCheckListId == data.checkListAdvanceId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        var stepCount = commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(data.checkListMasterId, data.checkListGroupId, "Advance", "CheckList");

                        CheckListAdvanceMaster item = new CheckListAdvanceMaster();
                        item.CheckListMasterId = data.checkListMasterId;
                        item.CheckListGroupId = data.checkListGroupId;
                        item.CheckListStepNumber = stepCount + 1;
                        item.ActivityBeforeChangeOverDescription = data.activityBeforeChangeOverDescription;
                        item.Remarks = data.remarks;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = true;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListAdvanceMaster.Add(item);
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
                        res.CheckListMasterId = data.checkListMasterId;
                        res.CheckListGroupId = data.checkListGroupId;
                        res.CheckListStepNumber = data.checkListStepNumber;
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
        public CommonResponse ViewMultipleCheckListAdvance()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListAdvanceMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListAdvanceId = wf.AdvanceCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListStepNumber = wf.CheckListStepNumber,
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
        /// View Check List Advance By check List Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListAdvanceByCheckListMasterId(int checkListMasterId, int checkListGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListAdvanceMaster
                              where wf.IsDeleted == false && wf.CheckListMasterId == checkListMasterId && wf.CheckListGroupId == checkListGroupId
                              select new
                              {
                                  checkListAdvanceId = wf.AdvanceCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListStepNumber = wf.CheckListStepNumber,
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
        /// <param name="checkListAdvanceId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListAdvanceById(int checkListAdvanceId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListAdvanceMaster
                              where wf.IsDeleted == false && wf.AdvanceCheckListId == checkListAdvanceId
                              select new
                              {
                                  checkListAdvanceId = wf.AdvanceCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListStepNumber = wf.CheckListStepNumber,
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
        /// <param name="checkListAdvanceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListAdvance(int checkListAdvanceId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListAdvanceMaster.Where(m => m.AdvanceCheckListId == checkListAdvanceId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDeleted = true;
                    res.ModifiedOn = DateTime.Now;
                    db.SaveChanges();

                    commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListMasterId, res.CheckListGroupId, "Advance", "CheckList");

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
        /// <param name="checkListAdvanceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListAdvance(int checkListAdvanceId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListAdvanceMaster.Where(m => m.AdvanceCheckListId == checkListAdvanceId).FirstOrDefault();
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
        /// <param name="checkListAdvanceId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListAdvance(int checkListAdvanceId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListAdvanceMaster.Where(m => m.AdvanceCheckListId == checkListAdvanceId && m.IsDeleted == false).Count();
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
