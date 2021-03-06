﻿using Virtuoso.Roamie.Forms.Controls;

namespace Virtuoso.Roamie.Forms
{
    partial class ProfileSelectionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileSelectionDialog));
            this.OkBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IconPBOX = new System.Windows.Forms.PictureBox();
            this.ManageProfilesLBTN = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblProfileOverwriteWarning = new System.Windows.Forms.Label();
            this.ProfilesLVIEW = new Virtuoso.Roamie.Forms.Controls.ProfilesListView();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconPBOX)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkBTN
            // 
            this.OkBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkBTN.Enabled = false;
            this.OkBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OkBTN.Location = new System.Drawing.Point(16, 305);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 0;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CancelBTN.Location = new System.Drawing.Point(97, 305);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 1;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gradientPanel1.BackgroundImage")));
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Controls.Add(this.IconPBOX);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(423, 86);
            this.gradientPanel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(108, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Select a roaming profile";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(108, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Roamie will synchronize this computer with the selected roaming profile.";
            // 
            // IconPBOX
            // 
            this.IconPBOX.BackColor = System.Drawing.Color.Transparent;
            this.IconPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_48x48_Woman;
            this.IconPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IconPBOX.Location = new System.Drawing.Point(21, 14);
            this.IconPBOX.Name = "IconPBOX";
            this.IconPBOX.Size = new System.Drawing.Size(48, 48);
            this.IconPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconPBOX.TabIndex = 4;
            this.IconPBOX.TabStop = false;
            // 
            // ManageProfilesLBTN
            // 
            this.ManageProfilesLBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ManageProfilesLBTN.AutoSize = true;
            this.ManageProfilesLBTN.BackColor = System.Drawing.Color.Transparent;
            this.ManageProfilesLBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ManageProfilesLBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ManageProfilesLBTN.Location = new System.Drawing.Point(323, 310);
            this.ManageProfilesLBTN.Name = "ManageProfilesLBTN";
            this.ManageProfilesLBTN.Size = new System.Drawing.Size(82, 13);
            this.ManageProfilesLBTN.TabIndex = 1;
            this.ManageProfilesLBTN.TabStop = true;
            this.ManageProfilesLBTN.Text = "Manage profiles";
            this.ManageProfilesLBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ManageProfilesLBTN_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblProfileOverwriteWarning);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 25);
            this.panel1.TabIndex = 4;
            // 
            // lblProfileOverwriteWarning
            // 
            this.lblProfileOverwriteWarning.AutoEllipsis = true;
            this.lblProfileOverwriteWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(136)))));
            this.lblProfileOverwriteWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfileOverwriteWarning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblProfileOverwriteWarning.Location = new System.Drawing.Point(0, 0);
            this.lblProfileOverwriteWarning.Name = "lblProfileOverwriteWarning";
            this.lblProfileOverwriteWarning.Size = new System.Drawing.Size(421, 23);
            this.lblProfileOverwriteWarning.TabIndex = 0;
            this.lblProfileOverwriteWarning.Text = "Be careful";
            this.lblProfileOverwriteWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProfilesLVIEW
            // 
            this.ProfilesLVIEW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfilesLVIEW.DoNotMaskNewProfileItem = true;
            this.ProfilesLVIEW.HideSelection = false;
            this.ProfilesLVIEW.Location = new System.Drawing.Point(16, 124);
            this.ProfilesLVIEW.MultiSelect = false;
            this.ProfilesLVIEW.Name = "ProfilesLVIEW";
            this.ProfilesLVIEW.ShowInfoTips = true;
            this.ProfilesLVIEW.Size = new System.Drawing.Size(390, 171);
            this.ProfilesLVIEW.TabIndex = 3;
            this.ProfilesLVIEW.TileSize = new System.Drawing.Size(300, 40);
            this.ProfilesLVIEW.UseCompatibleStateImageBehavior = false;
            this.ProfilesLVIEW.View = System.Windows.Forms.View.Tile;
            this.ProfilesLVIEW.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProfilesLVIEW_MouseDoubleClick);
            this.ProfilesLVIEW.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ProfilesLVIEW_ItemSelectionChanged);
            this.ProfilesLVIEW.NewProfileRequested += new System.EventHandler(this.ProfilesLVIEW_NewProfileRequested);
            // 
            // ProfileSelectionDialog
            // 
            this.AcceptButton = this.OkBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBTN;
            this.ClientSize = new System.Drawing.Size(423, 339);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ManageProfilesLBTN);
            this.Controls.Add(this.ProfilesLVIEW);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileSelectionDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Roaming profile selection";
            this.Load += new System.EventHandler(this.RoamingProfileSelectionDialog_Load);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconPBOX)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Button CancelBTN;
        private ProfilesListView ProfilesLVIEW;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.LinkLabel ManageProfilesLBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox IconPBOX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblProfileOverwriteWarning;
    }
}