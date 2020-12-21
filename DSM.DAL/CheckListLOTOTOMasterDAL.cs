using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListLOTOTOMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListLOTOTOMasterDAL : ICheckListLOTOTOMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListLOTOTOMasterDAL));
        public CheckListLOTOTOMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListLOTOTO(CheckListLOTOTOCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListLototomaster.Where(m => m.LototoCheckListId == data.checkListLOTOTOId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        var stepCount = commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(data.checkListMasterId, data.checkListGroupId, "LOTOTO", "CheckList");

                        CheckListLototomaster item = new CheckListLototomaster();
                        item.CheckListMasterId = data.checkListMasterId;
                        item.CheckListGroupId = data.checkListGroupId;
                        item.CheckListLockStepNumber = stepCount + 1;
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
                        db.CheckListLototomaster.Add(item);
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
                        res.CheckListLockStepNumber = data.checkListLockStepNumber;
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
        public CommonResponse ViewMultipleCheckListLOTOTO()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListLototomaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListLOTOTOId = wf.LototoCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListLockStepNumber = wf.CheckListLockStepNumber,
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
        /// View Check List LOTOTO By Check List Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListLOTOTOByCheckListMasterId(int checkListMasterId,int checkListGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListLototomaster
                              where wf.IsDeleted == false && wf.CheckListMasterId == checkListMasterId && wf.CheckListGroupId == checkListGroupId
                              select new
                              {
                                  checkListLOTOTOId = wf.LototoCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListLockStepNumber = wf.CheckListLockStepNumber,
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
        /// <param name="checkListLOTOTOId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListLOTOTOById(int checkListLOTOTOId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListLototomaster
                              where wf.IsDeleted == false && wf.LototoCheckListId == checkListLOTOTOId
                              select new
                              {
                                  checkListLOTOTOId = wf.LototoCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListLockStepNumber = wf.CheckListLockStepNumber,
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
        /// <param name="checkListLOTOTOId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListLOTOTO(int checkListLOTOTOId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListLototomaster.Where(m => m.LototoCheckListId == checkListLOTOTOId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDeleted = true;
                    res.ModifiedOn = DateTime.Now;
                    db.SaveChanges();

                    commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListMasterId, res.CheckListGroupId, "LOTOTO", "CheckList");

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
        /// <param name="checkListLOTOTOId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListLOTOTO(int checkListLOTOTOId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListLototomaster.Where(m => m.LototoCheckListId == checkListLOTOTOId).FirstOrDefault();
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
        /// <param name="checkListLOTOTOId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListLOTOTO(int checkListLOTOTOId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListLototomaster.Where(m => m.LototoCheckListId == checkListLOTOTOId && m.IsDeleted == false).Count();
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
