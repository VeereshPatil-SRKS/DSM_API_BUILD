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
using static DSM.EntityModels.CheckListSubCategoryMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListSubCategoryMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListSubCategoryMaster checkListSubCategoryMaster;

        public CheckListSubCategoryMasterController(IOptions<AppSettings> appSettings, ICheckListSubCategoryMaster _checkListSubCategoryMaster)
        {
            _appSettings = appSettings.Value;
            checkListSubCategoryMaster = _checkListSubCategoryMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListSubCategory/AddAndEditCheckListSubCategory")]
        public async Task<IActionResult> AddAndEditCheckListSubCategory(CheckListSubCategoryCustom data)
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
            //calling CheckListSubCategoryDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSubCategoryMaster.AddAndEditCheckListSubCategory(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListSubCategory/ViewMultipleCheckListSubCategory")]
        public async Task<IActionResult> ViewMultipleCheckListSubCategory()
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
            //calling CheckListSubCategoryDAL busines layer
            CommonResponse response = checkListSubCategoryMaster.ViewMultipleCheckListSubCategory();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListSubCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListSubCategory/ViewCheckListSubCategoryById")]
        public async Task<IActionResult> ViewCheckListSubCategoryById(int checkListSubCategoryId)
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
            //calling CheckListSubCategoryDAL busines layer
            CommonResponse response = checkListSubCategoryMaster.ViewCheckListSubCategoryById(checkListSubCategoryId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListSubCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListSubCategory/DeleteCheckListSubCategory")]
        public async Task<IActionResult> DeleteCheckListSubCategory(int checkListSubCategoryId)
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
            //calling CheckListSubCategoryDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSubCategoryMaster.DeleteCheckListSubCategory(checkListSubCategoryId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListSubCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListSubCategory/ArchiveCheckListSubCategory")]
        public async Task<IActionResult> ArchiveCheckListSubCategory(int checkListSubCategoryId)
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
            //calling CheckListSubCategoryDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListSubCategoryMaster.ArchiveCheckListSubCategory(checkListSubCategoryId, userId);

            return Ok(response);
        }
    }
}