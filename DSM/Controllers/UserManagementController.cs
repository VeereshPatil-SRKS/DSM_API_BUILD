using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DSM.DAL.Helpers;
using DSM.Interface;
using DSM.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.UserManagementEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserManagement user;

        public UserManagementController(IOptions<AppSettings> appSettings, IUserManagement _user)
        {
            _appSettings = appSettings.Value;
            user = _user;
        }
        
        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("User/AddAndEditUser")]
        public async Task<IActionResult> AddAndEditUser(UserDetailsCustom data)
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
            long usersId = Convert.ToInt32(id);
            #endregion
            //calling UserDAL busines layer
            CommonResponse response = new CommonResponse();
            response = user.AddAndEditUser(data, usersId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("User/ViewMultipleUser")]
        public async Task<IActionResult> ViewMultipleUser()
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
            //calling UserDAL busines layer
            CommonResponse response = user.ViewMultipleUser(userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple User DropDown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ViewMultipleUserDropDown")]
        public async Task<IActionResult> ViewMultipleUserDropDown()
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
            //calling UserDAL busines layer
            CommonResponse response = user.ViewMultipleUserDropDown(userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple User For Super Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ViewMultipleUserForSuperAdmin")]
        public async Task<IActionResult> ViewMultipleUserForSuperAdmin()
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
            //calling UserDAL busines layer
            CommonResponse response = user.ViewMultipleUserForSuperAdmin();

            return Ok(response);
        }

        /// <summary>
        /// View Multiple User For Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ViewMultipleUserForAdmin")]
        public async Task<IActionResult> ViewMultipleUserForAdmin()
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
            //calling UserDAL busines layer
            CommonResponse response = user.ViewMultipleUserForAdmin();

            return Ok(response);
        }

        /// <summary>
        /// View Multiple User For Operator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ViewMultipleUserForOperator")]
        public async Task<IActionResult> ViewMultipleUserForOperator()
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
            //calling UserDAL busines layer
            CommonResponse response = user.ViewMultipleUserForOperator();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ViewUserById")]
        public async Task<IActionResult> ViewUserById(int userId)
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
            long usersId = Convert.ToInt32(id);
            #endregion
            //calling UserDAL busines layer
            CommonResponse response = user.ViewUserById(userId);

            return Ok(response);
        }

        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(int userId,string password)
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
            long usersId = Convert.ToInt32(id);
            #endregion
            //calling UserDAL busines layer
            CommonResponse response = user.UpdateUserPassword(userId,password,usersId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
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
            long usersId = Convert.ToInt32(id);
            #endregion
            //calling UserDAL busines layer
            CommonResponse response = new CommonResponse();
            response = user.DeleteUser(userId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ArchiveUser")]
        public async Task<IActionResult> ArchiveUser(int userId)
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
            long usersId = Convert.ToInt32(id);
            #endregion
            //calling UserDAL busines layer
            CommonResponse response = new CommonResponse();
            response = user.ArchiveUser(userId, usersId);

            return Ok(response);
        }

        /// <summary>
        /// Check User Name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/CheckUserName")]
        public async Task<IActionResult> CheckUserName(string userName,long usersId)
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
            //calling UserDAL busines layer
            CommonResponse response = new CommonResponse();
            response = user.CheckUserName(userName, usersId);

            return Ok(response);
        }
    }
}