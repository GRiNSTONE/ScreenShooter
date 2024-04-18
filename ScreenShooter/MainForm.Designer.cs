namespace ScreenShooter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbLog = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblProfile = new System.Windows.Forms.Label();
            this.cheEnableScreenShot = new System.Windows.Forms.CheckBox();
            this.tbScreenShotPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbDivideToMoths = new System.Windows.Forms.CheckBox();
            this.btnProfileList = new System.Windows.Forms.Button();
            this.chbEnableSound = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAbout = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNameOfProfile = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlbtnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tltbActiveProfileName = new System.Windows.Forms.ToolStripTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(12, 202);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(388, 82);
            this.lbLog.TabIndex = 1;
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(6, 14);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(107, 13);
            this.lblProfile.TabIndex = 3;
            this.lblProfile.Text = "Активный профиль:";
            // 
            // cheEnableScreenShot
            // 
            this.cheEnableScreenShot.AutoSize = true;
            this.cheEnableScreenShot.Checked = true;
            this.cheEnableScreenShot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cheEnableScreenShot.Location = new System.Drawing.Point(9, 13);
            this.cheEnableScreenShot.Name = "cheEnableScreenShot";
            this.cheEnableScreenShot.Size = new System.Drawing.Size(137, 17);
            this.cheEnableScreenShot.TabIndex = 5;
            this.cheEnableScreenShot.Text = "Программа включена";
            this.cheEnableScreenShot.UseVisualStyleBackColor = true;
            this.cheEnableScreenShot.CheckedChanged += new System.EventHandler(this.cheEnableScreenShot_CheckedChanged);
            // 
            // tbScreenShotPath
            // 
            this.tbScreenShotPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbScreenShotPath.Location = new System.Drawing.Point(51, 13);
            this.tbScreenShotPath.Name = "tbScreenShotPath";
            this.tbScreenShotPath.ReadOnly = true;
            this.tbScreenShotPath.Size = new System.Drawing.Size(242, 20);
            this.tbScreenShotPath.TabIndex = 6;
            this.tbScreenShotPath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbScreenShotPath_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Папка";
            // 
            // chbDivideToMoths
            // 
            this.chbDivideToMoths.AutoSize = true;
            this.chbDivideToMoths.Checked = true;
            this.chbDivideToMoths.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbDivideToMoths.Location = new System.Drawing.Point(51, 39);
            this.chbDivideToMoths.Name = "chbDivideToMoths";
            this.chbDivideToMoths.Size = new System.Drawing.Size(144, 17);
            this.chbDivideToMoths.TabIndex = 8;
            this.chbDivideToMoths.Text = "Разбивать по месяцам";
            this.chbDivideToMoths.UseVisualStyleBackColor = true;
            this.chbDivideToMoths.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chbDivideToMoths_MouseUp);
            // 
            // btnProfileList
            // 
            this.btnProfileList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileList.Location = new System.Drawing.Point(299, 9);
            this.btnProfileList.Name = "btnProfileList";
            this.btnProfileList.Size = new System.Drawing.Size(75, 23);
            this.btnProfileList.TabIndex = 9;
            this.btnProfileList.Text = "Профили";
            this.btnProfileList.UseVisualStyleBackColor = true;
            this.btnProfileList.Click += new System.EventHandler(this.btnProfileList_Click);
            // 
            // chbEnableSound
            // 
            this.chbEnableSound.AutoSize = true;
            this.chbEnableSound.Checked = true;
            this.chbEnableSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbEnableSound.Location = new System.Drawing.Point(51, 62);
            this.chbEnableSound.Name = "chbEnableSound";
            this.chbEnableSound.Size = new System.Drawing.Size(50, 17);
            this.chbEnableSound.TabIndex = 10;
            this.chbEnableSound.Text = "Звук";
            this.chbEnableSound.UseVisualStyleBackColor = true;
            this.chbEnableSound.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chbEnableSound_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnAbout);
            this.panel1.Controls.Add(this.cheEnableScreenShot);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 40);
            this.panel1.TabIndex = 11;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(299, 9);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 10;
            this.btnAbout.Text = "Описание";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblNameOfProfile);
            this.panel2.Controls.Add(this.lblProfile);
            this.panel2.Controls.Add(this.btnProfileList);
            this.panel2.Location = new System.Drawing.Point(12, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(389, 41);
            this.panel2.TabIndex = 12;
            // 
            // lblNameOfProfile
            // 
            this.lblNameOfProfile.AutoSize = true;
            this.lblNameOfProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNameOfProfile.Location = new System.Drawing.Point(119, 14);
            this.lblNameOfProfile.Name = "lblNameOfProfile";
            this.lblNameOfProfile.Size = new System.Drawing.Size(76, 13);
            this.lblNameOfProfile.TabIndex = 10;
            this.lblNameOfProfile.Text = "Имя профиля";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnOpenFolder);
            this.panel3.Controls.Add(this.tbScreenShotPath);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.chbDivideToMoths);
            this.panel3.Controls.Add(this.chbEnableSound);
            this.panel3.Location = new System.Drawing.Point(12, 105);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(389, 88);
            this.panel3.TabIndex = 13;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.Location = new System.Drawing.Point(299, 11);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFolder.TabIndex = 11;
            this.btnOpenFolder.Text = "Открыть";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.ContextMenuStrip = this.cmsTray;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Screen Shooter";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tltbActiveProfileName,
            this.toolStripSeparator1,
            this.tlbtnClose});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(181, 79);
            // 
            // tlbtnClose
            // 
            this.tlbtnClose.Name = "tlbtnClose";
            this.tlbtnClose.Size = new System.Drawing.Size(180, 22);
            this.tlbtnClose.Text = "Выход";
            this.tlbtnClose.Click += new System.EventHandler(this.tlbtnClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tltbActiveProfileName
            // 
            this.tltbActiveProfileName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tltbActiveProfileName.Name = "tltbActiveProfileName";
            this.tltbActiveProfileName.ReadOnly = true;
            this.tltbActiveProfileName.Size = new System.Drawing.Size(100, 23);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 296);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(429, 335);
            this.Name = "MainForm";
            this.Text = "Screen Shooter";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.cmsTray.ResumeLayout(false);
            this.cmsTray.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.CheckBox cheEnableScreenShot;
        private System.Windows.Forms.TextBox tbScreenShotPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbDivideToMoths;
        private System.Windows.Forms.Button btnProfileList;
        private System.Windows.Forms.CheckBox chbEnableSound;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNameOfProfile;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem tlbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox tltbActiveProfileName;
    }
}

