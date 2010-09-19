namespace Virtuoso.Roamie.Forms
{
    partial class RoamingOverviewDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoamingOverviewDialog));
            this.SyncActionCHBOX = new System.Windows.Forms.CheckBox();
            this.PublicModeCHBOX = new System.Windows.Forms.CheckBox();
            this.RoamingProfileLBTN = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PreferFullSyncCHBOX = new System.Windows.Forms.CheckBox();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SyncStatusPBOX = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.categoryItemSection1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MoreOptionsLINK = new System.Windows.Forms.LinkLabel();
            this.ThisComputerOverlayPBOX = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SyncStatusPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThisComputerOverlayPBOX)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SyncActionCHBOX
            // 
            this.SyncActionCHBOX.AutoSize = true;
            this.SyncActionCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.SyncActionCHBOX.Location = new System.Drawing.Point(23, 179);
            this.SyncActionCHBOX.Name = "SyncActionCHBOX";
            this.SyncActionCHBOX.Size = new System.Drawing.Size(145, 17);
            this.SyncActionCHBOX.TabIndex = 8;
            this.SyncActionCHBOX.Text = "Synchronize changes";
            this.SyncActionCHBOX.UseVisualStyleBackColor = true;
            this.SyncActionCHBOX.CheckedChanged += new System.EventHandler(this.SyncActionCHBOX_CheckedChanged);
            // 
            // PublicModeCHBOX
            // 
            this.PublicModeCHBOX.AutoSize = true;
            this.PublicModeCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.PublicModeCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PublicModeCHBOX.Location = new System.Drawing.Point(23, 220);
            this.PublicModeCHBOX.Name = "PublicModeCHBOX";
            this.PublicModeCHBOX.Size = new System.Drawing.Size(165, 17);
            this.PublicModeCHBOX.TabIndex = 9;
            this.PublicModeCHBOX.Text = "This is a public computer";
            this.PublicModeCHBOX.UseVisualStyleBackColor = true;
            this.PublicModeCHBOX.CheckedChanged += new System.EventHandler(this.PublicModeCHBOX_CheckedChanged);
            // 
            // RoamingProfileLBTN
            // 
            this.RoamingProfileLBTN.AutoSize = true;
            this.RoamingProfileLBTN.BackColor = System.Drawing.Color.Transparent;
            this.RoamingProfileLBTN.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.RoamingProfileLBTN.Location = new System.Drawing.Point(283, 110);
            this.RoamingProfileLBTN.Name = "RoamingProfileLBTN";
            this.RoamingProfileLBTN.Size = new System.Drawing.Size(74, 13);
            this.RoamingProfileLBTN.TabIndex = 6;
            this.RoamingProfileLBTN.Text = "(Profile detail)";
            this.RoamingProfileLBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RoamingProfileLBTN_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(332, 182);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // PreferFullSyncCHBOX
            // 
            this.PreferFullSyncCHBOX.AutoSize = true;
            this.PreferFullSyncCHBOX.Enabled = false;
            this.PreferFullSyncCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.PreferFullSyncCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PreferFullSyncCHBOX.Location = new System.Drawing.Point(23, 274);
            this.PreferFullSyncCHBOX.Name = "PreferFullSyncCHBOX";
            this.PreferFullSyncCHBOX.Size = new System.Drawing.Size(185, 17);
            this.PreferFullSyncCHBOX.TabIndex = 9;
            this.PreferFullSyncCHBOX.Text = "Perform full synchronization";
            this.PreferFullSyncCHBOX.UseVisualStyleBackColor = true;
            this.PreferFullSyncCHBOX.CheckedChanged += new System.EventHandler(this.ForceFullSyncCHBOX_CheckedChanged);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gradientPanel1.BackgroundImage")));
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(481, 45);
            this.gradientPanel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "View your roaming status and details";
            // 
            // SyncStatusPBOX
            // 
            this.SyncStatusPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_SyncDelta;
            this.SyncStatusPBOX.Location = new System.Drawing.Point(222, 68);
            this.SyncStatusPBOX.Name = "SyncStatusPBOX";
            this.SyncStatusPBOX.Size = new System.Drawing.Size(32, 32);
            this.SyncStatusPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SyncStatusPBOX.TabIndex = 14;
            this.SyncStatusPBOX.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Virtuoso.Roamie.Properties.Resources.Image_48x48_Web;
            this.pictureBox5.Location = new System.Drawing.Point(284, 57);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 48);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 14;
            this.pictureBox5.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "This computer";
            // 
            // categoryItemSection1
            // 
            this.categoryItemSection1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemSection1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(226)))), ((int)(((byte)(157)))));
            this.categoryItemSection1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.categoryItemSection1.ForeColor = System.Drawing.Color.Black;
            this.categoryItemSection1.Location = new System.Drawing.Point(10, 146);
            this.categoryItemSection1.MinimumSize = new System.Drawing.Size(300, 20);
            this.categoryItemSection1.Name = "categoryItemSection1";
            this.categoryItemSection1.SectionName = "Change current roaming settings";
            this.categoryItemSection1.Size = new System.Drawing.Size(459, 20);
            this.categoryItemSection1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(39, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(384, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "If checked, local changes will be synchronized with your remote profile on exit.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(39, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 26);
            this.label2.TabIndex = 17;
            this.label2.Text = "If checked, local copy of your Miranda profile will be deleted \r\nfrom this comput" +
                "er on exit.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(39, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(305, 26);
            this.label5.TabIndex = 17;
            this.label5.Text = "If checked, your entire profile file will be synchronized on exit.\r\nOtherwise onl" +
                "y changes made will be synchronized.";
            // 
            // MoreOptionsLINK
            // 
            this.MoreOptionsLINK.AutoSize = true;
            this.MoreOptionsLINK.Location = new System.Drawing.Point(20, 341);
            this.MoreOptionsLINK.Name = "MoreOptionsLINK";
            this.MoreOptionsLINK.Size = new System.Drawing.Size(69, 13);
            this.MoreOptionsLINK.TabIndex = 18;
            this.MoreOptionsLINK.TabStop = true;
            this.MoreOptionsLINK.Text = "More options";
            this.MoreOptionsLINK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MoreOptionsLINK_LinkClicked);
            // 
            // ThisComputerOverlayPBOX
            // 
            this.ThisComputerOverlayPBOX.BackColor = System.Drawing.Color.Transparent;
            this.ThisComputerOverlayPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_16x16_Delete;
            this.ThisComputerOverlayPBOX.Location = new System.Drawing.Point(34, 33);
            this.ThisComputerOverlayPBOX.Name = "ThisComputerOverlayPBOX";
            this.ThisComputerOverlayPBOX.Size = new System.Drawing.Size(16, 16);
            this.ThisComputerOverlayPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ThisComputerOverlayPBOX.TabIndex = 19;
            this.ThisComputerOverlayPBOX.TabStop = false;
            this.ThisComputerOverlayPBOX.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Virtuoso.Roamie.Properties.Resources.Image_48x48_Computer;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.ThisComputerOverlayPBOX);
            this.panel1.Location = new System.Drawing.Point(143, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(58, 59);
            this.panel1.TabIndex = 21;
            // 
            // RoamingOverviewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(481, 372);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MoreOptionsLINK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.categoryItemSection1);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.SyncStatusPBOX);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.RoamingProfileLBTN);
            this.Controls.Add(this.PreferFullSyncCHBOX);
            this.Controls.Add(this.PublicModeCHBOX);
            this.Controls.Add(this.SyncActionCHBOX);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RoamingOverviewDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roaming overview";
            this.Load += new System.EventHandler(this.RoamingStatusDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SyncStatusPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThisComputerOverlayPBOX)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox SyncActionCHBOX;
        private System.Windows.Forms.CheckBox PublicModeCHBOX;
        private System.Windows.Forms.LinkLabel RoamingProfileLBTN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox PreferFullSyncCHBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox SyncStatusPBOX;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label3;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection categoryItemSection1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel MoreOptionsLINK;
        private System.Windows.Forms.PictureBox ThisComputerOverlayPBOX;
        private System.Windows.Forms.Panel panel1;
    }
}