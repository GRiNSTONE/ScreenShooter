namespace ScreenShooter
{
    partial class ProfileList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileList));
            this.tbNameOfNewProfile = new System.Windows.Forms.TextBox();
            this.lbProfilesList = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnActive = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.profileModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.profileModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNameOfNewProfile
            // 
            this.tbNameOfNewProfile.Location = new System.Drawing.Point(12, 12);
            this.tbNameOfNewProfile.Name = "tbNameOfNewProfile";
            this.tbNameOfNewProfile.Size = new System.Drawing.Size(152, 20);
            this.tbNameOfNewProfile.TabIndex = 0;
            this.tbNameOfNewProfile.Text = "Новый профиль";
            this.tbNameOfNewProfile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbNameOfNewProfile_MouseClick);
            // 
            // lbProfilesList
            // 
            this.lbProfilesList.DisplayMember = "Id";
            this.lbProfilesList.FormattingEnabled = true;
            this.lbProfilesList.Location = new System.Drawing.Point(12, 38);
            this.lbProfilesList.Name = "lbProfilesList";
            this.lbProfilesList.Size = new System.Drawing.Size(152, 186);
            this.lbProfilesList.TabIndex = 1;
            this.lbProfilesList.ValueMember = "Id";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(170, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(170, 39);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(93, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnActive
            // 
            this.btnActive.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnActive.Location = new System.Drawing.Point(170, 200);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(93, 23);
            this.btnActive.TabIndex = 4;
            this.btnActive.Text = "Активировать";
            this.btnActive.UseVisualStyleBackColor = true;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // profileModelBindingSource
            // 
            this.profileModelBindingSource.DataSource = typeof(ScreenShooter.ProfileModel);
            // 
            // ProfileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 235);
            this.Controls.Add(this.btnActive);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbProfilesList);
            this.Controls.Add(this.tbNameOfNewProfile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProfileList";
            ((System.ComponentModel.ISupportInitialize)(this.profileModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNameOfNewProfile;
        private System.Windows.Forms.ListBox lbProfilesList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.BindingSource profileModelBindingSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}