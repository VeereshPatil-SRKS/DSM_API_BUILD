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
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.TargetOverAllEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class TargetOverAllController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ITargetOverAll targetOverAll;

        public TargetOverAllController(IOptions<AppSettings> appSettings, ITargetOverAll _targetOverAll)
        {
            _appSettings = appSettings.Value;
            targetOverAll = _targetOverAll;
        }

        /// <summary>
        /// Add and Edit TargetOverAll Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TargetOverAll/AddAndEditTargetOverAll")]
        public async Task<IActionResult> AddAndEditTargetOverAll(TargetOverAllCustom data)
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
            //calling TargetOverAllDAL busines layer
            CommonResponse response = new CommonResponse();
            response = targetOverAll.AddAndEditTargetOverAll(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple TargetOverAll Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TargetOverAll/ViewMultipleTargetOverAll")]
        public async Task<IActionResult> ViewMultipleTargetOverAll()
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
            //calling TargetOverAllDAL busines layer
            CommonResponse response = targetOverAll.ViewMultipleTargetOverAll(userId);

            return Ok(response);
        }

        /// <summary>
        /// View TargetOverAll Master By Id
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TargetOverAll/ViewTargetOverAllById")]
        public async Task<IActionResult> ViewTargetOverAllById(int targetId)
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
            //calling TargetOverAllDAL busines layer
            CommonResponse response = targetOverAll.ViewTargetOverAllById(targetId);

            return Ok(response);
        }

        /// <summary>
        /// Delete TargetOverAll Master
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TargetOverAll/DeleteTargetOverAll")]
        public async Task<IActionResult> DeleteTargetOverAll(int targetId)
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
            //calling TargetOverAllDAL busines layer
            CommonResponse response = new CommonResponse();
            response = targetOverAll.DeleteTargetOverAll(targetId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive TargetOverAll Master
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TargetOverAll/ArchiveTargetOverAll")]
        public async Task<IActionResult> ArchiveTargetOverAll(int targetId)
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
            //calling TargetOverAllDAL busines layer
            CommonResponse response = new CommonResponse();
            response = targetOverAll.ArchiveTargetOverAll(targetId, userId);

            return Ok(response);
        }
    }
}