using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListGroupMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListGroupMasterDAL : ICheckListGroupMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListGroupMasterDAL));
        public CheckListGroupMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListGroup(CheckListGroupCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == data.checkListGroupId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        CheckListGroupMaster item = new CheckListGroupMaster();
                        item.CheckListGroupName = data.checkListGroupName;
                        item.CheckListGroupDescription = data.checkListGroupDescription;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListGroupMaster.Add(item);
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
                        res.CheckListGroupName = data.checkListGroupName;
                        res.CheckListGroupDescription = data.checkListGroupDescription;
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
        public CommonResponse ViewMultipleCheckListGroup()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false
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
        /// View Multiple Document 
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListGroupByCheckListMasterId(int checkListMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var advanceCheckList = db.CheckListMaster.Where(m => m.CheckListId == checkListMasterId).Select(m => m.CheckListGroup).Distinct().FirstOrDefault();

                var list = advanceCheckList.Split(',').Select(x => long.Parse(x.Trim()));

                var result = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false && list.Contains(wf.CheckListGroupId)
                              select new
                              {
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = wf.CheckListGroupName,
                                  checkListGroupDescription = wf.CheckListGroupDescription,
                                  isActive = wf.IsActive
                              }).ToList();
                //var advanceCheckList = db.CheckListAdvanceMaster.Where(m => m.CheckListMasterId == checkListMasterId).Select(m => m.CheckListGroupId).Distinct().ToList();
                //var activityCheckList = db.CheckListActivityMaster.Where(m => m.CheckListMasterId == checkListMasterId).Select(m => m.CheckListGroupId).Distinct().ToList();
                //var lototoCheckList = db.CheckListLototomaster.Where(m => m.CheckListMasterId == checkListMasterId).Select(m => m.CheckListGroupId).Distinct().ToList();

                //var finalList = advanceCheckList.Union(activityCheckList).Union(lototoCheckList).OrderBy(x => x.Value).ToList();

                //var result = (from wf in db.CheckListGroupMaster
                //              where wf.IsDeleted == false  && finalList.Contains(wf.CheckListGroupId)
                //              select new
                //              {
                //                  checkListGroupId = wf.CheckListGroupId,
                //                  checkListGroupName = wf.CheckListGroupName,
                //                  checkListGroupDescription = wf.CheckListGroupDescription,
                //                  isActive = wf.IsActive
                //              }).ToList();
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
        /// View Multiple Check List Group By Check List Job Master Id
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        public CommonResponse ViewMultipleCheckListGroupByCheckListJobMasterId(int checkListJobMasterId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var advanceCheckList = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobMasterId).Select(m => m.CheckListGroup).Distinct().FirstOrDefault();

                var list = advanceCheckList.Split(',').Select(x => long.Parse(x.Trim()));

                var result = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false && list.Contains(wf.CheckListGroupId)
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
        /// View Document  by Id
        /// </summary>
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListGroupById(int checkListGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListGroupMaster
                              where wf.IsDeleted == false && wf.CheckListGroupId == checkListGroupId
                              select new
                              {
                                  checkListGroupId = wf.CheckListGroupId,
                                  checkListGroupName = wf.CheckListGroupName,
                                  checkListGroupDescription = wf.CheckListGroupDescription,
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
        /// <param name="checkListGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListGroup(int checkListGroupId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == checkListGroupId).FirstOrDefault();
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
        /// <param name="checkListGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListGroup(int checkListGroupId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == checkListGroupId).FirstOrDefault();
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
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListGroup(int checkListGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListGroupMaster.Where(m => m.CheckListGroupId == checkListGroupId && m.IsDeleted == false).Count();
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
