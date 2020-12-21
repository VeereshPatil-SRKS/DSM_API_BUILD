using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobMasterEntity;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.NotificationEntity;

namespace DSM.DAL
{
    public class NotificationDAL// : INotification
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(NotificationDAL));
        public NotificationDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// View Multiple Document 
        /// </summary>
        /// <returns></returns>
        public CommonResponse AllNotification(int userId)
        {
            NotificationCustom notificationCustom = new NotificationCustom();
            CommonResponse obj = new CommonResponse();
            try
            {
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
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).FirstOrDefault(),
                                  isCompleted = jbop.CheckListJobIsCompleted,
                                  isClosed = jbop.IsJobClosed,
                                  isAdminApproved = jbop.IsAdminApproved
                              }).ToList();

                var userDetails = db.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
                if (userDetails.RoleId == 2)
                {
                    result = (from wf in db.CheckListJobMaster
                              join jbop in db.CheckListJobWrtoperator on wf.CheckListJobId equals jbop.CheckListJobMasterId
                              where wf.IsDeleted == false && jbop.CheckListJobIsCompleted == true && jbop.CheckListJobIsPartialCompleted == false && jbop.IsAdminApproved == true && wf.CheckListJobSupervisorId == userId
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
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).FirstOrDefault(),
                                  isCompleted = jbop.CheckListJobIsCompleted,
                                  isClosed = jbop.IsJobClosed,
                                  isAdminApproved = jbop.IsAdminApproved
                              }).ToList();

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
                        checkListJobcustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListStartTime);
                        checkListJobcustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListEndTime);
                        if (item.assignedOperators != null)
                        {
                            if (item.assignedOperators.PrimaryResourceToAllFlag == true)
                            {
                                checkListJobcustom.assignedOperators = "All";
                            }
                            else
                            {
                                string operatorIds = item.assignedOperators.PrimaryResource;
                                List<long> opId = operatorIds.Split(',').Select(long.Parse).ToList();
                                var opItem = String.Join(",", db.UserDetails.Where(m => opId.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                                checkListJobcustom.assignedOperators = opItem;
                            }
                        }
                        if (item.isClosed == true)
                        {
                            if (item.isAdminApproved == true)
                            {
                                checkListJobcustom.checkListJobStatus = "Job Closed And Approved By Admin";
                            }
                            else
                            {
                                checkListJobcustom.checkListJobStatus = "Job Closed And Not Approved By Admin";
                            }
                        }
                        else if (item.isCompleted == true)
                        {
                            if (item.isAdminApproved == true)
                            {
                                checkListJobcustom.checkListJobStatus = "Job Completed By Operator And Approved By Admin";
                            }
                            else
                            {
                                checkListJobcustom.checkListJobStatus = "Job Completed By Operator And Not Approved By Admin";
                            }

                        }
                        else if (item.isCompleted == false)
                        {
                            checkListJobcustom.checkListJobStatus = "Job Not Yet Completed By Operator";
                        }
                        checkListJobcustoms.Add(checkListJobcustom);

                    }
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
        

    }
}
