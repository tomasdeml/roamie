namespace Virtuoso.Roamie.Forms.Controls.Configuration
{
    partial class AutoProfileOptions
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
            this.UseDefaultProfileCHBOX = new System.Windows.Forms.CheckBox();
            this.ProfileLBOX = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DoNotPublishCHKBOX = new System.Windows.Forms.CheckBox();
            this.RemoveOnExitCHKBOX = new System.Windows.Forms.CheckBox();
            this.LocalRBTN = new System.Windows.Forms.RadioButton();
            this.DownloadRBTN = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.categoryItemSection1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection();
            this.label3 = new System.Windows.Forms.Label();
            this.DefaultProfileSECTION = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection();
            this.SettingsGBOX = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.categoryItemHeader1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SettingsGBOX.SuspendLayout();
            this.SuspendLayout();
            // 
            // UseDefaultProfileCHBOX
            // 
            this.UseDefaultProfileCHBOX.AutoSize = true;
            this.UseDefaultProfileCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.UseDefaultProfileCHBOX.Location = new System.Drawing.Point(21, 75);
            this.UseDefaultProfileCHBOX.Name = "UseDefaultProfileCHBOX";
            this.UseDefaultProfileCHBOX.Size = new System.Drawing.Size(260, 17);
            this.UseDefaultProfileCHBOX.TabIndex = 2;
            this.UseDefaultProfileCHBOX.Text = "Use a specific profile when Miranda starts";
            this.UseDefaultProfileCHBOX.UseVisualStyleBackColor = true;
            this.UseDefaultProfileCHBOX.CheckedChanged += new System.EventHandler(this.EnableDefaultProfileCHBOX_CheckedChanged);
            // 
            // ProfileLBOX
            // 
            this.ProfileLBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProfileLBOX.FormattingEnabled = true;
            this.ProfileLBOX.Location = new System.Drawing.Point(6, 28);
            this.ProfileLBOX.Name = "ProfileLBOX";
            this.ProfileLBOX.Size = new System.Drawing.Size(254, 21);
            this.ProfileLBOX.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile";
            // 
            // DoNotPublishCHKBOX
            // 
            this.DoNotPublishCHKBOX.AutoSize = true;
            this.DoNotPublishCHKBOX.Location = new System.Drawing.Point(275, 79);
            this.DoNotPublishCHKBOX.Name = "DoNotPublishCHKBOX";
            this.DoNotPublishCHKBOX.Size = new System.Drawing.Size(137, 17);
            this.DoNotPublishCHKBOX.TabIndex = 6;
            this.DoNotPublishCHKBOX.Text = "Do not publish changes";
            this.DoNotPublishCHKBOX.UseVisualStyleBackColor = true;
            // 
            // RemoveOnExitCHKBOX
            // 
            this.RemoveOnExitCHKBOX.AutoSize = true;
            this.RemoveOnExitCHKBOX.Location = new System.Drawing.Point(275, 102);
            this.RemoveOnExitCHKBOX.Name = "RemoveOnExitCHKBOX";
            this.RemoveOnExitCHKBOX.Size = new System.Drawing.Size(168, 17);
            this.RemoveOnExitCHKBOX.TabIndex = 7;
            this.RemoveOnExitCHKBOX.Text = "Remove the database on exit";
            this.RemoveOnExitCHKBOX.UseVisualStyleBackColor = true;
            // 
            // LocalRBTN
            // 
            this.LocalRBTN.AutoSize = true;
            this.LocalRBTN.Location = new System.Drawing.Point(6, 101);
            this.LocalRBTN.Name = "LocalRBTN";
            this.LocalRBTN.Size = new System.Drawing.Size(115, 17);
            this.LocalRBTN.TabIndex = 4;
            this.LocalRBTN.TabStop = true;
            this.LocalRBTN.Text = "Use local database";
            this.LocalRBTN.UseVisualStyleBackColor = true;
            this.LocalRBTN.CheckedChanged += new System.EventHandler(this.StartupOptionControl_CheckedChanged);
            // 
            // DownloadRBTN
            // 
            this.DownloadRBTN.AutoSize = true;
            this.DownloadRBTN.Checked = true;
            this.DownloadRBTN.Location = new System.Drawing.Point(6, 78);
            this.DownloadRBTN.Name = "DownloadRBTN";
            this.DownloadRBTN.Size = new System.Drawing.Size(175, 17);
            this.DownloadRBTN.TabIndex = 3;
            this.DownloadRBTN.TabStop = true;
            this.DownloadRBTN.Text = "Download an existing database";
            this.DownloadRBTN.UseVisualStyleBackColor = true;
            this.DownloadRBTN.CheckedChanged += new System.EventHandler(this.StartupOptionControl_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Virtuoso.Roamie.Properties.Resources.Watermark_Sync;
            this.pictureBox1.Location = new System.Drawing.Point(643, 210);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // categoryItemSection1
            // 
            this.categoryItemSection1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemSection1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.categoryItemSection1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryItemSection1.ForeColor = System.Drawing.Color.Black;
            this.categoryItemSection1.Location = new System.Drawing.Point(10, 49);
            this.categoryItemSection1.MinimumSize = new System.Drawing.Size(300, 20);
            this.categoryItemSection1.Name = "categoryItemSection1";
            this.categoryItemSection1.SectionName = "Profile selection options";
            this.categoryItemSection1.Size = new System.Drawing.Size(765, 20);
            this.categoryItemSection1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(428, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "When enabled, Roamie will activate a specific profile automatically when Miranda " +
                "starts.";
            // 
            // DefaultProfileSECTION
            // 
            this.DefaultProfileSECTION.BackColor = System.Drawing.Color.Transparent;
            this.DefaultProfileSECTION.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.DefaultProfileSECTION.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DefaultProfileSECTION.ForeColor = System.Drawing.Color.Black;
            this.DefaultProfileSECTION.Location = new System.Drawing.Point(10, 131);
            this.DefaultProfileSECTION.MinimumSize = new System.Drawing.Size(300, 20);
            this.DefaultProfileSECTION.Name = "DefaultProfileSECTION";
            this.DefaultProfileSECTION.SectionName = "Default profile selection";
            this.DefaultProfileSECTION.Size = new System.Drawing.Size(765, 20);
            this.DefaultProfileSECTION.TabIndex = 4;
            // 
            // SettingsGBOX
            // 
            this.SettingsGBOX.BackColor = System.Drawing.Color.Transparent;
            this.SettingsGBOX.Controls.Add(this.ProfileLBOX);
            this.SettingsGBOX.Controls.Add(this.DoNotPublishCHKBOX);
            this.SettingsGBOX.Controls.Add(this.RemoveOnExitCHKBOX);
            this.SettingsGBOX.Controls.Add(this.label1);
            this.SettingsGBOX.Controls.Add(this.LocalRBTN);
            this.SettingsGBOX.Controls.Add(this.DownloadRBTN);
            this.SettingsGBOX.Controls.Add(this.label4);
            this.SettingsGBOX.Controls.Add(this.label5);
            this.SettingsGBOX.Location = new System.Drawing.Point(20, 157);
            this.SettingsGBOX.Name = "SettingsGBOX";
            this.SettingsGBOX.Size = new System.Drawing.Size(593, 149);
            this.SettingsGBOX.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(3, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Action";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(272, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "On exit";
            // 
            // categoryItemHeader1
            // 
            this.categoryItemHeader1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemHeader1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.categoryItemHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryItemHeader1.HeaderText = "Configure Roamie to automatically select a profile";
            this.categoryItemHeader1.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Favourite;
            this.categoryItemHeader1.Location = new System.Drawing.Point(0, 0);
            this.categoryItemHeader1.MinimumSize = new System.Drawing.Size(300, 40);
            this.categoryItemHeader1.Name = "categoryItemHeader1";
            this.categoryItemHeader1.Size = new System.Drawing.Size(792, 40);
            this.categoryItemHeader1.TabIndex = 0;
            // 
            // AutoProfileOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingsGBOX);
            this.Controls.Add(this.DefaultProfileSECTION);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.categoryItemSection1);
            this.Controls.Add(this.categoryItemHeader1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.UseDefaultProfileCHBOX);
            this.Name = "AutoProfileOptions";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.SettingsGBOX.ResumeLayout(false);
            this.SettingsGBOX.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox UseDefaultProfileCHBOX;
        private System.Windows.Forms.ComboBox ProfileLBOX;
        private System.Windows.Forms.CheckBox RemoveOnExitCHKBOX;
        private System.Windows.Forms.RadioButton LocalRBTN;
        private System.Windows.Forms.RadioButton DownloadRBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox DoNotPublishCHKBOX;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection categoryItemSection1;
        private System.Windows.Forms.Label label3;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection DefaultProfileSECTION;
        private System.Windows.Forms.Panel SettingsGBOX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader categoryItemHeader1;
    }
}
