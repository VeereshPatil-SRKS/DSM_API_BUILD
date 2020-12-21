using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using System.Linq;
using DSM.DAL.Resource;
using static DSM.EntityModels.CheckListSupervisorApprovalEntity;
using static DSM.EntityModels.CheckListJobMasterEntity;
using Microsoft.Extensions.Options;
using DSM.DAL.Helpers;
using DSM.DAL.App_Start;

namespace DSM.DAL
{
    public class CheckListSupervisorApprovalDAL : ICheckListSupervisorApproval
    {
        DSMContext db = new DSMContext();
        private readonly AppSettings appSettings;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListSupervisorApprovalDAL));
        public CheckListSupervisorApprovalDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        /// <summary>
        /// Check List For Supervisor Approval
        /// </summary>
        /// <param name="checkListSupervisorEntity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckListForSupervisorApproval(int checkListJobId , int checkListJobGroupId, long userId)
        {                                                    
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var result = (from wf in db.CheckListJobMaster
                              where wf.CheckListJobId == checkListJobId
                              select new
                              {
                                  checkListJobId = wf.CheckListJobId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  batchNumber = wf.BatchNumber,
                                  processOrderNumber = wf.ProcessOrderNumber,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListJobName = wf.CheckListJobName,
                                  checkListJobDescription = wf.CheckListJobDescription,
                                  checkListJobSupervisorId = wf.CheckListJobSupervisorId,
                                  checkListJobSupervisorName = db.UserDetails.Where(m => m.UserId == wf.CheckListJobSupervisorId).Select(m => m.UserFullName).FirstOrDefault(),
                                  checkListJobLineNumber = wf.CheckListJobLineNumber,
                                  checkListJobLineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.CheckListJobLineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                                  checkListShiftNumber = wf.CheckListShiftNumber,
                                  checkListShiftName = db.ShiftMaster.Where(m => m.ShiftId == wf.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault(),
                                  checkListStartTime = wf.CheckListStartTime,
                                  checkListEndTime = wf.CheckListEndTime,
                                  previousGrade = wf.PreviousGrade,
                                  previousGradeName = db.GradeMaster.Where(m => m.GradeId == wf.PreviousGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  currentGrade = wf.CurrentGrade,
                                  currentGradeName = db.GradeMaster.Where(m => m.GradeId == wf.CurrentGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  previousColor = wf.PreviousColor,
                                  previousColorName = db.ColorMaster.Where(m => m.ColorId == wf.PreviousColor).Select(m => m.ColorName).FirstOrDefault(),
                                  currentColor = wf.CurrentGrade,
                                  currentColorName = db.ColorMaster.Where(m => m.ColorId == wf.CurrentColor).Select(m => m.ColorName).FirstOrDefault(),
                                  checkListJobCategoryId = wf.CheckListJobCategoryId,
                                  checkListJobCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListJobCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListJobTypeId = wf.CheckListJobTypeId,
                                  checkListJobTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  assignedOperatorsgrp = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId && m.CheckListJobGroupId == checkListJobGroupId).ToList(),
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                                  jobCreatedBy = db.UserDetails.Where(m => m.UserId == wf.CreatedBy).Select(m => m.UserFullName).FirstOrDefault(),
                                  wf.OverAllRejected,
                                  wf.OverAllApproved,
                                  wf.OverAllJobCompleted,
                              }).FirstOrDefault();
               
                if (result != null)
                {
                    #region CheckListJob Details
                    CheckListJobDetails checkListJobCustom = new CheckListJobDetails();
                    checkListJobCustom.checkListJobId = result.checkListJobId;
                    checkListJobCustom.checkListMasterId = result.checkListMasterId;
                    checkListJobCustom.batchNumber = result.batchNumber;
                    checkListJobCustom.processOrderNumber = result.processOrderNumber;
                    checkListJobCustom.checkListMasterName = result.checkListMasterName;
                    checkListJobCustom.checkListJobName = result.checkListJobName;
                    checkListJobCustom.checkListJobDescription = result.checkListJobDescription;
                    checkListJobCustom.checkListJobCategoryId = result.checkListJobCategoryId;
                    checkListJobCustom.checkListJobCategoryName = result.checkListJobCategoryName;
                    checkListJobCustom.checkListJobTypeId = result.checkListJobTypeId;
                    checkListJobCustom.checkListJobTypeName = result.checkListJobTypeName;
                    checkListJobCustom.checkListJobSupervisorId = result.checkListJobSupervisorId;
                    checkListJobCustom.checkListJobSupervisorName = result.checkListJobSupervisorName;
                    checkListJobCustom.checkListJobLineNumber = result.checkListJobLineNumber;
                    checkListJobCustom.checkListJobLineNumberName = result.checkListJobLineNumberName;
                    checkListJobCustom.checkListShiftNumber = result.checkListShiftNumber;
                    checkListJobCustom.checkListShiftName = result.checkListShiftName;
                    checkListJobCustom.checkListStartTime = result.checkListStartTime;
                    checkListJobCustom.checkListEndTime = result.checkListEndTime;
                    checkListJobCustom.previousGrade = result.previousGrade;
                    checkListJobCustom.previousGradeName = result.previousGradeName;
                    checkListJobCustom.currentGrade = result.currentGrade;
                    checkListJobCustom.currentGradeName = result.currentGradeName;
                    checkListJobCustom.previousColor = result.previousColor;
                    checkListJobCustom.previousColorName = result.previousColorName;
                    checkListJobCustom.currentColor = result.currentColor;
                    checkListJobCustom.currentColorName = result.currentColorName;
                    checkListJobCustom.jobCreatedBy = result.jobCreatedBy;
                    checkListJobCustom.OverAllRejected = result.OverAllRejected;
                    checkListJobCustom.OverAllApproved = result.OverAllApproved;
                    checkListJobCustom.OverAllJobCompleted = result.OverAllJobCompleted;


                    var assignedOps = (String.Join(",", result.assignedOperators.Select(m => m.PrimaryResource).ToList()));
                    var assignedOperatorsgrp = (String.Join(",", result.assignedOperatorsgrp.Select(m => m.PrimaryResource).ToList()));

                    if (result.assignedOperators.Count != 0)
                    {
                        string operatorIdss = assignedOps;
                        string operatorIdssgrp = assignedOperatorsgrp;
                        List<long> opIds = operatorIdss.Split(',').Select(long.Parse).ToList();
                        List<long> opIdsgrp = operatorIdssgrp.Split(',').Select(long.Parse).ToList();
                        var opItem = String.Join(",", db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                        var opItemAll = String.Join(",", db.UserDetails.Where(m => opIdsgrp.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                        checkListJobCustom.assignedOperators = opItemAll;

                        var checkListJobWRToperator = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == result.checkListJobId).ToList();
                        var checkListJobWRToperatorCheck = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == result.checkListJobId && opIds.Contains(wf.OperatorId)).ToList();
                        if (checkListJobWRToperatorCheck.Count == checkListJobWRToperator.Count)
                        {
                            checkListJobCustom.approveButton = true;
                            checkListJobCustom.rejectButton = true;
                            var checkListJobWRToperatorcheckapproved = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                            if (checkListJobWRToperatorcheckapproved != null)
                            {
                                if (checkListJobWRToperatorcheckapproved.IsAdminApproved == true || checkListJobWRToperatorcheckapproved.IsJobRejected == true)
                                {
                                    checkListJobCustom.approveButton = false;
                                    checkListJobCustom.rejectButton = false;
                                }
                            }
                            else {
                               var checkListJobWRToperatorcheckapprovedrej = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == false && wf.CheckListJobIsPartialCompleted == true && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId).ToList();
                                if (checkListJobWRToperatorcheckapprovedrej.Count > 0)
                                {
                                    checkListJobCustom.approveButton = false;
                                    checkListJobCustom.rejectButton = false;
                                }
                                else if (checkListJobWRToperatorcheckapprovedrej.Count == 0)
                                {
                                    checkListJobCustom.approveButton = false;
                                    checkListJobCustom.rejectButton = false;
                                }

                            }
                        }

                        if ((checkListJobCustom.approveButton == false && checkListJobCustom.rejectButton == false && result.OverAllJobCompleted == true) && (result.OverAllRejected == false || result.OverAllApproved == false))
                        {
                            if (result.OverAllRejected == true || result.OverAllApproved == true)
                            {
                                checkListJobCustom.OverAllJobCompleted = false;
                                checkListJobCustom.OverAllJobRejected = false;
                            }
                            else
                            {
                                checkListJobCustom.OverAllJobCompleted = true;
                                checkListJobCustom.OverAllJobRejected = true;
                            }
                                
                        }
                        else
                        {
                            checkListJobCustom.OverAllJobCompleted = false;
                            checkListJobCustom.OverAllJobRejected = false;
                        }

                     }
                    long? checkListJobOperatorId = 0;
                    var checkListJobOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == result.checkListJobId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                    if (checkListJobOperator != null)
                    {
                        checkListJobOperatorId = checkListJobOperator.CheckListJobWrtoperatorId;
                        try
                        {
                            checkListJobCustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobStartTime);
                        }
                        catch (Exception ex)
                        { }
                        try
                        {
                            checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobEndTime);
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobOperator.CheckListJobStartTime, checkListJobOperator.CheckListJobEndTime);
                        }

                        catch (Exception ex)
                        {
                        }
                    }
                    #region Advance CheckListJob
                    var checkListJobAdvanceListJob = (from wf in db.CheckListJobAdvanceMaster
                                                      where wf.IsDeleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId
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
                    List<CheckListJobAdvanceCustom> checkListJobAdvanceCustomListJob = new List<CheckListJobAdvanceCustom>();
                    foreach (var checkListJobAdvance in checkListJobAdvanceListJob)
                    {
                        CheckListJobAdvanceCustom checkListJobAdvanceCustom = new CheckListJobAdvanceCustom();
                        checkListJobAdvanceCustom.checkListJobAdvanceId = checkListJobAdvance.checkListJobAdvanceId;
                        checkListJobAdvanceCustom.checkListJobMasterId = checkListJobAdvance.checkListJobMasterId;
                        checkListJobAdvanceCustom.checkListJobGroupId = checkListJobAdvance.checkListJobGroupId;
                        checkListJobAdvanceCustom.checkListJobStepNumber = checkListJobAdvance.checkListJobStepNumber;
                        checkListJobAdvanceCustom.activityBeforeChangeOverDescription = checkListJobAdvance.activityBeforeChangeOverDescription;
                        checkListJobAdvanceCustom.remarks = checkListJobAdvance.remarks;
                        

                        var checkListJobAvanceOperator = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobAdvanceId == checkListJobAdvance.checkListJobAdvanceId && m.CheckListJobOperatorId == checkListJobOperatorId).FirstOrDefault();
                        if (checkListJobAvanceOperator != null)
                        {
                            CheckListJobAdvanceOperatorCustom checkListJobAdvanceOperatorCustom = new CheckListJobAdvanceOperatorCustom();
                            checkListJobAdvanceOperatorCustom.checkListJobAdvanceOperatorId = checkListJobAvanceOperator.CheckListJobAdvanceOperatorId;
                            checkListJobAdvanceOperatorCustom.checkListJobOperatorId = checkListJobAvanceOperator.CheckListJobOperatorId;
                            checkListJobAdvanceOperatorCustom.operatorRemark = checkListJobAvanceOperator.OperatorRemark;
                            
                            if (checkListJobAvanceOperator.IsAdminApproved == true)
                            {
                                checkListJobAdvanceOperatorCustom.approveRejectFlag = false;
                            }
                            if (checkListJobAvanceOperator.IsAdminApproved == false)
                            {
                                checkListJobAdvanceOperatorCustom.approveRejectFlag = true;
                            }
                            if (checkListJobAvanceOperator.IsJobRejected == true)
                            {
                                checkListJobAdvanceOperatorCustom.isJobRejected = true;
                            }
                            else
                            {
                                checkListJobAdvanceOperatorCustom.isJobRejected = false;
                            }

                            checkListJobAdvanceCustom.checkListJobAdvanceOperatorCustom = checkListJobAdvanceOperatorCustom;
                        }
                        checkListJobAdvanceCustomListJob.Add(checkListJobAdvanceCustom);
                    }
                    checkListJobCustom.checkListJobAdvanceCustom = checkListJobAdvanceCustomListJob;
                    #endregion
                    #region Activity CheckListJob

                    List<CheckListJobActivityBySubCategory> checkListJobActivityBySubCategories = new List<CheckListJobActivityBySubCategory>();

                    var subCategories = db.CheckListSubCategoryMaster.Where(m => m.IsDeleted == false).ToList();
                    foreach (var subCategory in subCategories)
                    {
                        CheckListJobActivityBySubCategory checkListJobActivityBySubCategory = new CheckListJobActivityBySubCategory();
                        checkListJobActivityBySubCategory.activitySubCategoryId = subCategory.CheckListSubCategoryId;
                        checkListJobActivityBySubCategory.activitySubCategoryName = subCategory.CheckListSubCategoryName;

                        var activityCheckListJob = (from wf in db.CheckListJobActivityMaster
                                                    where wf.IsDeleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId && wf.ActivitySubCategoryId == subCategory.CheckListSubCategoryId
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
                                                        remarks = wf.Remarks,
                                                        isActive = wf.IsActive,
                                                        assetId = wf.AssetId,
                                                        expectedCompletionTime = wf.ExpectedCompletionTime
                                                    }).ToList();
                        List<CheckListJobActivityDetails> checkListJobActivityDetailsListJob = new List<CheckListJobActivityDetails>();
                        foreach (var activityCheck in activityCheckListJob)
                        {
                            CheckListJobActivityDetails checkListJobActivityDetails = new CheckListJobActivityDetails();
                            checkListJobActivityDetails.checkListJobActivityId = activityCheck.checkListJobActivityId;
                            checkListJobActivityDetails.checkListJobMasterId = activityCheck.checkListJobMasterId;
                            checkListJobActivityDetails.checkListJobGroupId = activityCheck.checkListJobGroupId;
                            checkListJobActivityDetails.checkListJobStepNumber = activityCheck.checkListJobStepNumber;
                            checkListJobActivityDetails.activityDescription = activityCheck.activityDescription;
                            checkListJobActivityDetails.remarks = activityCheck.remarks;
                            checkListJobActivityDetails.isActivityManditory = activityCheck.isActivityManditory;
                            checkListJobActivityDetails.isPhotoManditory = activityCheck.isPhotoManditory;
                            checkListJobActivityDetails.isBarCodeManditory = activityCheck.isBarCodeManditory;
                            checkListJobActivityDetails.assetId = activityCheck.assetId;
                            try
                            {
                                checkListJobActivityDetails.expectedCompletionTime = activityCheck.expectedCompletionTime;
                            }
                            catch (Exception ex)
                            { }

                            var checkListJobAvanceOperator = db.CheckListJobActivityOperator.Where(m => m.CheckListJobActivityId == activityCheck.checkListJobActivityId && m.CheckListJobOperatorId == checkListJobOperatorId).FirstOrDefault();
                            if (checkListJobAvanceOperator != null)
                            {
                                CheckListJobActivityOperatorCustom checkListJobActivityOperatorCustom = new CheckListJobActivityOperatorCustom();
                                checkListJobActivityOperatorCustom.checkListJobActivityOperatorId = checkListJobAvanceOperator.CheckListJobActivityOperatorId;
                                checkListJobActivityOperatorCustom.checkListJobOperatorId = checkListJobAvanceOperator.CheckListJobOperatorId;
                                checkListJobActivityOperatorCustom.operatorId = checkListJobAvanceOperator.OperatorId;
                                checkListJobActivityOperatorCustom.operatorScannedBarcodeNumber = checkListJobAvanceOperator.OperatorScannedBarcodeNumber;
                                checkListJobActivityOperatorCustom.barcodeAssetId = checkListJobAvanceOperator.BarcodeAssetId;
                                checkListJobActivityOperatorCustom.startTime = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobAvanceOperator.ActivityStartTime);
                                checkListJobActivityOperatorCustom.endTime = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobAvanceOperator.ActivityEndTime);
                                checkListJobActivityOperatorCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobAvanceOperator.ActivityStartTime, checkListJobAvanceOperator.ActivityEndTime);
                                checkListJobActivityOperatorCustom.operatorUpoadedDocumentId = checkListJobAvanceOperator.OperatorUpoadedDocumentId;
                                checkListJobActivityOperatorCustom.operatorUpoadedDocumentURL = appSettings.ImageUrl +
                                    (from wfd in db.DocumentUplodedMaster where wfd.IsDeleted == false && wfd.DocumentUploaderId == checkListJobAvanceOperator.OperatorUpoadedDocumentId
                                     select wfd.FileName).FirstOrDefault();
                                checkListJobActivityOperatorCustom.operatorRemark = checkListJobAvanceOperator.OperatorRemark;
                                if (checkListJobAvanceOperator.IsAdminApproved == true)
                                {
                                    checkListJobActivityOperatorCustom.approveRejectFlag = false;
                                }
                                if (checkListJobAvanceOperator.IsAdminApproved == false)
                                {
                                    checkListJobActivityOperatorCustom.approveRejectFlag = true;
                                }
                                if (checkListJobAvanceOperator.IsJobRejected == true)
                                {
                                    checkListJobActivityOperatorCustom.isJobRejected = true;
                                }
                                else
                                {
                                    checkListJobActivityOperatorCustom.isJobRejected = false;
                                }
                                checkListJobActivityDetails.checkListJobActivityOperatorCustom = checkListJobActivityOperatorCustom;
                            }

                          

                            checkListJobActivityDetailsListJob.Add(checkListJobActivityDetails);
                        }
                        if (activityCheckListJob.Count > 0)
                        {
                            checkListJobActivityBySubCategory.checkListActivityDetails = checkListJobActivityDetailsListJob;
                            checkListJobActivityBySubCategories.Add(checkListJobActivityBySubCategory);
                        }

                    }
                    checkListJobCustom.checkListJobActivityBySubCategory = checkListJobActivityBySubCategories;
                    #endregion
                    #region LOTOTO CheckListJob
                    List<CheckListJobLOTOTOCustom> checkListJobLOTOTOCustoms = new List<CheckListJobLOTOTOCustom>();
                   
                    var checkListJobTOTOListJob = (from wf in db.CheckListJobLototomaster
                                                   where wf.IsDeleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId
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
                    foreach (var checkListJobLOTOTO in checkListJobTOTOListJob)
                    {
                        CheckListJobLOTOTOCustom checkListJobLOTOTOCustom = new CheckListJobLOTOTOCustom();
                        checkListJobLOTOTOCustom.checkListJobLOTOTOId = checkListJobLOTOTO.checkListJobLOTOTOId;
                        checkListJobLOTOTOCustom.checkListJobMasterId = checkListJobLOTOTO.checkListJobMasterId;
                        checkListJobLOTOTOCustom.checkListJobGroupId = checkListJobLOTOTO.checkListJobGroupId;
                        checkListJobLOTOTOCustom.checkListJobLockStepNumber = checkListJobLOTOTO.checkListJobLockStepNumber;
                        checkListJobLOTOTOCustom.positionDescription = checkListJobLOTOTO.positionDescription;
                        checkListJobLOTOTOCustom.isLockOutRequired = checkListJobLOTOTO.isLockOutRequired;
                        checkListJobLOTOTOCustom.isTagOutRequired = checkListJobLOTOTO.isTagOutRequired;
                        checkListJobLOTOTOCustom.isTryOutRequired = checkListJobLOTOTO.isTryOutRequired;
                        checkListJobLOTOTOCustom.remarks = checkListJobLOTOTO.remarks;

                        var checkListJobLOTOTOOperator = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototoid == checkListJobLOTOTO.checkListJobLOTOTOId && m.CheckListJobOperatorId == checkListJobOperatorId).FirstOrDefault();
                        if (checkListJobLOTOTOOperator != null)
                        {
                            CheckListJobLOTOTOOperatorCustom checkListJobLOTOTOOperatorCustom = new CheckListJobLOTOTOOperatorCustom();
                            checkListJobLOTOTOOperatorCustom.checkListJobLototooperatorId = checkListJobLOTOTOOperator.CheckListJobLototooperatorId;
                            checkListJobLOTOTOOperatorCustom.checkListJobOperatorId = checkListJobLOTOTOOperator.CheckListJobOperatorId;
                            checkListJobLOTOTOOperatorCustom.operatorId = checkListJobLOTOTOOperator.OperatorId;
                            checkListJobLOTOTOOperatorCustom.overAllRemark = checkListJobLOTOTOOperator.OverAllRemark;
                            checkListJobLOTOTOOperatorCustom.lockOutDoneByOperator = checkListJobLOTOTOOperator.LockOutDoneByOperator;
                            checkListJobLOTOTOOperatorCustom.lockOutDoneByOperatorName = db.UserDetails.Where(m=>m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator).Select(m=>m.UserFullName).FirstOrDefault();
                            checkListJobLOTOTOOperatorCustom.lockOutRemark = checkListJobLOTOTOOperator.LockOutRemark;
                            checkListJobLOTOTOOperatorCustom.tagOutDoneByOperator = checkListJobLOTOTOOperator.TagOutDoneByOperator;
                            checkListJobLOTOTOOperatorCustom.tagOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                            checkListJobLOTOTOOperatorCustom.tagOutRemark = checkListJobLOTOTOOperator.TagOutRemark;


                           // checkListJobLOTOTOOperatorCustom.tryOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                            checkListJobLOTOTOOperatorCustom.tryOutDoneByOperator = checkListJobLOTOTOOperator.TryOutDoneByOperator;

                            var roleId11 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                            if (roleId11 != 2 && roleId11 != 1)
                            {
                                checkListJobLOTOTOOperatorCustom.tryOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                            }




                            checkListJobLOTOTOOperatorCustom.tryOutRemark = checkListJobLOTOTOOperator.TryOutRemark;







                            if (checkListJobLOTOTOOperator.IsAdminApproved == true)
                            {
                                checkListJobLOTOTOOperatorCustom.approveRejectFlag = false;
                            }
                            if (checkListJobLOTOTOOperator.IsAdminApproved == false)
                            {
                                checkListJobLOTOTOOperatorCustom.approveRejectFlag = true;
                            }
                            if (checkListJobLOTOTOOperator.IsJobRejected == true)
                            {
                                checkListJobLOTOTOOperatorCustom.isJobRejected = true;
                            }
                            else
                            {
                                checkListJobLOTOTOOperatorCustom.isJobRejected = false;
                            }

                            checkListJobLOTOTOCustom.checkListJobLOTOTOOperatorCustom = checkListJobLOTOTOOperatorCustom;


                                CheckListJobLOTOTOOperatorCustomAdmin checkListJobLOTOTOOperatorCustom1 = new CheckListJobLOTOTOOperatorCustomAdmin();
                                checkListJobLOTOTOOperatorCustom1.checkListJobLototooperatorId = checkListJobLOTOTOOperator.CheckListJobLototooperatorId;
                                checkListJobLOTOTOOperatorCustom1.checkListJobOperatorId = checkListJobLOTOTOOperator.CheckListJobOperatorId;
                                checkListJobLOTOTOOperatorCustom1.supervisorId = checkListJobLOTOTOOperator.OperatorId;

                                var roleId = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.OperatorId && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                                if (roleId == 2)
                                {
                                    checkListJobLOTOTOOperatorCustom1.supervisorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.OperatorId).Select(m => m.UserFullName).FirstOrDefault();

                                }


                                // checkListJobLOTOTOOperatorCustom.overAllRemark = checkListJobLOTOTOOperator.OverAllRemark;
                                checkListJobLOTOTOOperatorCustom1.lockOutDoneBy = checkListJobLOTOTOOperator.LockOutDoneByOperator;

                                var roleId1 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                                if (roleId1 == 2)
                                {
                                    checkListJobLOTOTOOperatorCustom1.lockOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                                }





                                if (checkListJobLOTOTOOperator.LockOutRemark == "True")
                                {
                                    checkListJobLOTOTOOperatorCustom1.lockOut = true;

                                }
                                else
                                {
                                    checkListJobLOTOTOOperatorCustom1.lockOut = false;
                                }

                                if (checkListJobLOTOTOOperator.TagOutRemark == "True")
                                {
                                    checkListJobLOTOTOOperatorCustom1.tagOut = true;

                                }
                                else
                                {
                                    checkListJobLOTOTOOperatorCustom1.tagOut = false;
                                }

                                if (checkListJobLOTOTOOperator.TagOutRemark == "True")
                                {
                                    checkListJobLOTOTOOperatorCustom1.tryOut = true;

                                }
                                else
                                {
                                    checkListJobLOTOTOOperatorCustom1.tryOut = false;


                            }
                                // checkListJobLOTOTOOperatorCustom.lockOut = Convert.ToBoolean(checkListJobLOTOTOOperator.LockOutRemark);
                                checkListJobLOTOTOOperatorCustom1.tagOutDoneBy = checkListJobLOTOTOOperator.TagOutDoneByOperator;

                                var roleId2 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                                if (roleId2 == 2)
                                {
                                    checkListJobLOTOTOOperatorCustom1.tagOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                                }





                                // checkListJobLOTOTOOperatorCustom.tagOut = Convert.ToBoolean(checkListJobLOTOTOOperator.TagOutRemark);


                                checkListJobLOTOTOOperatorCustom1.tryOutDoneBy = checkListJobLOTOTOOperator.TagOutDoneByOperator;

                                //checkListJobLOTOTOOperatorCustom.tryOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();


                                var roleId3 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                                if (roleId3 == 2)
                                {
                                    checkListJobLOTOTOOperatorCustom1.tryOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                                }


                                // checkListJobLOTOTOOperatorCustom.tryOut = Convert.ToBoolean(checkListJobLOTOTOOperator.TryOutRemark);

                                if (checkListJobLOTOTOOperator.LockOutRemark == "True" && checkListJobLOTOTOOperator.TagOutRemark == "True" && checkListJobLOTOTOOperator.TryOutRemark == "True")
                                {
                                    checkListJobLOTOTOOperatorCustom1.isSubmit = true;

                                }
                                else
                                {
                                    checkListJobLOTOTOOperatorCustom1.isSubmit = false;
                                }



                            if (checkListJobLOTOTOOperator.TryOutRemark != "True")
                            {

                                checkListJobLOTOTOOperatorCustom1.tryOutRemarks = checkListJobLOTOTOOperator.TryOutRemark;
                                checkListJobLOTOTOOperatorCustom1.tryOutDoneByOperatorId = checkListJobLOTOTOOperator.TryOutDoneByOperator;
                                checkListJobLOTOTOOperatorCustom1.tryOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                                checkListJobLOTOTOOperatorCustom1.overAllRemarks = checkListJobLOTOTOOperator.OverAllRemark;

                            }




                            checkListJobLOTOTOCustom.checkListJobLOTOTOCustomAdmin = checkListJobLOTOTOOperatorCustom1;


                            


                            checkListJobLOTOTOCustoms.Add(checkListJobLOTOTOCustom);
                        }
                    }

                    checkListJobCustom.checkListJobLOTOTOCustom = checkListJobLOTOTOCustoms;
                    #endregion

                    #region LOTOTO CheckListJob
                   // List<CheckListJobLOTOTOCustomAdmin> checkListJobLOTOTOCustoms1 = new List<CheckListJobLOTOTOCustomAdmin>();
                   // var checkListJobTOTOListJob1 = (from wf in db.CheckListJobLototomaster
                   //                                where wf.IsDeleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId
                   //                                select new
                   //                                {
                   //                                    checkListJobLOTOTOId = wf.CheckListJobLototoid,
                   //                                    checkListJobMasterId = wf.CheckListJobMasterId,
                   //                                    checkListJobMasterName = db.CheckListJobMaster.Where(m => m.CheckListJobId == wf.CheckListJobMasterId).Select(m => m.CheckListJobName).FirstOrDefault(),
                   //                                    checkListJobGroupId = wf.CheckListJobGroupId,
                   //                                    checkListJobGroupName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == wf.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault(),
                   //                                    checkListJobLockStepNumber = wf.CheckListJobLockStepNumber,
                   //                                    positionDescription = wf.PositionDescription,
                   //                                    isLockOutRequired = wf.IsLockOutRequired,
                   //                                    isTagOutRequired = wf.IsTagOutRequired,
                   //                                    isTryOutRequired = wf.IsTryOutRequired,
                   //                                    remarks = wf.Remarks,
                   //                                    isActive = wf.IsActive
                   //                                }).ToList();
                   // foreach (var checkListJobLOTOTO in checkListJobTOTOListJob1)
                   // {
                   //     CheckListJobLOTOTOCustomAdmin checkListJobLOTOTOCustoms11 = new CheckListJobLOTOTOCustomAdmin();
                   //     checkListJobLOTOTOCustoms11.checkListJobLOTOTOId = checkListJobLOTOTO.checkListJobLOTOTOId;
                   //     checkListJobLOTOTOCustoms11.checkListJobMasterId = checkListJobLOTOTO.checkListJobMasterId;
                   //     checkListJobLOTOTOCustoms11.checkListJobGroupId = checkListJobLOTOTO.checkListJobGroupId;
                   //     checkListJobLOTOTOCustoms11.checkListJobLockStepNumber = checkListJobLOTOTO.checkListJobLockStepNumber;
                   //     checkListJobLOTOTOCustoms11.positionDescription = checkListJobLOTOTO.positionDescription;
                   //     checkListJobLOTOTOCustoms11.isLockOutRequired = checkListJobLOTOTO.isLockOutRequired;
                   //     checkListJobLOTOTOCustoms11.isTagOutRequired = checkListJobLOTOTO.isTagOutRequired;
                   //     checkListJobLOTOTOCustoms11.isTryOutRequired = checkListJobLOTOTO.isTryOutRequired;
                   //     checkListJobLOTOTOCustoms11.remarks = checkListJobLOTOTO.remarks;

                   //     var checkListJobLOTOTOOperator1 = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototoid == checkListJobLOTOTO.checkListJobLOTOTOId && m.CheckListJobOperatorId == checkListJobOperatorId).FirstOrDefault();
                   //     if (checkListJobLOTOTOOperator1 != null)
                   //     {
                   //         CheckListJobLOTOTOOperatorCustomAdmin checkListJobLOTOTOOperatorCustom1 = new CheckListJobLOTOTOOperatorCustomAdmin();
                   //         checkListJobLOTOTOOperatorCustom1.checkListJobLototooperatorId = checkListJobLOTOTOOperator1.CheckListJobLototooperatorId;
                   //         checkListJobLOTOTOOperatorCustom1.checkListJobOperatorId = checkListJobLOTOTOOperator1.CheckListJobOperatorId;
                   //         checkListJobLOTOTOOperatorCustom1.supervisorId = checkListJobLOTOTOOperator1.OperatorId;

                   //         var roleId = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.OperatorId && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                   //         if (roleId == 2)
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.supervisorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.OperatorId).Select(m => m.UserFullName).FirstOrDefault();

                   //         }


                   //         // checkListJobLOTOTOOperatorCustom.overAllRemark = checkListJobLOTOTOOperator.OverAllRemark;
                   //         checkListJobLOTOTOOperatorCustom1.lockOutDoneBy = checkListJobLOTOTOOperator1.LockOutDoneByOperator;

                   //         var roleId1 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.LockOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                   //         if (roleId1 == 2)
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.lockOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.LockOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                   //         }





                   //         if (checkListJobLOTOTOOperator1.LockOutRemark == "True")
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.lockOut = true;

                   //         }
                   //         else
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.lockOut = false;
                   //         }

                   //         if (checkListJobLOTOTOOperator1.TagOutRemark == "True")
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.tagOut = true;

                   //         }
                   //         else
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.tagOut = false;
                   //         }

                   //         if (checkListJobLOTOTOOperator1.TryOutRemark == "True")
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.tryOut = true;

                   //         }
                   //         else
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.tryOut = false;
                   //         }
                   //         // checkListJobLOTOTOOperatorCustom.lockOut = Convert.ToBoolean(checkListJobLOTOTOOperator.LockOutRemark);
                   //         checkListJobLOTOTOOperatorCustom1.tagOutDoneBy = checkListJobLOTOTOOperator1.TagOutDoneByOperator;

                   //         var roleId2 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.TagOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                   //         if (roleId2 == 2)
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.tagOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                   //         }





                   //         // checkListJobLOTOTOOperatorCustom.tagOut = Convert.ToBoolean(checkListJobLOTOTOOperator.TagOutRemark);


                   //         checkListJobLOTOTOOperatorCustom1.tryOutDoneBy = checkListJobLOTOTOOperator1.TryOutDoneByOperator;

                   //         //checkListJobLOTOTOOperatorCustom.tryOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();


                   //         var roleId3 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.TryOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                   //         if (roleId3 == 2)
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.tryOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator1.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                   //         }


                   //         // checkListJobLOTOTOOperatorCustom.tryOut = Convert.ToBoolean(checkListJobLOTOTOOperator.TryOutRemark);

                   //         if (checkListJobLOTOTOOperator1.LockOutRemark == "True" && checkListJobLOTOTOOperator1.TagOutRemark == "True" && checkListJobLOTOTOOperator1.TryOutRemark == "True")
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.isSubmit = true;
                                
                   //         }
                   //         else
                   //         {
                   //             checkListJobLOTOTOOperatorCustom1.isSubmit = false;
                   //         }


                   //         checkListJobLOTOTOCustoms11.checkListJobLOTOTOOperatorCustom = checkListJobLOTOTOOperatorCustom1;
                   //     }

                   //     checkListJobLOTOTOCustoms1.Add(checkListJobLOTOTOCustoms11);
                   // }




                   //checkListJobCustom.checkListJobLOTOTOCustomAdmin = checkListJobLOTOTOCustoms1;
                    #endregion

                   
                    #endregion

                    obj.response = checkListJobCustom;
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


        //Mani
        //Post

        public CommonResponse AddAndEditCheckListForLOTOTO(CheckListJobLOTOTOCustom1 data, long userId = 0)
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
                        //item.Remarks = data.remarks;
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
                        // res.Remarks = data.remarks;
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



        //veeresh code
        //public CommonResponse AddAndEditCheckListJobLOTOTOAdmin(CheckListJobLOTOTOOperatorCustomNew data, long userId = 0)
        //{
        //    CommonResponse obj = new CommonResponse();
        //    try
        //    {
        //        var res = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == data.checkListJobOperatorId && m.CheckListJobLototoid == data.checkListJobLOTOTOId).FirstOrDefault();
        //        if (res == null)
        //        {
        //            try
        //            {
        //                CheckListJobLototooperator item = new CheckListJobLototooperator();
        //                item.CheckListJobOperatorId = data.checkListJobOperatorId;
        //                item.OperatorId = userId;
        //                item.OverAllRemark = data.overAllRemark;
        //                item.LockOutDoneByOperator = data.lockOutDoneBy;

        //                item.LockOutRemark =Convert.ToString( data.lockOut);
        //                item.TagOutDoneByOperator = data.tagOutDoneBy;

        //                item.TagOutRemark = Convert.ToString(data.tagOut);
        //                item.TryOutDoneByOperator = data.tryOutDoneBy;
        //                item.CheckListJobLototoid = data.checkListJobLOTOTOId;

        //                item.TryOutRemark = Convert.ToString(data.tryOut);
        //                item.IsJobRejected = false;
        //                item.IsActive = true;
        //                item.IsDeleted = false;
        //                item.IsAdminApproved = false;
        //                item.CreatedBy = userId;
        //                item.CreatedOn = DateTime.Now;
        //                db.CheckListJobLototooperator.Add(item);
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.AddedSucessfully;
        //                obj.isStatus = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //                obj.response = ResourceResponse.ExceptionMessage;
        //                obj.isStatus = false;
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                res.CheckListJobOperatorId = data.checkListJobOperatorId;
        //                res.OperatorId = userId;
        //                res.OverAllRemark = data.overAllRemark;
        //                res.LockOutDoneByOperator = data.lockOutDoneBy;
        //                res.LockOutRemark = Convert.ToString(data.lockOut);
        //                res.TagOutDoneByOperator = data.tagOutDoneBy;
        //                res.TagOutRemark = Convert.ToString(data.tagOut);
        //                res.TryOutDoneByOperator = data.tryOutDoneBy;
        //                res.CheckListJobLototoid = data.checkListJobLOTOTOId;
        //                res.TryOutRemark = Convert.ToString(data.tryOut);
        //                res.IsAdminApproved = false;
        //                res.IsJobRejected = false;
        //                res.JobRejectedReason = "";
        //                res.ModifiedBy = userId;
        //                res.ModifiedOn = DateTime.Now;
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.UpdatedSucessfully;
        //                obj.isStatus = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //                obj.response = ResourceResponse.ExceptionMessage;
        //                obj.isStatus = false;
        //            }
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


        public CommonResponse AddAndEditCheckListJobLOTOTOAdmin(CheckListJobLOTOTOOperatorCustomNew data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {

                var CKLWTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == data.checkListJobMasterId && m.CheckListJobGroupId == data.checkListJobGroupId && m.IsDeleted == false).Select(m => m.CheckListJobWrtoperatorId).FirstOrDefault();

                if (CKLWTOp != 0)

                {

                    var res = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == CKLWTOp && m.CheckListJobLototoid == data.checkListJobLOTOTOId && m.IsDeleted == false).FirstOrDefault();
                    if (res == null)
                    {

                        CheckListJobLototooperator item = new CheckListJobLototooperator();

                        item.CheckListJobOperatorId = Convert.ToInt64(CKLWTOp);
                        // item.OperatorId = data.supervisorId;
                        item.OperatorId = userId;
                        //item.OverAllRemark = data.overAllRemark;
                        item.LockOutDoneByOperator = data.lockOutDoneBy;

                        item.LockOutRemark = Convert.ToString(data.lockOut);
                        item.TagOutDoneByOperator = data.tagOutDoneBy;

                        item.TagOutRemark = Convert.ToString(data.tagOut);
                        item.TryOutDoneByOperator = data.tryOutDoneBy;
                        item.CheckListJobLototoid = data.checkListJobLOTOTOId;

                        item.TryOutRemark = Convert.ToString(data.tryOut);
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

                    else
                    {
                        res.CheckListJobOperatorId = Convert.ToInt64(CKLWTOp);
                        res.OperatorId = userId;
                        // res.OverAllRemark = data.overAllRemark;
                        res.LockOutDoneByOperator = data.lockOutDoneBy;
                        res.LockOutRemark = Convert.ToString(data.lockOut);
                        res.TagOutDoneByOperator = data.tagOutDoneBy;
                        res.TagOutRemark = Convert.ToString(data.tagOut);
                        res.TryOutDoneByOperator = data.tryOutDoneBy;
                        res.CheckListJobLototoid = data.checkListJobLOTOTOId;
                        res.TryOutRemark = Convert.ToString(data.tryOut);
                        res.IsAdminApproved = false;
                        res.IsJobRejected = false;
                        res.JobRejectedReason = "";
                        res.ModifiedBy = userId;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        obj.response = ResourceResponse.UpdatedSucessfully;
                        obj.isStatus = true;
                    }



                }
                else
                {
                    obj.response = "checkListJobWRTop not found";
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




        //Mani


        //Mani
        //public CommonResponse AddAndEditCheckListJobLOTOTOOperator(CheckListJobLOTOTOOperatorCustomNew data, long userId = 0)
        //{
        //    CommonResponse obj = new CommonResponse();
        //    try
        //    {
        //        var res = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == data.checkListJobOperatorId && m.CheckListJobLototoid == data.checkListJobLOTOTOId).FirstOrDefault();
        //        if (res == null)
        //        {
        //            try
        //            {
        //                CheckListJobLototooperator item = new CheckListJobLototooperator();
        //                item.CheckListJobOperatorId = data.checkListJobOperatorId;
        //                item.OperatorId = userId;
        //                item.OverAllRemark = data.overAllRemark;
        //                item.LockOutDoneByOperator = data.lockOutDoneByOperator;
        //                item.LockOutRemark = data.lockOutRemark;
        //                item.TagOutDoneByOperator = data.tagOutDoneByOperator;
        //                item.TagOutRemark = data.tagOutRemark;
        //                item.TryOutDoneByOperator = data.tryOutDoneByOperator;
        //                item.CheckListJobLototoid = data.checkListJobLOTOTOId;
        //                item.TryOutRemark = data.tryOutRemark;
        //                item.IsJobRejected = false;
        //                item.IsActive = true;
        //                item.IsDeleted = false;
        //                item.IsAdminApproved = false;
        //                item.CreatedBy = userId;
        //                item.CreatedOn = DateTime.Now;
        //                db.CheckListJobLototooperator.Add(item);
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.AddedSucessfully;
        //                obj.isStatus = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //                obj.response = ResourceResponse.ExceptionMessage;
        //                obj.isStatus = false;
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                res.CheckListJobOperatorId = data.checkListJobOperatorId;
        //                res.OperatorId = userId;
        //                res.OverAllRemark = data.overAllRemark;
        //                res.LockOutDoneByOperator = data.lockOutDoneByOperator;
        //                res.LockOutRemark = data.lockOutRemark;
        //                res.TagOutDoneByOperator = data.tagOutDoneByOperator;
        //                res.TagOutRemark = data.tagOutRemark;
        //                res.TryOutDoneByOperator = data.tryOutDoneByOperator;
        //                res.CheckListJobLototoid = data.checkListJobLOTOTOId;
        //                res.TryOutRemark = data.tryOutRemark;
        //                res.IsAdminApproved = false;
        //                res.IsJobRejected = false;
        //                res.JobRejectedReason = "";
        //                res.ModifiedBy = userId;
        //                res.ModifiedOn = DateTime.Now;
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.UpdatedSucessfully;
        //                obj.isStatus = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //                obj.response = ResourceResponse.ExceptionMessage;
        //                obj.isStatus = false;
        //            }
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

        //Mani








        //indivisual submit by admin

        //public CommonResponse AddAndEditCheckListJobLOTOTOByAdmin(CheckListJobLOTOTOByadmin data, long userId = 0)
        //{
        //    CommonResponse obj = new CommonResponse();
        //    try
        //    {
        //        var res = db.CheckListJobLototomaster.Where(m => m.CheckListJobLototoid == data.checkListJobOperatorId && m.CheckListJobLototoid == data.checkListJobLOTOTOId).FirstOrDefault();
        //        if (res == null)
        //        {
        //            try
        //            {
        //                CheckListJobLototooperator item = new CheckListJobLototooperator();
        //                item.CheckListJobOperatorId = data.checkListJobOperatorId;
        //                item.OperatorId = userId;
        //                item.OverAllRemark = data.overAllRemark;
        //                item.LockOutDoneByOperator = data.lockOutDoneByOperator;
        //                item.LockOutRemark = data.lockOutRemark;
        //                item.TagOutDoneByOperator = data.tagOutDoneByOperator;
        //                item.TagOutRemark = data.tagOutRemark;
        //                item.TryOutDoneByOperator = data.tryOutDoneByOperator;
        //                item.CheckListJobLototoid = data.checkListJobLOTOTOId;
        //                item.TryOutRemark = data.tryOutRemark;
        //                item.IsJobRejected = false;
        //                item.IsActive = true;
        //                item.IsDeleted = false;
        //                item.IsAdminApproved = false;
        //                item.CreatedBy = userId;
        //                item.CreatedOn = DateTime.Now;
        //                db.CheckListJobLototooperator.Add(item);
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.AddedSucessfully;
        //                obj.isStatus = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //                obj.response = ResourceResponse.ExceptionMessage;
        //                obj.isStatus = false;
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                res.CheckListJobOperatorId = data.checkListJobOperatorId;
        //                res.OperatorId = userId;
        //                res.OverAllRemark = data.overAllRemark;
        //                res.LockOutDoneByOperator = data.lockOutDoneByOperator;
        //                res.LockOutRemark = data.lockOutRemark;
        //                res.TagOutDoneByOperator = data.tagOutDoneByOperator;
        //                res.TagOutRemark = data.tagOutRemark;
        //                res.TryOutDoneByOperator = data.tryOutDoneByOperator;
        //                res.CheckListJobLototoid = data.checkListJobLOTOTOId;
        //                res.TryOutRemark = data.tryOutRemark;
        //                res.IsAdminApproved = false;
        //                res.IsJobRejected = false;
        //                res.JobRejectedReason = "";
        //                res.ModifiedBy = userId;
        //                res.ModifiedOn = DateTime.Now;
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.UpdatedSucessfully;
        //                obj.isStatus = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //                obj.response = ResourceResponse.ExceptionMessage;
        //                obj.isStatus = false;
        //            }
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






        //public CommonResponse CheckListJobOverallsubmit(int checkListJobId, int checkListJobGroupId, int checkListJobOperatorId)
        //{
        //    CommonResponse obj = new CommonResponse();
        //    try
        //    {
        //        // Flag flag = new Flag();
        //        // flag.advanceFlag = true;

        //        bool flag = false;

        //        //#region lototo
        //        //var lototoItem = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == checkListJobOperatorId &&  m.IsDeleted == false && m.IsActive == true).ToList(); 
        //        //var lototoJobIds = lototoItem.Select(m => m.CheckListJobLototooperatorId).ToList();
        //        ////List<long?> loJbIds = new List<long?>();

        //        //foreach (var item in lototoJobIds)
        //        //{
        //        //    var oneJs = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototooperatorId == item && m.IsDeleted==false).FirstOrDefault();
        //        //    if (oneJs.LockOutDoneByOperator!= null && oneJs.TagOutDoneByOperator!= null && oneJs.TryOutDoneByOperator!= null)
        //        //    {
        //        //        flag= true;
        //        //        continue;
        //        //    }
        //        //    else
        //        //    {
        //        //        flag = false;
        //        //        break;

        //        //    }


        //        //}
        //        //#endregion

        //        #region lototo
        //        var lototoItem = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
        //        var lototoJobIds = lototoItem.Select(m => m.CheckListJobLototoid).ToList();
        //        List<long?> loJbIds = new List<long?>();
        //        foreach (var item in lototoJobIds)
        //        {
        //            loJbIds.Add(item);
        //        }
        //        var lototoOpItem = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == checkListJobOperatorId && loJbIds.Contains(m.CheckListJobLototoid)).ToList();
        //        #endregion

                

        //        if (lototoItem.Count == lototoOpItem.Count)
        //        {
        //            obj.response = true;
        //            obj.isStatus = true;
        //        }




        //        //if (flag == true)
        //        //{
        //        //    obj.response = flag;
        //        //    obj.isStatus = true;
        //        //}
        //        else
        //        {
        //            obj.response = false;
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



        public CommonResponse CheckListJobOverallsubmit(int checkListJobId, int checkListJobGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
               

                bool flag = false;

                #region lototo
                var lototoItem = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
                var lototoJobIds = lototoItem.Select(m => m.CheckListJobLototoid).ToList();
                List<long?> loJbIds = new List<long?>();
                foreach (var item in lototoJobIds)
                {
                    loJbIds.Add(item);
                }


                var CKLWTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false).Select(m => m.CheckListJobWrtoperatorId).FirstOrDefault();

                if (CKLWTOp != 0)
                {
                    var lototoOpItem = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId ==Convert.ToInt64( CKLWTOp) && loJbIds.Contains(m.CheckListJobLototoid)).ToList();



                    if (lototoItem.Count <= lototoOpItem.Count)
                    {
                        obj.response = true;
                        obj.isStatus = true;
                    }

                }
                    
                #endregion

                else
                {
                    obj.response = false;
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


        public CommonResponse CheckListJobLototoCheckEnable(int checkListJobId, int checkListJobGroupId, int checkListJobOperatorId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                Flag1 flag = new Flag1();
                flag.advanceFlag = true;

                #region advance
                var advanceItem = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
                var advanceJobIds = advanceItem.Select(m => m.CheckListJobAdvanceId).ToList();
                List<long?> adJbIds = new List<long?>();
                foreach (var item in advanceJobIds)
                {
                    adJbIds.Add(item);
                }

                var CKLWTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false).Select(m => m.CheckListJobWrtoperatorId).FirstOrDefault();


                var advanceOpItem = db.CheckListJobAdvanceOperator.Where(m => m.IsDeleted == false).ToList();

                if (CKLWTOp != 0)
                {
                     advanceOpItem = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobOperatorId == CKLWTOp && adJbIds.Contains(m.CheckListJobAdvanceId)).ToList();
                }

                #endregion

                #region lototo
                var lototoItem = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
                var lototoJobIds = lototoItem.Select(m => m.CheckListJobLototoid).ToList();
                List<long?> loJbIds = new List<long?>();
                foreach (var item in lototoJobIds)
                {
                    loJbIds.Add(item);
                }



                var lototoOpItem = db.CheckListJobLototooperator.Where(m => m.IsDeleted==false).ToList();
                if (CKLWTOp != 0)
                {
                     lototoOpItem = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == CKLWTOp && loJbIds.Contains(m.CheckListJobLototoid)).ToList();
                }


                #endregion

                if (advanceItem.Count == advanceOpItem.Count)
                {
                    if (lototoItem.Count <= lototoOpItem.Count)
                    {
                        flag.lototoFlag = true;
                    }
                }

                


                obj.response = flag;
                obj.isStatus = true;

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }




        //Mani

        //Mani


        //Mani
        public CommonResponse CheckListForLototo(int checkListJobId, int checkListJobGroupId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var result = (from wf in db.CheckListJobMaster
                              where wf.CheckListJobId == checkListJobId
                              select new
                              {
                                  checkListJobId = wf.CheckListJobId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  //batchNumber = wf.BatchNumber,
                                  //processOrderNumber = wf.ProcessOrderNumber,
                                  //checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListJobName = wf.CheckListJobName,
                                  checkListJobDescription = wf.CheckListJobDescription,
                                  //checkListJobSupervisorId = wf.CheckListJobSupervisorId,
                                  //checkListJobSupervisorName = db.UserDetails.Where(m => m.UserId == wf.CheckListJobSupervisorId).Select(m => m.UserFullName).FirstOrDefault(),
                                  //checkListJobLineNumber = wf.CheckListJobLineNumber,
                                  //checkListJobLineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.CheckListJobLineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                                  //checkListShiftNumber = wf.CheckListShiftNumber,
                                  //checkListShiftName = db.ShiftMaster.Where(m => m.ShiftId == wf.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault(),
                                  //checkListStartTime = wf.CheckListStartTime,
                                  //checkListEndTime = wf.CheckListEndTime,
                                  //previousGrade = wf.PreviousGrade,
                                  //previousGradeName = db.GradeMaster.Where(m => m.GradeId == wf.PreviousGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  //currentGrade = wf.CurrentGrade,
                                  //currentGradeName = db.GradeMaster.Where(m => m.GradeId == wf.CurrentGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  //previousColor = wf.PreviousColor,
                                  //previousColorName = db.ColorMaster.Where(m => m.ColorId == wf.PreviousColor).Select(m => m.ColorName).FirstOrDefault(),
                                  //currentColor = wf.CurrentGrade,
                                  //currentColorName = db.ColorMaster.Where(m => m.ColorId == wf.CurrentColor).Select(m => m.ColorName).FirstOrDefault(),
                                  //checkListJobCategoryId = wf.CheckListJobCategoryId,
                                  //checkListJobCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListJobCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  //checkListJobTypeId = wf.CheckListJobTypeId,
                                  //checkListJobTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  //isActive = wf.IsActive,
                                  //assignedOperatorsgrp = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId && m.CheckListJobGroupId == checkListJobGroupId).ToList(),
                                  //assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                                  //jobCreatedBy = db.UserDetails.Where(m => m.UserId == wf.CreatedBy).Select(m => m.UserFullName).FirstOrDefault(),
                                  //wf.OverAllRejected,
                                  //wf.OverAllApproved,
                                  //wf.OverAllJobCompleted,
                              }).FirstOrDefault();

                if (result != null)
                {
                    #region CheckListJob Details
                    CheckListJobDetailsLotatoAdmin checkListJobCustom = new CheckListJobDetailsLotatoAdmin();
                    checkListJobCustom.checkListJobId = result.checkListJobId;
                    checkListJobCustom.checkListMasterId = result.checkListMasterId;
                   // checkListJobCustom.batchNumber = result.batchNumber;
                  //  checkListJobCustom.processOrderNumber = result.processOrderNumber;
                    //checkListJobCustom.checkListMasterName = result.checkListMasterName;
                    checkListJobCustom.checkListJobName = result.checkListJobName;
                    checkListJobCustom.checkListJobDescription = result.checkListJobDescription;
                    //checkListJobCustom.checkListJobCategoryId = result.checkListJobCategoryId;
                    //checkListJobCustom.checkListJobCategoryName = result.checkListJobCategoryName;
                    //checkListJobCustom.checkListJobTypeId = result.checkListJobTypeId;
                    //checkListJobCustom.checkListJobTypeName = result.checkListJobTypeName;
                    //checkListJobCustom.checkListJobSupervisorId = result.checkListJobSupervisorId;
                    //checkListJobCustom.checkListJobSupervisorName = result.checkListJobSupervisorName;
                    //checkListJobCustom.checkListJobLineNumber = result.checkListJobLineNumber;
                    //checkListJobCustom.checkListJobLineNumberName = result.checkListJobLineNumberName;
                    //checkListJobCustom.checkListShiftNumber = result.checkListShiftNumber;
                    //checkListJobCustom.checkListShiftName = result.checkListShiftName;
                    //checkListJobCustom.checkListStartTime = result.checkListStartTime;
                    //checkListJobCustom.checkListEndTime = result.checkListEndTime;
                    //checkListJobCustom.previousGrade = result.previousGrade;
                    //checkListJobCustom.previousGradeName = result.previousGradeName;
                    //checkListJobCustom.currentGrade = result.currentGrade;
                    //checkListJobCustom.currentGradeName = result.currentGradeName;
                    //checkListJobCustom.previousColor = result.previousColor;
                    //checkListJobCustom.previousColorName = result.previousColorName;
                    //checkListJobCustom.currentColor = result.currentColor;
                    //checkListJobCustom.currentColorName = result.currentColorName;
                    //checkListJobCustom.jobCreatedBy = result.jobCreatedBy;
                    //checkListJobCustom.OverAllRejected = result.OverAllRejected;
                    //checkListJobCustom.OverAllApproved = result.OverAllApproved;
                    //checkListJobCustom.OverAllJobCompleted = result.OverAllJobCompleted;


                    //var assignedOps = (String.Join(",", result.assignedOperators.Select(m => m.PrimaryResource).ToList()));
                    //var assignedOperatorsgrp = (String.Join(",", result.assignedOperatorsgrp.Select(m => m.PrimaryResource).ToList()));

                    //if (result.assignedOperators.Count != 0)
                    //{
                    //    string operatorIdss = assignedOps;
                    //    string operatorIdssgrp = assignedOperatorsgrp;
                    //    List<long> opIds = operatorIdss.Split(',').Select(long.Parse).ToList();
                    //    List<long> opIdsgrp = operatorIdssgrp.Split(',').Select(long.Parse).ToList();
                    //    var opItem = String.Join(",", db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                    //    var opItemAll = String.Join(",", db.UserDetails.Where(m => opIdsgrp.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                    //    checkListJobCustom.assignedOperators = opItemAll;

                    //    var checkListJobWRToperator = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == result.checkListJobId).ToList();
                    //    var checkListJobWRToperatorCheck = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == result.checkListJobId && opIds.Contains(wf.OperatorId)).ToList();
                    //    if (checkListJobWRToperatorCheck.Count == checkListJobWRToperator.Count)
                    //    {
                    //        checkListJobCustom.approveButton = true;
                    //        checkListJobCustom.rejectButton = true;
                    //        var checkListJobWRToperatorcheckapproved = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                    //        if (checkListJobWRToperatorcheckapproved != null)
                    //        {
                    //            if (checkListJobWRToperatorcheckapproved.IsAdminApproved == true || checkListJobWRToperatorcheckapproved.IsJobRejected == true)
                    //            {
                    //                checkListJobCustom.approveButton = false;
                    //                checkListJobCustom.rejectButton = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var checkListJobWRToperatorcheckapprovedrej = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == false && wf.CheckListJobIsPartialCompleted == true && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId).ToList();
                    //            if (checkListJobWRToperatorcheckapprovedrej.Count > 0)
                    //            {
                    //                checkListJobCustom.approveButton = false;
                    //                checkListJobCustom.rejectButton = false;
                    //            }
                    //            else if (checkListJobWRToperatorcheckapprovedrej.Count == 0)
                    //            {
                    //                checkListJobCustom.approveButton = false;
                    //                checkListJobCustom.rejectButton = false;
                    //            }

                    //        }
                    //    }

                    //    if ((checkListJobCustom.approveButton == false && checkListJobCustom.rejectButton == false && result.OverAllJobCompleted == true) && (result.OverAllRejected == false || result.OverAllApproved == false))
                    //    {
                    //        if (result.OverAllRejected == true || result.OverAllApproved == true)
                    //        {
                    //            checkListJobCustom.OverAllJobCompleted = false;
                    //            checkListJobCustom.OverAllJobRejected = false;
                    //        }
                    //        else
                    //        {
                    //            checkListJobCustom.OverAllJobCompleted = true;
                    //            checkListJobCustom.OverAllJobRejected = true;
                    //        }

                    //    }
                    //    else
                    //    {
                    //        checkListJobCustom.OverAllJobCompleted = false;
                    //        checkListJobCustom.OverAllJobRejected = false;
                    //    }

                    //}
                    long? checkListJobOperatorId = 0;
                    var checkListJobOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == result.checkListJobId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                    if (checkListJobOperator != null)
                    {
                        checkListJobOperatorId = checkListJobOperator.CheckListJobWrtoperatorId;
                        //try
                        //{
                        //    checkListJobCustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobStartTime);
                        //}
                        //catch (Exception ex)
                        //{ }
                        //try
                        //{
                        //    checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobEndTime);
                        //}
                        //catch (Exception ex)
                        //{
                        //}
                        //try
                        //{
                        //    checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobOperator.CheckListJobStartTime, checkListJobOperator.CheckListJobEndTime);
                        //}

                        //catch (Exception ex)
                        //{
                        //}
                    }
                    int sumbitcount = 0;
                    #region LOTOTO CheckListJob
                    List<CheckListJobLOTOTOCustomAdmin> checkListJobLOTOTOCustoms = new List<CheckListJobLOTOTOCustomAdmin>();
                    var checkListJobTOTOListJob = (from wf in db.CheckListJobLototomaster
                                                   where wf.IsDeleted == false && wf.CheckListJobMasterId == result.checkListJobId && wf.CheckListJobGroupId == checkListJobGroupId
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
                    foreach (var checkListJobLOTOTO in checkListJobTOTOListJob)
                    {
                        CheckListJobLOTOTOCustomAdmin checkListJobLOTOTOCustom = new CheckListJobLOTOTOCustomAdmin();
                        checkListJobLOTOTOCustom.checkListJobLOTOTOId = checkListJobLOTOTO.checkListJobLOTOTOId;
                        checkListJobLOTOTOCustom.checkListJobMasterId = checkListJobLOTOTO.checkListJobMasterId;
                        checkListJobLOTOTOCustom.checkListJobGroupId = checkListJobLOTOTO.checkListJobGroupId;
                        checkListJobLOTOTOCustom.checkListJobLockStepNumber = checkListJobLOTOTO.checkListJobLockStepNumber;
                        checkListJobLOTOTOCustom.positionDescription = checkListJobLOTOTO.positionDescription;
                        checkListJobLOTOTOCustom.isLockOutRequired = checkListJobLOTOTO.isLockOutRequired;
                        checkListJobLOTOTOCustom.isTagOutRequired = checkListJobLOTOTO.isTagOutRequired;
                        checkListJobLOTOTOCustom.isTryOutRequired = checkListJobLOTOTO.isTryOutRequired;
                        checkListJobLOTOTOCustom.remarks = checkListJobLOTOTO.remarks;

                        var checkListJobLOTOTOOperator = db.CheckListJobLototooperator.Where(m => m.CheckListJobLototoid == checkListJobLOTOTO.checkListJobLOTOTOId && m.CheckListJobOperatorId == checkListJobOperatorId).FirstOrDefault();
                        if (checkListJobLOTOTOOperator != null)
                        {
                            CheckListJobLOTOTOOperatorCustomAdmin checkListJobLOTOTOOperatorCustom = new CheckListJobLOTOTOOperatorCustomAdmin();
                            checkListJobLOTOTOOperatorCustom.checkListJobLototooperatorId = checkListJobLOTOTOOperator.CheckListJobLototooperatorId;
                            checkListJobLOTOTOOperatorCustom.checkListJobOperatorId = checkListJobLOTOTOOperator.CheckListJobOperatorId;
                            checkListJobLOTOTOOperatorCustom.supervisorId = checkListJobLOTOTOOperator.OperatorId;

                            var roleId = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.OperatorId && m.IsDeleted==false).Select(m => m.RoleId).FirstOrDefault();
                            if (roleId == 2)
                            {
                                checkListJobLOTOTOOperatorCustom.supervisorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.OperatorId).Select(m => m.UserFullName).FirstOrDefault();

                            }


                            // checkListJobLOTOTOOperatorCustom.overAllRemark = checkListJobLOTOTOOperator.OverAllRemark;
                            checkListJobLOTOTOOperatorCustom.lockOutDoneBy = checkListJobLOTOTOOperator.LockOutDoneByOperator;

                            var roleId1 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                            if (roleId1 == 2)
                            {
                                checkListJobLOTOTOOperatorCustom.lockOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                            }





                            if (checkListJobLOTOTOOperator.LockOutRemark =="True")
                            {
                                checkListJobLOTOTOOperatorCustom.lockOut = true;

                            }
                            else
                            {
                                checkListJobLOTOTOOperatorCustom.lockOut = false;
                            }

                            if (checkListJobLOTOTOOperator.TagOutRemark =="True")
                            {
                                checkListJobLOTOTOOperatorCustom.tagOut = true;

                            }
                            else
                            {
                                checkListJobLOTOTOOperatorCustom.tagOut = false;
                            }

                            if (checkListJobLOTOTOOperator.TryOutRemark =="True")
                            {
                                checkListJobLOTOTOOperatorCustom.tryOut = true;

                            }
                            else
                            {
                                checkListJobLOTOTOOperatorCustom.tryOut = true;
                            }
                           // checkListJobLOTOTOOperatorCustom.lockOut = Convert.ToBoolean(checkListJobLOTOTOOperator.LockOutRemark);
                            checkListJobLOTOTOOperatorCustom.tagOutDoneBy = checkListJobLOTOTOOperator.TagOutDoneByOperator;

                            var roleId2 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                            if (roleId2 == 2)
                            {
                                checkListJobLOTOTOOperatorCustom.tagOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                            }





                            // checkListJobLOTOTOOperatorCustom.tagOut = Convert.ToBoolean(checkListJobLOTOTOOperator.TagOutRemark);


                            checkListJobLOTOTOOperatorCustom.tryOutDoneBy = checkListJobLOTOTOOperator.TagOutDoneByOperator;

                            //checkListJobLOTOTOOperatorCustom.tryOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();


                            var roleId3 = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator && m.IsDeleted == false).Select(m => m.RoleId).FirstOrDefault();
                            if (roleId3 == 2)
                            {
                                checkListJobLOTOTOOperatorCustom.tryOutDoneByName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();

                            }


                            // checkListJobLOTOTOOperatorCustom.tryOut = Convert.ToBoolean(checkListJobLOTOTOOperator.TryOutRemark);

                            if (checkListJobLOTOTOOperator.LockOutRemark == "True" && checkListJobLOTOTOOperator.TagOutRemark == "True" )
                            {
                                checkListJobLOTOTOOperatorCustom.isSubmit = true;
                                sumbitcount++;
                            }
                            else
                            {
                                checkListJobLOTOTOOperatorCustom.isSubmit = false;
                            }


                            checkListJobLOTOTOCustom.checkListJobLOTOTOOperatorCustom = checkListJobLOTOTOOperatorCustom;
                        }

                        checkListJobLOTOTOCustoms.Add(checkListJobLOTOTOCustom);
                    }

                   
                  
                    var lototoItem = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
                    var lototoJobIds = lototoItem.Select(m => m.CheckListJobLototoid).ToList();
                    List<long?> loJbIds = new List<long?>();
                    foreach (var item in lototoJobIds)
                    {
                        loJbIds.Add(item);
                    }


                    var CKLWTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false).Select(m => m.CheckListJobWrtoperatorId).FirstOrDefault();

                    if (CKLWTOp != 0)
                    {
                        var lototoOpItem = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == Convert.ToInt64(CKLWTOp) && loJbIds.Contains(m.CheckListJobLototoid)).ToList();



                        if (lototoItem.Count <= sumbitcount)
                        {
                            checkListJobCustom.overAllSubmit = true;
                        }
                        else
                        {
                            checkListJobCustom.overAllSubmit = false;
                        }

                    }


                    checkListJobCustom.checkListJobLOTOTOCustom = checkListJobLOTOTOCustoms;
                    #endregion
                    #endregion

                    obj.response = checkListJobCustom;
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

        //Mani









        /// <summary>
        /// View Multiple Document 
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListJobForApproval(long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.IsJobClosed == false
                              select new
                              {
                                  checkListJobId = wf.CheckListJobId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListJobName = wf.CheckListJobName,
                                  checkListJobDescription = wf.CheckListJobDescription,
                                  checkListJobSupervisorId = wf.CheckListJobSupervisorId,
                                  checkListJobSupervisorName = db.UserDetails.Where(m => m.UserId == wf.CheckListJobSupervisorId).Select(m => m.UserFullName).FirstOrDefault(),
                                  checkListJobLineNumber = wf.CheckListJobLineNumber,
                                  checkListJobLineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.CheckListJobLineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                                  checkListShiftNumber = wf.CheckListShiftNumber,
                                  checkListShiftName = db.ShiftMaster.Where(m => m.ShiftId == wf.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault(),
                                  checkListStartTime = wf.CheckListStartTime,
                                  checkListEndTime = wf.CheckListEndTime,
                                  previousGrade = wf.PreviousGrade,
                                  previousGradeName = db.GradeMaster.Where(m => m.GradeId == wf.PreviousGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  currentGrade = wf.CurrentGrade,
                                  currentGradeName = db.GradeMaster.Where(m => m.GradeId == wf.CurrentGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  previousColor = wf.PreviousColor,
                                  previousColorName = db.ColorMaster.Where(m => m.ColorId == wf.PreviousColor).Select(m => m.ColorName).FirstOrDefault(),
                                  currentColor = wf.CurrentGrade,
                                  currentColorName = db.ColorMaster.Where(m => m.ColorId == wf.CurrentColor).Select(m => m.ColorName).FirstOrDefault(),
                                  checkListJobCategoryId = wf.CheckListJobCategoryId,
                                  checkListJobCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListJobCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListJobTypeId = wf.CheckListJobTypeId,
                                  checkListJobTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                                  isCompleted = jbop.CheckListJobIsCompleted,
                                  isClosed = jbop.IsJobClosed,
                                  isAdminApproved = jbop.IsAdminApproved,
                                  isJobRejected = jbop.IsJobRejected,
                                  checkListGroup = wf.CheckListGroup,
                                  overallrejected = wf.OverAllRejected,
                                  overallapproved = wf.OverAllApproved,
                                  overAllJobCompleted = wf.OverAllJobCompleted,
                                  estimatedTime = wf.EstimatedEndTime
                              }).OrderByDescending(m => m.checkListJobId).ToList();

                var userDetails = db.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
                if (userDetails.RoleId == 2)
                {
                    result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && wf.CheckListJobSupervisorId == userId && jbop.IsJobClosed == false 
                              select new
                              {
                                  checkListJobId = wf.CheckListJobId,
                                  checkListMasterId = wf.CheckListMasterId,
                                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                                  checkListJobName = wf.CheckListJobName,
                                  checkListJobDescription = wf.CheckListJobDescription,
                                  checkListJobSupervisorId = wf.CheckListJobSupervisorId,
                                  checkListJobSupervisorName = db.UserDetails.Where(m => m.UserId == wf.CheckListJobSupervisorId).Select(m => m.UserFullName).FirstOrDefault(),
                                  checkListJobLineNumber = wf.CheckListJobLineNumber,
                                  checkListJobLineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.CheckListJobLineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                                  checkListShiftNumber = wf.CheckListShiftNumber,
                                  checkListShiftName = db.ShiftMaster.Where(m => m.ShiftId == wf.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault(),
                                  checkListStartTime = wf.CheckListStartTime,
                                  checkListEndTime = wf.CheckListEndTime,
                                  previousGrade = wf.PreviousGrade,
                                  previousGradeName = db.GradeMaster.Where(m => m.GradeId == wf.PreviousGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  currentGrade = wf.CurrentGrade,
                                  currentGradeName = db.GradeMaster.Where(m => m.GradeId == wf.CurrentGrade).Select(m => m.GradeName).FirstOrDefault(),
                                  previousColor = wf.PreviousColor,
                                  previousColorName = db.ColorMaster.Where(m => m.ColorId == wf.PreviousColor).Select(m => m.ColorName).FirstOrDefault(),
                                  currentColor = wf.CurrentGrade,
                                  currentColorName = db.ColorMaster.Where(m => m.ColorId == wf.CurrentColor).Select(m => m.ColorName).FirstOrDefault(),
                                  checkListJobCategoryId = wf.CheckListJobCategoryId,
                                  checkListJobCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListJobCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                  checkListJobTypeId = wf.CheckListJobTypeId,
                                  checkListJobTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                  isActive = wf.IsActive,
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                                  isCompleted = jbop.CheckListJobIsCompleted,
                                  isClosed = jbop.IsJobClosed,
                                  isAdminApproved=jbop.IsAdminApproved,
                                  isJobRejected = jbop.IsJobRejected,
                                  checkListGroup = wf.CheckListGroup,
                                  overallrejected = wf.OverAllRejected,
                                  overallapproved = wf.OverAllApproved,
                                  overAllJobCompleted = wf.OverAllJobCompleted,
                                  estimatedTime = wf.EstimatedEndTime
                              }).OrderByDescending(m => m.checkListJobId).ToList();

                }
                if (result.Count() != 0)
                {
                    List<CheckListJobcustoms> checkListJobcustoms = new List<CheckListJobcustoms>();
                    foreach (var item in result)
                    {
                        CheckListJobcustoms checkListJobcustom = new CheckListJobcustoms();
                        checkListJobcustom.checkListJobId = item.checkListJobId;
                        checkListJobcustom.checkListMasterId = item.checkListMasterId;
                        checkListJobcustom.checkListJobName = item.checkListJobName;
                        checkListJobcustom.checkListJobDescription = item.checkListJobDescription;
                        checkListJobcustom.checkListJobCategoryId = item.checkListJobCategoryId;
                        checkListJobcustom.checkListJobTypeId = item.checkListJobTypeId;
                        checkListJobcustom.checkListJobSupervisorId = item.checkListJobSupervisorId;
                        checkListJobcustom.checkListJobLineNumber = item.checkListJobLineNumber;
                        checkListJobcustom.checkListShiftNumber = item.checkListShiftNumber;
                        checkListJobcustom.checkListStartTime = item.checkListStartTime;
                        checkListJobcustom.checkListEndTime = item.checkListEndTime;
                        checkListJobcustom.previousGrade = item.previousGrade;
                        checkListJobcustom.currentGrade = item.currentGrade;
                        checkListJobcustom.previousColor = item.previousColor;
                        checkListJobcustom.currentColor = item.currentColor;
                        checkListJobcustom.checkListMasterName = item.checkListMasterName;
                        checkListJobcustom.checkListJobSupervisorName = item.checkListJobSupervisorName;
                        checkListJobcustom.checkListJobLineNumberName = item.checkListJobLineNumberName;
                        checkListJobcustom.checkListShiftName = item.checkListShiftName;
                        checkListJobcustom.checkListStartTimeAMPM = item.checkListStartTime.ToString();
                        checkListJobcustom.checkListEndTimeAMPM = item.checkListEndTime.ToString();
                        checkListJobcustom.previousGradeName = item.previousGradeName;
                        checkListJobcustom.currentGradeName = item.currentGradeName;
                        checkListJobcustom.previousColorName = item.previousColorName;
                        checkListJobcustom.currentColorName = item.currentColorName;
                        checkListJobcustom.checkListJobCategoryName = item.checkListJobCategoryName;
                        checkListJobcustom.checkListJobTypeName = item.checkListJobTypeName;
                        checkListJobcustom.estimatedTime = item.estimatedTime;
                        checkListJobcustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListStartTime);
                        checkListJobcustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListEndTime);
                        bool isNotYetStarted = false;

                        var assignedOps = (String.Join(",", item.assignedOperators.Select(m => m.PrimaryResource).ToList()));
                        if (item.assignedOperators.Count != 0)
                        {
                            string operatorIds = assignedOps;
                            List<long> opId = operatorIds.Split(',').Select(long.Parse).ToList();
                            var opItem = String.Join(",", db.UserDetails.Where(m => opId.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                            checkListJobcustom.assignedOperators = opItem;
                            foreach (var its in opId)
                            {
                                var ress = db.CheckListJobWrtoperator.Where(m => m.OperatorId == its && m.CheckListJobMasterId == item.checkListJobId).FirstOrDefault();
                                if (ress != null)
                                {
                                    isNotYetStarted = true;
                                    break;
                                }
                            }
                            var checkListJobWRToperator = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.IsAdminApproved == true && wf.CheckListJobMasterId == item.checkListJobId).ToList();

                            var checkListJobWRToperatorCheck = db.CheckListJobWrtoperator.Where(wf => wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false && wf.CheckListJobMasterId == item.checkListJobId && opId.Contains(wf.OperatorId)).ToList();

                            if (checkListJobWRToperatorCheck.Count == checkListJobWRToperator.Count && checkListJobWRToperator.Count !=0 && checkListJobWRToperatorCheck.Count!= 0 && item.overAllJobCompleted == true && item.overallapproved == true)
                            {
                                checkListJobcustom.closeButtonEnable = true;
                                //checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(item.isClosed, item.isAdminApproved, item.isCompleted, item.isJobRejected, isNotYetStarted);
                            }
                            else
                            {
                                checkListJobcustom.closeButtonEnable = false;
                                //checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(item.isClosed, item.isAdminApproved, false, item.isJobRejected, isNotYetStarted);
                            }
                            checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(item.isClosed, item.overallapproved, item.overAllJobCompleted, item.overallrejected, isNotYetStarted);
                        }
                       
                      

                        checkListJobcustoms.Add(checkListJobcustom);

                    }
                    checkListJobcustoms = checkListJobcustoms.GroupBy(m=>m.checkListJobId).Select(m => m.First()).ToList();                                                   
                    obj.response = checkListJobcustoms;
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
        /// View Multiple Check List Group By Check List Job Master Id
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListGroupByCheckListJobMasterId(int checkListJobMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var advanceCheckList = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId).Select(m => m.CheckListJobGroupId).Distinct().ToList();
                var activityCheckList = db.CheckListJobActivityMaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId).Select(m => m.CheckListJobGroupId).Distinct().ToList();
                var lototoCheckList = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId).Select(m => m.CheckListJobGroupId).Distinct().ToList();

                var finalList = advanceCheckList.Union(activityCheckList).Union(lototoCheckList).OrderBy(x => x.Value).ToList();

                var result = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false && finalList.Contains(wf.CheckListGroupId)
                              select new
                              {
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = wf.CheckListGroupName,
                                  checkListGroupDescription = wf.CheckListGroupDescription,
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
        /// View Multiple Check List Group By Check List Job Master Id After Job Completion
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListGroupByCheckListJobMasterIdAfterJobCompletion(int checkListJobMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var advanceCheckList = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId).Select(m => m.CheckListJobGroupId).Distinct().ToList();
                var activityCheckList = db.CheckListJobActivityMaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId).Select(m => m.CheckListJobGroupId).Distinct().ToList();
                var lototoCheckList = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId).Select(m => m.CheckListJobGroupId).Distinct().ToList();

                var finalList = advanceCheckList.Union(activityCheckList).Union(lototoCheckList).OrderBy(x => x.Value).ToList();
                var checkListJobAssignedResourceMaster = (from wf in db.CheckListJobWrtoperator
                                                          where wf.CheckListJobMasterId == checkListJobMasterId && wf.IsDeleted == false && wf.CheckListJobIsCompleted == true && wf.CheckListJobIsPartialCompleted == false
                                                          select wf.CheckListJobGroupId).ToList();
                

                var result = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false && checkListJobAssignedResourceMaster.Contains(wf.CheckListGroupId)
                              select new
                              {
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = wf.CheckListGroupName,
                                  checkListGroupDescription = wf.CheckListGroupDescription,
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
        /// Close Job By CheckList Job MasterId
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        public CommonResponse CloseJobByCheckListJobMasterId(int checkListJobMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var checkListJobWrtoperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == checkListJobMasterId).ToList();
               
                if (checkListJobWrtoperator != null)
                {
                    foreach (var item in checkListJobWrtoperator)
                    {
                        item.IsJobClosed = true;
                        db.SaveChanges();
                    }
                    obj.response = ResourceResponse.JobClosedSuccessfully;
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
        /// View Multiple Supervisor For Reassigning Job
        /// </summary>
        /// <param name="superVisorId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleSupervisorForReassigningJob(int superVisorId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var check = (from wf in db.UserDetails
                             where wf.UserId != superVisorId && wf.IsDeleted == false && wf.IsActive == true
                             select new
                             {
                                 userId = wf.UserId,
                                 userFirstName = wf.UserFirstName,
                                 userFullName = wf.UserFullName,
                                 userLastName = wf.UserLastName,
                                 roleId = wf.RoleId,
                                 userName = wf.UserName,
                                 emailId = wf.EmailId,
                                 phoneNumber = wf.PhoneNumber
                             }).ToList();
                if (check.Count > 0)
                {
                    obj.isStatus = true;
                    obj.response = check;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception exception)
            {
                log.Error(exception);
                if (exception.InnerException != null)
                {
                    log.Error(exception.InnerException.ToString());
                }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Reassigning Job To Supervisor
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="superVisorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ReassigningJobToSupervisor(int checkListJobId, int superVisorId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var master = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobId && m.IsDeleted == false && m.IsActive == true).FirstOrDefault();
                if (master != null)
                {
                    CheckListJobMasterHistory checkListJobMasterHistory = new CheckListJobMasterHistory();
                    checkListJobMasterHistory.CheckListJobId = master.CheckListJobId;
                    checkListJobMasterHistory.CheckListMasterId = master.CheckListMasterId;
                    checkListJobMasterHistory.CheckListJobDescription = master.CheckListJobDescription;
                    checkListJobMasterHistory.CheckListJobCategoryId = master.CheckListJobCategoryId;
                    checkListJobMasterHistory.CheckListJobTypeId = master.CheckListJobTypeId;
                    checkListJobMasterHistory.CheckListJobSupervisorId = master.CheckListJobSupervisorId;
                    checkListJobMasterHistory.CheckListJobLineNumber = master.CheckListJobLineNumber;
                    checkListJobMasterHistory.CheckListShiftNumber = master.CheckListShiftNumber;
                    checkListJobMasterHistory.CheckListStartTime = master.CheckListStartTime;
                    checkListJobMasterHistory.CheckListEndTime = master.CheckListEndTime;
                    checkListJobMasterHistory.PreviousGrade = master.PreviousGrade;
                    checkListJobMasterHistory.CurrentGrade = master.CurrentGrade;
                    checkListJobMasterHistory.CheckListJobName = master.CheckListJobName;
                    checkListJobMasterHistory.PreviousColor = master.PreviousColor;
                    checkListJobMasterHistory.CurrentColor = master.CurrentColor;
                    checkListJobMasterHistory.CheckListGroup = master.CheckListGroup;
                    checkListJobMasterHistory.BatchNumber = master.BatchNumber;
                    checkListJobMasterHistory.ProcessOrderNumber = master.ProcessOrderNumber;
                    checkListJobMasterHistory.OverAllApproved = master.OverAllApproved;
                    checkListJobMasterHistory.OverAllRejected = master.OverAllRejected;
                    checkListJobMasterHistory.OverAllJobCompleted = master.OverAllJobCompleted;
                    checkListJobMasterHistory.IsActive = master.IsActive;
                    checkListJobMasterHistory.IsDeleted = master.IsDeleted;
                    checkListJobMasterHistory.CreatedBy = master.CreatedBy;
                    checkListJobMasterHistory.CreatedOn = DateTime.Now;
                    checkListJobMasterHistory.EstimatedEndTime = master.EstimatedEndTime;
                    checkListJobMasterHistory.ReassignedBy = userId;
                    checkListJobMasterHistory.ReassignedDate = DateTime.Now;
                    db.CheckListJobMasterHistory.Add(checkListJobMasterHistory);
                    db.SaveChanges();

                    master.CheckListJobSupervisorId = superVisorId;
                    master.ModifiedOn = DateTime.Now;
                    master.ModifiedBy = userId;
                    db.SaveChanges();
                    obj.isStatus = true;
                    obj.response = "SuperVisor Reassigned Successfully";
                }
            }
            catch (Exception exception)
            {
                log.Error(exception);
                if (exception.InnerException != null)
                {
                    log.Error(exception.InnerException.ToString());
                }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

    }
}
