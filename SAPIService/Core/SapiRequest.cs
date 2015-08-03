using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
namespace SiweiSoft.SAPIService.Core
{
    public class SapiRequest<TSession> where TSession : Session, new()
    {
        /// <summary>
        /// Sessions dictionary
        /// </summary>
        private static Dictionary<string, TSession> SessionsDictionary;

        ///// <summary>
        ///// 
        ///// </summary>
        //private ResponseBodyContext<TSession> responseContext;

        /// <summary>
        /// Http request
        /// </summary>
        private HttpListenerContext _context;

        /// <summary>
        /// Origin host, for cross origin
        /// </summary>
        private string _originHost;

        /// <summary>
        /// Cookie name
        /// </summary>
        private string _cookieName;

        /// <summary>
        /// Cookie expires
        /// </summary>
        private int _cookieExpires;

        /// <summary>
        /// Other configurations
        /// </summary>
        public Dictionary<string, object> Config { get; set; }

        /// <summary>
        /// Current session
        /// </summary>
        private TSession session { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="configuration"></param>
        /// <param name="session"></param>
        public SapiRequest(HttpListenerContext requestContext, string cookieName, string originHost
            , Dictionary<string, object> configuration)
        {
            _context = requestContext;
            _cookieName = cookieName;
            Config = configuration;
            if (SessionsDictionary == null)
                SessionsDictionary = new Dictionary<string, TSession>();
        }

        /// <summary>
        /// Response request
        /// </summary>
        public void Response()
        {
            if (_context != null)
            {
                if (_context.Request.RawUrl == "/favicon.ico")//浏览器会发送获取图标的请求
                {
                    _context.Response.OutputStream.Close();
                    return;
                }
                Cookie cookie = _context.Request.Cookies[_cookieName]; //获取用户请求中的cookie信息
                if (cookie == null)
                {
                    session = this.GenerateNewSession();
                }
                else
                {
                    string cookieString = cookie.Value;
                    if (!String.IsNullOrEmpty(cookieString) && SessionsDictionary.ContainsKey(cookieString))
                        session = SessionsDictionary[cookieString];
                    else
                        session = this.GenerateNewSession();
                }

                _context.Response.Headers.Add("Access-Control-Allow-Credentials: true");
                //For cross origin
                _originHost = String.IsNullOrEmpty(_originHost) ? "*" : _originHost;
                _context.Response.Headers.Add("Access-Control-Allow-Origin: " + _originHost);
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
            ResponseBodyContext<TSession> responseContext = new ResponseBodyContext<TSession>(Config, _context, session);
            ActionResult actionResult = responseContext.GetResponseBody();
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

        /// <summary>
        /// 生成一个新的Session
        /// </summary>
        /// <returns></returns>
        public TSession GenerateNewSession()
        {
            string cookieString = Guid.NewGuid().ToString();
            Cookie cookie = new Cookie(_cookieName, cookieString, "/")
            {
                Expires = DateTime.Now.AddSeconds(_cookieExpires)
            };
            _context.Response.SetCookie(cookie);
            TSession session = new TSession()
            {
                IsAuthorized = false
            };
            session.ResetExpireDate(_cookieExpires);
            SessionsDictionary.Add(cookieString, session);
            return session;
        }
    }
}
