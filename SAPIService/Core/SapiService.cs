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

    public class SapiService
    {
        #region private fields

        /// <summary>
        /// Binded IP
        /// </summary>
        private string ipAddress;

        /// <summary>
        /// Listened port
        /// </summary>
        private int port;

        /// <summary>
        /// Cross origin host
        /// </summary>
        private string originHost;

        /// <summary>
        /// With cross origin?
        /// </summary>
        private bool withCrossOrigin;

        /// <summary>
        /// File server path
        /// </summary>
        private string fileServerPath;

        /// <summary>
        /// Cookie name
        /// </summary>
        private string cookieName;

        /// <summary>
        /// With cookie?
        /// </summary>
        private bool withCookie;

        /// <summary>
        /// Cookies expires time(seconds)
        /// </summary>
        private int cookieExpires;

        /// <summary>
        /// Controllers assembly(full name)
        /// </summary>
        private string controllersAssembly;

        /// <summary>
        /// Http listener
        /// </summary>
        private HttpListener listener;

        #endregion private fields

        #region internal static fields

        /// <summary>
        /// Root path of server, for different site
        /// </summary>
        internal static string RootPath;

        /// <summary>
        /// Server configurations
        /// </summary>
        internal static Dictionary<string, object> ServerConfigs;

        /// <summary>
        /// Controllers informations
        /// </summary>
        internal static Dictionary<string, ControllerReflectionInfo> ControllersInfos;

        /// <summary>
        /// Sessions dictionary
        /// </summary>
        internal static Dictionary<string, Session> SessionsDictionary;

        #endregion internal static fields

        #region private const variable, for default values

        /// <summary>
        /// Default cookie expires(seconds)
        /// </summary>
        private const int defaultCookieExpires = 3600;

        #endregion private const variable, for default values

        #region public properties

        /// <summary>
        /// Service status
        /// </summary>
        public Status Status { get; private set; }

        #endregion public properties

        /// <summary>
        /// Constructor without arguement
        /// </summary>
        public SapiService()
        {
            ipAddress = "localhost";
            port = 8885;
            withCrossOrigin = false;
            withCookie = false;

            Status = Status.NotInitialized;
        }

        /// <summary>
        /// Constructor with arguements
        /// </summary>
        /// <param name="ipAddress">IP address on local host</param>
        /// <param name="port">Available port on local host</param>
        /// <param name="RootPath">Root path, for different web root path</param>
        /// <param name="originHost">Cross origin host</param>
        /// <param name="fileServerPath">File server path</param>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="cookieExpires">Cookie expires</param>
        /// <param name="controllersAssembly">Controllers assembly</param>
        /// <param name="serverConfig">Server configurations</param>
        public SapiService(string ipAddress, int port,
            string rootPath = null, string originHost = null,
            string fileServerPath = null, string cookieName = null,
            int? cookieExpires = null, string controllersAssembly = null,
            Dictionary<string, object> serverConfig = null)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            RootPath = rootPath;
            this.originHost = originHost;
            if (!String.IsNullOrEmpty(originHost))
                withCrossOrigin = true;
            this.fileServerPath = fileServerPath;
            this.cookieName = cookieName;
            if (!String.IsNullOrEmpty(cookieName))
                withCookie = true;
            this.cookieExpires = cookieExpires ?? defaultCookieExpires;
            this.controllersAssembly = controllersAssembly;
            ServerConfigs = serverConfig;

            Status = Status.NotInitialized;
        }

        /// <summary>
        /// Initialize web service
        /// </summary>
        public void Initialize()
        {
            if (String.IsNullOrEmpty(ipAddress) || port < 1)
            {
                Log.Comment(CommentType.Error, "Service configurations error.");
                return;
            }
            Log.Comment(CommentType.Info, "Initializing service ...");
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format("http://{0}:{1}/" +
                (String.IsNullOrEmpty(RootPath) ? null : (RootPath + "/")), ipAddress, port.ToString()));
            Status = Status.Ready;
            Log.Comment(CommentType.Info, "Service binded to ip:{0} and port {1}.", ipAddress, port.ToString());

            try
            {
                listener.Start();
                Status = Status.Running;

                Log.Comment(CommentType.Info, "Initialize sessions dictionary ...");
                SessionsDictionary = new Dictionary<string, Session>();

                Log.Comment(CommentType.Info, "Initialize controllers informations ...");
                Assembly assembly = String.IsNullOrEmpty(controllersAssembly) ? Assembly.GetCallingAssembly() : Assembly.LoadFrom(controllersAssembly);
                if (assembly != null)
                {
                    ControllersInfos = new Dictionary<string, ControllerReflectionInfo>();
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.Name.Length > 10 && type.Name.EndsWith("Controller"))
                        {
                            string key = type.Name.Replace("Controller", null).ToUpper();
                            if (ControllersInfos.ContainsKey(key))
                                Log.Comment(CommentType.Warn, "Duplicated key of controller：{0}, may cause confliction！", key);
                            else
                                ControllersInfos.Add(key, new ControllerReflectionInfo(type));
                        }
                    }
                }
                Log.Comment(CommentType.Info, "With cross origin: " + (withCrossOrigin ? "yes." : "no."));
                Log.Comment(CommentType.Info, "With cookie: " + (withCookie ? "yes." : "no."));
                Log.Comment(CommentType.Info, "Service started, waiting connection ...");
            }
            catch (HttpListenerException ex)
            {
                Status = Status.NotInitialized;
                Log.Comment(CommentType.Error, "Service run into an error: " + ex.Message);
            }
        }

        /// <summary>
        /// Process service
        /// </summary>
        public void Process<TSession>() where TSession : Session, new()
        {
            if (Status == Status.Running)
            {
                while (Status == Status.Running)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        if (context != null)
                            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ConcreteProcess<TSession>), context);
                    }
                    catch (HttpListenerException)
                    {
                        Log.Comment(CommentType.Warn, "Service threads stoped.");
                    }
                }
            }
            else
            {
                Log.Comment(CommentType.Error, "Service was not initialized or not initialized successfully.");
            }
        }

        /// <summary>
        /// Concrete process
        /// </summary>
        /// <param name="context"></param>
        private void ConcreteProcess<TSession>(object context) where TSession : Session, new()
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
                if (withCookie)
                {
                    //Set cros options
                    if (withCrossOrigin)
                        requestContext.Response.Headers.Add("Access-Control-Allow-Credentials: true");

                    //Get the cookie from the request
                    Cookie cookie = requestContext.Request.Cookies[cookieName];
                    if (cookie == null)
                    {
                        session = GenerateNewSession<TSession>(requestContext);
                    }
                    else
                    {
                        string cookieString = cookie.Value;
                        if (SessionsDictionary.ContainsKey(cookieString))
                            session = (TSession)SessionsDictionary[cookieString];
                        else
                            session = this.GenerateNewSession<TSession>(requestContext, expires: cookie.Expires);
                    }
                }
                if (withCrossOrigin)
                    requestContext.Response.Headers.Add("Access-Control-Allow-Origin: " + originHost);

                SapiRequest request = new SapiRequest(requestContext, session);
                request.Response();
            }
        }

        /// <summary>
        /// Stop service
        /// </summary>
        public void Stop()
        {
            Status = Status.Stopped;
            listener.Stop();
            Log.Comment(CommentType.Warn, "Service stoped.");
        }

        /// <summary>
        /// Generate a new session
        /// </summary>
        /// <returns></returns>
        private TSession GenerateNewSession<TSession>(HttpListenerContext context, DateTime? expires = null) 
            where TSession : Session, new()
        {
            string cookieString = Guid.NewGuid().ToString();
            Cookie cookie = new Cookie(cookieName, cookieString, "/")
            {
                Expires = expires ?? DateTime.Now.AddSeconds(cookieExpires)
            };
            context.Response.SetCookie(cookie);
            TSession session = new TSession()
            {
                IsAuthorized = false
            };
            session.ResetExpireDate(cookieExpires);
            SessionsDictionary.Add(cookieString, session);
            return session;
        }
    }
}
