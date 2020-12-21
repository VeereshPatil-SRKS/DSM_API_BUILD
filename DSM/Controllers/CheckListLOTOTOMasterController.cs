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
using static DSM.EntityModels.CheckListLOTOTOMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListLOTOTOMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListLOTOTOMaster checkListLOTOTOMaster;

        public CheckListLOTOTOMasterController(IOptions<AppSettings> appSettings, ICheckListLOTOTOMaster _checkListLOTOTOMaster)
        {
            _appSettings = appSettings.Value;
            checkListLOTOTOMaster = _checkListLOTOTOMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListLOTOTO/AddAndEditCheckListLOTOTO")]
        public async Task<IActionResult> AddAndEditCheckListLOTOTO(CheckListLOTOTOCustom data)
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
            //calling CheckListLOTOTODAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListLOTOTOMaster.AddAndEditCheckListLOTOTO(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListLOTOTO/ViewMultipleCheckListLOTOTO")]
        public async Task<IActionResult> ViewMultipleCheckListLOTOTO()
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
            //calling CheckListLOTOTODAL busines layer
            CommonResponse response = checkListLOTOTOMaster.ViewMultipleCheckListLOTOTO();

            return Ok(response);
        }

        /// <summary>
        /// View Check List LOTOTO By Check List Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListLOTOTO/ViewCheckListLOTOTOByCheckListMasterId")]
        public async Task<IActionResult> ViewCheckListLOTOTOByCheckListMasterId(int checkListMasterId, int checkListGroupId)
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
            //calling CheckListLOTOTODAL busines layer
            CommonResponse response = checkListLOTOTOMaster.ViewCheckListLOTOTOByCheckListMasterId(checkListMasterId,checkListGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListLOTOTOId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListLOTOTO/ViewCheckListLOTOTOById")]
        public async Task<IActionResult> ViewCheckListLOTOTOById(int checkListLOTOTOId)
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
            //calling CheckListLOTOTODAL busines layer
            CommonResponse response = checkListLOTOTOMaster.ViewCheckListLOTOTOById(checkListLOTOTOId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListLOTOTOId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListLOTOTO/DeleteCheckListLOTOTO")]
        public async Task<IActionResult> DeleteCheckListLOTOTO(int checkListLOTOTOId)
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
            //calling CheckListLOTOTODAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListLOTOTOMaster.DeleteCheckListLOTOTO(checkListLOTOTOId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListLOTOTOId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListLOTOTO/ArchiveCheckListLOTOTO")]
        public async Task<IActionResult> ArchiveCheckListLOTOTO(int checkListLOTOTOId)
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
            //calling CheckListLOTOTODAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListLOTOTOMaster.ArchiveCheckListLOTOTO(checkListLOTOTOId, userId);

            return Ok(response);
        }
    }
}