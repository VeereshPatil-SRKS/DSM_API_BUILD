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
using static DSM.EntityModels.CheckListMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListMaster checkListMaster;

        public CheckListMasterController(IOptions<AppSettings> appSettings, ICheckListMaster _checkListMaster)
        {
            _appSettings = appSettings.Value;
            checkListMaster = _checkListMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckList/AddAndEditCheckList")]
        public async Task<IActionResult> AddAndEditCheckList(CheckListCustom data)
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
            //calling CheckListDAL busines layer
            CommonResponseWithIds response = new CommonResponseWithIds();
            response = checkListMaster.AddAndEditCheckList(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// Get Automatic Check List Version Name
        /// </summary>
        /// <param name="chekcListName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/GetAutomaticCheckListVersionName")]
        public async Task<IActionResult> GetAutomaticCheckListVersionName(string chekcListName)
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
            //calling CheckListDAL busines layer
            CommonResponse response = checkListMaster.GetAutomaticCheckListVersionName(chekcListName);

            return Ok(response);
        }

        /// <summary>
        /// Get Auto Suggest For Check List Name
        /// </summary>
        /// <param name="chekcListName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/GetAutoSuggestForCheckListName")]
        public async Task<IActionResult> GetAutoSuggestForCheckListName(string chekcListName)
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
            //calling CheckListDAL busines layer
            CommonResponse response = checkListMaster.GetAutoSuggestForCheckListName(chekcListName);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/ViewMultipleCheckList")]
        public async Task<IActionResult> ViewMultipleCheckList()
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
            //calling CheckListDAL busines layer
            CommonResponse response = checkListMaster.ViewMultipleCheckList();

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Check List WRT Category Id And Type Id
        /// </summary>
        /// <param name="checkListCategoryId"></param>
        /// <param name="checkListTypeId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/ViewMultipleCheckListWRTCategoryIdAndTypeId")]
        public async Task<IActionResult> ViewMultipleCheckListWRTCategoryIdAndTypeId(int checkListCategoryId, int checkListTypeId)
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
            //calling CheckListDAL busines layer
            CommonResponse response = checkListMaster.ViewMultipleCheckListWRTCategoryIdAndTypeId(checkListCategoryId, checkListTypeId,userId);

            return Ok(response);
        }

        /// <summary>
        /// View Check List Details By Id
        /// </summary>
        /// <param name="checkListMasterId"></param>
        /// <param name="checkListGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/ViewCheckListDetailsByCheckListAndGroupId")]
        public async Task<IActionResult> ViewCheckListDetailsByCheckListAndGroupId(int checkListMasterId, int checkListGroupId)
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
            //calling CheckListDAL busines layer
            CommonResponse response = checkListMaster.ViewCheckListDetailsByCheckListAndGroupId(checkListMasterId, checkListGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/ViewCheckListById")]
        public async Task<IActionResult> ViewCheckListById(int checkListId)
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
            //calling CheckListDAL busines layer
            CommonResponse response = checkListMaster.ViewCheckListById(checkListId);

            return Ok(response);
        }
        
        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/DeleteCheckList")]
        public async Task<IActionResult> DeleteCheckList(int checkListId)
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
            //calling CheckListDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListMaster.DeleteCheckList(checkListId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/ArchiveCheckList")]
        public async Task<IActionResult> ArchiveCheckList(int checkListId)
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
            //calling CheckListDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListMaster.ArchiveCheckList(checkListId, userId);

            return Ok(response);
        }
        
        /// <summary>
        /// Check Check List Completly Created Or Not
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckList/CheckCheckListCompletlyCreatedOrNot")]
        public async Task<IActionResult> CheckCheckListCompletlyCreatedOrNot(int checkListId)
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
            //calling CheckListDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListMaster.CheckCheckListCompletlyCreatedOrNot(checkListId);

            return Ok(response);
        }
    }
}