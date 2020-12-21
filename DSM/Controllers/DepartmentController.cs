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
using static DSM.EntityModels.DepartmentEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IDepartment departmentMaster;

        public DepartmentController(IOptions<AppSettings> appSettings, IDepartment _departmentMaster)
        {
            _appSettings = appSettings.Value;
            departmentMaster = _departmentMaster;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Department/AddAndEditDepartment")]
        public async Task<IActionResult> AddAndEditDepartment(DepartmentCustom data)
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
            //calling DepartmentDAL busines layer
            CommonResponse response = new CommonResponse();
            response = departmentMaster.AddAndEditDepartment(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Department/ViewMultipleDepartment")]
        public async Task<IActionResult> ViewMultipleDepartment()
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
            //calling DepartmentDAL busines layer
            CommonResponse response = departmentMaster.ViewMultipleDepartment();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="departmentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Department/ViewDepartmentById")]
        public async Task<IActionResult> ViewDepartmentById(int departmentMasterId)
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
            //calling DepartmentDAL busines layer
            CommonResponse response = departmentMaster.ViewDepartmentById(departmentMasterId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="departmentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Department/DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int departmentMasterId)
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
            //calling DepartmentDAL busines layer
            CommonResponse response = new CommonResponse();
            response = departmentMaster.DeleteDepartment(departmentMasterId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="departmentMasterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Department/ArchiveDepartment")]
        public async Task<IActionResult> ArchiveDepartment(int departmentMasterId)
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
            //calling DepartmentDAL busines layer
            CommonResponse response = new CommonResponse();
            response = departmentMaster.ArchiveDepartment(departmentMasterId, userId);

            return Ok(response);
        }
    }
}