namespace Virtuoso.Miranda.Roamie.Forms.Controls.Configuration
{
    partial class PackedFilesManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackedFilesManagement));
            this.categoryItemHeader1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader();
            this.SuspendLayout();
            // 
            // categoryItemHeader1
            // 
            this.categoryItemHeader1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemHeader1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.categoryItemHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryItemHeader1.HeaderText = "Pack files with your database";
            this.categoryItemHeader1.Image = ((System.Drawing.Image)(resources.GetObject("categoryItemHeader1.Image")));
            this.categoryItemHeader1.Location = new System.Drawing.Point(0, 0);
            this.categoryItemHeader1.MinimumSize = new System.Drawing.Size(300, 40);
            this.categoryItemHeader1.Name = "categoryItemHeader1";
            this.categoryItemHeader1.Size = new System.Drawing.Size(792, 40);
            this.categoryItemHeader1.TabIndex = 0;
            // 
            // PackedFilesManagement
            // 
            this.Controls.Add(this.categoryItemHeader1);
            this.Name = "PackedFilesManagement";
            this.ResumeLayout(false);

        }

        #endregion

        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader categoryItemHeader1;
    }
}
