using MKSH.Common;
using MKSH.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MKSH.Web.Controllers
{
    [RoutePrefix("api/user")]
    public class LoginController : ApiController
    {
        private object authInfo;

        [HttpGet]
        [Route("login")]
        public HttpResponseMessage login(string UserName,string Password)
        {
            var ajaxResult = new AjaxResult();
            ajaxResult.State = "200";
            ajaxResult.Message = "1获取数据成功！";
            //假设用户名为"admin"，密码为"123"
            if (UserName == "admin" && Password == "123")
            {
                //如果用户登录成功，则可以得到该用户的身份数据。当然实际开发中，这里需要在数据库中获得该用户的角色及权限
                AuthInfo authInfo = new AuthInfo
                {
                    IsAdmin = true,
                    Roles = new List<string> { "admin", "owner" },
                    UserName = "admin",
                    timeout = DateTime.Now.AddMinutes(1)
                };
                try
                {
                    //生成token,SecureKey是配置的web.config中，用于加密token的key，打死也不能告诉别人
                    byte[] key = Encoding.Default.GetBytes(ConfigurationManager.AppSettings["SecureKey"]);
                    //采用HS256加密算法
                    string token = JWT.JsonWebToken.Encode(authInfo, key, JWT.JwtHashAlgorithm.HS256);
                    ajaxResult.Token = token;
                    ajaxResult.State = "1";

                }
                catch
                {
                    ajaxResult.State = "0";
                    ajaxResult.Message = "登陆失败";
                }
            }
            else
            {
                ajaxResult.State = "0";
                ajaxResult.Message = "用户名或密码不正确";
            }
            return new HttpResponseMessage { Content = new StringContent(ajaxResult.SerializeJson(), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
