using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace SiweiSoft.SAPIServer
{
    public partial class SAPIServer : Form
    {
        private Dictionary<string, string> configuration;
        private SapiService serviceInstance;
        private Thread serviceThread;

        public SAPIServer()
        {
            InitializeComponent();
            RefreshSystemConfiguration();

            //打印日志事件
            Log.LogEvent += LogComment;
        }

        private void SvcStartStop_Click(object sender, EventArgs e)
        {
            if (serviceInstance != null && serviceInstance.Status == Status.Running)
                serviceInstance.Stop();
            else
            {
                this.RefreshSystemConfiguration();
                if (configuration.ContainsKey("ServerIP") && configuration.ContainsKey("ServerPort"))
                    serviceInstance = new SapiService(configuration["ServerIP"],
                        Convert.ToInt32(configuration["ServerPort"]),
                        originHost: configuration.ContainsKey("OriginHost") && configuration["OriginHost"] != "" ? configuration["OriginHost"] : "*",
                        fileServerPath: configuration.ContainsKey("FileSavedPath") && configuration["FileSavedPath"] != "" ? configuration["FileSavedPath"] : null,
                        cookieName: configuration.ContainsKey("CookieName") && configuration["CookieName"] != "" ? configuration["CookieName"] : null,
                        cookieExpires: configuration.ContainsKey("CookieExpiredTime") && configuration["CookieExpiredTime"] != "" ? Convert.ToInt32(configuration["CookieExpiredTime"]) : 3600);
                serviceInstance.Initialize();
                if (serviceInstance.Status == Status.NotInitialized)
                {
                    Log.Comment(CommentType.Error, "服务启动失败。");
                    return;
                }
                serviceThread = new Thread(serviceInstance.Process<Session>);
                serviceThread.Start();
                Log.Comment(CommentType.Info, string.Format("服务正在运行。"));
            }
        }

        private void LogCommentM(CommentType commentType, string comment)
        {
            string mark = null;
            ItemType itemType = ItemType.Error;
            if (commentType == CommentType.Info)
            {
                mark = "消息";
                itemType = ItemType.Info;
            }
            else if (commentType == CommentType.Warn)
            {
                mark = "警告";
                itemType = ItemType.Warn;
            }
            else if (commentType == CommentType.Error)
            {
                mark = "错误";
                itemType = ItemType.Error;
            }

            string message = String.Format("{0} [{1}] {2}", DateTime.Now.ToString(), mark, comment);
            SListBoxItem item = new SListBoxItem(message, itemType);

            //添加滚动效果，在添加记录前，先计算滚动条是否在底部，从而决定添加后是否自动滚动
            bool scoll = false;
            if (logsBox.TopIndex == logsBox.Items.Count - (int)(logsBox.Height / logsBox.ItemHeight))
                scoll = true;
            //添加记录
            logsBox.Items.Add(item);
            //滚动到底部
            if (scoll)
                logsBox.TopIndex = logsBox.Items.Count - (int)(logsBox.Height / logsBox.ItemHeight);
        }

        /// <summary>
        /// 子线程中调用的打印日志的方法
        /// </summary>
        /// <param name="type">信息类型</param>
        /// <param name="message">信息</param>
        private void LogComment(CommentType type, string message)
        {
            this.Invoke(new Action<CommentType, string>(this.LogCommentM), type, message);
        }

        private void RefreshSystemConfiguration()
        {
            configuration = new Dictionary<string, string>();
            ConfigurationManager.RefreshSection("appSettings");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                configuration.Add(key, ConfigurationManager.AppSettings[key]);
            }
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            string svcSelfStart = selfStartService.Checked ? "1" : "0";
            string serverIP = localIPAddress.Text;
            string serverPort = localPort.Text;
            string root = rootPath.Text;
            string svcName = serviceName.Text;
            string ohost = originHost.Text;
            string filePath = fileSvPath.Text;
            string cName = cookieName.Text;
            string cExpires = cookieExpires.Text;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["SvcSelfStart"].Value = svcSelfStart;
            config.AppSettings.Settings["ServerIP"].Value = serverIP;
            config.AppSettings.Settings["ServerPort"].Value = serverPort;
            config.AppSettings.Settings["ServiceRoot"].Value = root;
            config.AppSettings.Settings["ServiceName"].Value = svcName;
            config.AppSettings.Settings["OriginHost"].Value = ohost;
            config.AppSettings.Settings["FileSavedPath"].Value = filePath;
            config.AppSettings.Settings["CookieName"].Value = cName;
            config.AppSettings.Settings["CookieExpiredTime"].Value = cExpires;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
