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
using static DSM.EntityModels.LineNumberMasterEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class LineNumberMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ILineNumberMaster lineNumberMaster;

        public LineNumberMasterController(IOptions<AppSettings> appSettings, ILineNumberMaster _lineNumberMaster)
        {
            _appSettings = appSettings.Value;
            lineNumberMaster = _lineNumberMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LineNumber/AddAndEditLineNumber")]
        public async Task<IActionResult> AddAndEditLineNumber(LineNumberCustom data)
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
            //calling LineNumberDAL busines layer
            CommonResponse response = new CommonResponse();
            response = lineNumberMaster.AddAndEditLineNumber(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("LineNumber/ViewMultipleLineNumber")]
        public async Task<IActionResult> ViewMultipleLineNumber()
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
            //calling LineNumberDAL busines layer
            CommonResponse response = lineNumberMaster.ViewMultipleLineNumber();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="lineNumberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LineNumber/ViewLineNumberById")]
        public async Task<IActionResult> ViewLineNumberById(int lineNumberId)
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
            //calling LineNumberDAL busines layer
            CommonResponse response = lineNumberMaster.ViewLineNumberById(lineNumberId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="lineNumberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LineNumber/DeleteLineNumber")]
        public async Task<IActionResult> DeleteLineNumber(int lineNumberId)
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
            //calling LineNumberDAL busines layer
            CommonResponse response = new CommonResponse();
            response = lineNumberMaster.DeleteLineNumber(lineNumberId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="lineNumberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LineNumber/ArchiveLineNumber")]
        public async Task<IActionResult> ArchiveLineNumber(int lineNumberId)
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
            //calling LineNumberDAL busines layer
            CommonResponse response = new CommonResponse();
            response = lineNumberMaster.ArchiveLineNumber(lineNumberId, userId);

            return Ok(response);
        }
    }
}