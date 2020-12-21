using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DSM.DAL.Helpers;
using DSM.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.CheckListJobAdvanceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobAdvanceMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobAdvanceMaster checkListJobAdvanceMaster;

        public CheckListJobAdvanceMasterController(IOptions<AppSettings> appSettings, ICheckListJobAdvanceMaster _checkListJobAdvanceMaster)
        {
            _appSettings = appSettings.Value;
            checkListJobAdvanceMaster = _checkListJobAdvanceMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListJobAdvance/AddAndEditCheckListJobAdvance")]
        public async Task<IActionResult> AddAndEditCheckListJobAdvance(CheckListJobAdvanceCustom data)
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
            //calling CheckListJobAdvanceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAdvanceMaster.AddAndEditCheckListJobAdvance(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvance/ViewMultipleCheckListJobAdvance")]
        public async Task<IActionResult> ViewMultipleCheckListJobAdvance()
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
            //calling CheckListJobAdvanceDAL busines layer
            CommonResponse response = checkListJobAdvanceMaster.ViewMultipleCheckListJobAdvance();

            return Ok(response);
        }

        /// <summary>
        /// View Check ListJob Advance By Check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvance/ViewCheckListJobAdvanceByCheckListJobMasterId")]
        public async Task<IActionResult> ViewCheckListJobAdvanceByCheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
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
            //calling CheckListJobAdvanceDAL busines layer
            CommonResponse response = checkListJobAdvanceMaster.ViewCheckListJobAdvanceByCheckListJobMasterId(checkListJobMasterId, checkListJobGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListJobAdvanceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvance/ViewCheckListJobAdvanceById")]
        public async Task<IActionResult> ViewCheckListJobAdvanceById(int checkListJobAdvanceId)
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
            //calling CheckListJobAdvanceDAL busines layer
            CommonResponse response = checkListJobAdvanceMaster.ViewCheckListJobAdvanceById(checkListJobAdvanceId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListJobAdvanceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvance/DeleteCheckListJobAdvance")]
        public async Task<IActionResult> DeleteCheckListJobAdvance(string checkListJobAdvanceId)
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
            //calling CheckListJobAdvanceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAdvanceMaster.DeleteCheckListJobAdvance(checkListJobAdvanceId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListJobAdvanceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAdvance/ArchiveCheckListJobAdvance")]
        public async Task<IActionResult> ArchiveCheckListJobAdvance(int checkListJobAdvanceId)
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
            //calling CheckListJobAdvanceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAdvanceMaster.ArchiveCheckListJobAdvance(checkListJobAdvanceId, userId);

            return Ok(response);
        }
    }
}