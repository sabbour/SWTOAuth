using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace SWTOAuth.Server
{
    public class CleanExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is HttpException)
            {
                var ex = context.Exception as HttpException;
                HttpStatusCode code;
                switch(ex.GetHttpCode()){
                    case 401: code = HttpStatusCode.Unauthorized; break;
                    case 403: code = HttpStatusCode.Forbidden; break;
                    case 404: code = HttpStatusCode.NotFound; break;
                    default: code = HttpStatusCode.InternalServerError; break;
                }
                context.Response = new HttpResponseMessage(code) { Content = new StringContent(ex.Message) };
            }
        }
    }
}