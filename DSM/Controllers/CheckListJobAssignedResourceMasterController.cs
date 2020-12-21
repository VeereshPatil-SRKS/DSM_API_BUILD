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
using static DSM.EntityModels.CheckListJobAssignedResourceMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class CheckListJobAssignedResourceMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICheckListJobAssignedResourceMaster checkListJobAssignedResourceMaster;

        public CheckListJobAssignedResourceMasterController(IOptions<AppSettings> appSettings, ICheckListJobAssignedResourceMaster _checkListJobAssignedResourceMaster)
        {
            _appSettings = appSettings.Value;
            checkListJobAssignedResourceMaster = _checkListJobAssignedResourceMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckListJobAssignedResource/AddAndEditCheckListJobAssignedResource")]
        public async Task<IActionResult> AddAndEditCheckListJobAssignedResource(CheckListJobAssignedResourceMasterCustom data)
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAssignedResourceMaster.AddAndEditCheckListJobAssignedResource(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// Add And Edit Check List Job Assigned Resource All
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAssignedResource/AddAndEditCheckListJobAssignedResourceAll")]
        public async Task<IActionResult> AddAndEditCheckListJobAssignedResourceAll(int checkListJobMasterId)
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAssignedResourceMaster.AddAndEditCheckListJobAssignedResourceAll(checkListJobMasterId, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAssignedResource/ViewMultipleCheckListJobAssignedResource")]
        public async Task<IActionResult> ViewMultipleCheckListJobAssignedResource()
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = checkListJobAssignedResourceMaster.ViewMultipleCheckListJobAssignedResource();

            return Ok(response);
        }

        /// <summary>
        /// View Check ListJob AssignedResource By check ListJob Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAssignedResource/ViewCheckListJobAssignedResourceBycheckListJobMasterId")]
        public async Task<IActionResult> ViewCheckListJobAssignedResourceBycheckListJobMasterId(int checkListJobMasterId, int checkListJobGroupId)
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = checkListJobAssignedResourceMaster.ViewCheckListJobAssignedResourceBycheckListJobMasterId(checkListJobMasterId, checkListJobGroupId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAssignedResource/ViewCheckListJobAssignedResourceById")]
        public async Task<IActionResult> ViewCheckListJobAssignedResourceById(int checkListJobAssignedResourceId)
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = checkListJobAssignedResourceMaster.ViewCheckListJobAssignedResourceById(checkListJobAssignedResourceId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAssignedResource/DeleteCheckListJobAssignedResource")]
        public async Task<IActionResult> DeleteCheckListJobAssignedResource(int checkListJobAssignedResourceId)
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAssignedResourceMaster.DeleteCheckListJobAssignedResource(checkListJobAssignedResourceId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="checkListJobAssignedResourceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckListJobAssignedResource/ArchiveCheckListJobAssignedResource")]
        public async Task<IActionResult> ArchiveCheckListJobAssignedResource(int checkListJobAssignedResourceId)
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
            //calling CheckListJobAssignedResourceDAL busines layer
            CommonResponse response = new CommonResponse();
            response = checkListJobAssignedResourceMaster.ArchiveCheckListJobAssignedResource(checkListJobAssignedResourceId, userId);

            return Ok(response);
        }
    }
}