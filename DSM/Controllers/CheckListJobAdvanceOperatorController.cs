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
using static DSM.EntityModels.CheckListJobAdvanceOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobAdvanceOperatorController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobAdvanceOperator checkListJobAdvanceOperator;

        public CheckListJobAdvanceOperatorController(IOptions<AppSettings> appSettings, ICheckListJobAdvanceOperator _checkListJobAdvanceOperator)
        {
            _appSettings = appSettings.Value;
            checkListJobAdvanceOperator = _checkListJobAdvanceOperator;
        }

        /// <summary>
        /// Add and Edit Document Operator
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [Route("CheckListJobAdvanceOperator/AddAndEditCheckListJobAdvanceOperator")]
        public async Task<IActionResult> AddAndEditCheckListJobAdvanceOperator(CheckListJobAdvanceOperatorCustom data)
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
            //calling CheckListJobAdvanceOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAdvanceOperator.AddAndEditCheckListJobAdvanceOperator(data, userId);

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/ApproveCheckListJobAdvanceOperator")]
        public async Task<IActionResult> ApproveCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId)
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
            CommonResponse response = checkListJobAdvanceOperator.ApproveCheckListJobAdvanceOperator(checkListJobAdvanceOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// Reject Check List Job Operator
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]

        [HttpGet]
        [Route("CheckListJobOperator/RejectCheckListJobAdvanceOperator")]
        public async Task<IActionResult> RejectCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId, string rejectReason)
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
            CommonResponse response = checkListJobAdvanceOperator.RejectCheckListJobAdvanceOperator(checkListJobAdvanceOperatorId, rejectReason);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Operator
        /// </summary>
        /// <param name="checkListJobAdvanceOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvanceOperator/DeleteCheckListJobAdvanceOperator")]
        public async Task<IActionResult> DeleteCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId)
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
            //calling CheckListJobAdvanceOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAdvanceOperator.DeleteCheckListJobAdvanceOperator(checkListJobAdvanceOperatorId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Operator
        /// </summary>
        /// <param name="checkListJobAdvanceOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvanceOperator/ArchiveCheckListJobAdvanceOperator")]
        public async Task<IActionResult> ArchiveCheckListJobAdvanceOperator(int checkListJobAdvanceOperatorId)
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
            //calling CheckListJobAdvanceOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAdvanceOperator.ArchiveCheckListJobAdvanceOperator(checkListJobAdvanceOperatorId, userId);

            return Ok(response);
        }
    }
}