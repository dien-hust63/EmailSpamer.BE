using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Api.Services;
using WebBanHang.Common;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.BL;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Api.Controllers.Authentication
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController: ControllerBase
    {
        private IConfiguration _config;
        IAuthenticationBL _authenBL;

        public AuthenticationController(IConfiguration config, IAuthenticationBL authenBL)
        {
            _config = config;
            _authenBL = authenBL;
        }

        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JWTService(_config);
            var token = jwt.GenerateSecurityToken("fake@email.com");
            return token;
        }

        [HttpPost("login")]
        public ServiceResult loginManagementApplication(LoginParam employee)
        {
           
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // check authentication from db
                bool isValid = _authenBL.checkUserAccount(employee);
                if (!isValid)
                {
                    serviceResult.setError("Email hoặc mật khẩu không chính xác.");
                    return serviceResult;
                }
                //trả về token và quyền
                var jwt = new JWTService(_config);
                var token = jwt.GenerateSecurityToken(employee.email);
                //List<RoleModule> permissionList = new List<RoleModule>();
                //List<RoleModule> permissionDefaultList = new List<RoleModule>();
                //UserInfo userInfo  = new UserInfo();
                //if (token != null)
                //{
                //    permissionList = _authenBL.GetListRoleModuleByUser(employee.email);
                //    userInfo = _authenBL.GetUserInfo(employee.email);
                //}
                //if(userInfo.statusid == (int)AccountStatus.NotActive)
                //{
                //    serviceResult.setError("Tài khoản chưa được kích hoạt hoặc bị ngừng kích hoạt.");
                //    return serviceResult;
                //}
                serviceResult.Data = new LoginInfo 
                { 
                    AccessToken = token,
                    //PermissionList = permissionList,
                    //UserInfo = userInfo
                };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpPost("getPermission")]
        public ServiceResult getPermissionUser(Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult.Data = _authenBL.GetListRoleModuleByUser(employee.email);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpPost("register")]
        public ServiceResult registerManagementApplication(Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _authenBL.registerManagementApplication(employee);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }
    }
}
