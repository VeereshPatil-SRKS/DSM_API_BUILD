using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListActivityMasterEntity;
using static DSM.EntityModels.CheckListAdvanceMasterEntity;
using static DSM.EntityModels.CheckListLOTOTOMasterEntity;
using static DSM.EntityModels.CheckListMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListMasterDAL : ICheckListMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListMasterDAL));
        public CheckListMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponseWithIds AddAndEditCheckList(CheckListCustom data, long userId)
        {
            CommonResponseWithIds obj = new CommonResponseWithIds();
            try
            {
                var res = db.CheckListMaster.Where(m => m.CheckListName == data.checkListName && m.CheckListVersion == data.checkListVersion && m.IsDeleted == false && m.IsActive==true).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        CheckListMaster item = new CheckListMaster();
                        item.CheckListName = data.checkListName.Trim();
                        item.CheckListDescription = data.checkListDescription;
                        item.CheckListOwner = data.checkListOwner;
                        item.CheckListVersion = data.checkListVersion;
                        item.CheckListCategoryId = data.checkListCategoryId;
                        item.CheckListTypeId = data.checkListTypeId;
                        item.CheckListGroup = data.checkListGroup;
                        //Mani
                        item.EstimatedEndTime = data.estimatedTime;
                        //Mani
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListMaster.Add(item);
                        db.SaveChanges();
                        obj.response = ResourceResponse.AddedSucessfully;
                        obj.isStatus = true;
                        obj.id = item.CheckListId;
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
                        res.CheckListName = data.checkListName;
                        res.CheckListDescription = data.checkListDescription;
                        res.CheckListOwner = data.checkListOwner;
                        res.CheckListVersion = data.checkListVersion;
                        res.CheckListCategoryId = data.checkListCategoryId;
                        res.CheckListTypeId = data.checkListTypeId;
                        res.CheckListGroup = data.checkListGroup;
                        res.EstimatedEndTime = data.estimatedTime;
                        res.ModifiedBy = userId;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        obj.response = ResourceResponse.UpdatedSucessfully;
                        obj.isStatus = true;
                        obj.id = data.checkListId;
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
        /// Get Automatic Check List Version Name
        /// </summary>
        /// <param name="checkListName"></param>
        /// <returns></returns>
        public CommonResponse GetAutomaticCheckListVersionName(string checkListName)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                CommonFunction commonFunction = new CommonFunction();
                string versionNumber = commonFunction.DynamicVersionNumber(checkListName, 0);
                if (versionNumber == "")
                {
                    obj.isStatus = false;
                    obj.response = ResourceResponse.ExceptionMessage;
                }
                else
                {
                    obj.isStatus = true;
                    obj.response = versionNumber;
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
        /// Get Auto Suggest For Check List Name
        /// </summary>
        /// <param name="checkListName"></param>
        /// <returns></returns>
        public CommonResponse GetAutoSuggestForCheckListName(string checkListName)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                CommonFunction commonFunction = new CommonFunction();
                var dbCheck = db.CheckListMaster.Where(m => m.CheckListVersion.Contains(checkListName) && m.IsDeleted == false).Select(m=>m.CheckListName).ToList();
                if (dbCheck == null)
                {
                    obj.isStatus = false;
                    obj.response = ResourceResponse.NoSuggestionsFound;
                }
                else
                {
                    obj.isStatus = true;
                    obj.response = dbCheck;
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
        public CommonResponse ViewMultipleCheckList()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListId = wf.CheckListId,
                                  checkListName = wf.CheckListName,
                                  checkListDescription = wf.CheckListDescription,
                                  checkListOwner = wf.CheckListOwner,
                                  checkListVersion = wf.CheckListVersion,
                                  checkListCategoryId = wf.CheckListCategoryId,
                                  checkListCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListTypeId = wf.CheckListTypeId,
                                  checkListTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  checkListGroup = wf.CheckListGroup,
                                  estimatedTime = wf.EstimatedEndTime,


                              }).ToList();
                List<CheckListCustoms> checkListCustoms = new List<CheckListCustoms>();
                foreach (var items in result)
                {
                    long checkListMasterId = items.checkListId;

                    var list = items.checkListGroup.Split(',').Select(x => long.Parse(x.Trim()));
                    
                    var groups = (from wf in db.CheckListGroupMaster
                     where wf.IsDeleted == false && list.Contains(wf.CheckListGroupId)
                     select  wf.CheckListGroupName).ToList();

                    CheckListCustoms checkListCustom = new CheckListCustoms();
                    checkListCustom.checkListId = items.checkListId;
                    checkListCustom.checkListName = items.checkListName;
                    checkListCustom.checkListVersion = items.checkListVersion;
                    checkListCustom.checkListDescription = items.checkListDescription;
                    checkListCustom.checkListCategoryId = items.checkListCategoryId;
                    checkListCustom.checkListTypeId = items.checkListTypeId;
                    checkListCustom.checkListOwner = items.checkListOwner;
                    checkListCustom.checkListCategoryName = items.checkListCategoryName;
                    checkListCustom.checkListTypeName = items.checkListTypeName;
                    checkListCustom.isActive = Convert.ToBoolean(items.isActive);
                    checkListCustom.checkListGroup = String.Join(",", groups);
                    checkListCustom.checkListGroupId = items.checkListGroup;
                    checkListCustom.checkListGroupList = db.CheckListGroupMaster.Where(m => list.Contains(m.CheckListGroupId)).ToList(); 
                    checkListCustom.estimatedTime = items.estimatedTime;
                    checkListCustoms.Add(checkListCustom);
                }

                if (checkListCustoms.Count() != 0)
                {
                    obj.response = checkListCustoms;
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
        /// View Multiple Check List WRT Category Id And Type Id
        /// </summary>
        /// <param name="checkListCategoryId"></param>
        /// <param name="checkListTypeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListWRTCategoryIdAndTypeId(int checkListCategoryId, int checkListTypeId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListId = wf.CheckListId,
                                  checkListName = wf.CheckListName,
                                  checkListDescription = wf.CheckListDescription,
                                  checkListOwner = wf.CheckListOwner,
                                  checkListVersion = wf.CheckListVersion,
                                  checkListCategoryId = wf.CheckListCategoryId,
                                  checkListCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListTypeId = wf.CheckListTypeId,
                                  checkListTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  checkListGroup = wf.CheckListGroup,
                              }).ToList();

                if (checkListCategoryId == 0 && checkListTypeId == 0)
                {
                    //do nothing
                }
                else if (checkListCategoryId == 0 && checkListTypeId != 0)
                {
                    result = result.Where(m => m.checkListTypeId == checkListTypeId).ToList();
                }
                else if (checkListCategoryId != 0 && checkListTypeId == 0)
                {
                    result = result.Where(m => m.checkListCategoryId == checkListCategoryId).ToList();
                }
                else if (checkListCategoryId != 0 && checkListTypeId != 0)
                {
                    result = result.Where(m => m.checkListTypeId == checkListTypeId && m.checkListCategoryId == checkListCategoryId).ToList();
                }

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
        /// View Check List Details By Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListDetailsByCheckListAndGroupId(int checkListMasterId, int checkListGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListMaster
                              where wf.IsDeleted == false && wf.CheckListId == checkListMasterId
                              select new
                              {
                                  checkListId = wf.CheckListId,
                                  checkListName = wf.CheckListName,
                                  checkListDescription = wf.CheckListDescription,
                                  checkListOwner = wf.CheckListOwner,
                                  checkListVersion = wf.CheckListVersion,
                                  checkListCategoryId = wf.CheckListCategoryId,
                                  checkListCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListTypeId = wf.CheckListTypeId,
                                  checkListTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  checkListGroup = wf.CheckListGroup,
                              }).FirstOrDefault();
                if (result != null)
                {
                    #region CheckList Details
                    var list = result.checkListGroup.Split(',').Select(x => long.Parse(x.Trim()));

                    var groups = (from wf in db.CheckListGroupMaster
                                  where wf.IsDeleted == false && list.Contains(wf.CheckListGroupId)
                                  select wf.CheckListGroupName).ToList();
                    CheckListDetails checkListCustom = new CheckListDetails();
                    checkListCustom.checkListId = result.checkListId;
                    checkListCustom.checkListName = result.checkListName;
                    checkListCustom.checkListVersion = result.checkListVersion;
                    checkListCustom.checkListCategoryName = result.checkListCategoryName;
                    checkListCustom.checkListTypeName = result.checkListTypeName;
                    checkListCustom.checkListOwner = result.checkListOwner;
                    checkListCustom.checkListGroup = String.Join(",", groups);
                    checkListCustom.checkListGroupId = result.checkListGroup;
                    checkListCustom.checkListGroupList = db.CheckListGroupMaster.Where(m=> list.Contains(m.CheckListGroupId)).ToList();

                    #region Advance CheckList
                    var checkListAdvanceList = (from wf in db.CheckListAdvanceMaster
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
                    List<CheckListAdvanceCustom> checkListAdvanceCustomList = new List<CheckListAdvanceCustom>();
                    foreach (var checkListAdvance in checkListAdvanceList)
                    {
                        CheckListAdvanceCustom checkListAdvanceCustom = new CheckListAdvanceCustom();
                        checkListAdvanceCustom.checkListAdvanceId = checkListAdvance.checkListAdvanceId;
                        checkListAdvanceCustom.checkListMasterId = checkListAdvance.checkListMasterId;
                        checkListAdvanceCustom.checkListGroupId = checkListAdvance.checkListGroupId;
                        checkListAdvanceCustom.checkListStepNumber = checkListAdvance.checkListStepNumber;
                        checkListAdvanceCustom.activityBeforeChangeOverDescription = checkListAdvance.activityBeforeChangeOverDescription;
                        checkListAdvanceCustom.remarks = checkListAdvance.remarks;
                        checkListAdvanceCustomList.Add(checkListAdvanceCustom);
                    }
                    checkListCustom.checkListAdvanceCustom = checkListAdvanceCustomList;
                    #endregion
                    #region Activity CheckList

                    List<CheckListActivityBySubCategory> checkListActivityBySubCategories = new List<CheckListActivityBySubCategory>();

                    var subCategories = db.CheckListSubCategoryMaster.Where(m => m.IsDeleted == false).ToList();
                    foreach (var subCategory in subCategories)
                    {
                        CheckListActivityBySubCategory checkListActivityBySubCategory = new CheckListActivityBySubCategory();
                        checkListActivityBySubCategory.activitySubCategoryId = subCategory.CheckListSubCategoryId;
                        checkListActivityBySubCategory.activitySubCategoryName = subCategory.CheckListSubCategoryName;

                        var activityCheckList = (from wf in db.CheckListActivityMaster
                                                 where wf.IsDeleted == false && wf.CheckListMasterId == checkListMasterId && wf.CheckListGroupId == checkListGroupId && wf.ActivitySubCategoryId == subCategory.CheckListSubCategoryId
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
                                                     remarks = wf.Remarks,
                                                     isActive = wf.IsActive,
                                                     assetId = wf.AssetId,
                                                     assetNumber = db.AssetMaster.Where(m => m.AssetId == wf.AssetId).Select(m => m.BarcodeAllocatedNumber).FirstOrDefault(),
                                                     expectedCompletionTime = wf.ExpectedCompletionTime

                                                 }).ToList();
                        List<CheckListActivityDetails> checkListActivityDetailsList = new List<CheckListActivityDetails>();
                        foreach (var activityCheck in activityCheckList)
                        {
                            CheckListActivityDetails checkListActivityDetails = new CheckListActivityDetails();
                            checkListActivityDetails.checkListActivityId = activityCheck.checkListActivityId;
                            checkListActivityDetails.checkListMasterId = activityCheck.checkListMasterId;
                            checkListActivityDetails.checkListGroupId = activityCheck.checkListGroupId;
                            checkListActivityDetails.checkListStepNumber = activityCheck.checkListStepNumber;
                            checkListActivityDetails.activityDescription = activityCheck.activityDescription;
                            checkListActivityDetails.remarks = activityCheck.remarks;
                            checkListActivityDetails.isActivityManditory = activityCheck.isActivityManditory;
                            checkListActivityDetails.isPhotoManditory = activityCheck.isPhotoManditory;
                            checkListActivityDetails.isBarCodeManditory = activityCheck.isBarCodeManditory;
                            checkListActivityDetails.assetId = activityCheck.assetId;
                            checkListActivityDetails.assetNumber = activityCheck.assetNumber;
                            try
                            {
                                checkListActivityDetails.expectedCompletionTime = activityCheck.expectedCompletionTime;
                            }
                            catch (Exception ex)
                            { }
                            checkListActivityDetailsList.Add(checkListActivityDetails);
                        }
                        if (activityCheckList.Count > 0)
                        {
                            checkListActivityBySubCategory.checkListActivityDetails = checkListActivityDetailsList;
                            checkListActivityBySubCategories.Add(checkListActivityBySubCategory);
                        }

                    }
                    checkListCustom.checkListActivityBySubCategory = checkListActivityBySubCategories;
                    #endregion
                    #region LOTOTO CheckList
                    List<CheckListLOTOTOCustom> checkListLOTOTOCustoms = new List<CheckListLOTOTOCustom>();
                    var checkListTOTOList = (from wf in db.CheckListLototomaster
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
                    foreach (var checkListLOTOTO in checkListTOTOList)
                    {
                        CheckListLOTOTOCustom checkListLOTOTOCustom = new CheckListLOTOTOCustom();
                        checkListLOTOTOCustom.checkListLOTOTOId = checkListLOTOTO.checkListLOTOTOId;
                        checkListLOTOTOCustom.checkListMasterId = checkListLOTOTO.checkListMasterId;
                        checkListLOTOTOCustom.checkListGroupId = checkListLOTOTO.checkListGroupId;
                        checkListLOTOTOCustom.checkListLockStepNumber = checkListLOTOTO.checkListLockStepNumber;
                        checkListLOTOTOCustom.positionDescription = checkListLOTOTO.positionDescription;
                        checkListLOTOTOCustom.isLockOutRequired = checkListLOTOTO.isLockOutRequired;
                        checkListLOTOTOCustom.isTagOutRequired = checkListLOTOTO.isTagOutRequired;
                        checkListLOTOTOCustom.isTryOutRequired = checkListLOTOTO.isTryOutRequired;
                        checkListLOTOTOCustom.remarks = checkListLOTOTO.remarks;
                        checkListLOTOTOCustoms.Add(checkListLOTOTOCustom);
                    }
                    checkListCustom.checkListLOTOTOCustom = checkListLOTOTOCustoms;
                    #endregion
                    #endregion

                    obj.response = checkListCustom;
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
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListById(int checkListId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListMaster
                              where wf.IsDeleted == false && wf.CheckListId == checkListId
                              select new
                              {
                                  checkListId = wf.CheckListId,
                                  checkListName = wf.CheckListName,
                                  checkListDescription = wf.CheckListDescription,
                                  checkListOwner = wf.CheckListOwner,
                                  checkListVersion = wf.CheckListVersion,
                                  checkListCategoryId = wf.CheckListCategoryId,
                                  checkListCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListTypeId = wf.CheckListTypeId,
                                  checkListTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  checkListGroup = wf.CheckListGroup,
                                  checkListGroupId = wf.CheckListGroup,
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
        /// <param name="checkListId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckList(int checkListId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListMaster.Where(m => m.CheckListId == checkListId).FirstOrDefault();
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
        /// <param name="checkListId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckList(int checkListId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListMaster.Where(m => m.CheckListId == checkListId).FirstOrDefault();
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
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckList(int checkListId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListMaster.Where(m => m.CheckListId == checkListId && m.IsDeleted == false).Count();
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
        /// Check Check List Completly Created Or Not
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListCompletlyCreatedOrNot(int checkListId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                bool result = commonFunction.GetCheckListCreatedOrNotStatus(checkListId);
                if (result)
                {
                    obj.isStatus = true;
                    obj.response = ResourceResponse.CheckListCreatedSuccessfully;
                }
                else
                {
                    obj.response = ResourceResponse.JobNotCompletelyCreated;
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
