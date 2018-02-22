namespace Streamy
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.log = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.confvideoagain = new System.Windows.Forms.Button();
            this.videoconfiguredlbl = new System.Windows.Forms.Label();
            this.confvideobtn = new System.Windows.Forms.Button();
            this.usernamelabel = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.configurevideo = new System.Windows.Forms.Button();
            this.videonotconfiguredlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.vlcisconfiguredlbl = new System.Windows.Forms.Label();
            this.downloadVLC = new System.Windows.Forms.Button();
            this.vlcinstalled = new System.Windows.Forms.Label();
            this.configurevlc = new System.Windows.Forms.Button();
            this.vlcnotconfiguredlabel = new System.Windows.Forms.Label();
            this.vlclabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.runasadminlbl = new System.Windows.Forms.Label();
            this.closevideobox = new System.Windows.Forms.CheckBox();
            this.spotcheckbox = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.FormattingEnabled = true;
            this.log.Location = new System.Drawing.Point(12, 315);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(425, 134);
            this.log.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.confvideoagain);
            this.panel1.Controls.Add(this.videoconfiguredlbl);
            this.panel1.Controls.Add(this.confvideobtn);
            this.panel1.Controls.Add(this.usernamelabel);
            this.panel1.Controls.Add(this.Username);
            this.panel1.Controls.Add(this.configurevideo);
            this.panel1.Controls.Add(this.videonotconfiguredlabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(31, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 102);
            this.panel1.TabIndex = 1;
            // 
            // confvideoagain
            // 
            this.confvideoagain.Location = new System.Drawing.Point(43, 43);
            this.confvideoagain.Name = "confvideoagain";
            this.confvideoagain.Size = new System.Drawing.Size(89, 23);
            this.confvideoagain.TabIndex = 14;
            this.confvideoagain.Text = "Configure";
            this.confvideoagain.UseVisualStyleBackColor = true;
            this.confvideoagain.Visible = false;
            this.confvideoagain.Click += new System.EventHandler(this.confvideoagain_Click);
            // 
            // videoconfiguredlbl
            // 
            this.videoconfiguredlbl.AutoSize = true;
            this.videoconfiguredlbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.videoconfiguredlbl.Location = new System.Drawing.Point(39, 27);
            this.videoconfiguredlbl.Name = "videoconfiguredlbl";
            this.videoconfiguredlbl.Size = new System.Drawing.Size(96, 13);
            this.videoconfiguredlbl.TabIndex = 13;
            this.videoconfiguredlbl.Text = "Blynk is configured";
            this.videoconfiguredlbl.Visible = false;
            // 
            // confvideobtn
            // 
            this.confvideobtn.Location = new System.Drawing.Point(133, 76);
            this.confvideobtn.Name = "confvideobtn";
            this.confvideobtn.Size = new System.Drawing.Size(35, 23);
            this.confvideobtn.TabIndex = 11;
            this.confvideobtn.Text = "OK";
            this.confvideobtn.UseVisualStyleBackColor = true;
            this.confvideobtn.Visible = false;
            this.confvideobtn.Click += new System.EventHandler(this.confvideobtn_Click);
            // 
            // usernamelabel
            // 
            this.usernamelabel.AutoSize = true;
            this.usernamelabel.Location = new System.Drawing.Point(5, 27);
            this.usernamelabel.Name = "usernamelabel";
            this.usernamelabel.Size = new System.Drawing.Size(53, 13);
            this.usernamelabel.TabIndex = 6;
            this.usernamelabel.Text = "Blynk API";
            this.usernamelabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.usernamelabel.Visible = false;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(60, 24);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 20);
            this.Username.TabIndex = 8;
            this.Username.Visible = false;
            // 
            // configurevideo
            // 
            this.configurevideo.Location = new System.Drawing.Point(30, 43);
            this.configurevideo.Name = "configurevideo";
            this.configurevideo.Size = new System.Drawing.Size(89, 23);
            this.configurevideo.TabIndex = 7;
            this.configurevideo.Text = "Configure";
            this.configurevideo.UseVisualStyleBackColor = true;
            this.configurevideo.Click += new System.EventHandler(this.button3_Click);
            // 
            // videonotconfiguredlabel
            // 
            this.videonotconfiguredlabel.AutoSize = true;
            this.videonotconfiguredlabel.Location = new System.Drawing.Point(17, 27);
            this.videonotconfiguredlabel.Name = "videonotconfiguredlabel";
            this.videonotconfiguredlabel.Size = new System.Drawing.Size(117, 13);
            this.videonotconfiguredlabel.TabIndex = 6;
            this.videonotconfiguredlabel.Text = "Netflix is not configured";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Blynk/Video Services";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(235, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 102);
            this.panel2.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label10.Location = new System.Drawing.Point(7, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Music app is running";
            this.label10.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Music app is not running";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Music Services";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.vlcisconfiguredlbl);
            this.panel3.Controls.Add(this.downloadVLC);
            this.panel3.Controls.Add(this.vlcinstalled);
            this.panel3.Controls.Add(this.configurevlc);
            this.panel3.Controls.Add(this.vlcnotconfiguredlabel);
            this.panel3.Controls.Add(this.vlclabel);
            this.panel3.Location = new System.Drawing.Point(31, 120);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(380, 83);
            this.panel3.TabIndex = 3;
            // 
            // vlcisconfiguredlbl
            // 
            this.vlcisconfiguredlbl.AutoSize = true;
            this.vlcisconfiguredlbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.vlcisconfiguredlbl.Location = new System.Drawing.Point(142, 30);
            this.vlcisconfiguredlbl.Name = "vlcisconfiguredlbl";
            this.vlcisconfiguredlbl.Size = new System.Drawing.Size(90, 13);
            this.vlcisconfiguredlbl.TabIndex = 15;
            this.vlcisconfiguredlbl.Text = "VLC is configured";
            this.vlcisconfiguredlbl.Visible = false;
            // 
            // downloadVLC
            // 
            this.downloadVLC.Location = new System.Drawing.Point(141, 46);
            this.downloadVLC.Name = "downloadVLC";
            this.downloadVLC.Size = new System.Drawing.Size(90, 23);
            this.downloadVLC.TabIndex = 6;
            this.downloadVLC.Text = "Download VLC";
            this.downloadVLC.UseVisualStyleBackColor = true;
            this.downloadVLC.Click += new System.EventHandler(this.downloadVLC_Click);
            // 
            // vlcinstalled
            // 
            this.vlcinstalled.AutoSize = true;
            this.vlcinstalled.Location = new System.Drawing.Point(136, 30);
            this.vlcinstalled.Name = "vlcinstalled";
            this.vlcinstalled.Size = new System.Drawing.Size(96, 13);
            this.vlcinstalled.TabIndex = 5;
            this.vlcinstalled.Text = "VLC is not installed";
            // 
            // configurevlc
            // 
            this.configurevlc.Location = new System.Drawing.Point(142, 46);
            this.configurevlc.Name = "configurevlc";
            this.configurevlc.Size = new System.Drawing.Size(89, 23);
            this.configurevlc.TabIndex = 3;
            this.configurevlc.Text = "Configure";
            this.configurevlc.UseVisualStyleBackColor = true;
            this.configurevlc.Click += new System.EventHandler(this.configurevlc_Click);
            // 
            // vlcnotconfiguredlabel
            // 
            this.vlcnotconfiguredlabel.AutoSize = true;
            this.vlcnotconfiguredlabel.Location = new System.Drawing.Point(133, 30);
            this.vlcnotconfiguredlabel.Name = "vlcnotconfiguredlabel";
            this.vlcnotconfiguredlabel.Size = new System.Drawing.Size(108, 13);
            this.vlcnotconfiguredlabel.TabIndex = 1;
            this.vlcnotconfiguredlabel.Text = "VLC is not configured";
            this.vlcnotconfiguredlabel.Visible = false;
            // 
            // vlclabel
            // 
            this.vlclabel.AutoSize = true;
            this.vlclabel.Location = new System.Drawing.Point(134, 7);
            this.vlclabel.Name = "vlclabel";
            this.vlclabel.Size = new System.Drawing.Size(113, 13);
            this.vlclabel.TabIndex = 0;
            this.vlclabel.Text = "VLC (Media streaming)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Settings";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(31, 241);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Run at startup";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // runasadminlbl
            // 
            this.runasadminlbl.AutoSize = true;
            this.runasadminlbl.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.runasadminlbl.Location = new System.Drawing.Point(47, 258);
            this.runasadminlbl.Name = "runasadminlbl";
            this.runasadminlbl.Size = new System.Drawing.Size(102, 13);
            this.runasadminlbl.TabIndex = 6;
            this.runasadminlbl.Text = "Please run as admin";
            this.runasadminlbl.Visible = false;
            // 
            // closevideobox
            // 
            this.closevideobox.AutoSize = true;
            this.closevideobox.Checked = true;
            this.closevideobox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closevideobox.Location = new System.Drawing.Point(31, 275);
            this.closevideobox.Name = "closevideobox";
            this.closevideobox.Size = new System.Drawing.Size(200, 17);
            this.closevideobox.TabIndex = 10;
            this.closevideobox.Text = "Automatically close video service tab";
            this.closevideobox.UseVisualStyleBackColor = true;
            // 
            // spotcheckbox
            // 
            this.spotcheckbox.AutoSize = true;
            this.spotcheckbox.Checked = true;
            this.spotcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.spotcheckbox.Location = new System.Drawing.Point(31, 292);
            this.spotcheckbox.Name = "spotcheckbox";
            this.spotcheckbox.Size = new System.Drawing.Size(219, 17);
            this.spotcheckbox.TabIndex = 12;
            this.spotcheckbox.Text = "Automatically open music app on request";
            this.spotcheckbox.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Streamy";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 471);
            this.Controls.Add(this.spotcheckbox);
            this.Controls.Add(this.closevideobox);
            this.Controls.Add(this.runasadminlbl);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.log);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Streamy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label vlclabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label vlcnotconfiguredlabel;
        private System.Windows.Forms.Button configurevlc;
        private System.Windows.Forms.Button configurevideo;
        private System.Windows.Forms.Label videonotconfiguredlabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label vlcinstalled;
        private System.Windows.Forms.Button downloadVLC;
        private System.Windows.Forms.Label usernamelabel;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Button confvideobtn;
        private System.Windows.Forms.Label videoconfiguredlbl;
        private System.Windows.Forms.Button confvideoagain;
        private System.Windows.Forms.Label runasadminlbl;
        private System.Windows.Forms.Label vlcisconfiguredlbl;
        private System.Windows.Forms.CheckBox closevideobox;
        private System.Windows.Forms.CheckBox spotcheckbox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

