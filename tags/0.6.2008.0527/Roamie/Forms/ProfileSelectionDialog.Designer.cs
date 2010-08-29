﻿using Virtuoso.Miranda.Roamie.Forms.Controls;
namespace Virtuoso.Miranda.Roamie.Forms
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
            this.ProfilesLVIEW = new Virtuoso.Miranda.Roamie.Forms.Controls.ProfilesListView();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.ManageProfilesLBTN = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.IconPBOX = new System.Windows.Forms.PictureBox();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconPBOX)).BeginInit();
            this.SuspendLayout();
            // 
            // OkBTN
            // 
            this.OkBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkBTN.Enabled = false;
            this.OkBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OkBTN.Location = new System.Drawing.Point(16, 302);
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
            this.CancelBTN.Location = new System.Drawing.Point(97, 302);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 1;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // ProfilesLVIEW
            // 
            this.ProfilesLVIEW.DoNotMaskNewProfileItem = true;
            this.ProfilesLVIEW.HideSelection = false;
            this.ProfilesLVIEW.Location = new System.Drawing.Point(16, 73);
            this.ProfilesLVIEW.MultiSelect = false;
            this.ProfilesLVIEW.Name = "ProfilesLVIEW";
            this.ProfilesLVIEW.ShowInfoTips = true;
            this.ProfilesLVIEW.Size = new System.Drawing.Size(390, 219);
            this.ProfilesLVIEW.TabIndex = 3;
            this.ProfilesLVIEW.TileSize = new System.Drawing.Size(300, 40);
            this.ProfilesLVIEW.UseCompatibleStateImageBehavior = false;
            this.ProfilesLVIEW.View = System.Windows.Forms.View.Tile;
            this.ProfilesLVIEW.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProfilesLVIEW_MouseDoubleClick);
            this.ProfilesLVIEW.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ProfilesLVIEW_ItemSelectionChanged);
            this.ProfilesLVIEW.NewProfileRequested += new System.EventHandler(this.ProfilesLVIEW_NewProfileRequested);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.ManageProfilesLBTN);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Controls.Add(this.IconPBOX);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.YellowGreen;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(423, 86);
            this.gradientPanel1.TabIndex = 2;
            // 
            // ManageProfilesLBTN
            // 
            this.ManageProfilesLBTN.AutoSize = true;
            this.ManageProfilesLBTN.BackColor = System.Drawing.Color.Transparent;
            this.ManageProfilesLBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ManageProfilesLBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ManageProfilesLBTN.Location = new System.Drawing.Point(70, 44);
            this.ManageProfilesLBTN.Name = "ManageProfilesLBTN";
            this.ManageProfilesLBTN.Size = new System.Drawing.Size(95, 13);
            this.ManageProfilesLBTN.TabIndex = 1;
            this.ManageProfilesLBTN.TabStop = true;
            this.ManageProfilesLBTN.Text = "Manage profiles...";
            this.ManageProfilesLBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ManageProfilesLBTN_LinkClicked);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(70, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a roaming profile you wish to download or upload your database with.";
            // 
            // IconPBOX
            // 
            this.IconPBOX.BackColor = System.Drawing.Color.Transparent;
            this.IconPBOX.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Image_48x48_Get;
            this.IconPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IconPBOX.Location = new System.Drawing.Point(16, 9);
            this.IconPBOX.Name = "IconPBOX";
            this.IconPBOX.Size = new System.Drawing.Size(48, 48);
            this.IconPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconPBOX.TabIndex = 4;
            this.IconPBOX.TabStop = false;
            // 
            // ProfileSelectionDialog
            // 
            this.AcceptButton = this.OkBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBTN;
            this.ClientSize = new System.Drawing.Size(423, 336);
            this.Controls.Add(this.ProfilesLVIEW);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.gradientPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileSelectionDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a roaming profile...";
            this.Load += new System.EventHandler(this.RoamingProfileSelectionDialog_Load);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconPBOX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Button CancelBTN;
        private ProfilesListView ProfilesLVIEW;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.LinkLabel ManageProfilesLBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox IconPBOX;
    }
}