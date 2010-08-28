namespace Virtuoso.Miranda.Roamie.Forms
{
    partial class StartupDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupDialog));
            this.label2 = new System.Windows.Forms.Label();
            this.OkBTN = new System.Windows.Forms.Button();
            this.DownloadPBOX = new System.Windows.Forms.PictureBox();
            this.LocalPBOX = new System.Windows.Forms.PictureBox();
            this.NewPBOX = new System.Windows.Forms.PictureBox();
            this.UseLocalRBTN = new System.Windows.Forms.RadioButton();
            this.RoamNewOnExitCHBOX = new System.Windows.Forms.CheckBox();
            this.RoamLocalOnExitCHBOX = new System.Windows.Forms.CheckBox();
            this.SandboxModeCHBOX = new System.Windows.Forms.CheckBox();
            this.PublicComputerCHBOX = new System.Windows.Forms.CheckBox();
            this.CreateNewRBTN = new System.Windows.Forms.RadioButton();
            this.DownloadExistingRBTN = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OptionsLINK = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "What do you want to do?";
            // 
            // OkBTN
            // 
            this.OkBTN.Enabled = false;
            this.OkBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OkBTN.Location = new System.Drawing.Point(11, 339);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 0;
            this.OkBTN.Text = "Continue";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // DownloadPBOX
            // 
            this.DownloadPBOX.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Image_32x32_Web;
            this.DownloadPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DownloadPBOX.Location = new System.Drawing.Point(22, 115);
            this.DownloadPBOX.Name = "DownloadPBOX";
            this.DownloadPBOX.Size = new System.Drawing.Size(29, 32);
            this.DownloadPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DownloadPBOX.TabIndex = 16;
            this.DownloadPBOX.TabStop = false;
            // 
            // LocalPBOX
            // 
            this.LocalPBOX.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Image_32x32_Local;
            this.LocalPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LocalPBOX.Location = new System.Drawing.Point(22, 201);
            this.LocalPBOX.Name = "LocalPBOX";
            this.LocalPBOX.Size = new System.Drawing.Size(28, 26);
            this.LocalPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LocalPBOX.TabIndex = 16;
            this.LocalPBOX.TabStop = false;
            // 
            // NewPBOX
            // 
            this.NewPBOX.Image = ((System.Drawing.Image)(resources.GetObject("NewPBOX.Image")));
            this.NewPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NewPBOX.Location = new System.Drawing.Point(22, 268);
            this.NewPBOX.Name = "NewPBOX";
            this.NewPBOX.Size = new System.Drawing.Size(32, 32);
            this.NewPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.NewPBOX.TabIndex = 16;
            this.NewPBOX.TabStop = false;
            // 
            // UseLocalRBTN
            // 
            this.UseLocalRBTN.AutoSize = true;
            this.UseLocalRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.UseLocalRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UseLocalRBTN.Location = new System.Drawing.Point(68, 201);
            this.UseLocalRBTN.Name = "UseLocalRBTN";
            this.UseLocalRBTN.Size = new System.Drawing.Size(131, 17);
            this.UseLocalRBTN.TabIndex = 6;
            this.UseLocalRBTN.Text = "Use local database";
            this.UseLocalRBTN.UseVisualStyleBackColor = true;
            this.UseLocalRBTN.CheckedChanged += new System.EventHandler(this.RadioBtn_Checked);
            // 
            // RoamNewOnExitCHBOX
            // 
            this.RoamNewOnExitCHBOX.AutoSize = true;
            this.RoamNewOnExitCHBOX.Checked = global::Virtuoso.Miranda.Roamie.Properties.Settings.Default.CreateNewDb_RoamOnExit_Checked;
            this.RoamNewOnExitCHBOX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RoamNewOnExitCHBOX.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Virtuoso.Miranda.Roamie.Properties.Settings.Default, "CreateNewDb_RoamOnExit_Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.RoamNewOnExitCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoamNewOnExitCHBOX.Location = new System.Drawing.Point(88, 294);
            this.RoamNewOnExitCHBOX.Name = "RoamNewOnExitCHBOX";
            this.RoamNewOnExitCHBOX.Size = new System.Drawing.Size(167, 17);
            this.RoamNewOnExitCHBOX.TabIndex = 9;
            this.RoamNewOnExitCHBOX.Text = "On exit, upload the database";
            this.RoamNewOnExitCHBOX.UseVisualStyleBackColor = true;
            // 
            // RoamLocalOnExitCHBOX
            // 
            this.RoamLocalOnExitCHBOX.AutoSize = true;
            this.RoamLocalOnExitCHBOX.Checked = global::Virtuoso.Miranda.Roamie.Properties.Settings.Default.UseLocalDb_RoamOnExit_Checked;
            this.RoamLocalOnExitCHBOX.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Virtuoso.Miranda.Roamie.Properties.Settings.Default, "UseLocalDb_RoamOnExit_Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.RoamLocalOnExitCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoamLocalOnExitCHBOX.Location = new System.Drawing.Point(88, 227);
            this.RoamLocalOnExitCHBOX.Name = "RoamLocalOnExitCHBOX";
            this.RoamLocalOnExitCHBOX.Size = new System.Drawing.Size(167, 17);
            this.RoamLocalOnExitCHBOX.TabIndex = 7;
            this.RoamLocalOnExitCHBOX.Text = "On exit, upload the database";
            this.RoamLocalOnExitCHBOX.UseVisualStyleBackColor = true;
            // 
            // SandboxModeCHBOX
            // 
            this.SandboxModeCHBOX.AutoSize = true;
            this.SandboxModeCHBOX.Checked = global::Virtuoso.Miranda.Roamie.Properties.Settings.Default.DownloadDb_Sandbox_Checked;
            this.SandboxModeCHBOX.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Virtuoso.Miranda.Roamie.Properties.Settings.Default, "DownloadDb_Sandbox_Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SandboxModeCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SandboxModeCHBOX.Location = new System.Drawing.Point(88, 167);
            this.SandboxModeCHBOX.Name = "SandboxModeCHBOX";
            this.SandboxModeCHBOX.Size = new System.Drawing.Size(254, 17);
            this.SandboxModeCHBOX.TabIndex = 5;
            this.SandboxModeCHBOX.Text = "On exit, discard changes made to the database";
            this.SandboxModeCHBOX.UseVisualStyleBackColor = false;
            // 
            // PublicComputerCHBOX
            // 
            this.PublicComputerCHBOX.AutoSize = true;
            this.PublicComputerCHBOX.Checked = global::Virtuoso.Miranda.Roamie.Properties.Settings.Default.DownloadDb_PublicPc_Checked;
            this.PublicComputerCHBOX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PublicComputerCHBOX.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Virtuoso.Miranda.Roamie.Properties.Settings.Default, "DownloadDb_PublicPc_Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PublicComputerCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PublicComputerCHBOX.Location = new System.Drawing.Point(88, 141);
            this.PublicComputerCHBOX.Name = "PublicComputerCHBOX";
            this.PublicComputerCHBOX.Size = new System.Drawing.Size(288, 17);
            this.PublicComputerCHBOX.TabIndex = 4;
            this.PublicComputerCHBOX.Text = "This is a public computer, remove the database on exit";
            this.PublicComputerCHBOX.UseVisualStyleBackColor = true;
            // 
            // CreateNewRBTN
            // 
            this.CreateNewRBTN.AutoSize = true;
            this.CreateNewRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.CreateNewRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CreateNewRBTN.Location = new System.Drawing.Point(68, 268);
            this.CreateNewRBTN.Name = "CreateNewRBTN";
            this.CreateNewRBTN.Size = new System.Drawing.Size(155, 17);
            this.CreateNewRBTN.TabIndex = 8;
            this.CreateNewRBTN.Text = "Create a new database";
            this.CreateNewRBTN.UseVisualStyleBackColor = true;
            this.CreateNewRBTN.CheckedChanged += new System.EventHandler(this.RadioBtn_Checked);
            // 
            // DownloadExistingRBTN
            // 
            this.DownloadExistingRBTN.AutoSize = true;
            this.DownloadExistingRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.DownloadExistingRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DownloadExistingRBTN.Location = new System.Drawing.Point(68, 115);
            this.DownloadExistingRBTN.Name = "DownloadExistingRBTN";
            this.DownloadExistingRBTN.Size = new System.Drawing.Size(201, 17);
            this.DownloadExistingRBTN.TabIndex = 3;
            this.DownloadExistingRBTN.Text = "Download an existing database";
            this.DownloadExistingRBTN.UseVisualStyleBackColor = true;
            this.DownloadExistingRBTN.CheckedChanged += new System.EventHandler(this.RadioBtn_Checked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Watermark_Sync;
            this.pictureBox1.Location = new System.Drawing.Point(283, 187);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.YellowGreen;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(432, 84);
            this.gradientPanel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(66, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Welcome";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Image_48x48_Roamie;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(46, 47);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(66, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "To begin your session, choose an option below and click the Continue button.";
            // 
            // OptionsLINK
            // 
            this.OptionsLINK.AutoSize = true;
            this.OptionsLINK.BackColor = System.Drawing.Color.Transparent;
            this.OptionsLINK.Location = new System.Drawing.Point(384, 87);
            this.OptionsLINK.Name = "OptionsLINK";
            this.OptionsLINK.Size = new System.Drawing.Size(44, 13);
            this.OptionsLINK.TabIndex = 22;
            this.OptionsLINK.TabStop = true;
            this.OptionsLINK.Text = "Options";
            this.OptionsLINK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OptionsLINK_LinkClicked);
            // 
            // StartupDialog
            // 
            this.AcceptButton = this.OkBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 377);
            this.Controls.Add(this.OptionsLINK);
            this.Controls.Add(this.SandboxModeCHBOX);
            this.Controls.Add(this.NewPBOX);
            this.Controls.Add(this.LocalPBOX);
            this.Controls.Add(this.DownloadPBOX);
            this.Controls.Add(this.RoamNewOnExitCHBOX);
            this.Controls.Add(this.RoamLocalOnExitCHBOX);
            this.Controls.Add(this.PublicComputerCHBOX);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.CreateNewRBTN);
            this.Controls.Add(this.UseLocalRBTN);
            this.Controls.Add(this.DownloadExistingRBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gradientPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome - ";
            this.Load += new System.EventHandler(this.StartupDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton DownloadExistingRBTN;
        private System.Windows.Forms.RadioButton UseLocalRBTN;
        private System.Windows.Forms.RadioButton CreateNewRBTN;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.CheckBox PublicComputerCHBOX;
        private System.Windows.Forms.CheckBox SandboxModeCHBOX;
        private System.Windows.Forms.CheckBox RoamLocalOnExitCHBOX;
        private System.Windows.Forms.CheckBox RoamNewOnExitCHBOX;
        private System.Windows.Forms.PictureBox DownloadPBOX;
        private System.Windows.Forms.PictureBox LocalPBOX;
        private System.Windows.Forms.PictureBox NewPBOX;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel OptionsLINK;
    }
}