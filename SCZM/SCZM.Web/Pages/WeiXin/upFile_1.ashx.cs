using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCZM.Web.Pages.WeiXin
{
    /// <summary>
    /// upFile_1 的摘要说明
    /// </summary>
    public class upFile_1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}