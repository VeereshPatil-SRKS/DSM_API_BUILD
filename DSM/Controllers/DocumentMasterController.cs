using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DSM.DAL.Helpers;
using DSM.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DocumentMasterEntity;

namespace DSM.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
    [ApiController]
    public class DocumentMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IDocumentMaster documentMaster;

        public DocumentMasterController(IOptions<AppSettings> appSettings, IDocumentMaster _documentMaster)
        {
            _appSettings = appSettings.Value;
            documentMaster = _documentMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DocumentMaster/AddAndEditDocumentMaster")]
        public async Task<IActionResult> AddAndEditDocumentMaster(DocumentMasterCustom data)
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
            //calling DocumentMasterDAL busines layer
            CommonResponse response = new CommonResponse();
            response = documentMaster.AddAndEditDocumentMaster(data, userId);
            
            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentMaster/ViewMultipleDocumentMaster")]
        public async Task<IActionResult> ViewMultipleDocumentMaster()
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
            //calling DocumentMasterDAL busines layer
            CommonResponse response = documentMaster.ViewMultipleDocumentMaster();
            
             return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentMaster/ViewDocumentMasterById")]
        public async Task<IActionResult> ViewDocumentMasterById(int documentMasterId)
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
            //calling DocumentMasterDAL busines layer
            CommonResponse response = documentMaster.ViewDocumentMasterById(documentMasterId);
            
            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentMaster/DeleteDocumentMaster")]
        public async Task<IActionResult> DeleteDocumentMaster(int documentMasterId)
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
            //calling DocumentMasterDAL busines layer
            CommonResponse response = new CommonResponse();
            response = documentMaster.DeleteDocumentMaster(documentMasterId, userId);
            
             return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="documentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentMaster/ArchiveDocumentMaster")]
        public async Task<IActionResult> ArchiveDocumentMaster(int documentMasterId)
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
            //calling DocumentMasterDAL busines layer
            CommonResponse response = new CommonResponse();
            response = documentMaster.ArchiveDocumentMaster(documentMasterId, userId);
            
             return Ok(response);
        }

    }
}