using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CheckListJobAssignedResourceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class CheckListJobAssignedResourceMasterDAL : ICheckListJobAssignedResourceMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListJobAssignedResourceMasterDAL));
        public CheckListJobAssignedResourceMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobAssignedResource(CheckListJobAssignedResourceMasterCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == data.checkListJobMasterId && m.CheckListJobGroupId == data.checkListJobGroupId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        CheckListJobAssignedResourceMaster item = new CheckListJobAssignedResourceMaster();
                        item.CheckListJobMasterId = data.checkListJobMasterId;
                        item.CheckListJobGroupId = data.checkListJobGroupId;
                        item.PrimaryResource = data.primaryResource;
                        item.PrimaryResourceToAllFlag = data.primaryResourceToAllFlag;
                        item.SecondaryResource = data.secondaryResource;
                        item.SecondaryResourceToAllFlag = data.secondaryResourceToAllFlag;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.CheckListJobAssignedResourceMaster.Add(item);
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
                        res.PrimaryResource = data.primaryResource;
                        res.PrimaryResourceToAllFlag = data.primaryResourceToAllFlag;
                        res.SecondaryResource = data.secondaryResource;
                        res.SecondaryResourceToAllFlag = data.secondaryResourceToAllFlag;
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
        /// Add And Edit Check List Job Assigned Resource All
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditCheckListJobAssignedResourceAll(int checkListJobMasterId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            try
            {
                var job = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobMasterId).FirstOrDefault();
               
                List<int> gppids = job.CheckListGroup.Split(',').Select(int.Parse).ToList();
                foreach (var grp in gppids)
                    {
                        int checkListJobGroupId = grp;



                        var res = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == checkListJobMasterId && m.CheckListJobGroupId == checkListJobGroupId).FirstOrDefault();

                        var result = String.Join(",", db.UserDetails.Where(wf => wf.IsDeleted == false && wf.RoleId == 3).Select(wf => wf.UserId).ToList());

                        if (res == null)
                        {
                            try
                            {
                                CheckListJobAssignedResourceMaster item = new CheckListJobAssignedResourceMaster();
                                item.CheckListJobMasterId = checkListJobMasterId;
                                item.CheckListJobGroupId = checkListJobGroupId;
                                item.PrimaryResource = result;
                                item.PrimaryResourceToAllFlag = true;
                                item.IsActive = true;
                                item.IsDeleted = false;
                                item.CreatedBy = userId;
                                item.CreatedOn = DateTime.Now;
                                db.CheckListJobAssignedResourceMaster.Add(item);
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
                                if (res.PrimaryResource == null || res.PrimaryResource.Length == 0)
                                {
                                    res.CheckListJobMasterId = checkListJobMasterId;
                                    res.CheckListJobGroupId = checkListJobGroupId;
                                    res.PrimaryResource = result;
                                    res.PrimaryResourceToAllFlag = true;
                                    res.ModifiedBy = userId;
                                    res.ModifiedOn = DateTime.Now;
                                    db.SaveChanges();
                                    obj.response = ResourceResponse.UpdatedSucessfully;
                                    obj.isStatus = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                                obj.response = ResourceResponse.ExceptionMessage;
                                obj.isStatus = false;
                            }
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
        public CommonResponse ViewMultipleCheckListJobAssignedResource()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobAssignedResourceMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  checkListJobAssignedResourceId= wf.CheckListJobAssignedResourceMasterId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  primaryResource = wf.PrimaryResource,
                                  primaryResourceToAllFlag = wf.PrimaryResourceToAllFlag,
                                  secondaryResource = wf.SecondaryResource,
                                  secondaryResourceToAllFlag = wf.SecondaryResourceToAllFlag,
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
        /// View Check ListJob AssignedResource By check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobAssignedResourceBycheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobAssignedResourceMaster
                              where wf.IsDeleted == false && wf.CheckListJobMasterId == checkListJobMasterId && wf.CheckListJobGroupId == checkListJobGroupId
                              select new
                              {
                                  checkListJobAssignedResourceId = wf.CheckListJobAssignedResourceMasterId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  primaryResource = wf.PrimaryResource,
                                  primaryResourceToAllFlag = wf.PrimaryResourceToAllFlag,
                                  secondaryResource = wf.SecondaryResource,
                                  secondaryResourceToAllFlag = wf.SecondaryResourceToAllFlag,
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
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <returns></returns>
        public CommonResponse ViewCheckListJobAssignedResourceById(int checkListJobAssignedResourceId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.CheckListJobAssignedResourceMaster
                              where wf.IsDeleted == false && wf.CheckListJobAssignedResourceMasterId == checkListJobAssignedResourceId
                              select new
                              {
                                  checkListJobAssignedResourceId = wf.CheckListJobAssignedResourceMasterId,
                                  checkListJobMasterId = wf.CheckListJobMasterId,
                                  checkListJobGroupId = wf.CheckListJobGroupId,
                                  primaryResource = wf.PrimaryResource,
                                  primaryResourceToAllFlag = wf.PrimaryResourceToAllFlag,
                                  secondaryResource = wf.SecondaryResource,
                                  secondaryResourceToAllFlag = wf.SecondaryResourceToAllFlag,
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
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteCheckListJobAssignedResource(int checkListJobAssignedResourceId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobAssignedResourceMasterId == checkListJobAssignedResourceId).FirstOrDefault();
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
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveCheckListJobAssignedResource(int checkListJobAssignedResourceId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobAssignedResourceMasterId == checkListJobAssignedResourceId).FirstOrDefault();
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
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <returns></returns>
        public CommonResponse CheckCheckListJobAssignedResource(int checkListJobAssignedResourceId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobAssignedResourceMasterId == checkListJobAssignedResourceId && m.IsDeleted == false).Count();
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


        public CommonResponse AddAndEditReAssignedCheckListJobResources(CheckListJobAssignedResourceMasterCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.ReAssignedcheckListJobResourcesOperator.Where(m => m.CheckListJobMasterId == data.checkListJobMasterId && m.CheckListJobGroupId == data.checkListJobGroupId && m.IsDeleted == false).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        ReAssignedcheckListJobResourcesOperator item = new ReAssignedcheckListJobResourcesOperator();

                        var assignOp=db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == data.checkListJobMasterId && m.CheckListJobGroupId == data.checkListJobGroupId && m.IsDeleted == false).FirstOrDefault();
                        
                        if (assignOp != null)
                        {
                            item.SecondaryResource = assignOp.PrimaryResource;
                            item.ChecklistJobFirstOperatorIds = assignOp.PrimaryResource;
                        }

                        //var mainOp = db.CheckListJobMaster.Where(m => m.CheckListJobId == data.checkListJobMasterId).FirstOrDefault();
                        //if (mainOp!=null)
                        //{
                        //    item.ChecklistJobFirstOperatorIds= mainOp.as


                        //}
                        item.IsReassigned = 1;
                        item.CheckListJobMasterId = data.checkListJobMasterId;
                        item.CheckListJobGroupId = data.checkListJobGroupId;
                        item.PrimaryResource = data.primaryResource;
                        item.PrimaryResourceToAllFlag = data.primaryResourceToAllFlag;
                        //item.SecondaryResource = data.secondaryResource;
                        item.SecondaryResourceToAllFlag = data.secondaryResourceToAllFlag;

                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.ReAssignedcheckListJobResourcesOperator.Add(item);
                        db.SaveChanges();

                        //update FirstAssign tbl
                        assignOp.PrimaryResource = data.primaryResource;
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
                        var assignOp = db.CheckListJobAssignedResourceMaster.Where(m => m.CheckListJobMasterId == data.checkListJobMasterId && m.CheckListJobGroupId == data.checkListJobGroupId && m.IsDeleted == false).FirstOrDefault();

                        if (assignOp != null)
                        {
                            res.SecondaryResource = assignOp.PrimaryResource;
                        }



                        res.CheckListJobMasterId = data.checkListJobMasterId;
                        res.CheckListJobGroupId = data.checkListJobGroupId;
                        res.PrimaryResource = data.primaryResource;
                        res.PrimaryResourceToAllFlag = data.primaryResourceToAllFlag;

                       
                        //res.SecondaryResource = data.secondaryResource;
                        res.SecondaryResourceToAllFlag = data.secondaryResourceToAllFlag;
                        res.ModifiedBy = userId;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();

                        assignOp.PrimaryResource = data.primaryResource;
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




    }
}
