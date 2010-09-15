namespace Virtuoso.Roamie.Forms.Controls.Configuration
{
    partial class ProfileManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileManagement));
            this.ProfilesLVIEW = new ProfilesListView();
            this.ProfilesCMENU = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProfileBTN = new Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton();
            this.EditProfileBTN = new Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.categoryItemHeader1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader();
            this.ProfilesCMENU.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProfilesLVIEW
            // 
            this.ProfilesLVIEW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfilesLVIEW.ContextMenuStrip = this.ProfilesCMENU;
            this.ProfilesLVIEW.HideSelection = false;
            this.ProfilesLVIEW.Location = new System.Drawing.Point(13, 89);
            this.ProfilesLVIEW.MultiSelect = false;
            this.ProfilesLVIEW.Name = "ProfilesLVIEW";
            this.ProfilesLVIEW.Size = new System.Drawing.Size(641, 294);
            this.ProfilesLVIEW.TabIndex = 2;
            this.ProfilesLVIEW.TileSize = new System.Drawing.Size(300, 40);
            this.ProfilesLVIEW.UseCompatibleStateImageBehavior = false;
            this.ProfilesLVIEW.View = System.Windows.Forms.View.Tile;
            this.ProfilesLVIEW.DoubleClick += new System.EventHandler(this.ProfilesLVIEW_DoubleClick);
            this.ProfilesLVIEW.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ProfilesLVIEW_ItemSelectionChanged);
            // 
            // ProfilesCMENU
            // 
            this.ProfilesCMENU.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ProfilesCMENU.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditProfileToolStripMenuItem,
            this.toolStripSeparator2,
            this.ViewProfileToolStripMenuItem,
            this.TestToolStripMenuItem,
            this.DeleteProfileToolStripMenuItem});
            this.ProfilesCMENU.Name = "ProfilesCMENU";
            this.ProfilesCMENU.Size = new System.Drawing.Size(151, 98);
            this.ProfilesCMENU.Opening += new System.ComponentModel.CancelEventHandler(this.ProfilesCMENU_Opening);
            // 
            // EditProfileToolStripMenuItem
            // 
            this.EditProfileToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.EditProfileToolStripMenuItem.Image = global::Virtuoso.Roamie.Properties.Resources.Icon_193;
            this.EditProfileToolStripMenuItem.Name = "EditProfileToolStripMenuItem";
            this.EditProfileToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.EditProfileToolStripMenuItem.Text = "&Edit";
            this.EditProfileToolStripMenuItem.Click += new System.EventHandler(this.ProfilesCMENU_ItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // ViewProfileToolStripMenuItem
            // 
            this.ViewProfileToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ViewProfileToolStripMenuItem.Image = global::Virtuoso.Roamie.Properties.Resources.Icon_292;
            this.ViewProfileToolStripMenuItem.Name = "ViewProfileToolStripMenuItem";
            this.ViewProfileToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.ViewProfileToolStripMenuItem.Text = "&Show details";
            this.ViewProfileToolStripMenuItem.Click += new System.EventHandler(this.ProfilesCMENU_ItemClick);
            // 
            // TestToolStripMenuItem
            // 
            this.TestToolStripMenuItem.Image = global::Virtuoso.Roamie.Properties.Resources.Icon_237;
            this.TestToolStripMenuItem.Name = "TestToolStripMenuItem";
            this.TestToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.TestToolStripMenuItem.Text = "&Test connection";
            this.TestToolStripMenuItem.Click += new System.EventHandler(this.ProfilesCMENU_ItemClick);
            // 
            // DeleteProfileToolStripMenuItem
            // 
            this.DeleteProfileToolStripMenuItem.Image = global::Virtuoso.Roamie.Properties.Resources.CbtnIcon_Delete;
            this.DeleteProfileToolStripMenuItem.Name = "DeleteProfileToolStripMenuItem";
            this.DeleteProfileToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.DeleteProfileToolStripMenuItem.Text = "&Delete";
            this.DeleteProfileToolStripMenuItem.Click += new System.EventHandler(this.ProfilesCMENU_ItemClick);
            // 
            // DeleteProfileBTN
            // 
            this.DeleteProfileBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteProfileBTN.Enabled = false;
            this.DeleteProfileBTN.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.DeleteProfileBTN.FlatAppearance.BorderSize = 3;
            this.DeleteProfileBTN.Image = ((System.Drawing.Image)(resources.GetObject("DeleteProfileBTN.Image")));
            this.DeleteProfileBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteProfileBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DeleteProfileBTN.Location = new System.Drawing.Point(682, 136);
            this.DeleteProfileBTN.Name = "DeleteProfileBTN";
            this.DeleteProfileBTN.Size = new System.Drawing.Size(83, 41);
            this.DeleteProfileBTN.TabIndex = 4;
            this.DeleteProfileBTN.Text = " Delete";
            this.DeleteProfileBTN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteProfileBTN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteProfileBTN.UseVisualStyleBackColor = true;
            this.DeleteProfileBTN.Click += new System.EventHandler(this.DeleteProfileBTN_Click);
            // 
            // EditProfileBTN
            // 
            this.EditProfileBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditProfileBTN.Enabled = false;
            this.EditProfileBTN.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.EditProfileBTN.FlatAppearance.BorderSize = 3;
            this.EditProfileBTN.Image = ((System.Drawing.Image)(resources.GetObject("EditProfileBTN.Image")));
            this.EditProfileBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EditProfileBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EditProfileBTN.Location = new System.Drawing.Point(682, 89);
            this.EditProfileBTN.Name = "EditProfileBTN";
            this.EditProfileBTN.Size = new System.Drawing.Size(83, 41);
            this.EditProfileBTN.TabIndex = 3;
            this.EditProfileBTN.Text = "  Edit";
            this.EditProfileBTN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EditProfileBTN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditProfileBTN.UseVisualStyleBackColor = true;
            this.EditProfileBTN.Click += new System.EventHandler(this.EditProfileBTN_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(10, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(644, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a profile below and click the Edit button to modify its settings or click " +
                "the Delete button to remove it. For more options, right click an item.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Virtuoso.Roamie.Properties.Resources.Watermark_Profiles;
            this.pictureBox1.Location = new System.Drawing.Point(627, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(165, 220);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // categoryItemHeader1
            // 
            this.categoryItemHeader1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemHeader1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.categoryItemHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryItemHeader1.HeaderText = "Manage your roaming profiles";
            this.categoryItemHeader1.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Profile;
            this.categoryItemHeader1.Location = new System.Drawing.Point(0, 0);
            this.categoryItemHeader1.MinimumSize = new System.Drawing.Size(300, 40);
            this.categoryItemHeader1.Name = "categoryItemHeader1";
            this.categoryItemHeader1.Size = new System.Drawing.Size(792, 40);
            this.categoryItemHeader1.TabIndex = 0;
            // 
            // ProfileManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.categoryItemHeader1);
            this.Controls.Add(this.ProfilesLVIEW);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EditProfileBTN);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DeleteProfileBTN);
            this.DoubleBuffered = true;
            this.Name = "ProfileManagement";
            this.ProfilesCMENU.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProfilesListView ProfilesLVIEW;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton DeleteProfileBTN;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton EditProfileBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip ProfilesCMENU;
        private System.Windows.Forms.ToolStripMenuItem ViewProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem TestToolStripMenuItem;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader categoryItemHeader1;
    }
}
