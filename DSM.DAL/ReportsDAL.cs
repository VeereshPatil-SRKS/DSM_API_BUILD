using DSM.DAL.App_Start;
using DSM.DAL.Helpers;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobMasterEntity;
using static DSM.EntityModels.CheckListSupervisorApprovalEntity;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.ReportsEntity;

namespace DSM.DAL
{
    public class ReportsDAL : IReports
    {
        DSMContext db = new DSMContext();
        private readonly AppSettings appSettings;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ReportsDAL));
        public static IConfiguration configuration;


        public ReportsDAL(DSMContext _db, IOptions<AppSettings> _appSettings, IConfiguration _configuration)
        {
            db = _db;
            appSettings = _appSettings.Value;
            configuration = _configuration;
        }
       
        #region Reports
        /// <summary>
        /// View Multiple Check List Job For Reports
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListJobForReports(string checkListJobStartTime, string checkListJobEndTime, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();

            #region date time conerstion
            DateTime fromDate = DateTime.Now;

            try
            {
                string[] strArray = checkListJobStartTime.Split('/');
                string[] textArray1 = new string[] { (string)strArray[2], "-", (string)strArray[1], "-", (string)strArray[0] };
                fromDate = Convert.ToDateTime(string.Concat((string[])textArray1));
            }
            catch
            {
                fromDate = Convert.ToDateTime(checkListJobStartTime);
            }
            DateTime toDate = DateTime.Now;
            try
            {
                string[] strArray2 = checkListJobEndTime.Split('/');
                string[] textArray2 = new string[] { (string)strArray2[2], "-", (string)strArray2[1], "-", (string)strArray2[0] };
                toDate = Convert.ToDateTime(string.Concat((string[])textArray2));
            }
            catch
            {
                toDate = Convert.ToDateTime(checkListJobEndTime);
            }

            #endregion

            try
            {
                long typeId = commonFunction.GetTypeDetails(appSettings);
                #region old code
                //var result = (from wf in db.CheckListJobMaster
                //              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                //              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && jbop.IsJobClosed == true // && wf.CheckListJobTypeId == typeId
                //              select new
                //              {
                //                  checkListJobId = wf.CheckListJobId,
                //                  checkListMasterId = wf.CheckListMasterId,
                //                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                //                  checkListJobName = wf.CheckListJobName,
                //                  checkListJobDescription = wf.CheckListJobDescription,
                //                  checkListJobSupervisorId = wf.CheckListJobSupervisorId,
                //                  checkListJobSupervisorName = db.UserDetails.Where(m => m.UserId == wf.CheckListJobSupervisorId).Select(m => m.UserFullName).FirstOrDefault(),
                //                  checkListJobLineNumber = wf.CheckListJobLineNumber,
                //                  checkListJobLineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.CheckListJobLineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                //                  checkListShiftNumber = wf.CheckListShiftNumber,
                //                  checkListShiftName = db.ShiftMaster.Where(m => m.ShiftId == wf.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault(),
                //                  checkListStartTime = wf.CheckListStartTime,
                //                  checkListEndTime = wf.CheckListEndTime,
                //                  previousGrade = wf.PreviousGrade,
                //                  previousGradeName = db.GradeMaster.Where(m => m.GradeId == wf.PreviousGrade).Select(m => m.GradeName).FirstOrDefault(),
                //                  currentGrade = wf.CurrentGrade,
                //                  currentGradeName = db.GradeMaster.Where(m => m.GradeId == wf.CurrentGrade).Select(m => m.GradeName).FirstOrDefault(),
                //                  previousColor = wf.PreviousColor,
                //                  previousColorName = db.ColorMaster.Where(m => m.ColorId == wf.PreviousColor).Select(m => m.ColorName).FirstOrDefault(),
                //                  currentColor = wf.CurrentGrade,
                //                  currentColorName = db.ColorMaster.Where(m => m.ColorId == wf.CurrentColor).Select(m => m.ColorName).FirstOrDefault(),
                //                  checkListJobCategoryId = wf.CheckListJobCategoryId,
                //                  checkListJobCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListJobCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                //                  checkListJobTypeId = wf.CheckListJobTypeId,
                //                  checkListJobTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                //                  isActive = wf.IsActive,
                //                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                //                  isCompleted = jbop.CheckListJobIsCompleted,
                //                  isClosed = jbop.IsJobClosed,
                //                  isAdminApproved = jbop.IsAdminApproved,
                //                  isJobRejected = jbop.IsJobRejected,
                //                  overallrejected = wf.OverAllRejected,
                //                  overallapproved = wf.OverAllApproved
                //              }).ToList();

                //var userDetails = db.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
                //if (userDetails.RoleId == 2)
                //{
                //    result = (from wf in db.CheckListJobMaster
                //              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                //              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && wf.CheckListJobSupervisorId == userId
                //              select new
                //              {
                //                  checkListJobId = wf.CheckListJobId,
                //                  checkListMasterId = wf.CheckListMasterId,
                //                  checkListMasterName = db.CheckListMaster.Where(m => m.CheckListId == wf.CheckListMasterId).Select(m => m.CheckListName).FirstOrDefault(),
                //                  checkListJobName = wf.CheckListJobName,
                //                  checkListJobDescription = wf.CheckListJobDescription,
                //                  checkListJobSupervisorId = wf.CheckListJobSupervisorId,
                //                  checkListJobSupervisorName = db.UserDetails.Where(m => m.UserId == wf.CheckListJobSupervisorId).Select(m => m.UserFullName).FirstOrDefault(),
                //                  checkListJobLineNumber = wf.CheckListJobLineNumber,
                //                  checkListJobLineNumberName = db.LineNumberMaster.Where(m => m.LineNumberId == wf.CheckListJobLineNumber).Select(m => m.LineNumberName).FirstOrDefault(),
                //                  checkListShiftNumber = wf.CheckListShiftNumber,
                //                  checkListShiftName = db.ShiftMaster.Where(m => m.ShiftId == wf.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault(),
                //                  checkListStartTime = wf.CheckListStartTime,
                //                  checkListEndTime = wf.CheckListEndTime,
                //                  previousGrade = wf.PreviousGrade,
                //                  previousGradeName = db.GradeMaster.Where(m => m.GradeId == wf.PreviousGrade).Select(m => m.GradeName).FirstOrDefault(),
                //                  currentGrade = wf.CurrentGrade,
                //                  currentGradeName = db.GradeMaster.Where(m => m.GradeId == wf.CurrentGrade).Select(m => m.GradeName).FirstOrDefault(),
                //                  previousColor = wf.PreviousColor,
                //                  previousColorName = db.ColorMaster.Where(m => m.ColorId == wf.PreviousColor).Select(m => m.ColorName).FirstOrDefault(),
                //                  currentColor = wf.CurrentGrade,
                //                  currentColorName = db.ColorMaster.Where(m => m.ColorId == wf.CurrentColor).Select(m => m.ColorName).FirstOrDefault(),
                //                  checkListJobCategoryId = wf.CheckListJobCategoryId,
                //                  checkListJobCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wf.CheckListJobCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                //                  checkListJobTypeId = wf.CheckListJobTypeId,
                //                  checkListJobTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                //                  isActive = wf.IsActive,
                //                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                //                  isCompleted = jbop.CheckListJobIsCompleted,
                //                  isClosed = jbop.IsJobClosed,
                //                  isAdminApproved = jbop.IsAdminApproved,
                //                  isJobRejected = jbop.IsJobRejected,
                //                  overallrejected = wf.OverAllRejected,
                //                  overallapproved = wf.OverAllApproved
                //              }).ToList();

                //}
                #endregion

                var result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && jbop.IsJobClosed == true 
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

                if ((checkListJobStartTime == null) && (checkListJobEndTime == null))
                {
                    result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && jbop.IsJobClosed == true
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
                                  where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true &&
                                  //wf.CheckListJobSupervisorId == userId &&
                                  jbop.IsJobClosed == true
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

                    }

                }
                else if ((checkListJobStartTime != null) && (checkListJobEndTime == null))
                {
                    result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && jbop.IsJobClosed == true && jbop.CheckListJobStartTime >= fromDate
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
                                  where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true &&        // wf.CheckListJobSupervisorId == userId && 
                                  jbop.IsJobClosed == true && jbop.CheckListJobStartTime >= fromDate
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

                    }

                }
                else if ((checkListJobStartTime == null) && (checkListJobEndTime != null))
                {
                    result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && jbop.IsJobClosed == true && jbop.CheckListJobEndTime <= toDate
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
                                  where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true &&        // wf.CheckListJobSupervisorId == userId && 
                                  jbop.IsJobClosed == true && jbop.CheckListJobEndTime <= toDate
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

                    }

                }
                else if ((checkListJobStartTime != null) && (checkListJobEndTime != null))
                {
                    result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && jbop.IsJobClosed == true && jbop.CheckListJobStartTime >= fromDate && jbop.CheckListJobEndTime <= toDate
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
                                  where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true &&       //wf.CheckListJobSupervisorId == userId &&
                                  jbop.IsJobClosed == true && jbop.CheckListJobStartTime >= fromDate && jbop.CheckListJobEndTime <= toDate
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

                    }

                }

                if (result.Count() != 0)
                {
                    List<CheckListJobcustoms> checkListJobcustoms = new List<CheckListJobcustoms>();
                    foreach (var item in result)
                    {
                        CheckListJobcustoms checkListJobcustom = new CheckListJobcustoms();
                        checkListJobcustom.checkListJobId = item.checkListJobId;
                        checkListJobcustom.checkListMasterId = item.checkListMasterId;
                        //checkListJobcustom.checkListJobName = item.checkListJobName;
                        checkListJobcustom.checkListJobName = item.checkListJobName + "_" + item.checkListJobLineNumberName + "_" + item.checkListShiftName + "_" + item.checkListStartTime.ToString();
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
                        checkListJobcustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListStartTime);
                        checkListJobcustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListEndTime);
                        bool isNotYetStarted = false;
                        if (item.assignedOperators.Count != 0)
                        {
                            string operatorIds = (String.Join(",", item.assignedOperators.Select(m => m.PrimaryResource).ToList()));
                            List<long> opId = operatorIds.Split(',').Select(long.Parse).ToList();
                            var opItem = String.Join(",", db.UserDetails.Where(m => opId.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                            checkListJobcustom.assignedOperators = opItem;
                            foreach (var its in opId)
                            {
                                var ress = db.CheckListJobWrtoperator.Where(m => m.OperatorId == its).FirstOrDefault();
                                if (ress != null)
                                {
                                    isNotYetStarted = true;
                                    break;
                                }
                            }
                        }
                        checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(item.isClosed, item.overallapproved, item.isCompleted, item.overallrejected, isNotYetStarted);
                        checkListJobcustoms.Add(checkListJobcustom);

                    }
                    checkListJobcustoms = checkListJobcustoms.GroupBy(m => m.checkListJobId).Select(m => m.First()).ToList();
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
        /// Check List For Supervisor Approval
        /// </summary>
        /// <param name="checkListSupervisorEntity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckListForSupervisorApproval(int checkListJobId, int checkListJobGroupId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                long typeId = commonFunction.GetTypeDetails(appSettings);
                var result = (from wf in db.CheckListJobMaster
                              where wf.CheckListJobId == checkListJobId && wf.CheckListJobTypeId == typeId
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
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).ToList(),
                                  jobCreatedBy = db.UserDetails.Where(m => m.UserId == wf.CreatedBy).Select(m => m.UserFullName).FirstOrDefault(),
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

                    if (result.assignedOperators.Count != 0)
                    {
                        string operatorIdss = (String.Join(",", result.assignedOperators.Select(m => m.PrimaryResource).ToList()));
                        List<long> opIds = operatorIdss.Split(',').Select(long.Parse).ToList();
                        var opItem = String.Join(",", db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                        checkListJobCustom.assignedOperators = opItem;
                    }
                    long? checkListJobOperatorId = 0;
                    var checkListJobOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == result.checkListJobId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                    checkListJobOperatorId = checkListJobOperator.CheckListJobWrtoperatorId;
                    checkListJobCustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobStartTime);
                    //checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobEndTime);
                    checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.JobApprovedTime);
                    //checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobOperator.CheckListJobStartTime, checkListJobOperator.CheckListJobEndTime);
                    checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobOperator.CheckListJobStartTime, checkListJobOperator.JobApprovedTime);

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
                                checkListJobActivityOperatorCustom.operatorUpoadedDocumentId = checkListJobAvanceOperator.OperatorUpoadedDocumentId;
                                checkListJobActivityOperatorCustom.startTime = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobAvanceOperator.ActivityStartTime);
                                checkListJobActivityOperatorCustom.endTime = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobAvanceOperator.ActivityEndTime);
                                checkListJobActivityOperatorCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobAvanceOperator.ActivityStartTime, checkListJobAvanceOperator.ActivityEndTime);
                                string opDashboard = appSettings.ImageUrlSave +"\\"+
                                    (from wfd in db.DocumentUplodedMaster
                                     where wfd.IsDeleted == false && wfd.DocumentUploaderId == checkListJobAvanceOperator.OperatorUpoadedDocumentId
                                     select wfd.FileName).FirstOrDefault();
                                try
                                {
                                    string base64Image = commonFunction.FileToBase64(opDashboard);
                                    checkListJobActivityOperatorCustom.operatorUpoadedDocumentURL = base64Image;
                                }
                                catch (Exception ex)
                                { }

                                checkListJobActivityOperatorCustom.operatorRemark = checkListJobAvanceOperator.OperatorRemark;
                                if (checkListJobAvanceOperator.IsAdminApproved == true)
                                {
                                    checkListJobActivityOperatorCustom.approveRejectFlag = false;
                                }
                                if (checkListJobAvanceOperator.IsAdminApproved == false)
                                {
                                    checkListJobActivityOperatorCustom.approveRejectFlag = true;
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
                            checkListJobLOTOTOOperatorCustom.lockOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                            checkListJobLOTOTOOperatorCustom.lockOutRemark = checkListJobLOTOTOOperator.LockOutRemark;
                            checkListJobLOTOTOOperatorCustom.tagOutDoneByOperator = checkListJobLOTOTOOperator.TagOutDoneByOperator;
                            checkListJobLOTOTOOperatorCustom.tagOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                            checkListJobLOTOTOOperatorCustom.tagOutRemark = checkListJobLOTOTOOperator.TagOutRemark;
                            checkListJobLOTOTOOperatorCustom.tryOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                            checkListJobLOTOTOOperatorCustom.tryOutDoneByOperator = checkListJobLOTOTOOperator.TryOutDoneByOperator;
                            checkListJobLOTOTOOperatorCustom.tryOutRemark = checkListJobLOTOTOOperator.TryOutRemark;

                            if (checkListJobLOTOTOOperator.IsAdminApproved == true)
                            {
                                checkListJobLOTOTOOperatorCustom.approveRejectFlag = false;
                            }
                            if (checkListJobLOTOTOOperator.IsAdminApproved == false)
                            {
                                checkListJobLOTOTOOperatorCustom.approveRejectFlag = true;
                            }

                            checkListJobLOTOTOCustom.checkListJobLOTOTOOperatorCustom = checkListJobLOTOTOOperatorCustom;
                        }

                        checkListJobLOTOTOCustoms.Add(checkListJobLOTOTOCustom);
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

        /// <summary>
        /// CheckListForSupervisorApprovalAll
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckListForSupervisorApprovalAll(int checkListJobId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                long typeId = commonFunction.GetTypeDetails(appSettings);
                var result = (from wf in db.CheckListJobMaster
                              where wf.CheckListJobId == checkListJobId //&& wf.CheckListJobTypeId == typeId
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
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId ).ToList(),
                                  jobCreatedBy = db.UserDetails.Where(m => m.UserId == wf.CreatedBy).Select(m => m.UserFullName).FirstOrDefault(),
                                  checkListGroup = wf.CheckListGroup
                              }).FirstOrDefault();

                if (result != null)
                {
                    #region CheckListJob Details
                    CheckListJobDetailsAll checkListJobCustom = new CheckListJobDetailsAll();
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

                    checkListJobCustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", result.checkListStartTime);
                    checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", result.checkListEndTime);
                    checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(result.checkListStartTime, result.checkListEndTime);

                    if (result.assignedOperators.Count != 0)
                    {
                        string operatorIdss = (String.Join(",", result.assignedOperators.Select(m => m.PrimaryResource).ToList()));
                        List<long> opIds = operatorIdss.Split(',').Select(long.Parse).Distinct().ToList();
                        var opItem = String.Join(",", db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                        checkListJobCustom.assignedOperators = opItem;
                        var allOpList = db.UserDetails.Where(m => m.RoleId == 3 && m.IsDeleted == false && m.IsActive == true).ToList();
                        if (opIds.Count == allOpList.Count)
                        {
                            var ckJbWRTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == result.checkListJobId).Select(m=>m.OperatorId).ToList();
                            opItem = String.Join(",", db.UserDetails.Where(m => ckJbWRTOp.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                            checkListJobCustom.assignedOperators = opItem;
                        }
                    }
                    List<long> grpIds = result.checkListGroup.Split(',').Select(long.Parse).ToList();
                    List<GroupItem> groupItems = new List<GroupItem>();
                    int ctr = 0;
                    DateTime st = DateTime.Now;
                    foreach (var grpId in grpIds)
                    {
                        GroupItem groupItem = new GroupItem();
                        long checkListJobGroupId = grpId;
                        long? checkListJobOperatorId = 0;

                        groupItem.checkListJobGroupId = checkListJobGroupId;

                        groupItem.checkListJobGroupName = db.CheckListGroupMaster.Where(m=>m.CheckListGroupId == checkListJobGroupId).Select(m=>m.CheckListGroupName).FirstOrDefault();

                        var checkListJobOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == result.checkListJobId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();

                        checkListJobOperatorId = checkListJobOperator.CheckListJobWrtoperatorId;

                        groupItem.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobStartTime);
                        //checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobEndTime);
                        groupItem.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobEndTime);
                        //checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobOperator.CheckListJobStartTime, checkListJobOperator.CheckListJobEndTime);
                        groupItem.totalTimeTook = commonFunction.GetDateDifference(checkListJobOperator.CheckListJobStartTime, checkListJobOperator.CheckListJobEndTime);

                        //if (ctr == 0 || st > checkListJobOperator.CheckListJobStartTime)
                        //{
                        //    st = Convert.ToDateTime(checkListJobOperator.CheckListJobStartTime);
                        //    checkListJobCustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.CheckListJobStartTime);
                        //}

                        //checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobOperator.JobApprovedTime);
                        //checkListJobCustom.totalTimeTook = commonFunction.GetDateDifference(st, checkListJobOperator.JobApprovedTime);

                       


                        ctr++;

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

                                checkListJobAdvanceCustom.checkListJobAdvanceOperatorCustom = checkListJobAdvanceOperatorCustom;
                            }
                            checkListJobAdvanceCustomListJob.Add(checkListJobAdvanceCustom);
                        }
                        groupItem.checkListJobAdvanceCustom = checkListJobAdvanceCustomListJob;
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
                                    checkListJobActivityOperatorCustom.operatorUpoadedDocumentId = checkListJobAvanceOperator.OperatorUpoadedDocumentId;
                                    checkListJobActivityOperatorCustom.startTime = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobAvanceOperator.ActivityStartTime);
                                    checkListJobActivityOperatorCustom.endTime = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", checkListJobAvanceOperator.ActivityEndTime);
                                    checkListJobActivityOperatorCustom.totalTimeTook = commonFunction.GetDateDifference(checkListJobAvanceOperator.ActivityStartTime, checkListJobAvanceOperator.ActivityEndTime);
                                    string opDashboard = appSettings.ImageUrlSave + "\\" +
                                        (from wfd in db.DocumentUplodedMaster
                                         where wfd.IsDeleted == false && wfd.DocumentUploaderId == checkListJobAvanceOperator.OperatorUpoadedDocumentId
                                         select wfd.FileName).FirstOrDefault();
                                    try
                                    {
                                        string base64Image = commonFunction.FileToBase64(opDashboard);
                                        checkListJobActivityOperatorCustom.operatorUpoadedDocumentURL = base64Image;
                                    }
                                    catch (Exception ex)
                                    { }

                                    checkListJobActivityOperatorCustom.operatorRemark = checkListJobAvanceOperator.OperatorRemark;
                                    if (checkListJobAvanceOperator.IsAdminApproved == true)
                                    {
                                        checkListJobActivityOperatorCustom.approveRejectFlag = false;
                                    }
                                    if (checkListJobAvanceOperator.IsAdminApproved == false)
                                    {
                                        checkListJobActivityOperatorCustom.approveRejectFlag = true;
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
                        groupItem.checkListJobActivityBySubCategory = checkListJobActivityBySubCategories;
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
                                checkListJobLOTOTOOperatorCustom.lockOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.LockOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                                checkListJobLOTOTOOperatorCustom.lockOutRemark = checkListJobLOTOTOOperator.LockOutRemark;
                                checkListJobLOTOTOOperatorCustom.tagOutDoneByOperator = checkListJobLOTOTOOperator.TagOutDoneByOperator;
                                checkListJobLOTOTOOperatorCustom.tagOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TagOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                                checkListJobLOTOTOOperatorCustom.tagOutRemark = checkListJobLOTOTOOperator.TagOutRemark;
                                checkListJobLOTOTOOperatorCustom.tryOutDoneByOperatorName = db.UserDetails.Where(m => m.UserId == checkListJobLOTOTOOperator.TryOutDoneByOperator).Select(m => m.UserFullName).FirstOrDefault();
                                checkListJobLOTOTOOperatorCustom.tryOutDoneByOperator = checkListJobLOTOTOOperator.TryOutDoneByOperator;
                                checkListJobLOTOTOOperatorCustom.tryOutRemark = checkListJobLOTOTOOperator.TryOutRemark;

                                if (checkListJobLOTOTOOperator.IsAdminApproved == true)
                                {
                                    checkListJobLOTOTOOperatorCustom.approveRejectFlag = false;
                                }
                                if (checkListJobLOTOTOOperator.IsAdminApproved == false)
                                {
                                    checkListJobLOTOTOOperatorCustom.approveRejectFlag = true;
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









                            }

                            checkListJobLOTOTOCustoms.Add(checkListJobLOTOTOCustom);
                        }
                        groupItem.checkListJobLOTOTOCustom = checkListJobLOTOTOCustoms;
                        #endregion
                        groupItems.Add(groupItem);

                    }
                    checkListJobCustom.groupItems = groupItems;
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

        /// <summary>
        /// Check For Job Approval By CheckList Job Id And Check List Group Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse CheckForJobApprovalByCheckListJobIdAndCheckListGroupId(int checkListJobId, int checkListJobGroupId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var result = (from wf in db.CheckListJobMaster
                              join wfs in db.CheckListJobAssignedResourceMaster on wf.CheckListJobId equals wfs.CheckListJobMasterId
                              where wf.CheckListJobId == checkListJobId && wfs.CheckListJobGroupId == checkListJobGroupId
                              select new
                              {
                                  checkListjobWRTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobIsCompleted == true && m.CheckListJobIsPartialCompleted == false && m.IsActive == true).FirstOrDefault()
                              }).FirstOrDefault();

                if (result != null)
                {
                    if (result.checkListjobWRTOp != null)
                    {
                        obj.response = null;
                        obj.isStatus = true;
                    }
                    else
                    {
                        obj.response = ResourceResponse.JobNotYetCompleted;
                        obj.isStatus = false;
                    }
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
        #endregion

        #region Graphs

        /// <summary>
        /// BarChartAllCheckListAverageTimeTakenOverAllMonthly  - Level 1 Graph
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse BarChartAllCheckListAverageTimeTakenOverAllMonthly(string fromDate, string toDate, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;
                try
                {
                    
                    startDate = Convert.ToDateTime(fromDate);
                    startDate = new DateTime(startDate.Year, startDate.Month, 1);
                   
                }
                catch (Exception ex)
                {

                }
                try
                {
                    endDate = Convert.ToDateTime(toDate);
                    endDate = new DateTime(endDate.Year, endDate.Month, 1);
                    endDate = endDate.AddMonths(1).AddDays(-1);
                }
                catch (Exception ex)
                {

                }
                #endregion

                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                alllabels.Add("Line-1");
                alllabels.Add("Line-2");
                //alllabels.Add("Average");
                long typeId = commonFunction.GetTypeDetails(appSettings);

                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;

                    for (DateTime st = startDate; st <= endDate; st = st.AddMonths(1))
                    {

                        DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                        st1 = new DateTime(st1.Year, st1.Month, 1);
                        DateTime st2 = st1.AddMonths(1).AddDays(-1);
                        st2 = Convert.ToDateTime(st2.ToShortDateString() + " 23:59:59");
                        int lineNumber = 0;

                        switch (item)
                        {
                            case "Line-1":
                                lineNumber = 1;
                                break;
                            case "Line-2":
                                lineNumber = 2;
                                break;
                            case "Average":
                                lineNumber = 0;
                                break;
                        }

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

                        if (lineNumber != 0)
                        {
                            jobs = jobs.Where(m => m.CheckListJobLineNumber == lineNumber).ToList();
                        }
                        decimal total = 0;

                        if (item == "Line-1")
                        {
                            labels.Add(st.ToString("MMM/yyyy"));
                        }

                        foreach (var job in jobs)
                        {
                           // total = + commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);

                            //changed By veeresh
                            total = total + commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                        }
                        if (jobs.Count != 0)
                        {
                            if (lineNumber != 0)
                            {
                                values.Add(Math.Round(total / Convert.ToDecimal(jobs.Count), 2));
                            }
                            else
                            {
                                //average of line -1 and line -2 
                                //values.Add(Math.Round(total / (jobs.Count * Convert.ToDecimal(2)), 2));
                            }
                        }
                        else
                        {
                            values.Add(0);
                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }

                BarCharData barCharDataAvg = new BarCharData();
                barCharDataAvg.label = "Average";
                List<decimal> valuesAvg = new List<decimal>();
                for (int i = 0; i < labels.Count; i++)
                {
                    decimal total = barCharDatas[0].data[i] + barCharDatas[1].data[i];
                    valuesAvg.Add(Math.Round(total /  Convert.ToDecimal(2), 2));
                }
                barCharDataAvg.data = valuesAvg.ToArray();
                barCharDatas.Add(barCharDataAvg);


                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                obj.isStatus = true;
                obj.response = pieChart;

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
        /// BarChartAllCheckListAverageTimeTaken  - Level 2 Graph
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponseTable LineChartAllCheckListAverageTimeTakenOverAll(string fromDate, string toDate, long userId)
        {
            CommonResponseTable obj = new CommonResponseTable();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;
                try
                {
                    startDate = Convert.ToDateTime(fromDate);
                }
                catch (Exception ex)
                {
                    startDate = startDate.AddDays(-7);
                }
                try
                {
                    endDate = Convert.ToDateTime(toDate);
                }
                catch (Exception ex)
                {
                }
                #endregion

                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                //  alllabels.Add("Estimated");
                alllabels.Add("Target");
                alllabels.Add("Line-1");
                alllabels.Add("Line-2");
               // alllabels.Add("Average");
                long typeId = commonFunction.GetTypeDetails(appSettings);
                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;


                    for (DateTime st = startDate; st <= endDate; st = st.AddDays(1))
                    {
                        //if (item != "Estimated")
                            if (item != "Target")
                            {
                            DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                            DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

                            int lineNumber = 0;

                            switch (item)
                            {
                                case "Line-1":
                                    lineNumber = 1;
                                    break;
                                case "Line-2":
                                    lineNumber = 2;
                                    break;
                                case "Average":
                                    lineNumber = 0;
                                    break;
                            }

                            var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.IsDeleted == false).ToList();

                            if (lineNumber != 0)
                            {
                                jobs = jobs.Where(m => m.CheckListJobLineNumber == lineNumber).ToList();
                            }

                            decimal total = 0;
                            foreach (var job in jobs)
                            {
                                total = total + commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                            }

                            if (jobs.Count != 0)
                            {
                                if (item == "Average")
                                {
                                   // values.Add(Math.Round(total / (jobs.Count * Convert.ToDecimal(2)), 2));
                                }
                                else
                                {
                                    values.Add(Math.Round(total / Convert.ToDecimal(jobs.Count), 2));
                                }
                             
                            }
                            else
                            {
                                values.Add(0);
                            }

                        }
                        else
                        {


                            var sdatestring = CurrentDate.ToString("yyyy-MM-dd");

                            var startTimee = Convert.ToDateTime(sdatestring + " 00:00:00");
                            var endTimee = Convert.ToDateTime(sdatestring + " 23:59:59");

                            //var targetThisYear = db.TargetOverall.Where(m => m.TargetStartTime >= startTimee && m.TargetEndTime <= endTimee).FirstOrDefault();

                            var targetThisYear = db.TargetOverall.Where(m => m.TargetStartTime <= CurrentDate && m.TargetEndTime >= CurrentDate).FirstOrDefault();
                            if (targetThisYear != null)
                            {
                                //var targetThisYear = db.TargetOverall.Where(m => m.TargetStartTime <= CurrentDate && m.TargetEndTime >= CurrentDate).FirstOrDefault();
                                labels.Add(st.ToString("dd/MM/yyyy"));
                                values.Add(targetThisYear.TargetValue);
                            }
                            else
                            {
                                labels.Add(st.ToString("dd/MM/yyyy"));
                                values.Add(0);
                            }




                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }


                BarCharData barCharDataAvg = new BarCharData();
                barCharDataAvg.label = "Average";
                List<decimal> valuesAvg = new List<decimal>();
                for (int i = 0; i < labels.Count; i++)
                {
                    decimal total = barCharDatas[1].data[i] + barCharDatas[2].data[i];
                    valuesAvg.Add(Math.Round(total / Convert.ToDecimal(2), 2));
                }
                barCharDataAvg.data = valuesAvg.ToArray();
                barCharDatas.Add(barCharDataAvg);


                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                LineGraphTable lineGraphTable = new LineGraphTable();
                try
                {
                    decimal target = (barCharDatas[0].data.Sum() / barCharDatas[0].data.Count());
                    target = Math.Round(target, 1);
                    decimal line1Sumation = (barCharDatas[1].data.Sum() / barCharDatas[1].data.Count());
                    line1Sumation = Math.Round(line1Sumation, 1);
                    decimal line2Sumation = (barCharDatas[2].data.Sum() / barCharDatas[2].data.Count());
                    line2Sumation = Math.Round(line2Sumation, 1);
                    decimal avgSumation = (barCharDatas[3].data.Sum() / barCharDatas[3].data.Count());
                    avgSumation = Math.Round(avgSumation, 1);
                    lineGraphTable.estimatedL1 = target.ToString();
                    lineGraphTable.estimatedL2 = target.ToString();
                    lineGraphTable.estimatedAverage = target.ToString();
                    lineGraphTable.actualL1 = line1Sumation.ToString();
                    lineGraphTable.actualL2 = line2Sumation.ToString();
                    lineGraphTable.actualAverage = avgSumation.ToString();
                }
                catch (Exception ex)
                { }

                obj.isStatus = true;
                obj.response = pieChart;
                obj.responseTable = lineGraphTable;

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
        /// Bar Chart All Check List Average Time Taken Over All Line Wise  - Level 3 Graph
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="lineNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse BarChartAllCheckListAverageTimeTakenOverAllLineWise(string fromDate, string toDate, int lineNumber, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;
                try
                {
                    startDate = Convert.ToDateTime(fromDate);
                }
                catch (Exception ex)
                {
                    startDate = startDate.AddDays(-7);
                }
                try
                {
                    endDate = Convert.ToDateTime(toDate);
                }
                catch (Exception ex)
                {

                }
                #endregion

                if (lineNumber == 0)
                {
                    lineNumber = 1;
                }

                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                alllabels.Add("Target");
                alllabels.Add("Actual");

                long typeId = commonFunction.GetTypeDetails(appSettings);
                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;

                    for (DateTime st = startDate; st <= endDate; st = st.AddDays(1))
                    {

                        DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                        DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobLineNumber == lineNumber && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

                        foreach (var job in jobs)
                        {
                            if (item == "Target")
                            {
                                if (lineNumber != 0)
                                {
                                    var linename = db.LineNumberMaster.Where(m => m.LineNumberId == lineNumber).Select(m => m.LineNumberName).FirstOrDefault();
                                    labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName+" - "+ linename);
                                }
                                else
                                {
                                    labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName);
                                }

                                decimal total = 0;
                                total = Math.Round(Convert.ToDecimal(job.EstimatedEndTime), 2);
                                values.Add(total);
                            }
                            else
                            {
                                decimal total = 0;
                                total = commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                                values.Add(total);
                            }
                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }
                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                obj.isStatus = true;
                obj.response = pieChart;

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
        /// Bar Chart All Check List Detailed Change Over Line Wise
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="lineNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse BarChartAllCheckListDetailedChangeOverLineWise(string fromDate, string toDate, int lineNumber,string groupIds, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;
                try
                {
                    startDate = Convert.ToDateTime(fromDate);
                }
                catch (Exception ex)
                {
                    startDate = startDate.AddDays(-5);
                }
                try
                {
                    endDate = Convert.ToDateTime(toDate);
                }
                catch (Exception ex)
                {

                }
                #endregion

                if (lineNumber == 0)
                {
                    lineNumber = 1;
                }

                if (groupIds == "0")
                {
                    groupIds = "1,2,3";
                }

                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                long typeId = commonFunction.GetTypeDetails(appSettings);

                var groupList = groupIds.Split(',').Select(x => long.Parse(x.Trim()));
                var groups = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false && groupList.Contains(wf.CheckListGroupId)
                              select wf.CheckListGroupName).ToList();
                
                DateTime CurrentDate = DateTime.Now;

                foreach (var item in groups)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;
                    int counter = 0;
                    for (DateTime st = startDate; st <= endDate; st = st.AddDays(1))
                    {
                       
                        DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                        DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobLineNumber == lineNumber && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();
                        
                        foreach (var job in jobs)
                        {
                            labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName);
                            

                            decimal total = 0;
                            var groupsId = (from wf in db.CheckListGroupMaster
                                          where wf.IsDeleted == false && wf.CheckListGroupName == item
                                            select wf.CheckListGroupId).FirstOrDefault();
                            var checkListJobWRTOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == job.CheckListJobId && m.CheckListJobGroupId == groupsId).FirstOrDefault();
                            if (checkListJobWRTOperator != null)
                            {
                                total = commonFunction.GetDateDifferenceInMins(checkListJobWRTOperator.CheckListJobStartTime, checkListJobWRTOperator.CheckListJobEndTime);
                                values.Add(total);
                            }
                            else
                            {
                                values.Add(0);
                            }
                            
                        }
                       
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }
                labels = labels.Distinct().ToList();
                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                obj.isStatus = true;
                obj.response = pieChart;

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
        /// Pie Chart All Activity Time Taken By Check List Job Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse PieChartAllActivityTimeTakenByCheckListJobId(string checkListJobName,string groupName, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                PieChart pieChart = new PieChart();
                List<string> labels = new List<string>();
                List<int> values = new List<int>();

                long typeId = commonFunction.GetTypeDetails(appSettings);
                var jobs = db.CheckListJobMaster.Where(m => m.IsDeleted == false && m.CheckListJobName == checkListJobName && m.CheckListJobTypeId == typeId && m.IsDeleted == false).FirstOrDefault();
                double durationAverage = 0;
                int count = 0;


                //List<long> grpIds = jobs.CheckListGroup.Split(',').Select(long.Parse).ToList();
                //List<long?> gd = new List<long?>();
                //foreach (var grs in grpIds)
                //{
                //    gd.Add(Convert.ToInt32(grs));
                //}
                if (jobs != null)
                {
                    var groupId = db.CheckListGroupMaster.Where(m => m.CheckListGroupName == groupName && m.IsDeleted == false).FirstOrDefault();
                    var jobWRTOp = db.CheckListJobWrtoperator.Where(m => m.CheckListJobGroupId == groupId.CheckListGroupId && m.IsJobClosed == true && m.CheckListJobMasterId == jobs.CheckListJobId).FirstOrDefault();

                    var jobOperator = db.CheckListJobActivityOperator.Where(m => m.CheckListJobOperatorId == jobWRTOp.CheckListJobWrtoperatorId).ToList();
                    foreach (var jo in jobOperator)
                    {
                        if (jo.ActivityStartTime != null && jo.ActivityEndTime != null)
                        {
                            DateTime startDateTime = Convert.ToDateTime(jo.ActivityStartTime);
                            DateTime endDateTime = Convert.ToDateTime(jo.ActivityEndTime);
                            var seconds = (endDateTime - startDateTime).TotalSeconds;
                            var activityName = db.CheckListJobActivityMaster.Where(m => m.CheckListJobActivityId == jo.CheckListJobActivityId).Select(m => m.ActivityDescription).FirstOrDefault();
                            labels.Add(activityName);
                            values.Add(Convert.ToInt32(seconds / (60)));
                        }
                    }

                    pieChart.labels = labels.ToArray();
                    pieChart.values = values.ToArray();
                    if (pieChart.values.Length == 0)
                    {
                        obj.isStatus = false;
                    }
                    else
                    {
                        obj.isStatus = true;
                    }

                    obj.response = pieChart;
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
        /// Pie Chart All Jobs Details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse PieChartAllJobsDetails(long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                PieChart pieChart = new PieChart();
                List<string> labels = new List<string>();
                List<int> values = new List<int>();
                var jobItem = db.CheckListJobMaster.Where(m => m.IsDeleted == false).ToList();
                long typeId = commonFunction.GetTypeDetails(appSettings);
                #region All Jobs
                labels.Add("All Jobs");
                values.Add(jobItem.Count);
                #endregion


                var result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false 
                              select new { wf, jbop }).ToList();

                #region Job Approved
                var jobApproved = result.Where(m => m.jbop.IsAdminApproved == true && m.jbop.IsJobClosed == true && m.jbop.CheckListJobIsCompleted == true && m.jbop.CheckListJobIsPartialCompleted == false && m.wf.OverAllApproved == true && m.wf.OverAllRejected != true).ToList();
                jobApproved = jobApproved.GroupBy(m => m.wf.CheckListJobId).Select(m => m.First()).ToList();
                labels.Add("Job Approved");
                values.Add(jobApproved.Count);
                #endregion

                #region Waiting For Job Closur
                var waitingForJobClosure = result.Where(m => m.jbop.IsAdminApproved == true && m.jbop.IsJobClosed == false && m.jbop.CheckListJobIsCompleted == true && m.jbop.CheckListJobIsPartialCompleted == false && m.wf.OverAllJobCompleted == true).ToList();
                waitingForJobClosure = waitingForJobClosure.GroupBy(m => m.wf.CheckListJobId).Select(m => m.First()).ToList();
                labels.Add("Waiting For Job Closure");
                values.Add(waitingForJobClosure.Count);
                #endregion

                #region Need Job Approval
                var needJobApproval = result.Where(m => m.jbop.IsAdminApproved == false && m.jbop.IsJobClosed == false && m.jbop.CheckListJobIsCompleted == true && m.jbop.CheckListJobIsPartialCompleted == false && m.wf.OverAllApproved!=true && m.wf.OverAllRejected != true).ToList();
                needJobApproval = needJobApproval.GroupBy(m => m.wf.CheckListJobId).Select(m => m.First()).ToList();
                labels.Add("Need Job Approval");
                values.Add(needJobApproval.Count);
                #endregion

                #region Job Ongoing
                var jobOngoing = result.Where(m => m.jbop.IsAdminApproved == false && m.jbop.IsJobClosed == false && m.jbop.CheckListJobIsCompleted == false && m.jbop.CheckListJobIsPartialCompleted == true && m.wf.OverAllRejected != true).ToList();
                jobOngoing = jobOngoing.GroupBy(m => m.wf.CheckListJobId).Select(m => m.First()).ToList();
                labels.Add("Job Ongoing");
                values.Add(jobOngoing.Count);
                #endregion

                #region Job Rejected
                var jobRejected = result.Where(m => m.jbop.IsAdminApproved == false && m.jbop.IsJobClosed == false && m.jbop.CheckListJobIsCompleted == true && m.jbop.CheckListJobIsPartialCompleted == false && m.jbop.IsJobRejected == true && m.wf.OverAllRejected == true).ToList();
                jobRejected = jobRejected.GroupBy(m => m.wf.CheckListJobId).Select(m => m.First()).ToList();
                labels.Add("Job Rejected");
                values.Add(jobRejected.Count);
                #endregion

                #region Not Yet Started
                int notYetStarted = 0;
                foreach (var job in jobItem)
                {
                    List<long> grpIds = job.CheckListGroup.Split(',').Select(long.Parse).ToList();
                    var jobWRTOp = db.CheckListJobWrtoperator.Where(m => grpIds.Contains(m.CheckListJobMasterId)).ToList();
                    if (grpIds.Count == jobWRTOp.Count)
                    {
                        notYetStarted++;
                    }
                }
                labels.Add("Not Yet Started");
                values.Add(notYetStarted);
                #endregion

                pieChart.labels = labels.ToArray();
                pieChart.values = values.ToArray();
                obj.isStatus = true;
                obj.response = pieChart;

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
        /// Bar Chart All Check List Average Time Taken
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse BarChartAllCheckListAverageTimeTaken(long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                PieChart pieChart = new PieChart();
                List<string> labels = new List<string>();
                List<int> values = new List<int>();
                var jobItem = db.CheckListMaster.Where(m => m.IsDeleted == false).ToList();

                long typeId = commonFunction.GetTypeDetails(appSettings);


                foreach (var job in jobItem)
                {
                    long checkListId = job.CheckListId;

                    var jobs = db.CheckListJobMaster.Where(m => m.CheckListMasterId == checkListId).ToList();
                    double durationAverage = 0;
                    int count = 1;
                    foreach (var jbs in jobs)
                    {
                        List<long> grpIds = jbs.CheckListGroup.Split(',').Select(long.Parse).ToList();
                        List<long?> gd = new List<long?>();
                        foreach (var grs in grpIds)
                        {
                            gd.Add(Convert.ToInt32(grs));
                        }
                        var jobWRTOp = db.CheckListJobWrtoperator.Where(m => gd.Contains(m.CheckListJobGroupId) && m.IsJobClosed == true && m.CheckListJobMasterId == jbs.CheckListJobId).ToList();
                        if (grpIds.Count <= jobWRTOp.Count)
                        {
                            DateTime startDateTime = Convert.ToDateTime(jbs.CheckListStartTime);
                            DateTime endDateTime = Convert.ToDateTime(jbs.CheckListEndTime);
                            var seconds = (endDateTime - startDateTime).TotalSeconds;

                            durationAverage = durationAverage + seconds;
                            count++;
                        }
                    }

                    TimeSpan time = TimeSpan.FromSeconds(durationAverage);
                    string averageDur = time.ToString(@"hh\:mm\:ss");
                    try
                    {
                        string[] arry = averageDur.Split(':');
                        averageDur = arry[0] + " Hrs " + arry[1] + " Mins " + arry[2] + " Secs";
                    }
                    catch (Exception ex)
                    {
                    }

                    labels.Add(job.CheckListName);
                    values.Add(Convert.ToInt32(durationAverage/(count*60)));
                }
                

                pieChart.labels = labels.ToArray();
                pieChart.values = values.ToArray();
                obj.isStatus = true;
                obj.response = pieChart;

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
        /// Donut Chart All Activity Average Time Taken
        /// </summary>
        /// <param name="checkListId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DonutChartAllActivityAverageTimeTaken(int checkListId, long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                PieChart pieChart = new PieChart();
                List<string> labels = new List<string>();
                List<int> values = new List<int>();
                long typeId = commonFunction.GetTypeDetails(appSettings);
                if (checkListId == 0)
                {
                    var jobItem = db.CheckListMaster.Where(m => m.IsDeleted == false).ToList();
                    foreach (var job in jobItem)
                    {
                        long checkListIds = job.CheckListId;

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListMasterId == checkListIds).ToList();
                        double durationAverage = 0;
                        int count = 1;
                        foreach (var jbs in jobs)
                        {
                            List<long> grpIds = jbs.CheckListGroup.Split(',').Select(long.Parse).ToList();
                            List<long?> gd = new List<long?>();
                            foreach (var grs in grpIds)
                            {
                                gd.Add(Convert.ToInt32(grs));
                            }
                            var jobWRTOp = db.CheckListJobWrtoperator.Where(m => gd.Contains(m.CheckListJobGroupId) && m.IsJobClosed == true && m.CheckListJobMasterId == jbs.CheckListJobId).ToList();
                            if (grpIds.Count <= jobWRTOp.Count)
                            {
                                DateTime startDateTime = Convert.ToDateTime(jbs.CheckListStartTime);
                                DateTime endDateTime = Convert.ToDateTime(jbs.CheckListEndTime);
                                var seconds = (endDateTime - startDateTime).TotalSeconds;

                                durationAverage = durationAverage + seconds;
                                count++;
                            }
                        }

                        TimeSpan time = TimeSpan.FromSeconds(durationAverage);
                        string averageDur = time.ToString(@"hh\:mm\:ss");
                        try
                        {
                            string[] arry = averageDur.Split(':');
                            averageDur = arry[0] + " Hrs " + arry[1] + " Mins " + arry[2] + " Secs";
                        }
                        catch (Exception ex)
                        {
                        }

                        labels.Add(job.CheckListName);
                        values.Add(Convert.ToInt32(durationAverage / (count * 60)));
                    }
                }
                else
                {
                    var jobItem = db.CheckListJobMaster.Where(m => m.IsDeleted == false && m.CheckListJobId == checkListId).FirstOrDefault();
                    //var jobIds = jobItem.Select(m => m.CheckListJobId).ToList();
                    //List<long?> jbd = new List<long?>();
                    //foreach (var jbs in jobIds)
                    //{
                    //    jbd.Add(jbs);
                    //}

                    var jobActivity = db.CheckListJobActivityMaster.Where(m => m.IsDeleted == false && m.CheckListJobMasterId == jobItem.CheckListJobId).ToList();
                    //jobActivity = jobActivity.GroupBy(m => m.CheckListJobActivityId).Select(m => m.First()).ToList();
                    foreach (var it in jobActivity)
                    {
                        int count = 1;
                        double duration = 0;
                        var jbOp = db.CheckListJobActivityOperator.Where(m => m.CheckListJobActivityId == it.CheckListJobActivityId).ToList();
                        
                        foreach (var jb in jbOp)
                        {
                            DateTime startDateTime = Convert.ToDateTime(jb.ActivityStartTime);
                            DateTime endDateTime = Convert.ToDateTime(jb.ActivityEndTime);
                            var seconds = (endDateTime - startDateTime).TotalSeconds;

                            duration = duration + seconds;
                            count++;
                        }
                        labels.Add(it.ActivityDescription);
                        values.Add(Convert.ToInt32(duration / (count * 60)));
                    }

                }

                pieChart.labels = labels.ToArray();
                pieChart.values = values.ToArray();
                if (pieChart.values.Length == 0)
                {
                    obj.isStatus = false;
                }
                else
                {
                    obj.isStatus = true;
                }
                
                obj.response = pieChart;

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }


        //public CommonResponse BarChartAllCheckListJObForYear()
        //{
        //    CommonResponse obj = new CommonResponse();
        //    CommonFunction commonFunction = new CommonFunction();
        //    try
        //    {
        //        #region Date
        //        DateTime startDate = DateTime.Now;
        //        DateTime endDate = startDate.AddYears(-1);

        //        //try
        //        //{
        //        //    startDate = Convert.ToDateTime(fromDate);
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    startDate = startDate.AddDays(-7);
        //        //}
        //        //try
        //        //{
        //        //    endDate = Convert.ToDateTime(toDate);
        //        //}
        //        //catch (Exception ex)
        //        //{

        //        //}
        //        #endregion


        //        ActualVsExpected pieChart = new ActualVsExpected();
        //        List<string> labels = new List<string>();
        //        List<BarCharData> barCharDatas = new List<BarCharData>();

        //        List<string> alllabels = new List<string>();
        //        alllabels.Add("Target");
        //        alllabels.Add("Actual");

        //        long typeId = commonFunction.GetTypeDetails(appSettings);
        //        DateTime CurrentDate = DateTime.Now;

        //        foreach (var item in alllabels)
        //        {
        //            BarCharData barCharData = new BarCharData();
        //            List<decimal> values = new List<decimal>();
        //            barCharData.label = item;

        //            for (DateTime st = endDate; st <= startDate; st = st.AddDays(1))
        //            {

        //                DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
        //                DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

        //                var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

        //                foreach (var job in jobs)
        //                {
        //                    if (item == "Target")
        //                    {
        //                        labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName);
        //                        decimal total = 0;
        //                        total = Math.Round(Convert.ToDecimal(job.EstimatedEndTime), 2);
        //                        values.Add(total);
        //                    }
        //                    else
        //                    {
        //                        decimal total = 0;
        //                        total = commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
        //                        values.Add(total);
        //                    }
        //                }
        //            }
        //            barCharData.data = values.ToArray();
        //            barCharDatas.Add(barCharData);
        //        }
        //        pieChart.labels = labels.ToArray();
        //        pieChart.datas = barCharDatas;

        //        obj.isStatus = true;
        //        obj.response = pieChart;

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //        obj.response = ResourceResponse.ExceptionMessage;
        //        obj.isStatus = false;
        //    }
        //    return obj;
        //}


        public CommonResponse BarChartAllCheckListJObForYear()
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date

                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddYears(-1);
                //DateTime startDate = DateTime.Now;
                //DateTime endDate = DateTime.Now;
                //try
                //{

                //    startDate = Convert.ToDateTime(fromDate);
                //    startDate = new DateTime(startDate.Year, startDate.Month, 1);

                //}
                //catch (Exception ex)
                //{

                //}
                //try
                //{
                //    endDate = Convert.ToDateTime(toDate);
                //    endDate = new DateTime(endDate.Year, endDate.Month, 1);
                //    endDate = endDate.AddMonths(1).AddDays(-1);
                //}
                //catch (Exception ex)
                //{

                //}
                #endregion

                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                alllabels.Add("Line-1");
                alllabels.Add("Line-2");
                //alllabels.Add("Average");
                long typeId = commonFunction.GetTypeDetails(appSettings);

                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;

                    for (DateTime st = endDate; st <= startDate; st = st.AddMonths(1))
                    {

                        DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                        st1 = new DateTime(st1.Year, st1.Month, 1);
                        DateTime st2 = st1.AddMonths(1).AddDays(-1);
                        st2 = Convert.ToDateTime(st2.ToShortDateString() + " 23:59:59");
                        int lineNumber = 0;

                        switch (item)
                        {
                            case "Line-1":
                                lineNumber = 1;
                                break;
                            case "Line-2":
                                lineNumber = 2;
                                break;
                            case "Average":
                                lineNumber = 0;
                                break;
                        }

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

                        if (lineNumber != 0)
                        {
                            jobs = jobs.Where(m => m.CheckListJobLineNumber == lineNumber).ToList();
                        }
                        decimal total = 0;

                        if (item == "Line-1")
                        {
                            labels.Add(st.ToString("MMM/yyyy"));
                        }

                        foreach (var job in jobs)
                        {
                            // total = + commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);

                            //changed By veeresh
                            total = total + commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                        }
                        if (jobs.Count != 0)
                        {
                            if (lineNumber != 0)
                            {
                                values.Add(Math.Round(total / Convert.ToDecimal(jobs.Count), 2));
                            }
                            else
                            {
                                //average of line -1 and line -2 
                                //values.Add(Math.Round(total / (jobs.Count * Convert.ToDecimal(2)), 2));
                            }
                        }
                        else
                        {
                            values.Add(0);
                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }

                BarCharData barCharDataAvg = new BarCharData();
                barCharDataAvg.label = "Average";
                List<decimal> valuesAvg = new List<decimal>();
                for (int i = 0; i < labels.Count; i++)
                {
                    decimal total = barCharDatas[0].data[i] + barCharDatas[1].data[i];
                    valuesAvg.Add(Math.Round(total / Convert.ToDecimal(2), 2));
                }
                barCharDataAvg.data = valuesAvg.ToArray();
                barCharDatas.Add(barCharDataAvg);


                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                obj.isStatus = true;
                obj.response = pieChart;

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }



        //public CommonResponse BarChartAllCheckListJObForMonth()
        //{
        //    CommonResponse obj = new CommonResponse();
        //    CommonFunction commonFunction = new CommonFunction();
        //    try
        //    {
        //        #region Date
        //        DateTime startDate = DateTime.Now;
        //        DateTime endDate = startDate.AddMonths(-1);

        //        #endregion


        //        ActualVsExpected pieChart = new ActualVsExpected();
        //        List<string> labels = new List<string>();
        //        List<BarCharData> barCharDatas = new List<BarCharData>();

        //        List<string> alllabels = new List<string>();
        //        alllabels.Add("Target");
        //        alllabels.Add("Actual");

        //        long typeId = commonFunction.GetTypeDetails(appSettings);
        //        DateTime CurrentDate = DateTime.Now;

        //        foreach (var item in alllabels)
        //        {
        //            BarCharData barCharData = new BarCharData();
        //            List<decimal> values = new List<decimal>();
        //            barCharData.label = item;

        //            for (DateTime st = endDate; st <= startDate; st = st.AddDays(1))
        //            {

        //                DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
        //                DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

        //                var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

        //                foreach (var job in jobs)
        //                {
        //                    if (item == "Target")
        //                    {
        //                        labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName);
        //                        decimal total = 0;
        //                        total = Math.Round(Convert.ToDecimal(job.EstimatedEndTime), 2);
        //                        values.Add(total);
        //                    }
        //                    else
        //                    {
        //                        decimal total = 0;
        //                        total = commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
        //                        values.Add(total);
        //                    }
        //                }
        //            }
        //            barCharData.data = values.ToArray();
        //            barCharDatas.Add(barCharData);
        //        }
        //        pieChart.labels = labels.ToArray();
        //        pieChart.datas = barCharDatas;

        //        obj.isStatus = true;
        //        obj.response = pieChart;

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
        //        obj.response = ResourceResponse.ExceptionMessage;
        //        obj.isStatus = false;
        //    }
        //    return obj;
        //}

        public CommonResponseTable BarChartAllCheckListJObForMonth()
        {
            CommonResponseTable obj = new CommonResponseTable();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                 DateTime endDate = startDate.AddMonths(-1);
                //try
                //{
                //    startDate = Convert.ToDateTime(fromDate);
                //}
                //catch (Exception ex)
                //{
                //    startDate = startDate.AddDays(-7);
                //}
                //try
                //{
                //    endDate = Convert.ToDateTime(toDate);
                //}
                //catch (Exception ex)
                //{
                //}
                #endregion

                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                //  alllabels.Add("Estimated");
                alllabels.Add("Target");
                alllabels.Add("Line-1");
                alllabels.Add("Line-2");
                // alllabels.Add("Average");
                long typeId = commonFunction.GetTypeDetails(appSettings);
                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;


                  //  for (DateTime st = startDate; st <= endDate; st = st.AddDays(1))

                      for(  DateTime st = endDate; st <= startDate; st = st.AddDays(1))
                    {
                        //if (item != "Estimated")
                        if (item != "Target")
                        {
                            DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                            DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

                            int lineNumber = 0;

                            switch (item)
                            {
                                case "Line-1":
                                    lineNumber = 1;
                                    break;
                                case "Line-2":
                                    lineNumber = 2;
                                    break;
                                case "Average":
                                    lineNumber = 0;
                                    break;
                            }

                            var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.IsDeleted == false).ToList();

                            if (lineNumber != 0)
                            {
                                jobs = jobs.Where(m => m.CheckListJobLineNumber == lineNumber).ToList();
                            }

                            decimal total = 0;
                            foreach (var job in jobs)
                            {
                                total = total + commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                            }

                            if (jobs.Count != 0)
                            {
                                if (item == "Average")
                                {
                                    // values.Add(Math.Round(total / (jobs.Count * Convert.ToDecimal(2)), 2));
                                }
                                else
                                {
                                    values.Add(Math.Round(total / Convert.ToDecimal(jobs.Count), 2));
                                }

                            }
                            else
                            {
                                values.Add(0);
                            }

                        }
                        else
                        {


                            var sdatestring = CurrentDate.ToString("yyyy-MM-dd");

                            var startTimee = Convert.ToDateTime(sdatestring + " 00:00:00");
                            var endTimee = Convert.ToDateTime(sdatestring + " 23:59:59");

                            //var targetThisYear = db.TargetOverall.Where(m => m.TargetStartTime >= startTimee && m.TargetEndTime <= endTimee).FirstOrDefault();

                            var targetThisYear = db.TargetOverall.Where(m => m.TargetStartTime <= CurrentDate && m.TargetEndTime >= CurrentDate).FirstOrDefault();
                            if (targetThisYear != null)
                            {
                                //var targetThisYear = db.TargetOverall.Where(m => m.TargetStartTime <= CurrentDate && m.TargetEndTime >= CurrentDate).FirstOrDefault();
                                labels.Add(st.ToString("dd/MM/yyyy"));
                                values.Add(targetThisYear.TargetValue);
                            }
                            else
                            {
                                labels.Add(st.ToString("dd/MM/yyyy"));
                                values.Add(0);
                            }




                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }


                BarCharData barCharDataAvg = new BarCharData();
                barCharDataAvg.label = "Average";
                List<decimal> valuesAvg = new List<decimal>();
                for (int i = 0; i < labels.Count; i++)
                {
                    decimal total = barCharDatas[1].data[i] + barCharDatas[2].data[i];
                    valuesAvg.Add(Math.Round(total / Convert.ToDecimal(2), 2));
                }
                barCharDataAvg.data = valuesAvg.ToArray();
                barCharDatas.Add(barCharDataAvg);


                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                LineGraphTable lineGraphTable = new LineGraphTable();
                try
                {
                    decimal target = (barCharDatas[0].data.Sum() / barCharDatas[0].data.Count());
                    target = Math.Round(target, 1);
                    decimal line1Sumation = (barCharDatas[1].data.Sum() / barCharDatas[1].data.Count());
                    line1Sumation = Math.Round(line1Sumation, 1);
                    decimal line2Sumation = (barCharDatas[2].data.Sum() / barCharDatas[2].data.Count());
                    line2Sumation = Math.Round(line2Sumation, 1);
                    decimal avgSumation = (barCharDatas[3].data.Sum() / barCharDatas[3].data.Count());
                    avgSumation = Math.Round(avgSumation, 1);
                    lineGraphTable.estimatedL1 = target.ToString();
                    lineGraphTable.estimatedL2 = target.ToString();
                    lineGraphTable.estimatedAverage = target.ToString();
                    lineGraphTable.actualL1 = line1Sumation.ToString();
                    lineGraphTable.actualL2 = line2Sumation.ToString();
                    lineGraphTable.actualAverage = avgSumation.ToString();
                }
                catch (Exception ex)
                { }

                obj.isStatus = true;
                obj.response = pieChart;
                obj.responseTable = lineGraphTable;

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }


        public CommonResponse BarChartAllCheckListJObForMonthLine1()
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddMonths(-1);

                #endregion


                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                alllabels.Add("Target");
                alllabels.Add("Actual");

                long typeId = commonFunction.GetTypeDetails(appSettings);
                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;

                    for (DateTime st = endDate; st <= startDate; st = st.AddDays(1))
                    {

                        DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                        DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobLineNumber == 1 && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

                        foreach (var job in jobs)
                        {
                            if (item == "Target")
                            {
                                var linename = db.LineNumberMaster.Where(m => m.LineNumberId == 1).Select(m => m.LineNumberName).FirstOrDefault();
                              
                                labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName + " - " + linename);
                                decimal total = 0;
                                total = Math.Round(Convert.ToDecimal(job.EstimatedEndTime), 2);
                                values.Add(total);
                            }
                            else
                            {
                                decimal total = 0;
                                total = commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                                values.Add(total);
                            }
                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }
                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                obj.isStatus = true;
                obj.response = pieChart;

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }


        public CommonResponse BarChartAllCheckListJObForMonthLine2()
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                #region Date
                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddMonths(-1);

                #endregion


                ActualVsExpected pieChart = new ActualVsExpected();
                List<string> labels = new List<string>();
                List<BarCharData> barCharDatas = new List<BarCharData>();

                List<string> alllabels = new List<string>();
                alllabels.Add("Target");
                alllabels.Add("Actual");

                long typeId = commonFunction.GetTypeDetails(appSettings);
                DateTime CurrentDate = DateTime.Now;

                foreach (var item in alllabels)
                {
                    BarCharData barCharData = new BarCharData();
                    List<decimal> values = new List<decimal>();
                    barCharData.label = item;

                    for (DateTime st = endDate; st <= startDate; st = st.AddDays(1))
                    {

                        DateTime st1 = Convert.ToDateTime(st.ToShortDateString() + " 00:00:00");
                        DateTime st2 = Convert.ToDateTime(st.ToShortDateString() + " 23:59:59");

                        var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobLineNumber == 2 && m.CheckListJobTypeId == typeId && m.IsDeleted == false).ToList();

                        foreach (var job in jobs)
                        {
                            if (item == "Target")
                            {

                                var linename = db.LineNumberMaster.Where(m => m.LineNumberId == 2).Select(m => m.LineNumberName).FirstOrDefault();

                                labels.Add(st.ToString("dd/MM/yyyy") + " - " + job.CheckListJobName+" - "+ linename);
                                decimal total = 0;
                                total = Math.Round(Convert.ToDecimal(job.EstimatedEndTime), 2);
                                values.Add(total);
                            }
                            else
                            {
                                decimal total = 0;
                                total = commonFunction.GetDateDifferenceInMins(job.CheckListStartTime, job.CheckListEndTime);
                                values.Add(total);
                            }
                        }
                    }
                    barCharData.data = values.ToArray();
                    barCharDatas.Add(barCharData);
                }
                pieChart.labels = labels.ToArray();
                pieChart.datas = barCharDatas;

                obj.isStatus = true;
                obj.response = pieChart;

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }


        public CommonResponse ChangeOverTimeReport(COReport data)
        {
            CommonResponse obj = new CommonResponse();

            try
            {
                string[] machinelistids;


                DateTime FromDate = DateTime.Now;
                try
                {
                    string[] dt = data.fromDate.Split('/');
                    string frDate = dt[2] + '-' + dt[1] + '-' + dt[0];
                    FromDate = Convert.ToDateTime(frDate);
                }
                catch
                {
                    FromDate = Convert.ToDateTime(data.fromDate);
                }
                DateTime ToDate = DateTime.Now;
                try
                {
                    string[] dt = data.toDate.Split('/');
                    string torDate = dt[2] + '-' + dt[1] + '-' + dt[0];
                    ToDate = Convert.ToDateTime(torDate);
                }
                catch
                {
                    ToDate = Convert.ToDateTime(data.toDate).AddHours(24);
                }



                int dateDifference = Convert.ToDateTime(ToDate).Subtract(Convert.ToDateTime(FromDate)).Days;





                FileInfo templateFile = new FileInfo(@"C:\SRKS_ifacility\MainTemplate\DSM_ChageOverTime_Report_Template.xlsx");
                //FileInfo templateFile = new FileInfo(@"C:\SRKS_ifacility\MainTemplate\ChangeOverTimeReport.xlsx");

                ExcelPackage templatep = new ExcelPackage(templateFile);
                ExcelWorksheet Templatews = templatep.Workbook.Worksheets[0];
                //ExcelWorksheet TemplateGraph = templatep.Workbook.Worksheets[1];


                //excel file save and  downloaded link


                var line12 = db.LineNumberMaster.Where(m => m.LineNumberId == data.lineNo).Select(m => m.LineNumberName).FirstOrDefault();


                string ImageUrlSave = configuration.GetSection("AppSettings").GetSection("ImageUrlSave").Value;
                string ImageUrl = configuration.GetSection("AppSettings").GetSection("ImageUrl").Value;

                String FileDir = ImageUrlSave + "\\" + "ChangeOverTimeReport_" + line12 + "_" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd") + ".xlsx";
                String retrivalPath = ImageUrl + "ChangeOverTimeReport_" + line12 + "_" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd") + ".xlsx";


                FileInfo newFile = new FileInfo(FileDir);

                if (newFile.Exists)
                {
                    try
                    {
                        newFile.Delete();
                        newFile = new FileInfo(FileDir);
                    }

                    catch (Exception ex)

                    {
                        obj.response = ResourceResponse.ExceptionMessage; ;
                    }
                }


                //Using the File for generation and populating it
                ExcelPackage p = null;
                p = new ExcelPackage(newFile);
                ExcelWorksheet worksheet = null;
                ExcelWorksheet worksheetGraph = null;


                try
                {
                    //worksheet = p.Workbook.Worksheets.Add(Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy"), Templatews);
                    worksheet = p.Workbook.Worksheets.Add(line12, Templatews);
                    //worksheetGraph = p.Workbook.Worksheets.Add("Break down analyis", TemplateGraph);
                }
                catch { }

                if (worksheet == null)
                {
                    worksheet = p.Workbook.Worksheets.Add(Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy") + "1", Templatews);
                    //worksheetGraph = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("dd-MM-yyyy") + "Break down analyis", TemplateGraph);
                }
                else if (worksheetGraph == null)
                {
                    //worksheetGraph = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("dd-MM-yyyy") + "Break down analyis", TemplateGraph);
                }
                int sheetcount = p.Workbook.Worksheets.Count;
                p.Workbook.Worksheets.MoveToStart(sheetcount - 1);
                worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                int StartRow = 5;



                DateTime st1 = Convert.ToDateTime(FromDate.ToShortDateString() + " 00:00:00");
                DateTime st2 = Convert.ToDateTime(ToDate.ToShortDateString() + " 23:59:59");



                var jobs = db.CheckListJobMaster.Where(m => m.CheckListStartTime >= st1 && m.CheckListStartTime <= st2 && m.OverAllJobCompleted == true && m.OverAllApproved == true && m.CheckListJobLineNumber == data.lineNo && m.IsDeleted == false).OrderBy(m => m.CheckListStartTime).ToList();

                if (jobs.Count > 0)
                {


                    double totoltime = 0;
                    int countNo = 0;

                    foreach (var job in jobs)
                    {


                        var weeke = (DateTime)job.CheckListStartTime;

                        var datee = Convert.ToDateTime(job.CheckListStartTime);
                        worksheet.Cells["A" + StartRow].Value = datee.ToString("dd-MMM-yy");

                        var shiftname = db.ShiftMaster.Where(m => m.ShiftId == job.CheckListShiftNumber).Select(m => m.ShiftName).FirstOrDefault();
                        worksheet.Cells["B" + StartRow].Value = shiftname;

                        var preGrade = db.GradeMaster.Where(m => m.GradeId == job.PreviousGrade).Select(m => m.GradeName).FirstOrDefault();
                        worksheet.Cells["C" + StartRow].Value = preGrade;

                        var nxtGrade = db.GradeMaster.Where(m => m.GradeId == job.CurrentColor).Select(m => m.GradeName).FirstOrDefault();
                        worksheet.Cells["D" + StartRow].Value = nxtGrade;
                        var typeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == job.CheckListJobTypeId).Select(m => m.CheckListTypeName).FirstOrDefault();
                        worksheet.Cells["E" + StartRow].Value = typeName;

                        var Stdatee = Convert.ToDateTime(job.CheckListStartTime);
                        worksheet.Cells["F" + StartRow].Value = Stdatee.ToString("yyyy-MM-dd hh:mm:ss");

                        var Eddatee = Convert.ToDateTime(job.CheckListEndTime);
                        worksheet.Cells["G" + StartRow].Value = Eddatee.ToString("yyyy-MM-dd hh:mm:ss");

                        var totaltimee = ((DateTime)(job.CheckListEndTime) - (DateTime)(job.CheckListStartTime)).TotalMinutes;
                        worksheet.Cells["H" + StartRow].Value = Math.Round(totaltimee, 2);
                        //var weekkk = 33 / 7;
                        //worksheet.Cells["I" + StartRow].Value = weekkk;

                        if (totaltimee <= job.EstimatedEndTime)
                        {
                            worksheet.Cells["M" + StartRow].Value = "Time Achieved";

                            using (ExcelRange Rng = worksheet.Cells["M" + StartRow])
                            {
                                // Rng.Value = "Text Color & Background Color";
                                // Rng.Merge = true;
                                // Rng.Style.Font.Bold = true;

                                // Rng.Style.Font.Color.SetColor(Color.Red);
                                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Rng.Style.Fill.BackgroundColor.SetColor(Color.Green);
                            }




                        }
                        else
                        {
                            worksheet.Cells["M" + StartRow].Value = "Extra Time";
                            using (ExcelRange Rng = worksheet.Cells["M" + StartRow])
                            {
                                // Rng.Value = "Text Color & Background Color";
                                // Rng.Merge = true;
                                // Rng.Style.Font.Bold = true;

                                // Rng.Style.Font.Color.SetColor(Color.Red);
                                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Rng.Style.Fill.BackgroundColor.SetColor(Color.Red);
                            }


                        }

                        var usernames = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == job.CheckListJobId).ToList();
                        List<string> userlist = new List<string>();
                        string alluserIds = "";
                        foreach (var alluser in usernames)
                        {

                            var alluserId1 = alluser.PrimaryResource;
                            alluserIds += alluserId1 + ",";
                        }
                        alluserIds = alluserIds.TrimEnd(',');
                        var userarray = alluserIds.Split(',');
                        var userlists = userarray.ToList().Distinct();

                        //var disUs = userLists.Distinct();
                        string usernameall = "";
                        foreach (var urs in userlists)
                        {
                            int usIds = Convert.ToInt32(urs);
                            var usernamess = db.UserDetails.Where(m => m.UserId == usIds).Select(m => m.UserName).FirstOrDefault();
                            usernameall = usernamess + ",";


                        }

                        worksheet.Cells["N" + StartRow].Value = usernameall.TrimEnd(',');



                        totoltime = totoltime + totaltimee;
                        countNo = countNo + 1;
                        StartRow++;

                    }


                    worksheet.Cells["C2"].Value = "Average COT " + line12 + " (Hrs)";
                    worksheet.Cells["D2"].Value = Math.Round(((totoltime / countNo) / 60), 2);

                    worksheet.Cells["H2"].Value = countNo;

                }

                p.Save();

                obj.isStatus = true;
                obj.response = retrivalPath;

                //Downloding Excel
                //  string path1 = System.IO.Path.Combine(FileDir, "OEE_Report" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd") + ".xlsx");


            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }

            return obj;
        }


        #endregion
    }
}
