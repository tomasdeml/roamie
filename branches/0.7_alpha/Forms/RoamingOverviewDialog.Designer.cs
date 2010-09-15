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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoamingOverviewDialog));
            this.label2 = new System.Windows.Forms.Label();
            this.SyncActionCHBOX = new System.Windows.Forms.CheckBox();
            this.PublicModeCHBOX = new System.Windows.Forms.CheckBox();
            this.RoamingProfileLBTN = new System.Windows.Forms.LinkLabel();
            this.OkBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OptionsBTN = new System.Windows.Forms.Button();
            this.StatusPBOX = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.StatusLVIEW = new System.Windows.Forms.ListView();
            this.DescriptionCOLUMN = new System.Windows.Forms.ColumnHeader();
            this.PreferFullSyncCHBOX = new System.Windows.Forms.CheckBox();
            this.ForceFullSyncHBTN = new System.Windows.Forms.PictureBox();
            this.HelpTIP = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ForceFullSyncHBTN)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(50, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "On exit";
            // 
            // SyncActionCHBOX
            // 
            this.SyncActionCHBOX.AutoSize = true;
            this.SyncActionCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SyncActionCHBOX.Location = new System.Drawing.Point(54, 272);
            this.SyncActionCHBOX.Name = "SyncActionCHBOX";
            this.SyncActionCHBOX.Size = new System.Drawing.Size(196, 17);
            this.SyncActionCHBOX.TabIndex = 8;
            this.SyncActionCHBOX.Text = "Synchronize remote database";
            this.SyncActionCHBOX.UseVisualStyleBackColor = true;
            this.SyncActionCHBOX.CheckedChanged += new System.EventHandler(this.OnExitCHBOX_CheckedChanged);
            // 
            // PublicModeCHBOX
            // 
            this.PublicModeCHBOX.AutoSize = true;
            this.PublicModeCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PublicModeCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PublicModeCHBOX.Location = new System.Drawing.Point(54, 295);
            this.PublicModeCHBOX.Name = "PublicModeCHBOX";
            this.PublicModeCHBOX.Size = new System.Drawing.Size(200, 17);
            this.PublicModeCHBOX.TabIndex = 9;
            this.PublicModeCHBOX.Text = "Remove downloaded database";
            this.PublicModeCHBOX.UseVisualStyleBackColor = true;
            this.PublicModeCHBOX.CheckedChanged += new System.EventHandler(this.PublicModeCHBOX_CheckedChanged);
            // 
            // RoamingProfileLBTN
            // 
            this.RoamingProfileLBTN.AutoSize = true;
            this.RoamingProfileLBTN.BackColor = System.Drawing.Color.Transparent;
            this.RoamingProfileLBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RoamingProfileLBTN.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.RoamingProfileLBTN.Location = new System.Drawing.Point(87, 202);
            this.RoamingProfileLBTN.Name = "RoamingProfileLBTN";
            this.RoamingProfileLBTN.Size = new System.Drawing.Size(49, 13);
            this.RoamingProfileLBTN.TabIndex = 6;
            this.RoamingProfileLBTN.Text = "(name)";
            this.RoamingProfileLBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RoamingProfileLBTN_LinkClicked);
            // 
            // OkBTN
            // 
            this.OkBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkBTN.Location = new System.Drawing.Point(12, 360);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(119, 23);
            this.OkBTN.TabIndex = 0;
            this.OkBTN.Text = "Save changes";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBTN.Location = new System.Drawing.Point(137, 360);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 1;
            this.CancelBTN.Text = "Close";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(434, 206);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // OptionsBTN
            // 
            this.OptionsBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsBTN.BackColor = System.Drawing.Color.Transparent;
            this.OptionsBTN.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.OptionsBTN.FlatAppearance.BorderSize = 3;
            this.OptionsBTN.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OptionsBTN.Font = new System.Drawing.Font("Tahoma", 8F);
            this.OptionsBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptionsBTN.Location = new System.Drawing.Point(496, 360);
            this.OptionsBTN.Name = "OptionsBTN";
            this.OptionsBTN.Size = new System.Drawing.Size(75, 23);
            this.OptionsBTN.TabIndex = 2;
            this.OptionsBTN.Text = "Options";
            this.OptionsBTN.UseVisualStyleBackColor = false;
            this.OptionsBTN.Click += new System.EventHandler(this.OptionsBTN_Click);
            // 
            // StatusPBOX
            // 
            this.StatusPBOX.BackColor = System.Drawing.Color.Transparent;
            this.StatusPBOX.Location = new System.Drawing.Point(12, 12);
            this.StatusPBOX.Name = "StatusPBOX";
            this.StatusPBOX.Size = new System.Drawing.Size(32, 32);
            this.StatusPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.StatusPBOX.TabIndex = 11;
            this.StatusPBOX.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(50, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Roaming status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(50, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Active profile";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Profile;
            this.pictureBox4.Location = new System.Drawing.Point(54, 192);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(27, 32);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // StatusLVIEW
            // 
            this.StatusLVIEW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLVIEW.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DescriptionCOLUMN});
            this.StatusLVIEW.GridLines = true;
            this.StatusLVIEW.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.StatusLVIEW.Location = new System.Drawing.Point(54, 44);
            this.StatusLVIEW.MultiSelect = false;
            this.StatusLVIEW.Name = "StatusLVIEW";
            this.StatusLVIEW.ShowItemToolTips = true;
            this.StatusLVIEW.Size = new System.Drawing.Size(517, 118);
            this.StatusLVIEW.TabIndex = 4;
            this.StatusLVIEW.UseCompatibleStateImageBehavior = false;
            this.StatusLVIEW.View = System.Windows.Forms.View.Details;
            // 
            // DescriptionCOLUMN
            // 
            this.DescriptionCOLUMN.Text = "Description";
            this.DescriptionCOLUMN.Width = 500;
            // 
            // PreferFullSyncCHBOX
            // 
            this.PreferFullSyncCHBOX.AutoSize = true;
            this.PreferFullSyncCHBOX.Enabled = false;
            this.PreferFullSyncCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PreferFullSyncCHBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PreferFullSyncCHBOX.Location = new System.Drawing.Point(54, 318);
            this.PreferFullSyncCHBOX.Name = "PreferFullSyncCHBOX";
            this.PreferFullSyncCHBOX.Size = new System.Drawing.Size(174, 17);
            this.PreferFullSyncCHBOX.TabIndex = 9;
            this.PreferFullSyncCHBOX.Text = "Prefer full synchronization";
            this.PreferFullSyncCHBOX.UseVisualStyleBackColor = true;
            this.PreferFullSyncCHBOX.CheckedChanged += new System.EventHandler(this.ForceFullSyncCHBOX_CheckedChanged);
            // 
            // ForceFullSyncHBTN
            // 
            this.ForceFullSyncHBTN.Cursor = System.Windows.Forms.Cursors.Help;
            this.ForceFullSyncHBTN.Image = global::Virtuoso.Roamie.Properties.Resources.Icon_228_16x16;
            this.ForceFullSyncHBTN.Location = new System.Drawing.Point(234, 318);
            this.ForceFullSyncHBTN.Name = "ForceFullSyncHBTN";
            this.ForceFullSyncHBTN.Size = new System.Drawing.Size(16, 16);
            this.ForceFullSyncHBTN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ForceFullSyncHBTN.TabIndex = 12;
            this.ForceFullSyncHBTN.TabStop = false;
            this.HelpTIP.SetToolTip(this.ForceFullSyncHBTN, "If checked, Roamie will prefer full synchronization.\r\nUse when it takes a long ti" +
                    "me to apply deltas.\r\n\r\nAll deltas will be merged with your database and deleted." +
                    "");
            // 
            // HelpTIP
            // 
            this.HelpTIP.AutoPopDelay = 15000;
            this.HelpTIP.InitialDelay = 100;
            this.HelpTIP.ReshowDelay = 100;
            this.HelpTIP.ToolTipTitle = "Hint";
            // 
            // RoamingOverviewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 396);
            this.Controls.Add(this.ForceFullSyncHBTN);
            this.Controls.Add(this.StatusLVIEW);
            this.Controls.Add(this.OptionsBTN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.StatusPBOX);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.RoamingProfileLBTN);
            this.Controls.Add(this.PreferFullSyncCHBOX);
            this.Controls.Add(this.PublicModeCHBOX);
            this.Controls.Add(this.SyncActionCHBOX);
            this.Controls.Add(this.label2);
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
            ((System.ComponentModel.ISupportInitialize)(this.StatusPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ForceFullSyncHBTN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox SyncActionCHBOX;
        private System.Windows.Forms.CheckBox PublicModeCHBOX;
        private System.Windows.Forms.LinkLabel RoamingProfileLBTN;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox StatusPBOX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button OptionsBTN;
        private System.Windows.Forms.ListView StatusLVIEW;
        private System.Windows.Forms.ColumnHeader DescriptionCOLUMN;
        private System.Windows.Forms.CheckBox PreferFullSyncCHBOX;
        private System.Windows.Forms.PictureBox ForceFullSyncHBTN;
        private System.Windows.Forms.ToolTip HelpTIP;
    }
}