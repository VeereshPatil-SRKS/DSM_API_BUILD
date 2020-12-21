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
using static DSM.EntityModels.ShiftMasterEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class ShiftMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IShiftMaster shiftMaster;

        public ShiftMasterController(IOptions<AppSettings> appSettings, IShiftMaster _shiftMaster)
        {
            _appSettings = appSettings.Value;
            shiftMaster = _shiftMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Shift/AddAndEditShift")]
        public async Task<IActionResult> AddAndEditShift(ShiftCustom data)
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
            //calling ShiftDAL busines layer
            CommonResponse response = new CommonResponse();
            response = shiftMaster.AddAndEditShift(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Shift/ViewMultipleShift")]
        public async Task<IActionResult> ViewMultipleShift()
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
            //calling ShiftDAL busines layer
            CommonResponse response = shiftMaster.ViewMultipleShift();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Shift/ViewShiftById")]
        public async Task<IActionResult> ViewShiftById(int shiftId)
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
            //calling ShiftDAL busines layer
            CommonResponse response = shiftMaster.ViewShiftById(shiftId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Shift/DeleteShift")]
        public async Task<IActionResult> DeleteShift(int shiftId)
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
            //calling ShiftDAL busines layer
            CommonResponse response = new CommonResponse();
            response = shiftMaster.DeleteShift(shiftId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Shift/ArchiveShift")]
        public async Task<IActionResult> ArchiveShift(int shiftId)
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
            //calling ShiftDAL busines layer
            CommonResponse response = new CommonResponse();
            response = shiftMaster.ArchiveShift(shiftId, userId);

            return Ok(response);
        }
    }
}