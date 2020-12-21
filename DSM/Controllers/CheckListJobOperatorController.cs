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
using static DSM.EntityModels.CheckListJobOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobOperatorController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobOperator checkListJobOperator;

        public CheckListJobOperatorController(IOptions<AppSettings> appSettings, ICheckListJobOperator _checkListJobOperator)
        {
            _appSettings = appSettings.Value;
            checkListJobOperator = _checkListJobOperator;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [Route("CheckListJobOperator/AddAndEditCheckListJobOperator")]
        public async Task<IActionResult> AddAndEditCheckListJobOperator(CheckListJobOperatorCustom data)
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
            CommonResponseWithIds response = new CommonResponseWithIds();
            response = checkListJobOperator.AddAndEditCheckListJobOperator(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// Approve Check List Job Operator
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/ApproveCheckListJobOperator")]
        public async Task<IActionResult> ApproveCheckListJobOperator(int checkListJobOperatorId)
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
            CommonResponse response = checkListJobOperator.ApproveCheckListJobOperator(checkListJobOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// Reject Check List Job Operator
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/RejectCheckListJobOperator")]
        public async Task<IActionResult> RejectCheckListJobOperator(int checkListJobOperatorId, string rejectReason)
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
            CommonResponse response = checkListJobOperator.RejectCheckListJobOperator(checkListJobOperatorId,rejectReason);

            return Ok(response);
        }

        /// <summary>
        /// Over All Submit Check List Job Operator
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [HttpGet]
        [Route("CheckListJobOperator/OverAllSubmitCheckListJobOperator")]
        public async Task<IActionResult> OverAllSubmitCheckListJobOperator(int checkListJobOperatorId)
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
            CommonResponse response = checkListJobOperator.OverAllSubmitCheckListJobOperator(checkListJobOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document 
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobOperator/DeleteCheckListJobOperator")]
        public async Task<IActionResult> DeleteCheckListJobOperator(int checkListJobOperatorId)
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
            CommonResponse response = new CommonResponse();
            response = checkListJobOperator.DeleteCheckListJobOperator(checkListJobOperatorId, userId);

            return Ok(response);
        }


        /// <summary>
        /// Approve Check List Job Operator Based On Group
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobOperator/ApproveCheckListJobOperatorBasedOnGroup")]
        public async Task<IActionResult> ApproveCheckListJobOperatorBasedOnGroup(int checkListJobId, int checkListJobGroupId)
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
            CommonResponse response = new CommonResponse();
            response = checkListJobOperator.ApproveCheckListJobOperatorBasedOnGroup(checkListJobId, checkListJobGroupId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Reject CheckList Job Operator Based On Group
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListJobOperator/RejectCheckListJobOperatorBasedOnGroup")]
        public async Task<IActionResult> RejectCheckListJobOperatorBasedOnGroup(CheckListJobOperatorBasedOnGroup data)
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
            CommonResponse response = new CommonResponse();
            response = checkListJobOperator.RejectCheckListJobOperatorBasedOnGroup(data, userId);

            return Ok(response);
        }

    }
}