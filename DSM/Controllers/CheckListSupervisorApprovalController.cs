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
using static DSM.EntityModels.CheckListSupervisorApprovalEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListSupervisorApprovalController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListSupervisorApproval checkListSupervisorApproval;

        public CheckListSupervisorApprovalController(IOptions<AppSettings> appSettings, ICheckListSupervisorApproval _checkListSupervisorApproval)
        {
            _appSettings = appSettings.Value;
            checkListSupervisorApproval = _checkListSupervisorApproval;
        }

        /// <summary>
        /// Add and Edit Document Operator
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("CheckListSupervisorApproval/CheckListForSupervisorApproval")]
        public async Task<IActionResult> CheckListForSupervisorApproval(int checkListJobId, int checkListJobGroupId)
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
            //calling CheckListJobActivityOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSupervisorApproval.CheckListForSupervisorApproval(checkListJobId,checkListJobGroupId, userId);

            return Ok(response);
        }


        //Mani
        //Post
        [HttpPost]
        [Route("CheckListSupervisorApproval/AddAndEditCheckListForLOTOTO")]
        public async Task<IActionResult> AddAndEditCheckListForLOTOTO(CheckListJobLOTOTOCustom1 data)
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
            //calling CheckListJobLOTOTODAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSupervisorApproval.AddAndEditCheckListForLOTOTO(data, userId);

            return Ok(response);
        }

        //Mani

        [HttpPost]
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "2")]
        [Route("CheckListSupervisorApproval/AddAndEditCheckListJobLOTOTOAdmin")]
        public async Task<IActionResult> AddAndEditCheckListJobLOTOTOAdmin(CheckListJobLOTOTOOperatorCustomNew data)
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
            //calling CheckListJobLOTOTOOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSupervisorApproval.AddAndEditCheckListJobLOTOTOAdmin(data, userId);

            return Ok(response);
        }

        //Mani


        //Mani

        //Mani
        [HttpGet]
        [Route("CheckListSupervisorApproval/CheckListJobOverallsubmit")]
        public async Task<IActionResult> CheckListJobOverallsubmit(int checkListJobId, int checkListJobGroupId)
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
            CommonResponse response = checkListSupervisorApproval.CheckListJobOverallsubmit(checkListJobId, checkListJobGroupId);

            return Ok(response);
        }
        //Mani



        [HttpGet]
        [Route("CheckListSupervisorApproval/CheckListJobLototoCheckEnable")]
        public async Task<IActionResult> CheckListJobLototoCheckEnable(int checkListJobMasterId, int checkListGroupId, int checkListJobOperatorId)
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
            CommonResponse response = checkListSupervisorApproval.CheckListJobLototoCheckEnable(checkListJobMasterId, checkListGroupId, checkListJobOperatorId);

            return Ok(response);
        }











        //Mani
        //Get
        [HttpGet]
         // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("CheckListSupervisorApproval/CheckListForLototo")]
        public async Task<IActionResult> CheckListForLototo(int checkListJobId, int checkListJobGroupId)
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
            //calling CheckListJobActivityOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSupervisorApproval.CheckListForLototo(checkListJobId, checkListJobGroupId, userId);

            return Ok(response);
        }

        //Mani









        /// <summary>
        /// View Multiple Check List Job For Approval
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListSupervisorApproval/ViewMultipleCheckListJobForApproval")]
        public async Task<IActionResult> ViewMultipleCheckListJobForApproval()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = checkListSupervisorApproval.ViewMultipleCheckListJobForApproval(userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Check List Group By Check List Job Master Id
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListSupervisorApproval/ViewMultipleCheckListGroupByCheckListJobMasterId")]
        public async Task<IActionResult> ViewMultipleCheckListGroupByCheckListJobMasterId(int checkListJobMasterId)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = checkListSupervisorApproval.ViewMultipleCheckListGroupByCheckListJobMasterId(checkListJobMasterId);

            return Ok(response);
        }

        /// <summary>
        /// Close Job By Check List Job MasterId
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListSupervisorApproval/CloseJobByCheckListJobMasterId")]
        public async Task<IActionResult> CloseJobByCheckListJobMasterId(int checkListJobMasterId)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = checkListSupervisorApproval.CloseJobByCheckListJobMasterId(checkListJobMasterId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Check List Group By Check List Job Master Id After Job Completion
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListSupervisorApproval/ViewMultipleCheckListGroupByCheckListJobMasterIdAfterJobCompletion")]
        public async Task<IActionResult> ViewMultipleCheckListGroupByCheckListJobMasterIdAfterJobCompletion(int checkListJobMasterId)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = checkListSupervisorApproval.ViewMultipleCheckListGroupByCheckListJobMasterIdAfterJobCompletion(checkListJobMasterId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Supervisor For Reassigning Job
        /// </summary>
        /// <param name="superVisorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListSupervisorApproval/ViewMultipleSupervisorForReassigningJob")]
        public async Task<IActionResult> ViewMultipleSupervisorForReassigningJob(int superVisorId)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = checkListSupervisorApproval.ViewMultipleSupervisorForReassigningJob(superVisorId);

            return Ok(response);
        }

        /// <summary>
        /// Reassigning Job To Supervisor
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="superVisorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListSupervisorApproval/ReassigningJobToSupervisor")]
        public async Task<IActionResult> ReassigningJobToSupervisor(int checkListJobId, int superVisorId)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = checkListSupervisorApproval.ReassigningJobToSupervisor(checkListJobId, superVisorId, userId);

            return Ok(response);
        }
    }
}