using DSM.DAL.App_Start;
using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.HelpContentEntity;

namespace DSM.DAL
{
    public class HelpContentDAL : IHelpContent
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HelpContentDAL));
        public HelpContentDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit HelpContent 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditHelpContent(HelpContentCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.HelpContentMaster.Where(m => m.HelpContentId == data.helpContentId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        HelpContentMaster item = new HelpContentMaster();
                        item.HelpContentShortName = data.helpContentShortName;
                        item.HelpContentDescription = data.helpContentDescription;
                        item.InstructionLink = data.instructionLink;
                        item.VisbleToRoleId = data.visbleToRoleId;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.HelpContentMaster.Add(item);
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
                        res.HelpContentShortName = data.helpContentShortName;
                        res.HelpContentDescription = data.helpContentDescription;
                        res.InstructionLink = data.instructionLink;
                        res.VisbleToRoleId = data.visbleToRoleId;
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
        /// View Multiple HelpContent 
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleHelpContent()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.HelpContentMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  helpContentId = wf.HelpContentId,
                                  helpContentShortName = wf.HelpContentShortName,
                                  helpContentDescription = wf.HelpContentDescription,
                                  instructionLink = wf.InstructionLink,
                                  visbleToRoleId = wf.VisbleToRoleId,
                                  isActive = wf.IsActive
                              }).ToList();
                List<HelpView> helpViews = new List<HelpView>();
                foreach (var res in result)
                {
                    HelpView helpView = new HelpView();
                    List<long> roleIds = res.visbleToRoleId.Split(',').Select(long.Parse).ToList();
                    var resultRole = db.RoleMaster.Where(m => roleIds.Contains(m.RoleId) && m.IsDeleted == false).Select(m => new
                    {
                        roleId = m.RoleId,
                        roleName = m.RoleName,
                        roleDescription = m.RoleDescription,
                        isActive = m.IsActive
                    }).ToList();

                    var roleNames = resultRole.Select(m=>m.roleName).ToList();

                    helpView.helpContentId = res.helpContentId;
                    helpView.helpContentShortName = res.helpContentShortName;
                    helpView.helpContentDescription = res.helpContentDescription;
                    helpView.instructionLink = res.instructionLink;
                    helpView.visbleToRoleId = String.Join(",", roleNames);
                    helpView.visbleToRole = resultRole;
                    helpView.isActive = res.isActive;
                    helpViews.Add(helpView);
                }

                if (helpViews.Count() != 0)
                {
                    obj.response = helpViews;
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
        /// View Multiple HelpContent in Help Screen
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleHelpContentInHelpScreen(long userId)
        {
            CommonResponse obj = new CommonResponse();
            CommonFunction commonFunction = new CommonFunction();
            var userDetails = commonFunction.GetUserDetails(userId);

            try
            {
                string roleId = userDetails.RoleId.ToString();
                var result = (from wf in db.HelpContentMaster
                              where wf.IsDeleted == false && wf.VisbleToRoleId.Contains(roleId)
                              select new
                              {
                                  helpContentId = wf.HelpContentId,
                                  helpContentShortName = wf.HelpContentShortName,
                                  helpContentDescription = wf.HelpContentDescription,
                                  instructionLink = wf.InstructionLink,
                                  visbleToRoleId = wf.VisbleToRoleId,
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
        /// View HelpContent  by Id
        /// </summary>
        /// <param name="helpContentId"></param>
        /// <returns></returns>
        public CommonResponse ViewHelpContentById(int helpContentId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.HelpContentMaster
                              where wf.IsDeleted == false && wf.HelpContentId == helpContentId
                              select new
                              {
                                  helpContentId = wf.HelpContentId,
                                  helpContentShortName = wf.HelpContentShortName,
                                  helpContentDescription = wf.HelpContentDescription,
                                  instructionLink = wf.InstructionLink,
                                  visbleToRoleId = wf.VisbleToRoleId,
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
        /// Delete HelpContent 
        /// </summary>
        /// <param name="helpContentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteHelpContent(int helpContentId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.HelpContentMaster.Where(m => m.HelpContentId == helpContentId).FirstOrDefault();
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
        /// Archive HelpContent 
        /// </summary>
        /// <param name="helpContentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveHelpContent(int helpContentId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.HelpContentMaster.Where(m => m.HelpContentId == helpContentId).FirstOrDefault();
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
        /// Check HelpContent 
        /// </summary>
        /// <param name="helpContentId"></param>
        /// <returns></returns>
        public CommonResponse CheckHelpContent(int helpContentId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.HelpContentMaster.Where(m => m.HelpContentId == helpContentId && m.IsDeleted == false).Count();
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
