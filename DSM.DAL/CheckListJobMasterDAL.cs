using DSM.DAL.App_Start;
using DSM.DAL.Helpers;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobMasterEntity;
using static DSM.EntityModels.CheckListSupervisorApprovalEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobMasterDAL : ICheckListJobMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobMasterDAL));
        private readonly AppSettings appSettings;
        public CheckListJobMasterDAL(DSMContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// 

        //public CommonResponseWithIds AddAndEditCheckListJob(CheckListJobCustom data, long userId)
        //{
        //    CommonResponseWithIds obj = new CommonResponseWithIds();
        //    CommonFunction commonFunction = new CommonFunction();
        //    try
        //    {
        //        var res = db.CheckListJobMaster.Where(m => m.CheckListJobId == data.checkListJobId).FirstOrDefault();
        //        var groupName = (from wf in db.CheckListMaster
        //                         where wf.IsDeleted == false && wf.CheckListId == data.checkListMasterId
        //                         select wf.CheckListGroup).FirstOrDefault();
        //        DateTime st = DateTime.Now;
        //        DateTime et = DateTime.Now;

        //        long typeId = commonFunction.GetTypeDetails(appSettings);
        //        #region ST and ET
        //        try
        //        {
        //            st = Convert.ToDateTime(data.checkListStartTime);
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        try
        //        {
        //            //et = Convert.ToDateTime(data.checkListEndTime);
        //            et = st.AddMinutes(Convert.ToDouble(data.estimatedTime));
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        #endregion
        //        if (res == null)
        //        {
        //            try
        //            {
        //                CheckListJobMaster item = new CheckListJobMaster();
        //                item.CheckListMasterId = data.checkListMasterId;
        //                item.CheckListJobName = data.checkListJobName;
        //                item.CheckListJobDescription = data.checkListJobDescription ;
        //                item.CheckListJobCategoryId = data.checkListJobCategoryId ;
        //                item.CheckListJobTypeId = data.checkListJobTypeId;
        //                item.CheckListJobSupervisorId = data.checkListJobSupervisorId ;
        //                item.CheckListJobLineNumber = data.checkListJobLineNumber ;
        //                item.CheckListShiftNumber = data.checkListShiftNumber ;
        //                item.CheckListStartTime = st ;
        //                item.CheckListEndTime = et ;
        //                item.PreviousGrade = data.previousGrade ;


        //                item.CurrentGrade = data.currentGrade ;
        //                item.PreviousColor = data.previousColor ;
        //                item.CurrentColor = data.currentGrade ;
        //                item.CheckListGroup = groupName;
        //                item.BatchNumber = data.batchNumber;
        //                item.ProcessOrderNumber =data. processOrderNumber;
        //                item.OverAllApproved =false;
        //                item.OverAllRejected =false;
        //                item.OverAllJobCompleted = false;
        //                item.IsActive = true;
        //                item.IsDeleted = false;
        //                item.CreatedBy = userId;
        //                item.CreatedOn = DateTime.Now;
        //                item.EstimatedEndTime = data.estimatedTime;
        //                db.CheckListJobMaster.Add(item);
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.AddedSucessfully;
        //                obj.isStatus = true;
        //                obj.id = item.CheckListJobId;
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
        //                res.CheckListJobName = data.checkListJobName;
        //                res.CheckListJobSupervisorId = data.checkListJobSupervisorId;
        //                res.CheckListJobLineNumber = data.checkListJobLineNumber;
        //                res.CheckListShiftNumber = data.checkListShiftNumber;
        //                res.CheckListStartTime = st;
        //                res.CheckListEndTime = et;
        //                res.CheckListGroup = groupName;
        //                res.BatchNumber = data.batchNumber;
        //                res.ProcessOrderNumber = data.processOrderNumber;
        //                res.ModifiedBy = userId;
        //                res.EstimatedEndTime = data.estimatedTime;
        //                res.ModifiedOn = DateTime.Now;
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.UpdatedSucessfully;
        //                obj.isStatus = true;
        //                obj.id = data.checkListJobId;
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




        //public CommonResponseWithIds AddAndEditCheckListJob(CheckListJobCustom data, long userId)
        //{
        //    CommonResponseWithIds obj = new CommonResponseWithIds();
        //    CommonFunction commonFunction = new CommonFunction();
        //    try
        //    {
        //        var res = db.CheckListJobMaster.Where(m => m.CheckListJobId == data.checkListJobId).FirstOrDefault();
        //        var groupName = (from wf in db.CheckListMaster
        //                         where wf.IsDeleted == false && wf.CheckListId == data.checkListMasterId
        //                         select wf.CheckListGroup).FirstOrDefault();
        //        DateTime st = DateTime.Now;
        //        DateTime et = DateTime.Now;

        //        long typeId = commonFunction.GetTypeDetails(appSettings);
        //        #region ST and ET
        //        try
        //        {
        //            st = Convert.ToDateTime(data.checkListStartTime);
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        try
        //        {
        //            //et = Convert.ToDateTime(data.checkListEndTime);
        //            et = st.AddMinutes(Convert.ToDouble(data.estimatedTime));
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        #endregion
        //        if (res == null)
        //       {
        //            try
        //            {
        //                CheckListJobMaster item = new CheckListJobMaster();
        //                item.CheckListMasterId = data.checkListMasterId;
        //                item.CheckListJobName = data.checkListJobName;
        //                item.CheckListJobDescription = data.checkListJobDescription;
        //                item.CheckListJobCategoryId = data.checkListJobCategoryId;
        //                item.CheckListJobTypeId = data.checkListJobTypeId;
        //                item.CheckListJobSupervisorId = data.checkListJobSupervisorId;
        //                item.CheckListJobLineNumber = data.checkListJobLineNumber;
        //                item.CheckListShiftNumber = data.checkListShiftNumber;
        //                item.CheckListStartTime = st;
        //                item.CheckListEndTime = et;

        //                if (data.previousGrade != 0)
        //                {
        //                    item.PreviousGrade = data.previousGrade;
        //                }

        //                else
        //                {
        //                    string[] SplitpreviousGradeButton = data.previousGradeButton.Split('-');



        //                    //   List<string> listIds = new List<string>();
        //                    //  listIds = SplitpreviousGradeButton.ToList();

        //                    // var pregrade = db.GradeMaster.Where(m => m.GradeName == listIds[0] || m.GradeCode == listIds[1]).FirstOrDefault();

        //                    var pregrade = db.GradeMaster.Where(m => m.GradeName == SplitpreviousGradeButton[0] || m.GradeCode == SplitpreviousGradeButton[1]).FirstOrDefault();
        //                    if (pregrade == null)
        //                    {

        //                        GradeMaster item1 = new GradeMaster();
        //                        // item1.GradeName = listIds[0];
        //                        // item1.GradeCode = listIds[1];
        //                        // item1.GradeDescription = listIds[0];

        //                        item1.GradeName = SplitpreviousGradeButton[0];
        //                        item1.GradeCode = SplitpreviousGradeButton[1];
        //                        item1.GradeDescription = SplitpreviousGradeButton[0];

        //                        item1.IsActive = true;
        //                        item1.IsDeleted = false;
        //                        item1.CreatedBy = userId;
        //                        item1.CreatedOn = DateTime.Now;
        //                        db.GradeMaster.Add(item1);
        //                        db.SaveChanges();
        //                       // obj.response = ResourceResponse.AddedSucessfully;
        //                       // obj.isStatus = true;


        //                        item.PreviousGrade = item1.GradeId;

        //                    }
        //                    else
        //                    {

        //                    //    pregrade.GradeName = listIds[0];
        //                    //    pregrade.GradeCode = listIds[1];
        //                    //    pregrade.GradeDescription = listIds[0];

        //                        pregrade.GradeName = SplitpreviousGradeButton[0];
        //                        pregrade.GradeCode = SplitpreviousGradeButton[1];
        //                        pregrade.GradeDescription = SplitpreviousGradeButton[0];


        //                        pregrade.ModifiedBy = userId;
        //                        pregrade.ModifiedOn = DateTime.Now;
        //                        db.SaveChanges();
        //                      //  obj.response = ResourceResponse.UpdatedSucessfully;
        //                      //  obj.isStatus = true;

        //                        item.PreviousGrade = pregrade.GradeId;

        //                    }

        //                }





        //                //item.CurrentGrade = data.currentGrade;

        //                if (data.currentGrade != 0)
        //                {
        //                    item.CurrentGrade = data.currentGrade;
        //                }
        //                else
        //                {
        //                    string[] SplitcurrentGradeButton = data.currentGradeButton.Split('-');

        //                    //List<string> listcurrentGradeIds = new List<string>();
        //                    //listcurrentGradeIds = SplitcurrentGradeButton.ToList();

        //                    //var pregrade = db.GradeMaster.Where(m => m.GradeName == listcurrentGradeIds[0] || m.GradeCode == listcurrentGradeIds[1]).FirstOrDefault();

        //                    var pregrade = db.GradeMaster.Where(m => m.GradeName == SplitcurrentGradeButton[0] || m.GradeCode == SplitcurrentGradeButton[1]).FirstOrDefault();

        //                    if (pregrade == null)
        //                    {

        //                        GradeMaster item1 = new GradeMaster();
        //                        item1.GradeName = SplitcurrentGradeButton[0];
        //                        item1.GradeCode = SplitcurrentGradeButton[1];
        //                        item1.GradeDescription = SplitcurrentGradeButton[0];
        //                        item1.IsActive = true;
        //                        item1.IsDeleted = false;
        //                        item1.CreatedBy = userId;
        //                        item1.CreatedOn = DateTime.Now;
        //                        db.GradeMaster.Add(item1);
        //                        db.SaveChanges();
        //                      //  obj.response = ResourceResponse.AddedSucessfully;
        //                      //  obj.isStatus = true;


        //                        item.CurrentGrade = item1.GradeId;

        //                    }
        //                    else
        //                    {
        //                        pregrade.GradeName = SplitcurrentGradeButton[0];
        //                        pregrade.GradeCode = SplitcurrentGradeButton[1];
        //                        pregrade.GradeDescription = SplitcurrentGradeButton[0];
        //                        pregrade.ModifiedBy = userId;
        //                        pregrade.ModifiedOn = DateTime.Now;
        //                        db.SaveChanges();
        //                       // obj.response = ResourceResponse.UpdatedSucessfully;
        //                      //  obj.isStatus = true;

        //                        item.PreviousGrade = pregrade.GradeId;

        //                    }

        //                }



        //                item.PreviousColor = data.previousColor;
        //                item.CurrentColor = data.currentColor;
        //                item.CheckListGroup = groupName;
        //                item.BatchNumber = data.batchNumber;
        //                item.ProcessOrderNumber = data.processOrderNumber;
        //                item.OverAllApproved = false;
        //                item.OverAllRejected = false;
        //                item.OverAllJobCompleted = false;
        //                item.IsActive = true;
        //                item.IsDeleted = false;
        //                item.CreatedBy = userId;
        //                item.CreatedOn = DateTime.Now;
        //                item.EstimatedEndTime = data.estimatedTime;
        //                db.CheckListJobMaster.Add(item);
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.AddedSucessfully;
        //                obj.isStatus = true;
        //                obj.id = item.CheckListJobId;
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
        //                res.CheckListJobName = data.checkListJobName;
        //                res.CheckListJobSupervisorId = data.checkListJobSupervisorId;
        //                res.CheckListJobLineNumber = data.checkListJobLineNumber;
        //                res.CheckListShiftNumber = data.checkListShiftNumber;
        //                res.CheckListStartTime = st;
        //                res.CheckListEndTime = et;
        //                res.CheckListGroup = groupName;
        //                res.BatchNumber = data.batchNumber;
        //                res.ProcessOrderNumber = data.processOrderNumber;
        //                res.ModifiedBy = userId;
        //                res.EstimatedEndTime = data.estimatedTime;
        //                res.ModifiedOn = DateTime.Now;
        //                db.SaveChanges();
        //                obj.response = ResourceResponse.UpdatedSucessfully;
        //                obj.isStatus = true;
        //                obj.id = data.checkListJobId;
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



        public CommonResponseWithIds AddAndEditCheckListJob(CheckListJobCustom data, long userId)
        {
            CommonResponseWithIds obj = new CommonResponseWithIds();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var res = db.CheckListJobMaster.Where(m => m.CheckListJobId == data.checkListJobId).FirstOrDefault();
                var groupName = (from wf in db.CheckListMaster
                                 where wf.IsDeleted == false && wf.CheckListId == data.checkListMasterId
                                 select wf.CheckListGroup).FirstOrDefault();
                DateTime st = DateTime.Now;
                DateTime et = DateTime.Now;

                long typeId = commonFunction.GetTypeDetails(appSettings);
                #region ST and ET
                try
                {
                    st = Convert.ToDateTime(data.checkListStartTime);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    //et = Convert.ToDateTime(data.checkListEndTime);
                    et = st.AddMinutes(Convert.ToDouble(data.estimatedTime));
                }
                catch (Exception ex)
                {

                }
                #endregion
                if (res == null)
                {
                    try
                    {
                        CheckListJobMaster item = new CheckListJobMaster();
                        item.CheckListMasterId = data.checkListMasterId;
                        item.CheckListJobName = data.checkListJobName;
                        item.CheckListJobDescription = data.checkListJobDescription;
                        item.CheckListJobCategoryId = data.checkListJobCategoryId;
                        item.CheckListJobTypeId = data.checkListJobTypeId;
                        item.CheckListJobSupervisorId = data.checkListJobSupervisorId;
                        item.CheckListJobLineNumber = data.checkListJobLineNumber;
                        item.CheckListShiftNumber = data.checkListShiftNumber;
                        item.CheckListStartTime = st;
                        item.CheckListEndTime = et;

                        string[] SplitpreviousGradeButton = data.previousGrade.Split('-');
                        var pregrade = db.GradeMaster.Where(m => m.IsDeleted == false && m.GradeName == SplitpreviousGradeButton[0] ).FirstOrDefault();

                          //  var pregrade = db.GradeMaster.Where(m => m.GradeName == SplitpreviousGradeButton[0] || m.GradeCode == SplitpreviousGradeButton[1]).FirstOrDefault();
                          
                            
                            
                            if (pregrade == null)
                            {

                                GradeMaster item1 = new GradeMaster();
                                // item1.GradeName = listIds[0];
                                // item1.GradeCode = listIds[1];
                                // item1.GradeDescription = listIds[0];

                                item1.GradeName = SplitpreviousGradeButton[0];
                                item1.GradeCode = SplitpreviousGradeButton[1];
                                item1.GradeDescription = SplitpreviousGradeButton[0];

                                item1.IsActive = true;
                                item1.IsDeleted = false;
                                item1.CreatedBy = userId;
                                item1.CreatedOn = DateTime.Now;
                                db.GradeMaster.Add(item1);
                                db.SaveChanges();
                                // obj.response = ResourceResponse.AddedSucessfully;
                                // obj.isStatus = true;


                                item.PreviousGrade = item1.GradeId;

                            }
                            else
                            {

                                //    pregrade.GradeName = listIds[0];
                                //    pregrade.GradeCode = listIds[1];
                                //    pregrade.GradeDescription = listIds[0];

                                pregrade.GradeName = SplitpreviousGradeButton[0];
                              //  pregrade.GradeCode = SplitpreviousGradeButton[1];
                                pregrade.GradeDescription = SplitpreviousGradeButton[0];


                                pregrade.ModifiedBy = userId;
                                pregrade.ModifiedOn = DateTime.Now;
                                db.SaveChanges();
                                //  obj.response = ResourceResponse.UpdatedSucessfully;
                                //  obj.isStatus = true;

                                item.PreviousGrade = pregrade.GradeId;

                            }

                          string[] SplitcurrentGradeButton = data.currentGrade.Split('-');

                            //List<string> listcurrentGradeIds = new List<string>();
                            //listcurrentGradeIds = SplitcurrentGradeButton.ToList();

                            //var pregrade = db.GradeMaster.Where(m => m.GradeName == listcurrentGradeIds[0] || m.GradeCode == listcurrentGradeIds[1]).FirstOrDefault();

                            var curGrade = db.GradeMaster.Where(m =>m.IsDeleted == false && m.GradeName == SplitcurrentGradeButton[0]).FirstOrDefault();

                            if (curGrade == null)
                            {

                                GradeMaster item1 = new GradeMaster();
                                item1.GradeName = SplitcurrentGradeButton[0];
                                item1.GradeCode = SplitcurrentGradeButton[1];
                                item1.GradeDescription = SplitcurrentGradeButton[0];
                                item1.IsActive = true;
                                item1.IsDeleted = false;
                                item1.CreatedBy = userId;
                                item1.CreatedOn = DateTime.Now;
                                db.GradeMaster.Add(item1);
                                db.SaveChanges();
                                //  obj.response = ResourceResponse.AddedSucessfully;
                                //  obj.isStatus = true;


                                item.CurrentGrade = item1.GradeId;

                            }
                            else
                            {
                                curGrade.GradeName = SplitcurrentGradeButton[0];
                               // pregrade.GradeCode = SplitcurrentGradeButton[1];
                                curGrade.GradeDescription = SplitcurrentGradeButton[0];
                                curGrade.ModifiedBy = userId;
                                curGrade.ModifiedOn = DateTime.Now;
                                db.SaveChanges();
                                // obj.response = ResourceResponse.UpdatedSucessfully;
                                //  obj.isStatus = true;

                                item.CurrentGrade = curGrade.GradeId;

                            }

                       



                        item.PreviousColor = data.previousColor;
                        item.CurrentColor = data.currentColor;
                        item.CheckListGroup = groupName;
                        item.BatchNumber = data.batchNumber;
                        item.ProcessOrderNumber = data.processOrderNumber;
                        item.OverAllApproved = false;
                        item.OverAllRejected = false;
                        item.OverAllJobCompleted = false;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        item.EstimatedEndTime = data.estimatedTime;
                        db.CheckListJobMaster.Add(item);
                        db.SaveChanges();
                        obj.response = ResourceResponse.AddedSucessfully;
                        obj.isStatus = true;
                        obj.id = item.CheckListJobId;
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
                        res.CheckListJobName = data.checkListJobName;
                        res.CheckListJobSupervisorId = data.checkListJobSupervisorId;
                        res.CheckListJobLineNumber = data.checkListJobLineNumber;
                        res.CheckListShiftNumber = data.checkListShiftNumber;
                        res.CheckListStartTime = st;
                        res.CheckListEndTime = et;
                        res.CheckListGroup = groupName;
                        res.BatchNumber = data.batchNumber;
                        res.ProcessOrderNumber = data.processOrderNumber;
                        res.ModifiedBy = userId;
                        res.EstimatedEndTime = data.estimatedTime;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        obj.response = ResourceResponse.UpdatedSucessfully;
                        obj.isStatus = true;
                        obj.id = data.checkListJobId;
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
        /// GetCheckListJobType
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse GetCheckListJobType(int checkListId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListMaster
                              where wf.CheckListId == checkListId
                              select new
                              {
                                  checkListTypeId = wf.CheckListTypeId,
                                  checkListTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wf.CheckListTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
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
        /// Check List Update End Time Of Job By Check List Job Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListEndTime"></param>
        /// <returns></returns>
        public CommonResponse UpdateCheckListEndTimeOfJobByCheckListJobId(int checkListJobId, string checkListEndTime, long userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                DateTime et = DateTime.Now;
                try
                {
                    et = Convert.ToDateTime(checkListEndTime);
                }
                catch (Exception ex)
                {

                }

                var result = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobId).FirstOrDefault();
                if (result != null)
                {
                    result.CheckListEndTime = et;
                    result.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    obj.response = ResourceResponse.UpdatedSucessfully;
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
        /// Move Check List Data To Check List Job
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse MoveCheckListDataToCheckListJob(int checkListJobId, int checkListId, int userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var checkListAdvancedList = db.CheckListAdvanceMaster.Where(m => m.CheckListMasterId == checkListId && m.IsDeleted == false).ToList();
                var checkListActivityList = db.CheckListActivityMaster.Where(m => m.CheckListMasterId == checkListId && m.IsDeleted == false).ToList();
                var checkListLOTOTOList = db.CheckListLototomaster.Where(m => m.CheckListMasterId == checkListId && m.IsDeleted == false).ToList();
                #region Advance checkList
                foreach (var checkListAdvanced in checkListAdvancedList)
                {
                    CheckListJobAdvanceMaster item = new CheckListJobAdvanceMaster();
                    item.CheckListJobMasterId = checkListJobId;
                    item.CheckListJobGroupId = checkListAdvanced.CheckListGroupId;
                    item.CheckListJobStepNumber = checkListAdvanced.CheckListStepNumber;
                    item.ActivityBeforeChangeOverDescription = checkListAdvanced.ActivityBeforeChangeOverDescription;
                    item.Remarks = checkListAdvanced.Remarks;
                    item.IsActive = true;
                    item.IsDeleted = false;
                    item.IsAdminApproved = true;
                    item.CreatedBy = userId;
                    item.CreatedOn = DateTime.Now;
                    db.CheckListJobAdvanceMaster.Add(item);
                    db.SaveChanges();
                }
                #endregion
                #region Activity
                foreach (var checkListActivity in checkListActivityList)
                {
                    CheckListJobActivityMaster item = new CheckListJobActivityMaster();
                    item.CheckListJobMasterId = checkListJobId;
                    item.CheckListJobGroupId = checkListActivity.CheckListGroupId;
                    item.ActivitySubCategoryId = checkListActivity.ActivitySubCategoryId;
                    item.CheckListJobStepNumber = checkListActivity.CheckListStepNumber;
                    item.Remarks = checkListActivity.Remarks;
                    item.ActivityDescription = checkListActivity.ActivityDescription;
                    item.IsActivityManditory = checkListActivity.IsActivityManditory;
                    item.IsPhotoManditory = checkListActivity.IsPhotoManditory;
                    item.IsBarCodeManditory = checkListActivity.IsBarCodeManditory;
                    item.ExpectedCompletionTime = checkListActivity.ExpectedCompletionTime;
                    item.AssetId = checkListActivity.AssetId;
                    item.IsActive = true;
                    item.IsDeleted = false;
                    item.IsAdminApproved = true;
                    item.CreatedBy = userId;
                    item.CreatedOn = DateTime.Now;
                    db.CheckListJobActivityMaster.Add(item);
                    db.SaveChanges();
                }
                #endregion
                #region LOTOTO
                foreach (var checkListLOTOTO in checkListLOTOTOList)
                {
                    CheckListJobLototomaster item = new CheckListJobLototomaster();
                    item.CheckListJobMasterId = checkListJobId;
                    item.CheckListJobGroupId = checkListLOTOTO.CheckListGroupId;
                    item.CheckListJobLockStepNumber = checkListLOTOTO.CheckListLockStepNumber;
                    item.PositionDescription = checkListLOTOTO.PositionDescription;
                    item.IsLockOutRequired = checkListLOTOTO.IsLockOutRequired;
                    item.IsTagOutRequired = checkListLOTOTO.IsTagOutRequired;
                    item.IsTryOutRequired = checkListLOTOTO.IsTryOutRequired;
                    item.Remarks = checkListLOTOTO.Remarks;
                    item.IsActive = true;
                    item.IsDeleted = false;
                    item.IsAdminApproved = true;
                    item.CreatedBy = userId;
                    item.CreatedOn = DateTime.Now;
                    db.CheckListJobLototomaster.Add(item);
                    db.SaveChanges();
                }
                #endregion

                obj.isStatus = true;
                obj.response = ResourceResponse.MovedSuccessfully;
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
        public CommonResponse ViewMultipleCheckListJob()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobMaster
                              where wf.IsDeleted == false
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
                                  checkListGroup = wf.CheckListGroup,
                                  estimatedTime = wf.EstimatedEndTime
                              }).OrderByDescending(m => m.checkListJobId).ToList();
                if (result.Count() != 0)
                {
                    List<CheckListJobcustoms> checkListJobcustoms = new List<CheckListJobcustoms>();
                    foreach (var item in result)
                    {

                        long? checkListMasterId = item.checkListMasterId;
                        var advanceCheckList = db.CheckListAdvanceMaster.Where(m => m.CheckListMasterId == checkListMasterId).Select(m => m.CheckListGroupId).Distinct().ToList();
                        var activityCheckList = db.CheckListActivityMaster.Where(m => m.CheckListMasterId == checkListMasterId).Select(m => m.CheckListGroupId).Distinct().ToList();
                        var lototoCheckList = db.CheckListLototomaster.Where(m => m.CheckListMasterId == checkListMasterId).Select(m => m.CheckListGroupId).Distinct().ToList();


                        string checkListGroup = item.checkListGroup;
                        List<long> ckId = checkListGroup.Split(',').Select(long.Parse).ToList();

                        var finalList = advanceCheckList.Union(activityCheckList).Union(lototoCheckList).OrderBy(x => x.Value).ToList();
                        var groups = (from wf in db.CheckListGroupMaster
                                      where wf.IsDeleted == false && ckId.Contains(wf.CheckListGroupId)
                                      select wf.CheckListGroupName).ToList();

                        CheckListJobcustoms checkListJobcustom = new CheckListJobcustoms();
                        checkListJobcustom.checkListJobId = item.checkListJobId;
                        checkListJobcustom.checkListMasterId = item.checkListMasterId;
                        checkListJobcustom.checkListJobName = item.checkListJobName;
                        checkListJobcustom.batchNumber = item.batchNumber;
                        checkListJobcustom.processOrderNumber = item.processOrderNumber;
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
                        checkListJobcustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListStartTime);
                        checkListJobcustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.checkListEndTime);
                        checkListJobcustom.previousGradeName = item.previousGradeName;
                        checkListJobcustom.currentGradeName = item.currentGradeName;
                        checkListJobcustom.previousColorName = item.previousColorName;
                        checkListJobcustom.currentColorName = item.currentColorName;
                        checkListJobcustom.checkListJobCategoryName = item.checkListJobCategoryName;
                        checkListJobcustom.checkListJobTypeName = item.checkListJobTypeName;
                        checkListJobcustom.estimatedTime = item.estimatedTime;
                        if (item.assignedOperators.Count != 0)
                        {
                            var opts = item.assignedOperators.Select(m => m.PrimaryResource).ToList();
                            string operatorIds = String.Join(",", opts);
                            List<long> opId = operatorIds.Split(',').Select(long.Parse).ToList();
                            var opItem = String.Join(",", db.UserDetails.Where(m => opId.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                            checkListJobcustom.assignedOperators = opItem;

                        }
                        checkListJobcustom.checkListJobGroupName = String.Join(",", groups);
                        checkListJobcustom.checkListJobGroup = item.checkListGroup;

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

        /// <summary>
        /// View Multiple CheckList Job WRT User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListJobWRTUser(int userId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                CommonFunction commonFunction = new CommonFunction();
                var userDetails = commonFunction.GetUserDetails(userId);

                if (userDetails.RoleId == 2 || userDetails.RoleId == 1)
                {
                    if (userDetails.RoleId == 1)
                        obj.response = ResourceResponse.NoJobsAssignedSuperAdmin;
                    if (userDetails.RoleId == 2)
                        obj.response = ResourceResponse.NoJobsAssignedAdmin;
                    obj.isStatus = false;
                }
                else
                {


                    var result = (from wf in db.CheckListJobMaster
                                  where wf.IsDeleted == false
                                  select new
                                  {
                                      checkListJobId = wf.CheckListJobId,
                                      checkListMasterId = wf.CheckListMasterId,
                                      checkListMasterDetails = (from wfs in db.CheckListMaster
                                                                where wfs.CheckListId == wf.CheckListMasterId
                                                                select new
                                                                {
                                                                    checkListId = wfs.CheckListId,
                                                                    checkListName = wfs.CheckListName,
                                                                    checkListDescription = wfs.CheckListDescription,
                                                                    checkListOwner = wfs.CheckListOwner,
                                                                    checkListVersion = wfs.CheckListVersion,
                                                                    checkListCategoryId = wfs.CheckListCategoryId,
                                                                    checkListCategoryName = db.CheckListCategoryMaster.Where(m => m.CheckListCategoryId == wfs.CheckListCategoryId).Select(m => m.CheckListCategoryName).FirstOrDefault(),
                                                                    checkListTypeId = wfs.CheckListTypeId,
                                                                    checkListTypeName = db.CheckListTypeMaster.Where(m => m.CheckListTypeId == wfs.CheckListTypeId).Select(m => m.CheckListTypeName).FirstOrDefault(),
                                                                    isActive = wfs.IsActive
                                                                }).FirstOrDefault(),
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
                                      overallrejected = wf.OverAllRejected,
                                      overallapproved = wf.OverAllApproved,
                                      overallcompleted = wf.OverAllJobCompleted,
                                      estimatedTime = wf.EstimatedEndTime

                                  }).OrderByDescending(wf => wf.checkListStartTime).ToList();
                    if (result.Count() != 0)
                    {
                        List<CheckListJobcustoms> checkListJobcustoms = new List<CheckListJobcustoms>();
                        foreach (var item in result)
                        {
                            if (item.assignedOperators.Count != 0)
                            {
                                foreach (var opItems in item.assignedOperators)
                                {
                                    string operatorIds = opItems.PrimaryResource;
                                    List<long> opId = operatorIds.Split(',').Select(long.Parse).ToList();
                                    if (opId.Contains(userId) || opItems.PrimaryResourceToAllFlag == true || opItems.SecondaryResourceToAllFlag == true)
                                    {
                                        CheckListJobcustoms checkListJobcustom = new CheckListJobcustoms();
                                        checkListJobcustom.checkListJobId = item.checkListJobId;
                                        checkListJobcustom.checkListMasterId = item.checkListMasterId;
                                        DateTime stDate = Convert.ToDateTime(item.checkListStartTime);
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
                                        checkListJobcustom.checkListJobGroupId = opItems.CheckListJobGroupId;
                                        checkListJobcustom.estimatedTime = item.estimatedTime;
                                        checkListJobcustom.checkListJobTypeName = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == opItems.CheckListJobGroupId).Select(m => m.CheckListGroupName).FirstOrDefault();
                                        bool isNotYetStarted = false;
                                        long opIdByCompletedOperator = 0;
                                        bool checkIsComepletedByOtherOperator = false;
                                        if (item.assignedOperators.Count != 0)
                                        {

                                            string operatorIdss = opItems.PrimaryResource;
                                            List<long> opIds = operatorIdss.Split(',').Select(long.Parse).ToList();
                                            var opItem = String.Join(",", db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                                            checkListJobcustom.assignedOperators = opItem;
                                            foreach (var its in opId)
                                            {
                                                var ress = db.CheckListJobWrtoperator.Where(m => m.OperatorId == its && m.CheckListJobMasterId == item.checkListJobId && m.CheckListJobGroupId == opItems.CheckListJobGroupId && m.CheckListJobIsCompleted == false).FirstOrDefault();
                                                if (ress != null)
                                                {
                                                    isNotYetStarted = true;
                                                    opIdByCompletedOperator = its;
                                                    break;
                                                }
                                                var ress1 = db.CheckListJobWrtoperator.Where(m => m.OperatorId == its && m.CheckListJobMasterId == item.checkListJobId && m.CheckListJobGroupId == opItems.CheckListJobGroupId && m.CheckListJobIsCompleted == true).FirstOrDefault();
                                                if (ress1 != null)
                                                {
                                                    checkIsComepletedByOtherOperator = true;
                                                    isNotYetStarted = true;
                                                    opIdByCompletedOperator = its;
                                                    break;
                                                }
                                            }

                                        }

                                        var checkListJobWRTOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == item.checkListJobId && m.OperatorId == userId && m.CheckListJobGroupId == opItems.CheckListJobGroupId).FirstOrDefault();

                                        if (checkListJobWRTOperator != null)
                                        {
                                            if (checkIsComepletedByOtherOperator != true)
                                            {
                                                checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(checkListJobWRTOperator.IsJobClosed, item.overallapproved, checkListJobWRTOperator.CheckListJobIsCompleted, checkListJobWRTOperator.IsJobRejected, isNotYetStarted);
                                            }
                                            else
                                            {
                                                checkListJobWRTOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == item.checkListJobId && m.OperatorId == opIdByCompletedOperator && m.CheckListJobGroupId == opItems.CheckListJobGroupId).FirstOrDefault();
                                                checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(checkListJobWRTOperator.IsJobClosed, checkListJobWRTOperator.IsAdminApproved, checkListJobWRTOperator.CheckListJobIsCompleted, checkListJobWRTOperator.IsJobRejected, isNotYetStarted);
                                            }

                                        }
                                        else if (isNotYetStarted == true && checkIsComepletedByOtherOperator == false)
                                        {
                                            //this is when other user completes that job
                                            checkListJobWRTOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == item.checkListJobId && m.OperatorId == opIdByCompletedOperator && m.CheckListJobGroupId == opItems.CheckListJobGroupId).FirstOrDefault();
                                            checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(checkListJobWRTOperator.IsJobClosed, checkListJobWRTOperator.IsAdminApproved, item.overallcompleted, checkListJobWRTOperator.IsJobRejected, isNotYetStarted);

                                        }
                                        else
                                        {
                                            if (checkIsComepletedByOtherOperator == true)
                                            {
                                                checkListJobWRTOperator = db.CheckListJobWrtoperator.Where(m => m.CheckListJobMasterId == item.checkListJobId && m.OperatorId == opIdByCompletedOperator && m.CheckListJobGroupId == opItems.CheckListJobGroupId).FirstOrDefault();
                                                checkListJobcustom.checkListJobStatus = commonFunction.GetJobStatus(checkListJobWRTOperator.IsJobClosed, checkListJobWRTOperator.IsAdminApproved, checkListJobWRTOperator.CheckListJobIsCompleted, checkListJobWRTOperator.IsJobRejected, isNotYetStarted);
                                            }
                                            else
                                            {
                                                checkListJobcustom.checkListJobStatus = "In Process";
                                            }

                                        }
                                        checkListJobcustoms.Add(checkListJobcustom);
                                    }
                                }
                            }

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
        /// View Check ListJob Details By Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobDetailsByCheckListJobAndGroupId(int checkListJobId, int checkListJobGroupId, int checkListJobOperatorId)
        {
            CommonResponse obj = new CommonResponse();
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
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId && m.CheckListJobGroupId == checkListJobGroupId).ToList(),
                                  checkListJobGroups = wf.CheckListGroup,
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
                    checkListJobCustom.checkListStartTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", result.checkListStartTime);
                    checkListJobCustom.checkListEndTimeAMPM = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", result.checkListEndTime);
                    if (result.assignedOperators.Count != 0)
                    {
                        var opts = result.assignedOperators.Select(m => m.PrimaryResource).ToList();
                        string operatorIdss = String.Join(",", opts);
                        List<long> opIds = operatorIdss.Split(',').Select(long.Parse).ToList();
                        var opItem = String.Join(",", db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                        checkListJobCustom.assignedOperators = opItem;

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
                            checkListJobAdvanceOperatorCustom.isJobRejected = checkListJobAvanceOperator.IsJobRejected;
                            //Mani
                            checkListJobAdvanceOperatorCustom.JobRejectedReason = checkListJobAvanceOperator.JobRejectedReason;
                            //Mani
                            checkListJobAdvanceCustom.checkListJobAdvanceOperatorCustom = checkListJobAdvanceOperatorCustom;
                        }
                        checkListJobAdvanceCustomListJob.Add(checkListJobAdvanceCustom);
                    }
                    checkListJobCustom.checkListJobAdvanceCustom = checkListJobAdvanceCustomListJob;
                    #endregion
                    #region Activity CheckListJob

                    List<CheckListJobActivityBySubCategory> checkListJobActivityBySubCategories = new List<CheckListJobActivityBySubCategory>();
                    var list = result.checkListJobGroups.Split(',').Select(x => int.Parse(x.Trim()));
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
                                                        expectedCompletionTime = wf.ExpectedCompletionTime,
                                                        assetId = wf.AssetId,
                                                        assetNumber = db.AssetMaster.Where(m => m.AssetId == wf.AssetId).Select(m => m.BarcodeAllocatedNumber).FirstOrDefault(),
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
                            checkListJobActivityDetails.assetNumber = activityCheck.assetNumber;
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
                                checkListJobActivityOperatorCustom.operatorUpoadedDocumentURL = appSettings.ImageUrl +
                                    (from wfd in db.DocumentUplodedMaster
                                     where wfd.IsDeleted == false && wfd.DocumentUploaderId == checkListJobAvanceOperator.OperatorUpoadedDocumentId
                                     select wfd.FileName).FirstOrDefault();
                                checkListJobActivityOperatorCustom.operatorRemark = checkListJobAvanceOperator.OperatorRemark;
                                checkListJobActivityOperatorCustom.startTime = checkListJobAvanceOperator.ActivityStartTime.ToString();
                                checkListJobActivityOperatorCustom.endTime = checkListJobAvanceOperator.ActivityEndTime.ToString();
                                checkListJobActivityOperatorCustom.isJobRejected = checkListJobAvanceOperator.IsJobRejected;
                                //Mani
                                checkListJobActivityOperatorCustom.JobRejectedReason = checkListJobAvanceOperator.JobRejectedReason;
                                //Mani
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
                            checkListJobLOTOTOOperatorCustom.lockOutRemark = checkListJobLOTOTOOperator.LockOutRemark;
                            checkListJobLOTOTOOperatorCustom.tagOutDoneByOperator = checkListJobLOTOTOOperator.TagOutDoneByOperator;
                            checkListJobLOTOTOOperatorCustom.tagOutRemark = checkListJobLOTOTOOperator.TagOutRemark;
                            checkListJobLOTOTOOperatorCustom.tryOutDoneByOperator = checkListJobLOTOTOOperator.TryOutDoneByOperator;
                            checkListJobLOTOTOOperatorCustom.tryOutRemark = checkListJobLOTOTOOperator.TryOutRemark;
                            checkListJobLOTOTOOperatorCustom.isJobRejected = checkListJobLOTOTOOperator.IsJobRejected;
                            //Mani
                            checkListJobLOTOTOOperatorCustom.JobRejectedReason = checkListJobLOTOTOOperator.JobRejectedReason;
                            //Mani
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
        /// Check List Job Enable Disable
        /// </summary>
        /// <returns></returns>
        public CommonResponse CheckListJobEnableDisable(int checkListJobId, int checkListJobGroupId, int checkListJobOperatorId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                Flag flag = new Flag();
                flag.advanceFlag = true;

                #region advance
                var advanceItem = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
                var advanceJobIds = advanceItem.Select(m => m.CheckListJobAdvanceId).ToList();
                List<long?> adJbIds = new List<long?>();
                foreach (var item in advanceJobIds)
                {
                    adJbIds.Add(item);
                }
                var advanceOpItem = db.CheckListJobAdvanceOperator.Where(m => m.CheckListJobOperatorId == checkListJobOperatorId && adJbIds.Contains(m.CheckListJobAdvanceId)).ToList();
                #endregion

                #region activity
                var activityItem = db.CheckListJobActivityMaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsActivityManditory == true && m.IsDeleted == false && m.IsActive == true).ToList();
                var activityJobIds = activityItem.Select(m => m.CheckListJobActivityId).ToList();
                List<long?> acJbIds = new List<long?>();
                foreach (var item in activityJobIds)
                {
                    acJbIds.Add(item);
                }
                var activityopItem = db.CheckListJobActivityOperator.Where(m => m.CheckListJobOperatorId == checkListJobOperatorId && acJbIds.Contains(m.CheckListJobActivityId)).ToList();
                #endregion

                #region lototo
                var lototoItem = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListJobId && m.CheckListJobGroupId == checkListJobGroupId && m.IsDeleted == false && m.IsActive == true).ToList();
                var lototoJobIds = lototoItem.Select(m => m.CheckListJobLototoid).ToList();
                List<long?> loJbIds = new List<long?>();
                foreach (var item in lototoJobIds)
                {
                    loJbIds.Add(item);
                }

                var opcheckCount = 0;
               //  var jobOp = db.CheckListJobLototooperator.Where(m=> m.CheckListJobOperatorId == checkListJobOperatorId &&)

                var lototoOpItem = db.CheckListJobLototooperator.Where(m => m.CheckListJobOperatorId == checkListJobOperatorId && loJbIds.Contains(m.CheckListJobLototoid)).ToList();
                foreach (var itt in lototoOpItem)
                {
                    var rolee = db.UserDetails.Where(m => m.UserId == itt.TryOutDoneByOperator).Select(m => m.RoleId).FirstOrDefault();
                    if (rolee == 1 || rolee == 2)
                    {
                        opcheckCount = 0;
                       
                    }
                    else
                    {

                        opcheckCount++;
                    }



                }
               


                #endregion

                if (advanceItem.Count == advanceOpItem.Count)
                {
                    flag.lototoFlag = true;
                }



                if (lototoItem.Count <= opcheckCount)
                {
                    flag.activityFlag = true;
                }

                if (activityItem.Count <= activityopItem.Count)
                {
                    flag.submitFlag = true;
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

        /// <summary>
        /// View Check List Assigned Job By Check List Job Id And Check List Job Operator Id
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <returns></returns>
        public CommonResponseResource ViewCheckListAssignedJobByCheckListJobIdAndCheckListJobOperatorId(int checkListJobMasterId, int checkListJobGroupId)
        {
            CommonResponseResource obj = new CommonResponseResource();
            try
            {
                var result = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();
                if (result != null)
                {
                    if (result.PrimaryResourceToAllFlag == true)
                    {
                        var resultOp = (from wf in db.UserDetails
                                        where wf.IsDeleted == false && wf.RoleId == 3
                                        select new
                                        {
                                            userId = wf.UserId,
                                            userName = wf.UserName,
                                            userFirstName = wf.UserFirstName,
                                            userLastName = wf.UserLastName,
                                            userFullName = wf.UserFullName,
                                        }).ToList();
                        obj.responsePrimary = resultOp;
                        obj.isStatus = true;
                    }
                    else
                    {
                        if (result.PrimaryResource != null)
                        {
                            try
                            {
                                List<long> opIds = result.PrimaryResource.Split(',').Select(long.Parse).ToList();
                                var resultOp = db.UserDetails.Where(m => opIds.Contains(m.UserId)).Select(m => new
                                {
                                    userId = m.UserId,
                                    userName = m.UserName,
                                    userFirstName = m.UserFirstName,
                                    userLastName = m.UserLastName,
                                    userFullName = m.UserFullName,
                                }).ToList();
                                obj.responsePrimary = resultOp;
                                obj.isStatus = true;

                            }
                            catch (Exception ex)
                            { }


                        }
                        else
                        {
                            obj.responsePrimary = ResourceResponse.NoItemsFound;
                        }
                    }

                    if (result.SecondaryResourceToAllFlag == true)
                    {
                        var resultOp = (from wf in db.UserDetails
                                        where wf.IsDeleted == false && wf.RoleId == 3
                                        select new
                                        {
                                            userId = wf.UserId,
                                            userName = wf.UserName,
                                            userFirstName = wf.UserFirstName,
                                            userLastName = wf.UserLastName,
                                            userFullName = wf.UserFullName,
                                        }).ToList();
                        obj.responseSecondary = resultOp;
                        obj.isStatus = true;
                    }
                    else
                    {
                        if (result.SecondaryResource != null)
                        {
                            try
                            {
                                List<long> opIds2 = result.SecondaryResource.Split(',').Select(long.Parse).ToList();
                                var resultOp2 = db.UserDetails.Where(m => opIds2.Contains(m.UserId)).Select(m => new
                                {
                                    userId = m.UserId,
                                    userName = m.UserName,
                                    userFirstName = m.UserFirstName,
                                    userLastName = m.UserLastName,
                                    userFullName = m.UserFullName,
                                }).ToList();
                                obj.responseSecondary = resultOp2;
                                obj.isStatus = true;
                            }
                            catch (Exception ex)
                            { }

                        }
                        else
                        {
                            obj.responseSecondary = ResourceResponse.NoItemsFound;
                        }
                    }

                }
                else
                {
                    obj.responsePrimary = ResourceResponse.NoItemsFound;
                    obj.responseSecondary = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.responsePrimary = ResourceResponse.ExceptionMessage;
                obj.responseSecondary = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Document  by Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobById(int checkListJobId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobMaster
                              where wf.IsDeleted == false
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
                                  assignedOperators = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == wf.CheckListJobId).FirstOrDefault()
                              }).FirstOrDefault();
                if (result != null)
                {

                    CheckListJobcustoms checkListJobcustom = new CheckListJobcustoms();
                    checkListJobcustom.checkListJobId = result.checkListJobId;
                    checkListJobcustom.checkListMasterId = result.checkListMasterId;
                    checkListJobcustom.checkListJobName = result.checkListJobName;
                    checkListJobcustom.checkListJobDescription = result.checkListJobDescription;
                    checkListJobcustom.checkListJobCategoryId = result.checkListJobCategoryId;
                    checkListJobcustom.checkListJobTypeId = result.checkListJobTypeId;
                    checkListJobcustom.checkListJobSupervisorId = result.checkListJobSupervisorId;
                    checkListJobcustom.checkListJobLineNumber = result.checkListJobLineNumber;
                    checkListJobcustom.checkListShiftNumber = result.checkListShiftNumber;
                    checkListJobcustom.checkListStartTime = result.checkListStartTime;
                    checkListJobcustom.checkListEndTime = result.checkListEndTime;
                    checkListJobcustom.previousGrade = result.previousGrade;
                    checkListJobcustom.currentGrade = result.currentGrade;
                    checkListJobcustom.previousColor = result.previousColor;
                    checkListJobcustom.currentColor = result.currentColor;
                    checkListJobcustom.checkListMasterName = result.checkListMasterName;
                    checkListJobcustom.checkListJobSupervisorName = result.checkListJobSupervisorName;
                    checkListJobcustom.checkListJobLineNumberName = result.checkListJobLineNumberName;
                    checkListJobcustom.checkListShiftName = result.checkListShiftName;
                    checkListJobcustom.checkListStartTimeAMPM = result.checkListStartTime.ToString();
                    checkListJobcustom.checkListEndTimeAMPM = result.checkListEndTime.ToString();
                    checkListJobcustom.previousGradeName = result.previousGradeName;
                    checkListJobcustom.currentGradeName = result.currentGradeName;
                    checkListJobcustom.previousColorName = result.previousColorName;
                    checkListJobcustom.currentColorName = result.currentColorName;
                    checkListJobcustom.checkListJobCategoryName = result.checkListJobCategoryName;
                    checkListJobcustom.checkListJobTypeName = result.checkListJobTypeName;
                    if (result.assignedOperators != null)
                    {
                        if (result.assignedOperators.PrimaryResourceToAllFlag == true)
                        {
                            checkListJobcustom.assignedOperators = "All";
                        }
                        else
                        {
                            string operatorIds = result.assignedOperators.PrimaryResource;
                            List<long> opId = operatorIds.Split(',').Select(long.Parse).ToList();
                            var opItem = String.Join(",", db.UserDetails.Where(m => opId.Contains(m.UserId)).Select(m => m.UserFullName).ToList());
                            checkListJobcustom.assignedOperators = opItem;
                        }
                    }

                    obj.response = checkListJobcustom;
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
        /// <param name="checkListJobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListJob(int checkListJobId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobId).FirstOrDefault();
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
        /// <param name="checkListJobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJob(int checkListJobId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobId).FirstOrDefault();
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
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListJob(int checkListJobId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobId && m.IsDeleted == false).Count();
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
