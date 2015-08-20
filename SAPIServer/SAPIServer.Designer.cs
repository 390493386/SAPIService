namespace SiweiSoft.SAPIServer
{
    partial class SAPIServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverTab = new System.Windows.Forms.TabControl();
            this.servicePage = new System.Windows.Forms.TabPage();
            this.SvcStartStop = new System.Windows.Forms.Button();
            this.configPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.expire = new System.Windows.Forms.Label();
            this.cookie = new System.Windows.Forms.Label();
            this.SaveConfig = new System.Windows.Forms.Button();
            this.svcConfig = new System.Windows.Forms.GroupBox();
            this.path = new System.Windows.Forms.Label();
            this.oHost = new System.Windows.Forms.Label();
            this.serviceName = new System.Windows.Forms.TextBox();
            this.svc = new System.Windows.Forms.Label();
            this.rootPath = new System.Windows.Forms.TextBox();
            this.root = new System.Windows.Forms.Label();
            this.localPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.localhost = new System.Windows.Forms.Label();
            this.svConfig = new System.Windows.Forms.GroupBox();
            this.selfStartService = new System.Windows.Forms.CheckBox();
            this.serverConfig = new System.Windows.Forms.Label();
            this.selfStartServer = new System.Windows.Forms.CheckBox();
            this.originHost = new System.Windows.Forms.TextBox();
            this.fileSvPath = new System.Windows.Forms.TextBox();
            this.cookieName = new System.Windows.Forms.TextBox();
            this.cookieExpires = new System.Windows.Forms.TextBox();
            this.logsBox = new SiweiSoft.SAPIServer.SListBox();
            this.localIPAddress = new System.Windows.Forms.TextBox();
            this.serverTab.SuspendLayout();
            this.servicePage.SuspendLayout();
            this.configPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.svcConfig.SuspendLayout();
            this.svConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverTab
            // 
            this.serverTab.Controls.Add(this.servicePage);
            this.serverTab.Controls.Add(this.configPage);
            this.serverTab.Location = new System.Drawing.Point(12, 12);
            this.serverTab.Name = "serverTab";
            this.serverTab.SelectedIndex = 0;
            this.serverTab.Size = new System.Drawing.Size(641, 449);
            this.serverTab.TabIndex = 0;
            // 
            // servicePage
            // 
            this.servicePage.Controls.Add(this.SvcStartStop);
            this.servicePage.Controls.Add(this.logsBox);
            this.servicePage.Location = new System.Drawing.Point(4, 22);
            this.servicePage.Name = "servicePage";
            this.servicePage.Padding = new System.Windows.Forms.Padding(3);
            this.servicePage.Size = new System.Drawing.Size(633, 423);
            this.servicePage.TabIndex = 0;
            this.servicePage.Text = "SAPI服务";
            this.servicePage.UseVisualStyleBackColor = true;
            // 
            // SvcStartStop
            // 
            this.SvcStartStop.Location = new System.Drawing.Point(6, 389);
            this.SvcStartStop.Name = "SvcStartStop";
            this.SvcStartStop.Size = new System.Drawing.Size(135, 23);
            this.SvcStartStop.TabIndex = 1;
            this.SvcStartStop.Text = "服务：开始/停止";
            this.SvcStartStop.UseVisualStyleBackColor = true;
            this.SvcStartStop.Click += new System.EventHandler(this.SvcStartStop_Click);
            // 
            // configPage
            // 
            this.configPage.Controls.Add(this.groupBox1);
            this.configPage.Controls.Add(this.SaveConfig);
            this.configPage.Controls.Add(this.svcConfig);
            this.configPage.Controls.Add(this.svConfig);
            this.configPage.Location = new System.Drawing.Point(4, 22);
            this.configPage.Name = "configPage";
            this.configPage.Padding = new System.Windows.Forms.Padding(3);
            this.configPage.Size = new System.Drawing.Size(633, 423);
            this.configPage.TabIndex = 1;
            this.configPage.Text = "系统配置";
            this.configPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cookieExpires);
            this.groupBox1.Controls.Add(this.cookieName);
            this.groupBox1.Controls.Add(this.expire);
            this.groupBox1.Controls.Add(this.cookie);
            this.groupBox1.Location = new System.Drawing.Point(7, 295);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // expire
            // 
            this.expire.AutoSize = true;
            this.expire.Location = new System.Drawing.Point(24, 54);
            this.expire.Name = "expire";
            this.expire.Size = new System.Drawing.Size(65, 12);
            this.expire.TabIndex = 13;
            this.expire.Text = "过期时间：";
            // 
            // cookie
            // 
            this.cookie.AutoSize = true;
            this.cookie.Location = new System.Drawing.Point(12, 25);
            this.cookie.Name = "cookie";
            this.cookie.Size = new System.Drawing.Size(77, 12);
            this.cookie.TabIndex = 12;
            this.cookie.Text = "cookie名称：";
            // 
            // SaveConfig
            // 
            this.SaveConfig.Location = new System.Drawing.Point(7, 389);
            this.SaveConfig.Name = "SaveConfig";
            this.SaveConfig.Size = new System.Drawing.Size(104, 23);
            this.SaveConfig.TabIndex = 2;
            this.SaveConfig.Text = "保存";
            this.SaveConfig.UseVisualStyleBackColor = true;
            this.SaveConfig.Click += new System.EventHandler(this.SaveConfig_Click);
            // 
            // svcConfig
            // 
            this.svcConfig.Controls.Add(this.localIPAddress);
            this.svcConfig.Controls.Add(this.fileSvPath);
            this.svcConfig.Controls.Add(this.originHost);
            this.svcConfig.Controls.Add(this.path);
            this.svcConfig.Controls.Add(this.oHost);
            this.svcConfig.Controls.Add(this.serviceName);
            this.svcConfig.Controls.Add(this.svc);
            this.svcConfig.Controls.Add(this.rootPath);
            this.svcConfig.Controls.Add(this.root);
            this.svcConfig.Controls.Add(this.localPort);
            this.svcConfig.Controls.Add(this.label2);
            this.svcConfig.Controls.Add(this.localhost);
            this.svcConfig.Location = new System.Drawing.Point(7, 99);
            this.svcConfig.Name = "svcConfig";
            this.svcConfig.Size = new System.Drawing.Size(620, 183);
            this.svcConfig.TabIndex = 1;
            this.svcConfig.TabStop = false;
            // 
            // path
            // 
            this.path.AutoSize = true;
            this.path.Location = new System.Drawing.Point(24, 156);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(65, 12);
            this.path.TabIndex = 11;
            this.path.Text = "文件路径：";
            // 
            // oHost
            // 
            this.oHost.AutoSize = true;
            this.oHost.Location = new System.Drawing.Point(36, 123);
            this.oHost.Name = "oHost";
            this.oHost.Size = new System.Drawing.Size(53, 12);
            this.oHost.TabIndex = 10;
            this.oHost.Text = "源主机：";
            // 
            // serviceName
            // 
            this.serviceName.Location = new System.Drawing.Point(122, 86);
            this.serviceName.Name = "serviceName";
            this.serviceName.Size = new System.Drawing.Size(221, 21);
            this.serviceName.TabIndex = 9;
            // 
            // svc
            // 
            this.svc.AutoSize = true;
            this.svc.Location = new System.Drawing.Point(24, 90);
            this.svc.Name = "svc";
            this.svc.Size = new System.Drawing.Size(65, 12);
            this.svc.TabIndex = 8;
            this.svc.Text = "服务名称：";
            // 
            // rootPath
            // 
            this.rootPath.Location = new System.Drawing.Point(122, 53);
            this.rootPath.Name = "rootPath";
            this.rootPath.Size = new System.Drawing.Size(221, 21);
            this.rootPath.TabIndex = 7;
            // 
            // root
            // 
            this.root.AutoSize = true;
            this.root.Location = new System.Drawing.Point(36, 57);
            this.root.Name = "root";
            this.root.Size = new System.Drawing.Size(53, 12);
            this.root.TabIndex = 6;
            this.root.Text = "根目录：";
            // 
            // localPort
            // 
            this.localPort.Location = new System.Drawing.Point(295, 20);
            this.localPort.Name = "localPort";
            this.localPort.Size = new System.Drawing.Size(48, 21);
            this.localPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";
            // 
            // localhost
            // 
            this.localhost.AutoSize = true;
            this.localhost.Location = new System.Drawing.Point(24, 24);
            this.localhost.Name = "localhost";
            this.localhost.Size = new System.Drawing.Size(65, 12);
            this.localhost.TabIndex = 2;
            this.localhost.Text = "主机配置：";
            // 
            // svConfig
            // 
            this.svConfig.Controls.Add(this.selfStartService);
            this.svConfig.Controls.Add(this.serverConfig);
            this.svConfig.Controls.Add(this.selfStartServer);
            this.svConfig.Location = new System.Drawing.Point(7, 7);
            this.svConfig.Name = "svConfig";
            this.svConfig.Size = new System.Drawing.Size(620, 79);
            this.svConfig.TabIndex = 0;
            this.svConfig.TabStop = false;
            // 
            // selfStartService
            // 
            this.selfStartService.AutoSize = true;
            this.selfStartService.Location = new System.Drawing.Point(122, 45);
            this.selfStartService.Name = "selfStartService";
            this.selfStartService.Size = new System.Drawing.Size(84, 16);
            this.selfStartService.TabIndex = 2;
            this.selfStartService.Text = "服务自启动";
            this.selfStartService.UseVisualStyleBackColor = true;
            // 
            // serverConfig
            // 
            this.serverConfig.AutoSize = true;
            this.serverConfig.Location = new System.Drawing.Point(24, 24);
            this.serverConfig.Name = "serverConfig";
            this.serverConfig.Size = new System.Drawing.Size(65, 12);
            this.serverConfig.TabIndex = 1;
            this.serverConfig.Text = "系统配置：";
            // 
            // selfStartServer
            // 
            this.selfStartServer.AutoSize = true;
            this.selfStartServer.Location = new System.Drawing.Point(122, 23);
            this.selfStartServer.Name = "selfStartServer";
            this.selfStartServer.Size = new System.Drawing.Size(72, 16);
            this.selfStartServer.TabIndex = 0;
            this.selfStartServer.Text = "开机运行";
            this.selfStartServer.UseVisualStyleBackColor = true;
            // 
            // originHost
            // 
            this.originHost.Location = new System.Drawing.Point(122, 119);
            this.originHost.Name = "originHost";
            this.originHost.Size = new System.Drawing.Size(221, 21);
            this.originHost.TabIndex = 12;
            // 
            // fileSvPath
            // 
            this.fileSvPath.Location = new System.Drawing.Point(122, 152);
            this.fileSvPath.Name = "fileSvPath";
            this.fileSvPath.Size = new System.Drawing.Size(221, 21);
            this.fileSvPath.TabIndex = 13;
            // 
            // cookieName
            // 
            this.cookieName.Location = new System.Drawing.Point(122, 21);
            this.cookieName.Name = "cookieName";
            this.cookieName.Size = new System.Drawing.Size(221, 21);
            this.cookieName.TabIndex = 14;
            // 
            // cookieExpires
            // 
            this.cookieExpires.Location = new System.Drawing.Point(122, 50);
            this.cookieExpires.Name = "cookieExpires";
            this.cookieExpires.Size = new System.Drawing.Size(221, 21);
            this.cookieExpires.TabIndex = 15;
            // 
            // logsBox
            // 
            this.logsBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.logsBox.FormattingEnabled = true;
            this.logsBox.ItemHeight = 12;
            this.logsBox.Location = new System.Drawing.Point(6, 7);
            this.logsBox.Name = "logsBox";
            this.logsBox.Size = new System.Drawing.Size(621, 376);
            this.logsBox.TabIndex = 0;
            // 
            // localIPAddress
            // 
            this.localIPAddress.Location = new System.Drawing.Point(122, 20);
            this.localIPAddress.Name = "localIPAddress";
            this.localIPAddress.Size = new System.Drawing.Size(151, 21);
            this.localIPAddress.TabIndex = 14;
            // 
            // SAPIServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 470);
            this.Controls.Add(this.serverTab);
            this.Name = "SAPIServer";
            this.Text = "SAPIServer";
            this.serverTab.ResumeLayout(false);
            this.servicePage.ResumeLayout(false);
            this.configPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.svcConfig.ResumeLayout(false);
            this.svcConfig.PerformLayout();
            this.svConfig.ResumeLayout(false);
            this.svConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl serverTab;
        private System.Windows.Forms.TabPage servicePage;
        private System.Windows.Forms.Button SvcStartStop;
        private SListBox logsBox;
        private System.Windows.Forms.TabPage configPage;
        private System.Windows.Forms.GroupBox svConfig;
        private System.Windows.Forms.Button SaveConfig;
        private System.Windows.Forms.GroupBox svcConfig;
        private System.Windows.Forms.Label localhost;
        private System.Windows.Forms.CheckBox selfStartService;
        private System.Windows.Forms.Label serverConfig;
        private System.Windows.Forms.CheckBox selfStartServer;
        private System.Windows.Forms.TextBox localPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rootPath;
        private System.Windows.Forms.Label root;
        private System.Windows.Forms.Label cookie;
        private System.Windows.Forms.Label path;
        private System.Windows.Forms.Label oHost;
        private System.Windows.Forms.TextBox serviceName;
        private System.Windows.Forms.Label svc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label expire;
        private System.Windows.Forms.TextBox cookieExpires;
        private System.Windows.Forms.TextBox cookieName;
        private System.Windows.Forms.TextBox fileSvPath;
        private System.Windows.Forms.TextBox originHost;
        private System.Windows.Forms.TextBox localIPAddress;
    }
}

