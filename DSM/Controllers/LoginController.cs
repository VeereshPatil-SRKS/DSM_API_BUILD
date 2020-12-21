using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSM.DAL.App_Start;
using DSM.DAL.Helpers;
using DSM.DAL.Resource;
using DSM.Interface;
using DSM.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.LoginEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;

        private readonly AppSettings _appSettings;
        private readonly ILogin logins;

        public LoginController(IUserService userService, IOptions<AppSettings> appSettings, ILogin _login)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            logins = _login;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Login/Login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            CommonResponseLogin response = new CommonResponseLogin();
            //calling DepartmentDAL busines layer
            LoginDet responseGet = new LoginDet();
            Security security = new Security();
            string passwordEncrypt = security.Encrypt(password);
            responseGet = logins.ViewLoginDet(userName, passwordEncrypt);

            if (responseGet.isStatus == true)
            {
                response.isStatus = true;
                response.response = responseGet;
                string token = _userService.Authenticate(userName, passwordEncrypt);
                response.token = token;

            }
            else
            {
                response.isStatus = false;
                response.response = ResourceResponse.LoginUnSuccessful;
                response.token = "";
            }

            return Ok(response);
        }
        
        

    }
}