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
using static DSM.EntityModels.CheckListActivityMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListActivityMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListActivityMaster checkListActivityMaster;

        public CheckListActivityMasterController(IOptions<AppSettings> appSettings, ICheckListActivityMaster _checkListActivityMaster)
        {
            _appSettings = appSettings.Value;
            checkListActivityMaster = _checkListActivityMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListActivity/AddAndEditCheckListActivity")]
        public async Task<IActionResult> AddAndEditCheckListActivity(CheckListActivityCustom data)
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
            //calling CheckListActivityDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListActivityMaster.AddAndEditCheckListActivity(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListActivity/ViewMultipleCheckListActivity")]
        public async Task<IActionResult> ViewMultipleCheckListActivity()
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
            //calling CheckListActivityDAL busines layer
            CommonResponse response = checkListActivityMaster.ViewMultipleCheckListActivity();

            return Ok(response);
        }

        /// <summary>
        /// View Check List Activity By check List Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListActivity/ViewCheckListActivityBycheckListMasterId")]
        public async Task<IActionResult> ViewCheckListActivityBycheckListMasterId(int checkListMasterId, int checkListGroupId)
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
            //calling CheckListActivityDAL busines layer
            CommonResponse response = checkListActivityMaster.ViewCheckListActivityBycheckListMasterId(checkListMasterId,checkListGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListActivityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListActivity/ViewCheckListActivityById")]
        public async Task<IActionResult> ViewCheckListActivityById(int checkListActivityId)
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
            //calling CheckListActivityDAL busines layer
            CommonResponse response = checkListActivityMaster.ViewCheckListActivityById(checkListActivityId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListActivityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListActivity/DeleteCheckListActivity")]
        public async Task<IActionResult> DeleteCheckListActivity(int checkListActivityId)
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
            //calling CheckListActivityDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListActivityMaster.DeleteCheckListActivity(checkListActivityId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListActivityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListActivity/ArchiveCheckListActivity")]
        public async Task<IActionResult> ArchiveCheckListActivity(int checkListActivityId)
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
            //calling CheckListActivityDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListActivityMaster.ArchiveCheckListActivity(checkListActivityId, userId);

            return Ok(response);
        }
    }
}