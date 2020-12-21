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
using static DSM.EntityModels.CheckListCategoryMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListCategoryMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListCategoryMaster checkListCategoryMaster;

        public CheckListCategoryMasterController(IOptions<AppSettings> appSettings, ICheckListCategoryMaster _checkListCategoryMaster)
        {
            _appSettings = appSettings.Value;
            checkListCategoryMaster = _checkListCategoryMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListCategory/AddAndEditCheckListCategory")]
        public async Task<IActionResult> AddAndEditCheckListCategory(CheckListCategoryCustom data)
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
            //calling CheckListCategoryDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListCategoryMaster.AddAndEditCheckListCategory(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListCategory/ViewMultipleCheckListCategory")]
        public async Task<IActionResult> ViewMultipleCheckListCategory()
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
            //calling CheckListCategoryDAL busines layer
            CommonResponse response = checkListCategoryMaster.ViewMultipleCheckListCategory();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListCategory/ViewCheckListCategoryById")]
        public async Task<IActionResult> ViewCheckListCategoryById(int checkListCategoryId)
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
            //calling CheckListCategoryDAL busines layer
            CommonResponse response = checkListCategoryMaster.ViewCheckListCategoryById(checkListCategoryId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListCategory/DeleteCheckListCategory")]
        public async Task<IActionResult> DeleteCheckListCategory(int checkListCategoryId)
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
            //calling CheckListCategoryDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListCategoryMaster.DeleteCheckListCategory(checkListCategoryId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListCategory/ArchiveCheckListCategory")]
        public async Task<IActionResult> ArchiveCheckListCategory(int checkListCategoryId)
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
            //calling CheckListCategoryDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListCategoryMaster.ArchiveCheckListCategory(checkListCategoryId, userId);

            return Ok(response);
        }
    }
}