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
                    ActionResult actionResult = null;

                    string requestMethod = _context.Request.HttpMethod.ToUpper();
                    if (requestMethod == "GET")
                        ResponseGet();
                    else if (requestMethod == "POST")
                        ResponseGet();

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
                }
                catch (Exception ex)
                {
                    _context.Response.StatusCode = 500;
                    Log.LogCommentC(CommentType.Error, "Unknow exception: " + ex.Message);
                }
                finally
                {
                    _context.Response.Close();
                }
            }
        }

        private ActionResult ResponseGet()
        {
            ActionResult actionResult = GetResponse();
            return actionResult;
        }

        private ActionResult GetResponse()
        {
            ActionResult actionResult = null;

            ActionInfo actionInfo;
            Controller controllerInstance = InitializeControllerInstance(out actionInfo);
            if (controllerInstance == null || actionInfo == null)
                Log.LogCommentC(CommentType.Error, "Raw url is not in correct format(correct format: /SAPI/ControllerName/ActionName).");
            else
            {
                if (!_session.IsAuthorized && actionInfo.NeedAuthorize)
                    actionResult = new ActionNotAuthorized();
                else
                    actionResult = (ActionResult)actionInfo.Action.Invoke(controllerInstance, null);
            }
            return actionResult;
        }

        /// <summary>
        /// Initialize controller instance
        /// </summary>
        /// <param name="actionInfo">Action info</param>
        /// <returns></returns>
        private Controller InitializeControllerInstance(out ActionInfo actionInfo)
        {
            Controller controller = null;
            actionInfo = null;

            //Get root name, controller name, and action name from request
            string[] urlParts = (_context.Request.RawUrl.Split('?'))[0].Split('/');
            if (urlParts.Length == 4 && urlParts[1] == "SAPI")
            {
                string controllerName = urlParts[2];
                string actionName = urlParts[3];

                ControllerReflectionInfo controllerInfo = _controllersInfos.ContainsKey(controllerName) ? _controllersInfos[controllerName] : null;
                if (controllerInfo != null)
                {
                    actionInfo = controllerInfo != null ? controllerInfo.GetMethodInfoByAlias(actionName) : null;
                    controller = ((Controller)controllerInfo.ControllerInstance).Clone();
                }
            }
            else
                Log.LogCommentC(CommentType.Error, "Raw url is not in correct format(correct format: /SAPI/ControllerName/ActionName).");
            return controller;
        }
    }
}
