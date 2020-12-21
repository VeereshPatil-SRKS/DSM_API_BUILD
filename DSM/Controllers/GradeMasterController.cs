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
using static DSM.EntityModels.GradeMasterEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class GradeMasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IGradeMaster gradeMaster;

        public GradeMasterController(IOptions<AppSettings> appSettings, IGradeMaster _gradeMaster)
        {
            _appSettings = appSettings.Value;
            gradeMaster = _gradeMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Grade/AddAndEditGrade")]
        public async Task<IActionResult> AddAndEditGrade(GradeCustom data)
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
            //calling GradeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = gradeMaster.AddAndEditGrade(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// AddAndEditGradeExcel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Grade/AddAndEditGradeExcel")]
        public async Task<IActionResult> AddAndEditGradeExcel(List<GradeCustom> data)
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
            //calling GradeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = gradeMaster.AddAndEditGradeExcel(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Grade/ViewMultipleGrade")]
        public async Task<IActionResult> ViewMultipleGrade()
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
            //calling GradeDAL busines layer
            CommonResponse response = gradeMaster.ViewMultipleGrade();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Grade/ViewGradeById")]
        public async Task<IActionResult> ViewGradeById(int gradeId)
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
            //calling GradeDAL busines layer
            CommonResponse response = gradeMaster.ViewGradeById(gradeId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Grade/DeleteGrade")]
        public async Task<IActionResult> DeleteGrade(int gradeId)
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
            //calling GradeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = gradeMaster.DeleteGrade(gradeId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Grade/ArchiveGrade")]
        public async Task<IActionResult> ArchiveGrade(int gradeId)
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
            //calling GradeDAL busines layer
            CommonResponse response = new CommonResponse();
            response = gradeMaster.ArchiveGrade(gradeId, userId);

            return Ok(response);
        }
    }
}