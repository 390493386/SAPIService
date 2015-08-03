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
    public enum ServiceStatus
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
        public ServiceStatus Status { get; set; }

        /// <summary>
        /// Http listener
        /// </summary>
        private HttpListener Listener
        {
            get;
            set;
        }

        /// <summary>
        /// Controllers informations
        /// </summary>
        public static Dictionary<string, ControllerReflectionInfo> ControllersInfos;

        /// <summary>
        /// Constructor without arguement
        /// </summary>
        public SapiService()
        {
            _ipAddress = "localhost";
            _port = 8885;
            _fullServiceName = "Web service(" + defaultServiceName + "," + _ipAddress + ":" + _port.ToString() + ")";
            _originHost = null;
            _cookieExpires = defaultCookieExpires;

            Status = ServiceStatus.NotInitialized;
        }

        /// <summary>
        /// Constructor with arguements
        /// </summary>
        /// <param name="ipAddress">IP address on local host</param>
        /// <param name="port">Available port on local host</param>
        /// <param name="serviceName">Service name</param>
        /// <param name="originHost">Cross origin host</param>
        /// <param name="fileServerPath">File server path</param>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="cookieExpires">Cookie expires</param>
        /// <param name="controllersAssembly">Controllers assembly</param>
        public SapiService(string ipAddress, int port, string serviceName = defaultServiceName, string originHost = null,
            string fileServerPath = null, string cookieName = null, int? cookieExpires = null, string controllersAssembly = null)
        {
            _ipAddress = ipAddress;
            _port = port;
            _fullServiceName = "Web service(" + serviceName + "," + _ipAddress + ":" + _port.ToString() + ")";
            _originHost = originHost;
            _fileServerPath = fileServerPath;
            _cookieName = cookieName;
            _cookieExpires = cookieExpires ?? defaultCookieExpires;
            _controllersAssembly = controllersAssembly;

            Status = ServiceStatus.NotInitialized;
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
            Listener = new HttpListener();
            Listener.Prefixes.Add(string.Format("http://{0}:{1}/", _ipAddress, _port.ToString()));
            Status = ServiceStatus.Ready;
            Log.LogCommentM(CommentType.Info, "{0}: Service binded to ip:{1} and port{2}.", _fullServiceName, _ipAddress, _port.ToString());

            try
            {
                Listener.Start();
                Status = ServiceStatus.Running;

                Log.LogCommentM(CommentType.Info, "{0}: initialize controllers informations ...", _fullServiceName);
                Assembly assembly = String.IsNullOrEmpty(_controllersAssembly) ? Assembly.GetCallingAssembly() : Assembly.LoadFrom(_controllersAssembly);

                Log.LogCommentM(CommentType.Info, "{0}: service started,waiting connection ...", _fullServiceName);
            }
            catch (HttpListenerException ex)
            {
                Status = ServiceStatus.NotInitialized;
                Log.LogCommentM(CommentType.Error, "{0}run into an error: " + ex.Message, _fullServiceName);
            }
        }

        /// <summary>
        /// Process service
        /// </summary>
        public void Process()
        {
            if (Status == ServiceStatus.Running)
            {
                while (Status == ServiceStatus.Running)
                {
                    try
                    {
                        HttpListenerContext context = Listener.GetContext();
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
        public void ConcreteProcess(object context)
        {
            if (context != null)
            {
                //SapiRequest<TSession> request = new SapiRequest<TSession>((HttpListenerContext)context, null, null);
            }
        }

        /// <summary>
        /// Stop service
        /// </summary>
        public void Stop()
        {
            Status = ServiceStatus.Stopped;
            Listener.Stop();
            Log.LogCommentM(CommentType.Warn, "{0}: service stoped.", _fullServiceName);
        }
    }
}
