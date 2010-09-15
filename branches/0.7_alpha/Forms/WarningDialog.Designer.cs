namespace Virtuoso.Roamie.Forms
{
    partial class WarningDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningDialog));
            this.DialogWatermarkPBOX = new System.Windows.Forms.PictureBox();
            this.DialogPicturePBOX = new System.Windows.Forms.PictureBox();
            this.TitleLBL = new System.Windows.Forms.Label();
            this.TopPANEL = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DialogWatermarkPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialogPicturePBOX)).BeginInit();
            this.TopPANEL.SuspendLayout();
            this.SuspendLayout();
            // 
            // DialogWatermarkPBOX
            // 
            this.DialogWatermarkPBOX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DialogWatermarkPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Icon_244_256x256_Trans;
            this.DialogWatermarkPBOX.Location = new System.Drawing.Point(329, 114);
            this.DialogWatermarkPBOX.Name = "DialogWatermarkPBOX";
            this.DialogWatermarkPBOX.Size = new System.Drawing.Size(172, 204);
            this.DialogWatermarkPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DialogWatermarkPBOX.TabIndex = 0;
            this.DialogWatermarkPBOX.TabStop = false;
            this.DialogWatermarkPBOX.Visible = false;
            // 
            // DialogPicturePBOX
            // 
            this.DialogPicturePBOX.BackColor = System.Drawing.Color.Transparent;
            this.DialogPicturePBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Icon_244_48x48;
            this.DialogPicturePBOX.Location = new System.Drawing.Point(10, 9);
            this.DialogPicturePBOX.Name = "DialogPicturePBOX";
            this.DialogPicturePBOX.Size = new System.Drawing.Size(48, 43);
            this.DialogPicturePBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DialogPicturePBOX.TabIndex = 1;
            this.DialogPicturePBOX.TabStop = false;
            // 
            // TitleLBL
            // 
            this.TitleLBL.AutoSize = true;
            this.TitleLBL.BackColor = System.Drawing.Color.Transparent;
            this.TitleLBL.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TitleLBL.Location = new System.Drawing.Point(64, 9);
            this.TitleLBL.Name = "TitleLBL";
            this.TitleLBL.Size = new System.Drawing.Size(32, 13);
            this.TitleLBL.TabIndex = 2;
            this.TitleLBL.Text = "Title";
            // 
            // TopPANEL
            // 
            this.TopPANEL.Controls.Add(this.DialogPicturePBOX);
            this.TopPANEL.Controls.Add(this.TitleLBL);
            this.TopPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPANEL.GradientColor = System.Drawing.Color.Khaki;
            this.TopPANEL.Location = new System.Drawing.Point(0, 0);
            this.TopPANEL.Name = "TopPANEL";
            this.TopPANEL.Rotation = -90F;
            this.TopPANEL.Size = new System.Drawing.Size(499, 86);
            this.TopPANEL.TabIndex = 3;
            // 
            // WarningDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 316);
            this.Controls.Add(this.TopPANEL);
            this.Controls.Add(this.DialogWatermarkPBOX);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WarningDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warning";
            ((System.ComponentModel.ISupportInitialize)(this.DialogWatermarkPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialogPicturePBOX)).EndInit();
            this.TopPANEL.ResumeLayout(false);
            this.TopPANEL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox DialogPicturePBOX;
        protected System.Windows.Forms.Label TitleLBL;
        protected internal System.Windows.Forms.PictureBox DialogWatermarkPBOX;
        protected Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel TopPANEL;
    }
}