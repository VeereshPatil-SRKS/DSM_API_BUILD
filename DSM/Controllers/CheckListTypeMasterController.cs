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
using static DSM.EntityModels.CheckListTypeMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListTypeMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListTypeMaster checkListTypeMaster;

        public CheckListTypeMasterController(IOptions<AppSettings> appSettings, ICheckListTypeMaster _checkListTypeMaster)
        {
            _appSettings = appSettings.Value;
            checkListTypeMaster = _checkListTypeMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListType/AddAndEditCheckListType")]
        public async Task<IActionResult> AddAndEditCheckListType(CheckListTypeCustom data)
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
            //calling CheckListTypeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListTypeMaster.AddAndEditCheckListType(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListType/ViewMultipleCheckListType")]
        public async Task<IActionResult> ViewMultipleCheckListType()
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
            //calling CheckListTypeDAL busines layer
            CommonResponse response = checkListTypeMaster.ViewMultipleCheckListType();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListType/ViewCheckListTypeById")]
        public async Task<IActionResult> ViewCheckListTypeById(int checkListTypeId)
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
            //calling CheckListTypeDAL busines layer
            CommonResponse response = checkListTypeMaster.ViewCheckListTypeById(checkListTypeId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListType/DeleteCheckListType")]
        public async Task<IActionResult> DeleteCheckListType(int checkListTypeId)
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
            //calling CheckListTypeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListTypeMaster.DeleteCheckListType(checkListTypeId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListType/ArchiveCheckListType")]
        public async Task<IActionResult> ArchiveCheckListType(int checkListTypeId)
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
            //calling CheckListTypeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListTypeMaster.ArchiveCheckListType(checkListTypeId, userId);

            return Ok(response);
        }
    }
}