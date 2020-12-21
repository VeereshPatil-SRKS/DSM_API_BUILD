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
using static DSM.EntityModels.CheckListGroupMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListGroupMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListGroupMaster checkListGroupMaster;

        public CheckListGroupMasterController(IOptions<AppSettings> appSettings, ICheckListGroupMaster _checkListGroupMaster)
        {
            _appSettings = appSettings.Value;
            checkListGroupMaster = _checkListGroupMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListGroup/AddAndEditCheckListGroup")]
        public async Task<IActionResult> AddAndEditCheckListGroup(CheckListGroupCustom data)
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListGroupMaster.AddAndEditCheckListGroup(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListGroup/ViewMultipleCheckListGroup")]
        public async Task<IActionResult> ViewMultipleCheckListGroup()
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = checkListGroupMaster.ViewMultipleCheckListGroup();

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Check List Group By Check List Master Id
        /// </summary>
        /// <param name="checkListMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListGroup/ViewMultipleCheckListGroupByCheckListMasterId")]
        public async Task<IActionResult> ViewMultipleCheckListGroupByCheckListMasterId(int checkListMasterId)
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = checkListGroupMaster.ViewMultipleCheckListGroupByCheckListMasterId(checkListMasterId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Check List Group By Check List Job Master Id
        /// </summary>
        /// <param name="checkListjobMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListGroup/ViewMultipleCheckListGroupByCheckListJobMasterId")]
        public async Task<IActionResult> ViewMultipleCheckListGroupByCheckListJobMasterId(int checkListjobMasterId)
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = checkListGroupMaster.ViewMultipleCheckListGroupByCheckListJobMasterId(checkListjobMasterId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListGroup/ViewCheckListGroupById")]
        public async Task<IActionResult> ViewCheckListGroupById(int checkListGroupId)
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = checkListGroupMaster.ViewCheckListGroupById(checkListGroupId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListGroup/DeleteCheckListGroup")]
        public async Task<IActionResult> DeleteCheckListGroup(int checkListGroupId)
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListGroupMaster.DeleteCheckListGroup(checkListGroupId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListGroup/ArchiveCheckListGroup")]
        public async Task<IActionResult> ArchiveCheckListGroup(int checkListGroupId)
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
            //calling CheckListGroupDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListGroupMaster.ArchiveCheckListGroup(checkListGroupId, userId);

            return Ok(response);
        }
    }
}