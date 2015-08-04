using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace SiweiSoft.SAPIService.Core
{
    public class SapiRequest
    {
        /// <summary>
        /// Http request
        /// </summary>
        private HttpListenerContext _context;

        /// <summary>
        /// Origin host, for cross origin
        /// </summary>
        private string _originHost;

        /// <summary>
        /// Other configurations
        /// </summary>
        private Dictionary<string, object> _config { get; set; }

        /// <summary>
        /// Current session
        /// </summary>
        private Session _session { get; set; }

        /// <summary>
        /// Controllers informations
        /// </summary>
        private Dictionary<string, ControllerReflectionInfo> _controllersInfos;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="session"></param>
        /// <param name="config"></param>
        /// <param name="originHost"></param>
        public SapiRequest(HttpListenerContext requestContext, Session session, 
            Dictionary<string, ControllerReflectionInfo> controllersInfos,
            Dictionary<string, object> config = null, string originHost = null)
        {
            _context = requestContext;
            _session = session;
            _controllersInfos = controllersInfos;
            _config = config;
            _originHost = originHost;
        }

        /// <summary>
        /// Response request
        /// </summary>
        public void Response()
        {
            if (_context != null)
            {
                _context.Response.Headers.Add("Access-Control-Allow-Credentials: true");
                _originHost = String.IsNullOrEmpty(_originHost) ? "*" : _originHost;
                _context.Response.Headers.Add("Access-Control-Allow-Origin: " + _originHost);    //For the cross origin
                try
                {
                    ResponseGet();
                }
                catch (Exception ex)
                {
                    _context.Response.StatusCode = 500;
                    _context.Response.Close();
                    Log.LogCommentC(CommentType.Error, "Unknow exception: " + ex.Message);
                }
            }
        }

        private void ResponseGet()
        {
            ActionResult actionResult = GetResponse();
            if (actionResult == null)
            {
                _context.Response.StatusCode = 404;
            }
            else
            {
                if (actionResult.Headers != null)  //添加请求的头部
                {
                    foreach (string head in actionResult.Headers)
                    {
                        _context.Response.Headers.Add(head);
                    }
                }
                if (actionResult.FileStream != null)  //下载文件请求
                {
                    int receivedLength = 0;
                    byte[] buffer = new byte[10240];
                    do
                    {
                        receivedLength = actionResult.FileStream.Read(buffer, 0, buffer.Length);
                        _context.Response.OutputStream.Write(buffer, 0, receivedLength);
                    }
                    while (receivedLength > 0);
                    actionResult.FileStream.Flush();
                    actionResult.FileStream.Close();
                    _context.Response.StatusCode = 200;
                }
                else if (actionResult.Result != null)  //数据请求
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(actionResult.Result));
                    _context.Response.ContentLength64 = buffer.Length;
                    _context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    _context.Response.StatusCode = 200;
                }

            }
            _context.Response.OutputStream.Close();
        }

        private ActionResult GetResponse()
        {
            ControllerReflectionInfo controllerInfo = _controllersInfos.ContainsKey("USER") ? _controllersInfos["USER"] : null;
            var action = controllerInfo.GetMethodInfoByAlias("GET");
            var controller = ((Controller)controllerInfo.ControllerInstance).Clone();
            controller.Parameters = new Dictionary<string, object>();
            return (ActionResult)action.Invoke(controllerInfo.ControllerInstance, null);
            //return null;
        }
    }
}
