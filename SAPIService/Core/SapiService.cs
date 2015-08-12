using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;

namespace SiweiSoft.SAPIService.Core
{
    /// <summary>
    /// Service status
    /// </summary>
    public enum Status
    {
        NotInitialized,
        Ready,
        Running,
        Stopped
    }

    public class SapiService<TSession> where TSession : Session, new()
    {
        #region private properties

        /// <summary>
        /// Full service name
        /// </summary>
        private string _fullServiceName;

        /// <summary>
        /// Binded IP
        /// </summary>
        private string _ipAddress;

        /// <summary>
        /// Listened port
        /// </summary>
        private int _port;

        /// <summary>
        /// Root path of server, for different site
        /// </summary>
        private string _rootPath;

        /// <summary>
        /// Cross origin host
        /// </summary>
        private string _originHost;

        /// <summary>
        /// File server path
        /// </summary>
        private string _fileServerPath;

        /// <summary>
        /// Cookie name
        /// </summary>
        private string _cookieName;

        /// <summary>
        /// Cookies expires time(seconds)
        /// </summary>
        private int _cookieExpires;

        /// <summary>
        /// Controllers assembly(full name)
        /// </summary>
        private string _controllersAssembly;

        /// <summary>
        /// Http listener
        /// </summary>
        private HttpListener listener;

        /// <summary>
        /// Server configurations
        /// </summary>
        private Dictionary<string, object> _serverConfig;

        /// <summary>
        /// Controllers informations
        /// </summary>
        private Dictionary<string, ControllerReflectionInfo> controllersInfos;

        /// <summary>
        /// Sessions dictionary
        /// </summary>
        private Dictionary<string, TSession> sessionsDictionary;

        /// <summary>
        /// Default service name
        /// </summary>
        private const string defaultServiceName = "WebService";

        /// <summary>
        /// Default cookie expires(seconds)
        /// </summary>
        private const int defaultCookieExpires = 3600;

        #endregion private properties

        /// <summary>
        /// Service status
        /// </summary>
        public Status Status { get; private set; }

        /// <summary>
        /// Constructor without arguement
        /// </summary>
        public SapiService()
        {
            _ipAddress = "localhost";
            _port = 8885;
            _fullServiceName = "Web service(" + defaultServiceName + "," + _ipAddress + ":" + _port.ToString() + ")";
            _originHost = "*";
            _cookieExpires = defaultCookieExpires;

            Status = Status.NotInitialized;
        }

        /// <summary>
        /// Constructor with arguements
        /// </summary>
        /// <param name="ipAddress">IP address on local host</param>
        /// <param name="port">Available port on local host</param>
        /// <param name="rootPath">Root path, for different web root path</param>
        /// <param name="serviceName">Service name</param>
        /// <param name="originHost">Cross origin host</param>
        /// <param name="fileServerPath">File server path</param>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="cookieExpires">Cookie expires</param>
        /// <param name="controllersAssembly">Controllers assembly</param>
        /// <param name="serverConfig">Server configurations</param>
        public SapiService(string ipAddress, int port, string rootPath = null,
            string serviceName = defaultServiceName, string originHost = "*",
            string fileServerPath = null, string cookieName = null,
            int? cookieExpires = null, string controllersAssembly = null,
            Dictionary<string, object> serverConfig = null)
        {
            _ipAddress = ipAddress;
            _port = port;
            _rootPath = rootPath;
            _fullServiceName = "Web service(" + serviceName + "," + _ipAddress + ":" + _port.ToString() + ")";
            _originHost = originHost;
            _fileServerPath = fileServerPath;
            _cookieName = cookieName;
            _cookieExpires = cookieExpires ?? defaultCookieExpires;
            _controllersAssembly = controllersAssembly;
            _serverConfig = serverConfig;

            Status = Status.NotInitialized;
        }

        /// <summary>
        /// Initialize web service
        /// </summary>
        public void Initialize()
        {
            if (String.IsNullOrEmpty(_ipAddress) || _port < 1)
            {
                Log.LogCommentM(CommentType.Error, "{0}: service configurations error.", _fullServiceName);
                return;
            }
            Log.LogCommentM(CommentType.Info, "{0}: Initializing service ...", _fullServiceName);
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format("http://{0}:{1}/", _ipAddress, _port.ToString()));
            Status = Status.Ready;
            Log.LogCommentM(CommentType.Info, "{0}: Service binded to ip:{1} and port{2}.", _fullServiceName, _ipAddress, _port.ToString());

            try
            {
                listener.Start();
                Status = Status.Running;

                Log.LogCommentM(CommentType.Info, "{0}: initialize sessions dictionary ...", _fullServiceName);
                sessionsDictionary = new Dictionary<string, TSession>();

                Log.LogCommentM(CommentType.Info, "{0}: initialize controllers informations ...", _fullServiceName);
                Assembly assembly = String.IsNullOrEmpty(_controllersAssembly) ? Assembly.GetCallingAssembly() : Assembly.LoadFrom(_controllersAssembly);
                if (assembly != null)
                {
                    controllersInfos = new Dictionary<string, ControllerReflectionInfo>();
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.Name.Length > 10 && type.Name.EndsWith("Controller"))
                        {
                            string key = type.Name.Replace("Controller", null).ToUpper();
                            if (controllersInfos.ContainsKey(key))
                                Log.LogCommentM(CommentType.Warn, "{0}: duplicated key of controller：{1}, may cause confliction！", _fullServiceName, key);
                            else
                                controllersInfos.Add(key, new ControllerReflectionInfo(type));
                        }
                    }
                }

                Log.LogCommentM(CommentType.Info, "{0}: service started, waiting connection ...", _fullServiceName);
            }
            catch (HttpListenerException ex)
            {
                Status = Status.NotInitialized;
                Log.LogCommentM(CommentType.Error, "{0}run into an error: " + ex.Message, _fullServiceName);
            }
        }

        /// <summary>
        /// Process service
        /// </summary>
        public void Process()
        {
            if (Status == Status.Running)
            {
                while (Status == Status.Running)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        ThreadPool.QueueUserWorkItem(new WaitCallback(this.ConcreteProcess), context);
                    }
                    catch (HttpListenerException)
                    {
                        Log.LogCommentC(CommentType.Warn, "{0}: service threads stoped.", _fullServiceName);
                    }
                }
            }
            else
            {
                Log.LogCommentC(CommentType.Error, "{0}: service was not initialized or not initialized successfully.", _fullServiceName);
            }
        }

        /// <summary>
        /// Concrete process
        /// </summary>
        /// <param name="context"></param>
        private void ConcreteProcess(object context)
        {
            if (context != null)
            {
                HttpListenerContext requestContext = (HttpListenerContext)context;

                if (requestContext.Request.RawUrl == "/favicon.ico")//Request for the icon
                {
                    //TODO: handle the request for the icon
                    requestContext.Response.OutputStream.Close();
                }
                else
                {
                    TSession session = null;
                    if (!String.IsNullOrEmpty(_cookieName))
                    {
                        //Get the cookie from the request
                        Cookie cookie = requestContext.Request.Cookies[_cookieName];
                        if (cookie == null)
                        {
                            session = GenerateNewSession(requestContext);
                        }
                        else
                        {
                            string cookieString = cookie.Value;
                            if (sessionsDictionary.ContainsKey(cookieString))
                                session = sessionsDictionary[cookieString];
                            else
                                session = this.GenerateNewSession(requestContext, expires: cookie.Expires);
                        }
                    }
                    SapiRequest request = new SapiRequest(requestContext, session, controllersInfos, _originHost, _serverConfig);
                    request.Response();
                }
            }
        }

        /// <summary>
        /// Stop service
        /// </summary>
        public void Stop()
        {
            Status = Status.Stopped;
            listener.Stop();
            Log.LogCommentM(CommentType.Warn, "{0}: service stoped.", _fullServiceName);
        }

        /// <summary>
        /// Generate a new session
        /// </summary>
        /// <returns></returns>
        private TSession GenerateNewSession(HttpListenerContext context, DateTime? expires = null)
        {
            string cookieString = Guid.NewGuid().ToString();
            Cookie cookie = new Cookie(_cookieName, cookieString, "/")
            {
                Expires = expires ?? DateTime.Now.AddSeconds(_cookieExpires)
            };
            context.Response.SetCookie(cookie);
            TSession session = new TSession()
            {
                IsAuthorized = false
            };
            session.ResetExpireDate(_cookieExpires);
            sessionsDictionary.Add(cookieString, session);
            return session;
        }
    }
}
