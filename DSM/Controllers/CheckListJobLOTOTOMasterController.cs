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
using static DSM.EntityModels.CheckListJobLOTOTOMaster;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobLOTOTOMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobLOTOTOMaster checkListJobLOTOTOMaster;

        public CheckListJobLOTOTOMasterController(IOptions<AppSettings> appSettings, ICheckListJobLOTOTOMaster _checkListJobLOTOTOMaster)
        {
            _appSettings = appSettings.Value;
            checkListJobLOTOTOMaster = _checkListJobLOTOTOMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListJobLOTOTO/AddAndEditCheckListJobLOTOTO")]
        public async Task<IActionResult> AddAndEditCheckListJobLOTOTO(CheckListJobLOTOTOCustom data)
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
            response = checkListJobLOTOTOMaster.AddAndEditCheckListJobLOTOTO(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTO/ViewMultipleCheckListJobLOTOTO")]
        public async Task<IActionResult> ViewMultipleCheckListJobLOTOTO()
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
            CommonResponse response = checkListJobLOTOTOMaster.ViewMultipleCheckListJobLOTOTO();

            return Ok(response);
        }

        /// <summary>
        /// View Check ListJob LOTOTO By Check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTO/ViewCheckListJobLOTOTOByCheckListJobMasterId")]
        public async Task<IActionResult> ViewCheckListJobLOTOTOByCheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
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
            CommonResponse response = checkListJobLOTOTOMaster.ViewCheckListJobLOTOTOByCheckListJobMasterId(checkListJobMasterId, checkListJobGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListJobLOTOTOId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTO/ViewCheckListJobLOTOTOById")]
        public async Task<IActionResult> ViewCheckListJobLOTOTOById(int checkListJobLOTOTOId)
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
            CommonResponse response = checkListJobLOTOTOMaster.ViewCheckListJobLOTOTOById(checkListJobLOTOTOId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListJobLOTOTOId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTO/DeleteCheckListJobLOTOTO")]
        public async Task<IActionResult> DeleteCheckListJobLOTOTO(string checkListJobLOTOTOId)
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
            response = checkListJobLOTOTOMaster.DeleteCheckListJobLOTOTO(checkListJobLOTOTOId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListJobLOTOTOId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobLOTOTO/ArchiveCheckListJobLOTOTO")]
        public async Task<IActionResult> ArchiveCheckListJobLOTOTO(int checkListJobLOTOTOId)
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
            response = checkListJobLOTOTOMaster.ArchiveCheckListJobLOTOTO(checkListJobLOTOTOId, userId);

            return Ok(response);
        }
    }
}