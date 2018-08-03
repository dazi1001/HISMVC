using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNet.Identity;
using MKSH.Common;
using MKSH.Models;
using MKSH.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MKSH.Web.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        [HttpGet]
        [Route("GetToken")]
        public HttpResponseMessage GetToken()
        {
            var ajaxResult = new AjaxResult();
            ajaxResult.State = "200";

            var payload = new Dictionary<string, object>
            {
                { "claim1", 0 },
                { "claim2", "claim2-value" },
                { "userName", "admin" },
                { "roles", "model1,model2,btn1,btn2" },
                { "timeout","2018-08-03"}
            };
     



            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            ajaxResult.Message = token;
            return new HttpResponseMessage { Content = new StringContent(ajaxResult.SerializeJson(), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        [Route("JieMi")]
        public HttpResponseMessage JieMi(string token ) {
            var ajaxResult = new AjaxResult();
            ajaxResult.State = "200";
            ajaxResult.Message = "1获取数据成功！";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);//token为之前生成的字符串
                Console.WriteLine(json);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
            return new HttpResponseMessage { Content = new StringContent(ajaxResult.SerializeJson(), System.Text.Encoding.UTF8, "application/json") };
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpGet]
        [ApiAuthorize]
        [Route("gettask")]
        public HttpResponseMessage gettask()
        {

            var ajaxResult = new AjaxResult();
            ajaxResult.State = "200";
            ajaxResult.Message = "1获取数据成功！";

            return new HttpResponseMessage { Content = new StringContent(ajaxResult.SerializeJson(), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpPost]
        [Route("saveTask")]
        public HttpResponseMessage saveTask()
        {

            var ajaxResult = new AjaxResult();
            ajaxResult.State = "200";
            ajaxResult.Message = "2获取数据成功！222";

            return new HttpResponseMessage { Content = new StringContent(ajaxResult.SerializeJson(), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        [Route("gettask2")]
        public HttpResponseMessage gettask2()
        {

            var ajaxResult = new AjaxResult();
            ajaxResult.State = "200";
            ajaxResult.Message = "2获取数据成功！";

            return new HttpResponseMessage { Content = new StringContent(ajaxResult.SerializeJson(), System.Text.Encoding.UTF8, "application/json") };
        }

    }
}
