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
            this.logsBox = new SiweiSoft.SAPIServer.SListBox();
            this.configPage = new System.Windows.Forms.TabPage();
            this.SaveConfig = new System.Windows.Forms.Button();
            this.svcConfig = new System.Windows.Forms.GroupBox();
            this.localPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.localIPAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.svConfig = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.serverConfig = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.serverTab.SuspendLayout();
            this.servicePage.SuspendLayout();
            this.configPage.SuspendLayout();
            this.svcConfig.SuspendLayout();
            this.svConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // SaveConfig
            // 
            this.SaveConfig.Location = new System.Drawing.Point(7, 394);
            this.SaveConfig.Name = "SaveConfig";
            this.SaveConfig.Size = new System.Drawing.Size(104, 23);
            this.SaveConfig.TabIndex = 2;
            this.SaveConfig.Text = "保存";
            this.SaveConfig.UseVisualStyleBackColor = true;
            // 
            // svcConfig
            // 
            this.svcConfig.Controls.Add(this.label6);
            this.svcConfig.Controls.Add(this.label5);
            this.svcConfig.Controls.Add(this.textBox2);
            this.svcConfig.Controls.Add(this.label4);
            this.svcConfig.Controls.Add(this.textBox1);
            this.svcConfig.Controls.Add(this.label3);
            this.svcConfig.Controls.Add(this.localPort);
            this.svcConfig.Controls.Add(this.label2);
            this.svcConfig.Controls.Add(this.localIPAddress);
            this.svcConfig.Controls.Add(this.label1);
            this.svcConfig.Location = new System.Drawing.Point(7, 92);
            this.svcConfig.Name = "svcConfig";
            this.svcConfig.Size = new System.Drawing.Size(620, 183);
            this.svcConfig.TabIndex = 1;
            this.svcConfig.TabStop = false;
            // 
            // localPort
            // 
            this.localPort.Location = new System.Drawing.Point(278, 21);
            this.localPort.Name = "localPort";
            this.localPort.Size = new System.Drawing.Size(46, 21);
            this.localPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";
            // 
            // localIPAddress
            // 
            this.localIPAddress.FormattingEnabled = true;
            this.localIPAddress.Items.AddRange(new object[] {
            "localhost",
            "127.0.0.1"});
            this.localIPAddress.Location = new System.Drawing.Point(103, 21);
            this.localIPAddress.Name = "localIPAddress";
            this.localIPAddress.Size = new System.Drawing.Size(150, 20);
            this.localIPAddress.TabIndex = 3;
            this.localIPAddress.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "主机配置：";
            // 
            // svConfig
            // 
            this.svConfig.Controls.Add(this.checkBox2);
            this.svConfig.Controls.Add(this.serverConfig);
            this.svConfig.Controls.Add(this.checkBox1);
            this.svConfig.Location = new System.Drawing.Point(7, 7);
            this.svConfig.Name = "svConfig";
            this.svConfig.Size = new System.Drawing.Size(620, 79);
            this.svConfig.TabIndex = 0;
            this.svConfig.TabStop = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(103, 43);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "服务自启动";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // serverConfig
            // 
            this.serverConfig.AutoSize = true;
            this.serverConfig.Location = new System.Drawing.Point(12, 22);
            this.serverConfig.Name = "serverConfig";
            this.serverConfig.Size = new System.Drawing.Size(65, 12);
            this.serverConfig.TabIndex = 1;
            this.serverConfig.Text = "系统配置：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(103, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "开机运行";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "根 目 录：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(221, 21);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(105, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(221, 21);
            this.textBox2.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "服务名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "源 主 机：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "文件路径：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "cookie 名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(7, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 73);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "过 期 时 间：";
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
            this.svcConfig.ResumeLayout(false);
            this.svcConfig.PerformLayout();
            this.svConfig.ResumeLayout(false);
            this.svConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label serverConfig;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox localPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox localIPAddress;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
    }
}

