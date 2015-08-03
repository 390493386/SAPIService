using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace SiweiSoft.SAPIService.Core
{
    public class SapiRequest<TSession> where TSession : Session
    {
        private ResponseBodyContext<TSession> responseContext;

        /// <summary>
        /// Http request
        /// </summary>
        private HttpListenerContext context;

        /// <summary>
        /// Other configurations
        /// </summary>
        public Dictionary<string, object> Config { get; set; }

        /// <summary>
        /// Current session
        /// </summary>
        public TSession Session { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="configuration"></param>
        /// <param name="session"></param>
        public SapiRequest(HttpListenerContext requestContext, Dictionary<string, object> configuration, TSession session)
        {
            context = requestContext;
            Config = configuration;
            Session = session;
        }

        /// <summary>
        /// Response request
        /// </summary>
        public void Response()
        {
            try
            {
                context.Response.Headers.Add("Access-Control-Allow-Credentials: true");
                context.Response.Headers.Add("Access-Control-Allow-Origin: " + Config["Origin"]); //允许跨源访问
                ResponseGet();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Close();
                Log.LogCommentC(CommentType.Error, "Unknow exception: " + ex.Message);
            }
        }

        private void ResponseGet()
        {
            responseContext = new ResponseBodyContext<TSession>(Config, context, Session);
            ActionResult actionResult = responseContext.GetResponseBody();
            if (actionResult == null)
            {
                context.Response.StatusCode = 404;
            }
            else
            {
                if (actionResult.Headers != null)  //添加请求的头部
                {
                    foreach (string head in actionResult.Headers)
                    {
                        context.Response.Headers.Add(head);
                    }
                }
                if (actionResult.FileStream != null)  //下载文件请求
                {
                    int receivedLength = 0;
                    byte[] buffer = new byte[10240];
                    do
                    {
                        receivedLength = actionResult.FileStream.Read(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Write(buffer, 0, receivedLength);
                    }
                    while (receivedLength > 0);
                    actionResult.FileStream.Flush();
                    actionResult.FileStream.Close();
                    context.Response.StatusCode = 200;
                }
                else if (actionResult.Result != null)  //数据请求
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(actionResult.Result));
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = 200;
                }

            }
            context.Response.OutputStream.Close();
        }
    }
}
