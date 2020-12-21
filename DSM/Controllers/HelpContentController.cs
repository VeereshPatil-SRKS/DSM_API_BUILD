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
using static DSM.EntityModels.HelpContentEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class HelpContentController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IHelpContent helpContentMaster;

        public HelpContentController(IOptions<AppSettings> appSettings, IHelpContent _helpContentMaster)
        {
            _appSettings = appSettings.Value;
            helpContentMaster = _helpContentMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("HelpContent/AddAndEditHelpContent")]
        public async Task<IActionResult> AddAndEditHelpContent(HelpContentCustom data)
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
            //calling HelpContentDAL busines layer
            CommonResponse response = new CommonResponse();
            response = helpContentMaster.AddAndEditHelpContent(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("HelpContent/ViewMultipleHelpContent")]
        public async Task<IActionResult> ViewMultipleHelpContent()
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
            //calling HelpContentDAL busines layer
            CommonResponse response = helpContentMaster.ViewMultipleHelpContent();

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Help Content In Help Screen
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2,3")]
        [HttpGet]
        [Route("HelpContent/ViewMultipleHelpContentInHelpScreen")]
        public async Task<IActionResult> ViewMultipleHelpContentInHelpScreen()
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
            //calling HelpContentDAL busines layer
            CommonResponse response = helpContentMaster.ViewMultipleHelpContentInHelpScreen(userId);

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="helpContentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HelpContent/ViewHelpContentById")]
        public async Task<IActionResult> ViewHelpContentById(int helpContentMasterId)
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
            //calling HelpContentDAL busines layer
            CommonResponse response = helpContentMaster.ViewHelpContentById(helpContentMasterId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="helpContentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HelpContent/DeleteHelpContent")]
        public async Task<IActionResult> DeleteHelpContent(int helpContentMasterId)
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
            //calling HelpContentDAL busines layer
            CommonResponse response = new CommonResponse();
            response = helpContentMaster.DeleteHelpContent(helpContentMasterId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="helpContentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HelpContent/ArchiveHelpContent")]
        public async Task<IActionResult> ArchiveHelpContent(int helpContentMasterId)
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
            //calling HelpContentDAL busines layer
            CommonResponse response = new CommonResponse();
            response = helpContentMaster.ArchiveHelpContent(helpContentMasterId, userId);

            return Ok(response);
        }
    }
}