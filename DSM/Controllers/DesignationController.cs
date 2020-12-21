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
using static DSM.EntityModels.DesignationEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IDesignation designation;

        public DesignationController(IOptions<AppSettings> appSettings, IDesignation _designation)
        {
            _appSettings = appSettings.Value;
            designation = _designation;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Designation/AddAndEditDesignation")]
        public async Task<IActionResult> AddAndEditDesignation(DesignationCustom data)
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
            //calling DesignationDAL busines layer
            CommonResponse response = new CommonResponse();
            response = designation.AddAndEditDesignation(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Designation/ViewMultipleDesignation")]
        public async Task<IActionResult> ViewMultipleDesignation()
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
            //calling DesignationDAL busines layer
            CommonResponse response = designation.ViewMultipleDesignation();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="designationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Designation/ViewDesignationById")]
        public async Task<IActionResult> ViewDesignationById(int designationId)
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
            //calling DesignationDAL busines layer
            CommonResponse response = designation.ViewDesignationById(designationId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="designationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Designation/DeleteDesignation")]
        public async Task<IActionResult> DeleteDesignation(int designationId)
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
            //calling DesignationDAL busines layer
            CommonResponse response = new CommonResponse();
            response = designation.DeleteDesignation(designationId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="designationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Designation/ArchiveDesignation")]
        public async Task<IActionResult> ArchiveDesignation(int designationId)
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
            //calling DesignationDAL busines layer
            CommonResponse response = new CommonResponse();
            response = designation.ArchiveDesignation(designationId, userId);

            return Ok(response);
        }
    }
}