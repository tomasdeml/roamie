namespace Virtuoso.Roamie.Forms
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
            this.OptionsLINK = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Where do you want to load your profile from?";
            // 
            // OkBTN
            // 
            this.OkBTN.Enabled = false;
            this.OkBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OkBTN.Location = new System.Drawing.Point(12, 342);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 0;
            this.OkBTN.Text = "Continue";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // DownloadPBOX
            // 
            this.DownloadPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Web;
            this.DownloadPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DownloadPBOX.Location = new System.Drawing.Point(25, 119);
            this.DownloadPBOX.Name = "DownloadPBOX";
            this.DownloadPBOX.Size = new System.Drawing.Size(29, 32);
            this.DownloadPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DownloadPBOX.TabIndex = 16;
            this.DownloadPBOX.TabStop = false;
            // 
            // LocalPBOX
            // 
            this.LocalPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_ComputerScreen;
            this.LocalPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LocalPBOX.Location = new System.Drawing.Point(24, 206);
            this.LocalPBOX.Name = "LocalPBOX";
            this.LocalPBOX.Size = new System.Drawing.Size(32, 32);
            this.LocalPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LocalPBOX.TabIndex = 16;
            this.LocalPBOX.TabStop = false;
            // 
            // NewPBOX
            // 
            this.NewPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Add;
            this.NewPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NewPBOX.Location = new System.Drawing.Point(24, 272);
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
            this.UseLocalRBTN.Location = new System.Drawing.Point(68, 212);
            this.UseLocalRBTN.Name = "UseLocalRBTN";
            this.UseLocalRBTN.Size = new System.Drawing.Size(106, 17);
            this.UseLocalRBTN.TabIndex = 6;
            this.UseLocalRBTN.Text = "This computer";
            this.UseLocalRBTN.UseVisualStyleBackColor = true;
            this.UseLocalRBTN.CheckedChanged += new System.EventHandler(this.RadioBtn_Checked);
            // 
            // RoamNewOnExitCHBOX
            // 
            this.RoamNewOnExitCHBOX.AutoSize = true;            
            this.RoamNewOnExitCHBOX.CheckState = System.Windows.Forms.CheckState.Checked;            
            this.RoamNewOnExitCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoamNewOnExitCHBOX.Location = new System.Drawing.Point(88, 305);
            this.RoamNewOnExitCHBOX.Name = "RoamNewOnExitCHBOX";
            this.RoamNewOnExitCHBOX.Size = new System.Drawing.Size(118, 17);
            this.RoamNewOnExitCHBOX.TabIndex = 9;
            this.RoamNewOnExitCHBOX.Text = "Synchronize on exit";
            this.RoamNewOnExitCHBOX.UseVisualStyleBackColor = true;
            // 
            // RoamLocalOnExitCHBOX
            // 
            this.RoamLocalOnExitCHBOX.AutoSize = true;    
            this.RoamLocalOnExitCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoamLocalOnExitCHBOX.Location = new System.Drawing.Point(88, 238);
            this.RoamLocalOnExitCHBOX.Name = "RoamLocalOnExitCHBOX";
            this.RoamLocalOnExitCHBOX.Size = new System.Drawing.Size(118, 17);
            this.RoamLocalOnExitCHBOX.TabIndex = 7;
            this.RoamLocalOnExitCHBOX.Text = "Synchronize on exit";
            this.RoamLocalOnExitCHBOX.UseVisualStyleBackColor = true;
            // 
            // SandboxModeCHBOX
            // 
            this.SandboxModeCHBOX.AutoSize = true; 
            this.SandboxModeCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SandboxModeCHBOX.Location = new System.Drawing.Point(88, 178);
            this.SandboxModeCHBOX.Name = "SandboxModeCHBOX";
            this.SandboxModeCHBOX.Size = new System.Drawing.Size(192, 17);
            this.SandboxModeCHBOX.TabIndex = 5;
            this.SandboxModeCHBOX.Text = "Do not synchronize remote location";
            this.SandboxModeCHBOX.UseVisualStyleBackColor = false;
            // 
            // PublicComputerCHBOX
            // 
            this.PublicComputerCHBOX.AutoSize = true;            
            this.PublicComputerCHBOX.CheckState = System.Windows.Forms.CheckState.Checked;            
            this.PublicComputerCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PublicComputerCHBOX.Location = new System.Drawing.Point(88, 152);
            this.PublicComputerCHBOX.Name = "PublicComputerCHBOX";
            this.PublicComputerCHBOX.Size = new System.Drawing.Size(143, 17);
            this.PublicComputerCHBOX.TabIndex = 4;
            this.PublicComputerCHBOX.Text = "This is a public computer";
            this.PublicComputerCHBOX.UseVisualStyleBackColor = true;
            // 
            // CreateNewRBTN
            // 
            this.CreateNewRBTN.AutoSize = true;
            this.CreateNewRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.CreateNewRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CreateNewRBTN.Location = new System.Drawing.Point(68, 279);
            this.CreateNewRBTN.Name = "CreateNewRBTN";
            this.CreateNewRBTN.Size = new System.Drawing.Size(87, 17);
            this.CreateNewRBTN.TabIndex = 8;
            this.CreateNewRBTN.Text = "New profile";
            this.CreateNewRBTN.UseVisualStyleBackColor = true;
            this.CreateNewRBTN.CheckedChanged += new System.EventHandler(this.RadioBtn_Checked);
            // 
            // DownloadExistingRBTN
            // 
            this.DownloadExistingRBTN.AutoSize = true;
            this.DownloadExistingRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.DownloadExistingRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DownloadExistingRBTN.Location = new System.Drawing.Point(68, 126);
            this.DownloadExistingRBTN.Name = "DownloadExistingRBTN";
            this.DownloadExistingRBTN.Size = new System.Drawing.Size(206, 17);
            this.DownloadExistingRBTN.TabIndex = 3;
            this.DownloadExistingRBTN.Text = "Web or another remote location";
            this.DownloadExistingRBTN.UseVisualStyleBackColor = true;
            this.DownloadExistingRBTN.CheckedChanged += new System.EventHandler(this.RadioBtn_Checked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Virtuoso.Roamie.Properties.Resources.Watermark_Sync;
            this.pictureBox1.Location = new System.Drawing.Point(283, 187);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // OptionsLINK
            // 
            this.OptionsLINK.AutoSize = true;
            this.OptionsLINK.BackColor = System.Drawing.Color.Transparent;
            this.OptionsLINK.Location = new System.Drawing.Point(384, 98);
            this.OptionsLINK.Name = "OptionsLINK";
            this.OptionsLINK.Size = new System.Drawing.Size(43, 13);
            this.OptionsLINK.TabIndex = 22;
            this.OptionsLINK.TabStop = true;
            this.OptionsLINK.Text = "Options";
            this.OptionsLINK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OptionsLINK_LinkClicked);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(108, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Begin your session by selecting a Miranda profile source. \r\nRoamie will retrieve " +
                "and load the profile for you.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(21, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(108, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Welcome";
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = global::Virtuoso.Roamie.Properties.Resources.Header_Golden;
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(432, 84);
            this.gradientPanel1.TabIndex = 1;
            // 
            // StartupDialog
            // 
            this.AcceptButton = this.OkBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roamie for Miranda - ";
            this.Load += new System.EventHandler(this.StartupDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
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
        private System.Windows.Forms.LinkLabel OptionsLINK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
    }
}