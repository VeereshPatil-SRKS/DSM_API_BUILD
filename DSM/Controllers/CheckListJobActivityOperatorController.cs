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
using static DSM.EntityModels.CheckListJobActivityOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobActivityOperatorController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobActivityOperator checkListJobActivityOperator;

        public CheckListJobActivityOperatorController(IOptions<AppSettings> appSettings, ICheckListJobActivityOperator _checkListJobActivityOperator)
        {
            _appSettings = appSettings.Value;
            checkListJobActivityOperator = _checkListJobActivityOperator;
        }

        /// <summary>
        /// Add and Edit Document Operator
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [Route("CheckListJobActivityOperator/AddAndEditCheckListJobActivityOperator")]
        public async Task<IActionResult> AddAndEditCheckListJobActivityOperator(CheckListJobActivityOperatorCustom data)
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
            response = checkListJobActivityOperator.AddAndEditCheckListJobActivityOperator(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// Check List Job Activity Operator Start Activity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [Route("CheckListJobActivityOperator/CheckListJobActivityOperatorStartActivity")]
        public async Task<IActionResult> CheckListJobActivityOperatorStartActivity(int checkListJobOperatorId, int checkListJobActivityId, string barcodeNumber)
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
            response = checkListJobActivityOperator.CheckListJobActivityOperatorStartActivity(checkListJobOperatorId, checkListJobActivityId, barcodeNumber, userId);

            return Ok(response);
        }

        /// <summary>
        /// Approve Check List Job Activity Operator
        /// </summary>
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/ApproveCheckListJobActivityOperator")]
        public async Task<IActionResult> ApproveCheckListJobActivityOperator(int checkListJobActivityOperatorId)
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
            CommonResponse response = checkListJobActivityOperator.ApproveCheckListJobActivityOperator(checkListJobActivityOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// Reject Check List Job Operator
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/RejectCheckListJobActivityOperator")]
        public async Task<IActionResult> RejectCheckListJobActivityOperator(int checkListJobActivityOperatorId,string rejectReason)
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
            CommonResponse response = checkListJobActivityOperator.RejectCheckListJobActivityOperator(checkListJobActivityOperatorId, rejectReason);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Operator
        /// </summary>
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivityOperator/DeleteCheckListJobActivityOperator")]
        public async Task<IActionResult> DeleteCheckListJobActivityOperator(int checkListJobActivityOperatorId)
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
            response = checkListJobActivityOperator.DeleteCheckListJobActivityOperator(checkListJobActivityOperatorId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Operator
        /// </summary>
        /// <param name="checkListJobActivityOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivityOperator/ArchiveCheckListJobActivityOperator")]
        public async Task<IActionResult> ArchiveCheckListJobActivityOperator(int checkListJobActivityOperatorId)
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
            response = checkListJobActivityOperator.ArchiveCheckListJobActivityOperator(checkListJobActivityOperatorId, userId);

            return Ok(response);
        }
    }
}