using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DSM.DAL.Helpers;
using DSM.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.RoleEntity;

namespace DSM.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IRole roleMaster;

        public RoleController(IOptions<AppSettings> appSettings, IRole _roleMaster)
        {
            _appSettings = appSettings.Value;
            roleMaster = _roleMaster;
        }

        /// <summary>
        /// Add and Edit Role Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [Route("Role/AddAndEditRole")]
        public async Task<IActionResult> AddAndEditRole(RoleCustom data)
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
            //calling RoleDAL busines layer
            CommonResponse response = new CommonResponse();
            response = roleMaster.AddAndEditRole(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Role Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("Role/ViewMultipleRole")]
        public async Task<IActionResult> ViewMultipleRole()
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
            //calling RoleDAL busines layer
            CommonResponse response = roleMaster.ViewMultipleRole(userId);

            return Ok(response);
        }

        /// <summary>
        /// View Role Master By Id
        /// </summary>
        /// <param name="roleMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Role/ViewRoleById")]
        public async Task<IActionResult> ViewRoleById(int roleMasterId)
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
            //calling RoleDAL busines layer
            CommonResponse response = roleMaster.ViewRoleById(roleMasterId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Role Master
        /// </summary>
        /// <param name="roleMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Role/DeleteRole")]
        public async Task<IActionResult> DeleteRole(int roleMasterId)
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
            //calling RoleDAL busines layer
            CommonResponse response = new CommonResponse();
            response = roleMaster.DeleteRole(roleMasterId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Role Master
        /// </summary>
        /// <param name="roleMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Role/ArchiveRole")]
        public async Task<IActionResult> ArchiveRole(int roleMasterId)
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
            //calling RoleDAL busines layer
            CommonResponse response = new CommonResponse();
            response = roleMaster.ArchiveRole(roleMasterId, userId);

            return Ok(response);
        }
    }
}