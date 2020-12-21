using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListActivityMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListActivityMasterDAL : ICheckListActivityMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListActivityMasterDAL));
        public CheckListActivityMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListActivity(CheckListActivityCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListActivityMaster.Where(m => m.ActivityCheckListId == data.checkListActivityId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        var stepCount = commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(data.checkListMasterId, data.checkListGroupId, "Activity", "CheckList");

                        CheckListActivityMaster item = new CheckListActivityMaster();
                        item.CheckListMasterId = data.checkListMasterId;
                        item.CheckListGroupId = data.checkListGroupId;
                        item.ActivitySubCategoryId = data.activitySubCategoryId;
                        item.CheckListStepNumber = stepCount + 1;
                        item.Remarks = data.remarks;
                        item.ActivityDescription = data.activityDescription;
                        item.IsActivityManditory = data.isActivityManditory;
                        item.IsPhotoManditory = data.isPhotoManditory;
                        item.IsBarCodeManditory = data.isBarCodeManditory;
                        item.AssetId = data.assetId;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = true;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        try
                        {
                            item.ExpectedCompletionTime = data.expectedCompletionTime;
                        }
                        catch (Exception ex)
                        { }
                        db.CheckListActivityMaster.Add(item);
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
                        res.ActivitySubCategoryId = data.activitySubCategoryId;
                        res.CheckListStepNumber = data.checkListStepNumber;
                        res.Remarks = data.remarks;
                        res.ActivityDescription = data.activityDescription;
                        res.IsActivityManditory = data.isActivityManditory;
                        res.IsPhotoManditory = data.isPhotoManditory;
                        res.IsBarCodeManditory = data.isBarCodeManditory;
                        res.AssetId = data.assetId;
                        try
                        {
                            res.ExpectedCompletionTime = data.expectedCompletionTime;
                        }
                        catch (Exception ex)
                        { }
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
        public CommonResponse ViewMultipleCheckListActivity()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListActivityMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListActivityId = wf.ActivityCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListStepNumber = wf.CheckListStepNumber,
                                  activityDescription = wf.ActivityDescription,
                                  isActivityManditory = wf.IsActivityManditory,
                                  isPhotoManditory = wf.IsPhotoManditory,
                                  isBarCodeManditory = wf.IsBarCodeManditory,
                                  expectedCompletionTime = wf.ExpectedCompletionTime,
                                  assetId = wf.AssetId,
                                  assetDetails = db.AssetMaster.Where(m => m.AssetId == wf.AssetId).FirstOrDefault(),
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
        /// View Check List Activity By check List Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListActivityBycheckListMasterId(int checkListMasterId,int checkListGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListActivityMaster
                              where wf.IsDeleted == false && wf.CheckListMasterId == checkListMasterId && wf.CheckListGroupId == checkListGroupId
                              select new
                              {
                                  checkListActivityId = wf.ActivityCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  activitySubCategoryId = wf.ActivitySubCategoryId,
                                  activitySubCategoryName = db.CheckListSubCategoryMaster.Where(m => m.CheckListSubCategoryId == wf.ActivitySubCategoryId).Select(m => m.CheckListSubCategoryName).FirstOrDefault(),
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListStepNumber = wf.CheckListStepNumber,
                                  activityDescription = wf.ActivityDescription,
                                  isActivityManditory = wf.IsActivityManditory,
                                  isPhotoManditory = wf.IsPhotoManditory,
                                  isBarCodeManditory = wf.IsBarCodeManditory,
                                  expectedCompletionTime = wf.ExpectedCompletionTime,
                                  assetId = wf.AssetId,
                                  remarks = wf.Remarks,
                                  assetDetails = db.AssetMaster.Where(m => m.AssetId == wf.AssetId).FirstOrDefault(),
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
        /// <param name="checkListActivityId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListActivityById(int checkListActivityId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListActivityMaster
                              where wf.IsDeleted == false && wf.ActivityCheckListId == checkListActivityId
                              select new
                              {
                                  checkListActivityId = wf.ActivityCheckListId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  activitySubCategoryId = wf.ActivitySubCategoryId,
                                  activitySubCategoryName = db.CheckListSubCategoryMaster.Where(m => m.CheckListSubCategoryId == wf.ActivitySubCategoryId).Select(m => m.CheckListSubCategoryName).FirstOrDefault(),
                                  checkListStepNumber = wf.CheckListStepNumber,
                                  activityDescription = wf.ActivityDescription,
                                  isActivityManditory = wf.IsActivityManditory,
                                  isPhotoManditory = wf.IsPhotoManditory,
                                  isBarCodeManditory = wf.IsBarCodeManditory,
                                  expectedCompletionTime = wf.ExpectedCompletionTime,
                                  assetId = wf.AssetId,
                                  remarks = wf.Remarks,
                                  assetDetails = db.AssetMaster.Where(m => m.AssetId == wf.AssetId).FirstOrDefault(),
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
        /// <param name="checkListActivityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListActivity(int checkListActivityId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListActivityMaster.Where(m => m.ActivityCheckListId == checkListActivityId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDeleted = true;
                    res.ModifiedOn = DateTime.Now;
                    db.SaveChanges();

                    commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListMasterId, res.CheckListGroupId, "Activity", "CheckList");

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
        /// <param name="checkListActivityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListActivity(int checkListActivityId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListActivityMaster.Where(m => m.ActivityCheckListId == checkListActivityId).FirstOrDefault();
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
        /// <param name="checkListActivityId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListActivity(int checkListActivityId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListActivityMaster.Where(m => m.ActivityCheckListId == checkListActivityId && m.IsDeleted == false).Count();
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
