using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DSM.DAL.Helpers;
using DSM.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.CheckListJobMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobMaster checkListJobMaster;

        public CheckListJobMasterController(IOptions<AppSettings> appSettings, ICheckListJobMaster _checkListJobMaster)
        {
            _appSettings = appSettings.Value;
            checkListJobMaster = _checkListJobMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListJob/AddAndEditCheckListJob")]
        public async Task<IActionResult> AddAndEditCheckListJob(CheckListJobCustom data)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponseWithIds response = new CommonResponseWithIds();
            response = checkListJobMaster.AddAndEditCheckListJob(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// Get Check List Job Type
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/GetCheckListJobType")]
        public async Task<IActionResult> GetCheckListJobType(int checkListId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobMaster.GetCheckListJobType(checkListId);

            return Ok(response);
        }


        /// <summary>
        /// Check List Update End Time Of Job By Check List Job Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListEndTime"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/UpdateCheckListEndTimeOfJobByCheckListJobId")]
        public async Task<IActionResult> UpdateCheckListEndTimeOfJobByCheckListJobId(int checkListJobId,string checkListEndTime)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobMaster.UpdateCheckListEndTimeOfJobByCheckListJobId(checkListJobId, checkListEndTime, userId);

            return Ok(response);
        }

        /// <summary>
        /// Move Check List Data To Check List Job
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/MoveCheckListDataToCheckListJob")]
        public async Task<IActionResult> MoveCheckListDataToCheckListJob(int checkListJobId, int checkListId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = checkListJobMaster.MoveCheckListDataToCheckListJob(checkListJobId, checkListId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/ViewMultipleCheckListJob")]
        public async Task<IActionResult> ViewMultipleCheckListJob()
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = checkListJobMaster.ViewMultipleCheckListJob();

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Check List Job WRTUser
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/ViewMultipleCheckListJobWRTUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2,3")]
        public async Task<IActionResult> ViewMultipleCheckListJobWRTUser()
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            int userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = checkListJobMaster.ViewMultipleCheckListJobWRTUser(userId);

            return Ok(response);
        }

        /// <summary>
        /// View Check List Details By Id
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/ViewCheckListJobDetailsByCheckListJobAndGroupId")]
        public async Task<IActionResult> ViewCheckListJobDetailsByCheckListJobAndGroupId(int checkListJobMasterId, int checkListGroupId,int checkListJobOperatorId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = checkListJobMaster.ViewCheckListJobDetailsByCheckListJobAndGroupId(checkListJobMasterId, checkListGroupId, checkListJobOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// View Check List Assigned Job By Check List Job Id And Check List Job Operator Id
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/ViewCheckListAssignedJobByCheckListJobIdAndCheckListJobOperatorId")]
        public async Task<IActionResult> ViewCheckListAssignedJobByCheckListJobIdAndCheckListJobOperatorId(int checkListJobMasterId, int checkListJobGroupId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponseResource response = checkListJobMaster.ViewCheckListAssignedJobByCheckListJobIdAndCheckListJobOperatorId(checkListJobMasterId, checkListJobGroupId);

            return Ok(response);
        }

        /// <summary>
        /// Check List Job Enable Disable
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <param name="checkListGroupId"></param>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/CheckListJobEnableDisable")]
        public async Task<IActionResult> CheckListJobEnableDisable(int checkListJobMasterId, int checkListGroupId, int checkListJobOperatorId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = checkListJobMaster.CheckListJobEnableDisable(checkListJobMasterId, checkListGroupId, checkListJobOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/ViewCheckListJobById")]
        public async Task<IActionResult> ViewCheckListJobById(int checkListJobId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = checkListJobMaster.ViewCheckListJobById(checkListJobId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/DeleteCheckListJob")]
        public async Task<IActionResult> DeleteCheckListJob(int checkListJobId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobMaster.DeleteCheckListJob(checkListJobId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJob/ArchiveCheckListJob")]
        public async Task<IActionResult> ArchiveCheckListJob(int checkListJobId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling CheckListJobDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobMaster.ArchiveCheckListJob(checkListJobId, userId);

            return Ok(response);
        }
    }
}