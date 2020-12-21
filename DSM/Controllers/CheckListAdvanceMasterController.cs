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
using static DSM.EntityModels.CheckListAdvanceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListAdvanceMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListAdvanceMaster checkListAdvanceMaster;

        public CheckListAdvanceMasterController(IOptions<AppSettings> appSettings, ICheckListAdvanceMaster _checkListAdvanceMaster)
        {
            _appSettings = appSettings.Value;
            checkListAdvanceMaster = _checkListAdvanceMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListAdvance/AddAndEditCheckListAdvance")]
        public async Task<IActionResult> AddAndEditCheckListAdvance(CheckListAdvanceCustom data)
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
            //calling CheckListAdvanceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListAdvanceMaster.AddAndEditCheckListAdvance(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListAdvance/ViewMultipleCheckListAdvance")]
        public async Task<IActionResult> ViewMultipleCheckListAdvance()
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
            //calling CheckListAdvanceDAL busines layer
            CommonResponse response = checkListAdvanceMaster.ViewMultipleCheckListAdvance();

            return Ok(response);
        }

        /// <summary>
        /// View Check List Advance By Check List Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListAdvance/ViewCheckListAdvanceByCheckListMasterId")]
        public async Task<IActionResult> ViewCheckListAdvanceByCheckListMasterId(int checkListMasterId, int checkListGroupId)
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
            //calling CheckListAdvanceDAL busines layer
            CommonResponse response = checkListAdvanceMaster.ViewCheckListAdvanceByCheckListMasterId(checkListMasterId,checkListGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListAdvanceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListAdvance/ViewCheckListAdvanceById")]
        public async Task<IActionResult> ViewCheckListAdvanceById(int checkListAdvanceId)
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
            //calling CheckListAdvanceDAL busines layer
            CommonResponse response = checkListAdvanceMaster.ViewCheckListAdvanceById(checkListAdvanceId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListAdvanceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListAdvance/DeleteCheckListAdvance")]
        public async Task<IActionResult> DeleteCheckListAdvance(int checkListAdvanceId)
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
            //calling CheckListAdvanceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListAdvanceMaster.DeleteCheckListAdvance(checkListAdvanceId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListAdvanceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListAdvance/ArchiveCheckListAdvance")]
        public async Task<IActionResult> ArchiveCheckListAdvance(int checkListAdvanceId)
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
            //calling CheckListAdvanceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListAdvanceMaster.ArchiveCheckListAdvance(checkListAdvanceId, userId);

            return Ok(response);
        }
    }
}