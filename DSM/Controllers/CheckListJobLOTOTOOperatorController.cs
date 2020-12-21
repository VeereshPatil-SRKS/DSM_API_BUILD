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
using static DSM.EntityModels.CheckListJobLOTOTOOperatorEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobLOTOTOOperatorController : ControllerBase
    {

        private readonly AppSettings _appSettings;
        private readonly ICheckListJobLOTOTOOperator checkListJobLOTOTOOperator;

        public CheckListJobLOTOTOOperatorController(IOptions<AppSettings> appSettings, ICheckListJobLOTOTOOperator _checkListJobLOTOTOOperator)
        {
            _appSettings = appSettings.Value;
            checkListJobLOTOTOOperator = _checkListJobLOTOTOOperator;
        }

        /// <summary>
        /// Add and Edit Document Operator
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [Route("CheckListJobLOTOTOOperator/AddAndEditCheckListJobLOTOTOOperator")]
        public async Task<IActionResult> AddAndEditCheckListJobLOTOTOOperator(CheckListJobLOTOTOOperatorCustom data)
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
            response = checkListJobLOTOTOOperator.AddAndEditCheckListJobLOTOTOOperator(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// Approve Check List Job Operator
        /// </summary>
        /// <param name="checkListJobLOTOTOOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/ApproveCheckListJobLOTOTOOperator")]
        public async Task<IActionResult> ApproveCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId)
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
            CommonResponse response = checkListJobLOTOTOOperator.ApproveCheckListJobLOTOTOOperator(checkListJobLOTOTOOperatorId);

            return Ok(response);
        }

        /// <summary>
        /// Reject Check List Job Operator
        /// </summary>
        /// <param name="checkListJobOperatorId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("CheckListJobOperator/RejectCheckListJobLOTOTOOperator")]
        public async Task<IActionResult> RejectCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId, string rejectReason)
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
            CommonResponse response = checkListJobLOTOTOOperator.RejectCheckListJobLOTOTOOperator(checkListJobLOTOTOOperatorId,rejectReason);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Operator
        /// </summary>
        /// <param name="checkListJobLOTOTOOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTOOperator/DeleteCheckListJobLOTOTOOperator")]
        public async Task<IActionResult> DeleteCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId)
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
            response = checkListJobLOTOTOOperator.DeleteCheckListJobLOTOTOOperator(checkListJobLOTOTOOperatorId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Operator
        /// </summary>
        /// <param name="checkListJobLOTOTOOperatorId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTOOperator/ArchiveCheckListJobLOTOTOOperator")]
        public async Task<IActionResult> ArchiveCheckListJobLOTOTOOperator(int checkListJobLOTOTOOperatorId)
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
            response = checkListJobLOTOTOOperator.ArchiveCheckListJobLOTOTOOperator(checkListJobLOTOTOOperatorId, userId);

            return Ok(response);
        }
    }
}