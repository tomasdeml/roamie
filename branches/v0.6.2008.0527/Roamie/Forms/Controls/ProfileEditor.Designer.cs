namespace Virtuoso.Miranda.Roamie.Forms.Controls
{
    partial class ProfileEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileEditor));
            this.TestBTN = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.RemoteAddressTBOX = new Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox();
            this.DescriptionTBOX = new Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox();
            this.DatabasePasswordTBOX = new Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox();
            this.LoginPasswordTBOX = new Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox();
            this.LoginNameTBOX = new Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox();
            this.ProfileNameTBOX = new Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ActionBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.DatabaseProviderLBOX = new System.Windows.Forms.ComboBox();
            this.HelpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PreferFullSync = new System.Windows.Forms.CheckBox();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.UploadPathExamplesLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestBTN
            // 
            resources.ApplyResources(this.TestBTN, "TestBTN");
            this.TestBTN.Name = "TestBTN";
            this.TestBTN.UseVisualStyleBackColor = true;
            this.TestBTN.Click += new System.EventHandler(this.TestBTN_Click);
            this.TestBTN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // RemoteAddressTBOX
            // 
            this.RemoteAddressTBOX.BannerFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RemoteAddressTBOX.BannerForeColor = System.Drawing.SystemColors.GrayText;
            this.RemoteAddressTBOX.BannerText = "ftp://my.com/dir/database.dat";
            this.RemoteAddressTBOX.FocusSelect = true;
            resources.ApplyResources(this.RemoteAddressTBOX, "RemoteAddressTBOX");
            this.RemoteAddressTBOX.Name = "RemoteAddressTBOX";
            this.RemoteAddressTBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // DescriptionTBOX
            // 
            this.DescriptionTBOX.BannerFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DescriptionTBOX.BannerForeColor = System.Drawing.SystemColors.GrayText;
            this.DescriptionTBOX.BannerText = "Type a profile description";
            this.DescriptionTBOX.FocusSelect = true;
            resources.ApplyResources(this.DescriptionTBOX, "DescriptionTBOX");
            this.DescriptionTBOX.Name = "DescriptionTBOX";
            this.DescriptionTBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // DatabasePasswordTBOX
            // 
            this.DatabasePasswordTBOX.BannerFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DatabasePasswordTBOX.BannerForeColor = System.Drawing.SystemColors.GrayText;
            this.DatabasePasswordTBOX.BannerText = "Type a password";
            this.DatabasePasswordTBOX.FocusSelect = true;
            resources.ApplyResources(this.DatabasePasswordTBOX, "DatabasePasswordTBOX");
            this.DatabasePasswordTBOX.Name = "DatabasePasswordTBOX";
            this.DatabasePasswordTBOX.UseSystemPasswordChar = true;
            this.DatabasePasswordTBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // LoginPasswordTBOX
            // 
            this.LoginPasswordTBOX.BannerFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoginPasswordTBOX.BannerForeColor = System.Drawing.SystemColors.GrayText;
            this.LoginPasswordTBOX.BannerText = "Type FTP login password";
            this.LoginPasswordTBOX.FocusSelect = true;
            resources.ApplyResources(this.LoginPasswordTBOX, "LoginPasswordTBOX");
            this.LoginPasswordTBOX.Name = "LoginPasswordTBOX";
            this.LoginPasswordTBOX.UseSystemPasswordChar = true;
            this.LoginPasswordTBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // LoginNameTBOX
            // 
            this.LoginNameTBOX.BannerFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoginNameTBOX.BannerForeColor = System.Drawing.SystemColors.GrayText;
            this.LoginNameTBOX.BannerText = "Type FTP login name";
            this.LoginNameTBOX.FocusSelect = true;
            resources.ApplyResources(this.LoginNameTBOX, "LoginNameTBOX");
            this.LoginNameTBOX.Name = "LoginNameTBOX";
            this.LoginNameTBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // ProfileNameTBOX
            // 
            this.ProfileNameTBOX.BannerFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ProfileNameTBOX.BannerForeColor = System.Drawing.SystemColors.GrayText;
            this.ProfileNameTBOX.BannerText = "Type a profile name";
            this.ProfileNameTBOX.FocusSelect = true;
            resources.ApplyResources(this.ProfileNameTBOX, "ProfileNameTBOX");
            this.ProfileNameTBOX.Name = "ProfileNameTBOX";
            this.ProfileNameTBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // ActionBTN
            // 
            resources.ApplyResources(this.ActionBTN, "ActionBTN");
            this.ActionBTN.Name = "ActionBTN";
            this.ActionBTN.UseVisualStyleBackColor = true;
            this.ActionBTN.Click += new System.EventHandler(this.ActionBTN_Click);
            this.ActionBTN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // CancelBTN
            // 
            resources.ApplyResources(this.CancelBTN, "CancelBTN");
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            this.CancelBTN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // DatabaseProviderLBOX
            // 
            this.DatabaseProviderLBOX.DisplayMember = "Profile";
            this.DatabaseProviderLBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.DatabaseProviderLBOX, "DatabaseProviderLBOX");
            this.DatabaseProviderLBOX.Name = "DatabaseProviderLBOX";
            this.DatabaseProviderLBOX.ValueMember = "Name";
            this.DatabaseProviderLBOX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // HelpToolTip
            // 
            this.HelpToolTip.AutoPopDelay = 5000;
            this.HelpToolTip.InitialDelay = 300;
            this.HelpToolTip.ReshowDelay = 100;
            this.HelpToolTip.ToolTipTitle = "Hint";
            // 
            // PreferFullSync
            // 
            resources.ApplyResources(this.PreferFullSync, "PreferFullSync");
            this.PreferFullSync.Name = "PreferFullSync";
            this.HelpToolTip.SetToolTip(this.PreferFullSync, resources.GetString("PreferFullSync.ToolTip"));
            this.PreferFullSync.UseVisualStyleBackColor = true;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.CancelBTN);
            this.gradientPanel1.Controls.Add(this.TestBTN);
            this.gradientPanel1.Controls.Add(this.ActionBTN);
            resources.ApplyResources(this.gradientPanel1, "gradientPanel1");
            this.gradientPanel1.GradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = 90F;
            // 
            // UploadPathExamplesLink
            // 
            resources.ApplyResources(this.UploadPathExamplesLink, "UploadPathExamplesLink");
            this.UploadPathExamplesLink.Name = "UploadPathExamplesLink";
            this.UploadPathExamplesLink.TabStop = true;
            this.UploadPathExamplesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UploadPathExamplesLINK_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ProfileEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.PreferFullSync);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.DatabaseProviderLBOX);
            this.Controls.Add(this.RemoteAddressTBOX);
            this.Controls.Add(this.DescriptionTBOX);
            this.Controls.Add(this.DatabasePasswordTBOX);
            this.Controls.Add(this.LoginPasswordTBOX);
            this.Controls.Add(this.LoginNameTBOX);
            this.Controls.Add(this.ProfileNameTBOX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UploadPathExamplesLink);
            this.Controls.Add(this.label8);
            this.MinimumSize = new System.Drawing.Size(402, 305);
            this.Name = "ProfileEditor";
            this.Load += new System.EventHandler(this.ProfileEditor_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.Button TestBTN;
        private System.Windows.Forms.Label label8;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox RemoteAddressTBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox DescriptionTBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox DatabasePasswordTBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox LoginPasswordTBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox LoginNameTBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CueBannerTextBox ProfileNameTBOX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ActionBTN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.ComboBox DatabaseProviderLBOX;
        private System.Windows.Forms.ToolTip HelpToolTip;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.LinkLabel UploadPathExamplesLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox PreferFullSync;
    }
}
