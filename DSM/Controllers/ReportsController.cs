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
using static DSM.EntityModels.ReportsEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IReports reports;

        public ReportsController(IOptions<AppSettings> appSettings, IReports _reports)
        {
            _appSettings = appSettings.Value;
            reports = _reports;
        }

        /// <summary>
        /// View Multiple Check List Job For Reports
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("Reports/ViewMultipleCheckListJobForReports")]
        public async Task<IActionResult> ViewMultipleCheckListJobForReports(string checkListJobStartTime, string checkListJobEndTime)
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

            //long userId =2;
            #endregion
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.ViewMultipleCheckListJobForReports( checkListJobStartTime,  checkListJobEndTime, userId);

            return Ok(response);
        }

        /// <summary>
        /// Check List For Supervisor Approval
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("Reports/CheckListForSupervisorApproval")]
        public async Task<IActionResult> CheckListForSupervisorApproval(int checkListJobId, int checkListJobGroupId)
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
            //calling CheckListJobActivityOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = reports.CheckListForSupervisorApproval(checkListJobId, checkListJobGroupId, userId);

            return Ok(response);
        }

        /// <summary>
        /// CheckListForSupervisorApprovalAll
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        [HttpGet]
       [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("Reports/CheckListForSupervisorApprovalAll")]
        public async Task<IActionResult> CheckListForSupervisorApprovalAll(int checkListJobId)
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
            //calling CheckListJobActivityOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = reports.CheckListForSupervisorApprovalAll(checkListJobId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Check For Job Approval By Check List Job Id And Check List Group Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <param name="checkListJobGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [Route("Reports/CheckForJobApprovalByCheckListJobIdAndCheckListGroupId")]
        public async Task<IActionResult> CheckForJobApprovalByCheckListJobIdAndCheckListGroupId(int checkListJobId, int checkListJobGroupId)
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
            //calling CheckListJobActivityOperatorDAL busines layer
            CommonResponse response = new CommonResponse();
            response = reports.CheckForJobApprovalByCheckListJobIdAndCheckListGroupId(checkListJobId, checkListJobGroupId, userId);

            return Ok(response);
        }


        #region Reports

        /// <summary>
        /// Bar Chart All Check List Average Time Taken Over All Monthly  - Level 1 Graph
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Reports/BarChartAllCheckListAverageTimeTakenOverAllMonthly")]
        public async Task<IActionResult> BarChartAllCheckListAverageTimeTakenOverAllMonthly(string fromDate, string toDate)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListAverageTimeTakenOverAllMonthly(fromDate, toDate, userId);

            return Ok(response);
        }

        /// <summary>
        /// Line Chart All Check List Average Time Taken Over All - Level 2 Graph
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("Reports/LineChartAllCheckListAverageTimeTakenOverAll")]
        public async Task<IActionResult> LineChartAllCheckListAverageTimeTakenOverAll(string fromDate, string toDate)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponseTable response = reports.LineChartAllCheckListAverageTimeTakenOverAll(fromDate, toDate, userId);

            return Ok(response);
        }

        /// <summary>
        /// Bar Chart All Check List Average Time Taken Over All Line Wise - Level 3 Graph
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("Reports/BarChartAllCheckListAverageTimeTakenOverAllLineWise")]
        public async Task<IActionResult> BarChartAllCheckListAverageTimeTakenOverAllLineWise(string fromDate, string toDate,int lineNumber)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListAverageTimeTakenOverAllLineWise(fromDate, toDate, lineNumber, userId);

            return Ok(response);
        }

        /// <summary>
        /// Bar Chart All Check List Detailed Change Over Line Wise - Level 4 Graph
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="groupIds"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Reports/BarChartAllCheckListDetailedChangeOverLineWise")]
        public async Task<IActionResult> BarChartAllCheckListDetailedChangeOverLineWise(string fromDate, string toDate,string groupIds, int lineNumber)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListDetailedChangeOverLineWise(fromDate, toDate, lineNumber, groupIds, userId);

            return Ok(response);
        }

        /// <summary>
        /// Pie Chart All Activity Time Taken By CheckList Job Id
        /// </summary>
        /// <param name="checkListJobId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Reports/PieChartAllActivityTimeTakenByCheckListJobId")]
        public async Task<IActionResult> PieChartAllActivityTimeTakenByCheckListJobId(string checkListJobName, string groupName)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.PieChartAllActivityTimeTakenByCheckListJobId(checkListJobName, groupName, userId);

            return Ok(response);
        }


        /// <summary>
        /// Pie Chart All Jobs Details
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("Reports/PieChartAllJobsDetails")]
        public async Task<IActionResult> PieChartAllJobsDetails()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.PieChartAllJobsDetails(userId);

            return Ok(response);
        }

        /// <summary>
        /// Bar Chart All Check List Average Time Taken
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("Reports/BarChartAllCheckListAverageTimeTaken")]
        public async Task<IActionResult> BarChartAllCheckListAverageTimeTaken()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListAverageTimeTaken(userId);

            return Ok(response);
        }
        
        /// <summary>
        /// Donut Chart All Activity Average Time Taken
        /// </summary>
        /// <param name="checkListId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1,2")]
        [HttpGet]
        [Route("Reports/DonutChartAllActivityAverageTimeTaken")]
        public async Task<IActionResult> DonutChartAllActivityAverageTimeTaken(int checkListId)
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.DonutChartAllActivityAverageTimeTaken(checkListId,userId);

            return Ok(response);
        }



        //veeresh code 03/03/2021
        // Switch to Andon dispaly Button -- 4 graphs  (1 year , 1 month , L1 1Month, L2 1Month)

        // graph for 1 year data

        [HttpGet]
        [Route("Reports/BarChartAllCheckListJObForYear")]
        public async Task<IActionResult> BarChartAllCheckListJObForYear()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListJObForYear();

            return Ok(response);
        }


        // graph for 1 month data
        [HttpGet]
        [Route("Reports/BarChartAllCheckListJObForMonth")]
        public async Task<IActionResult> BarChartAllCheckListJObForMonth()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponseTable response = reports.BarChartAllCheckListJObForMonth();

            return Ok(response);
        }


        // graph for 1 month data for Line-1
        [HttpGet]
        [Route("Reports/BarChartAllCheckListJObForMonthLine1")]
        public async Task<IActionResult> BarChartAllCheckListJObForMonthLine1()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListJObForMonthLine1();

            return Ok(response);
        }


        // graph for 1 month data for Line-2
        [HttpGet]
        [Route("Reports/BarChartAllCheckListJObForMonthLine2")]
        public async Task<IActionResult> BarChartAllCheckListJObForMonthLine2()
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
            //calling CheckListJobOperatorDAL busines layer
            CommonResponse response = reports.BarChartAllCheckListJObForMonthLine2();

            return Ok(response);
        }


        #endregion


        #region  change over report
        [HttpPost]
        [Route("Reports/ChangeOverTimeReport")]

        public async Task<IActionResult> ChangeOverTimeReport(COReport data)

        {
            CommonResponse response = new CommonResponse();

            response = reports.ChangeOverTimeReport(data);
            return Ok(response);
        }

        #endregion


    }
}