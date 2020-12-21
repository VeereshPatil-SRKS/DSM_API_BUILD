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
using static DSM.EntityModels.CheckListJobActivityMaster;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobActivityMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobActivityMaster checkListJobActivityMaster;

        public CheckListJobActivityMasterController(IOptions<AppSettings> appSettings, ICheckListJobActivityMaster _checkListJobActivityMaster)
        {
            _appSettings = appSettings.Value;
            checkListJobActivityMaster = _checkListJobActivityMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListJobActivity/AddAndEditCheckListJobActivity")]
        public async Task<IActionResult> AddAndEditCheckListJobActivity(CheckListJobActivityCustom data)
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
            //calling CheckListJobActivityDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobActivityMaster.AddAndEditCheckListJobActivity(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivity/ViewMultipleCheckListJobActivity")]
        public async Task<IActionResult> ViewMultipleCheckListJobActivity()
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
            //calling CheckListJobActivityDAL busines layer
            CommonResponse response = checkListJobActivityMaster.ViewMultipleCheckListJobActivity();

            return Ok(response);
        }

        /// <summary>
        /// View Check ListJob Activity By check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivity/ViewCheckListJobActivityBycheckListJobMasterId")]
        public async Task<IActionResult> ViewCheckListJobActivityBycheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
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
            //calling CheckListJobActivityDAL busines layer
            CommonResponse response = checkListJobActivityMaster.ViewCheckListJobActivityBycheckListJobMasterId(checkListJobMasterId, checkListJobGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListJobActivityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivity/ViewCheckListJobActivityById")]
        public async Task<IActionResult> ViewCheckListJobActivityById(int checkListJobActivityId)
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
            //calling CheckListJobActivityDAL busines layer
            CommonResponse response = checkListJobActivityMaster.ViewCheckListJobActivityById(checkListJobActivityId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListJobActivityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivity/DeleteCheckListJobActivity")]
        public async Task<IActionResult> DeleteCheckListJobActivity(string checkListJobActivityId)
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
            //calling CheckListJobActivityDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobActivityMaster.DeleteCheckListJobActivity(checkListJobActivityId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListJobActivityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobActivity/ArchiveCheckListJobActivity")]
        public async Task<IActionResult> ArchiveCheckListJobActivity(int checkListJobActivityId)
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
            //calling CheckListJobActivityDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobActivityMaster.ArchiveCheckListJobActivity(checkListJobActivityId, userId);

            return Ok(response);
        }
    }
}