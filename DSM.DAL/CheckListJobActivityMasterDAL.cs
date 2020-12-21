using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobActivityMaster;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobActivityMasterDAL : ICheckListJobActivityMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobActivityMasterDAL));
        public CheckListJobActivityMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobActivity(CheckListJobActivityCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == data.checkListJobActivityId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        var stepCount = commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(data.checkListJobMasterId, data.checkListJobGroupId, "Activity", "Job");

                        CheckListJobActivityMaster item = new CheckListJobActivityMaster();
                        item.CheckListJobMasterId = data.checkListJobMasterId;
                        item.CheckListJobGroupId = data.checkListJobGroupId;
                        item.ActivitySubCategoryId = data.activitySubCategoryId;
                        item.CheckListJobStepNumber = stepCount + 1;
                        item.Remarks = data.remarks;
                        item.ActivityDescription = data.activityDescription;
                        item.IsActivityManditory = data.isActivityManditory;
                        item.IsPhotoManditory = data.isPhotoManditory;
                        item.IsBarCodeManditory = data.isBarCodeManditory;
                        try
                        {
                            item.ExpectedCompletionTime = data.expectedCompletionTime;
                        }
                        catch (Exception ex)
                        { }
                        item.AssetId = data.assetId;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.IsAdminApproved = true;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobActivityMaster.Add(item);
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
                        res.ActivitySubCategoryId = data.activitySubCategoryId;
                        res.CheckListJobStepNumber = data.checkListJobStepNumber;
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
        public CommonResponse ViewMultipleCheckListJobActivity()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobActivityMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListJobActivityId = wf.CheckListJobActivityId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobStepNumber = wf.CheckListJobStepNumber,
                                  activityDescription = wf.ActivityDescription,
                                  isActivityManditory = wf.IsActivityManditory,
                                  isPhotoManditory = wf.IsPhotoManditory,
                                  isBarCodeManditory = wf.IsBarCodeManditory,
                                  expectedCompletionTime = wf.ExpectedCompletionTime,
                                  assetId = wf.AssetId,
                                  assetDetails = db.AssetMaster.Where(m=>m.AssetId == wf.AssetId).FirstOrDefault(),
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
        /// View Check ListJob Activity By check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobActivityBycheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobActivityMaster
                              where wf.IsDeleted == false && wf.CheckListJobMasterId == checkListJobMasterId && wf.CheckListJobGroupId == checkListJobGroupId
                              select new
                              {
                                  checkListJobActivityId = wf.CheckListJobActivityId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  activitySubCategoryId = wf.ActivitySubCategoryId,
                                  activitySubCategoryName = db.CheckListSubCategoryMaster.Where(m => m.CheckListSubCategoryId == wf.ActivitySubCategoryId).Select(m => m.CheckListSubCategoryName).FirstOrDefault(),
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  checkListJobStepNumber = wf.CheckListJobStepNumber,
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
        /// View Document  by Id
        /// </summary>
        /// <param name="checkListJobActivityId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobActivityById(int checkListJobActivityId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobActivityMaster
                              where wf.IsDeleted == false && wf.CheckListJobActivityId == checkListJobActivityId
                              select new
                              {
                                  checkListJobActivityId = wf.CheckListJobActivityId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                                  activitySubCategoryId = wf.ActivitySubCategoryId,
                                  activitySubCategoryName = db.CheckListSubCategoryMaster.Where(m => m.CheckListSubCategoryId == wf.ActivitySubCategoryId).Select(m => m.CheckListSubCategoryName).FirstOrDefault(),
                                  checkListJobStepNumber = wf.CheckListJobStepNumber,
                                  activityDescription = wf.ActivityDescription,
                                  isActivityManditory = wf.IsActivityManditory,
                                  isPhotoManditory = wf.IsPhotoManditory,
                                  isBarCodeManditory = wf.IsBarCodeManditory,
                                  expectedCompletionTime = wf.ExpectedCompletionTime,
                                  assetId = wf.AssetId,
                                  assetDetails = db.AssetMaster.Where(m => m.AssetId == wf.AssetId).FirstOrDefault(),
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
        /// <param name="checkListJobActivityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListJobActivity(string checkListJobActivityId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                string[] deletIds = checkListJobActivityId.Split(',');
                List<string> listIds = new List<string>();
                listIds = deletIds.ToList();
                foreach (var i in listIds)
                {
                    int idds = Convert.ToInt32(i);
                    var res = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == idds).FirstOrDefault();
                    if (res != null)
                    {
                        res.IsDeleted = true;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();

                        commonFunction.ChangeCheckListAndJobStepNumberOfPreviousItem(res.CheckListJobMasterId, res.CheckListJobGroupId, "Activity", "Job");

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
        /// <param name="checkListJobActivityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobActivity(int checkListJobActivityId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == checkListJobActivityId).FirstOrDefault();
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
        /// <param name="checkListJobActivityId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListJobActivity(int checkListJobActivityId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == checkListJobActivityId && m.IsDeleted == false).Count();
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
