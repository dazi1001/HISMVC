using MKSH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MKSH.Web.App_Start
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //前端请求api时会将token存放在名为"auth"的请求头中
            var authHeader = from h in actionContext.Request.Headers where h.Key == "auth" select h.Value.FirstOrDefault();
            if (authHeader != null)
            {
                string token = authHeader.FirstOrDefault();
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        //对token进行解密
                        string secureKey = System.Configuration.ConfigurationManager.AppSettings["SecureKey"];
                        AuthInfo authInfo = JWT.JsonWebToken.DecodeToObject<AuthInfo>(token, secureKey);
                        if (authInfo != null)
                        {
                            //将用户信息存放起来，供后续调用
                            actionContext.RequestContext.RouteData.Values.Add("auth", authInfo);
                            if (authInfo.timeout < DateTime.Now)
                            {
                                return false;
                            }
                            else {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return false;
        }

        
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            var response = filterContext.Response = filterContext.Response ?? new HttpResponseMessage();
            string msg = " 服务端拒绝访问：你没有权限！";
            AuthInfo obj = null;
            if (filterContext.RequestContext.RouteData.Values.Count >0)
            {
                obj = (AuthInfo)filterContext.RequestContext.RouteData.Values["auth"];
            }
            if (obj!=null && obj.timeout < DateTime.Now)
            {
                msg = "服务端拒绝访问：已超时！";
            }
            var content = new AjaxResult
            {
                State = "0",
                Message = msg
            };
            response.Content = new StringContent(Json.Encode(content), Encoding.UTF8, "application/json");
        }
     
    }
}