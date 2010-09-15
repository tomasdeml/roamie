namespace Virtuoso.Miranda.Roamie.Forms.Controls.Configuration
{
    partial class BehaviourOptions
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
            this.AllowSilentCHKBOX = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.categoryItemHeader1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader();
            this.categoryItemSection1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FullSyncAfterThresholdCHBOX = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // AllowSilentCHKBOX
            // 
            this.AllowSilentCHKBOX.AutoSize = true;
            this.AllowSilentCHKBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AllowSilentCHKBOX.Location = new System.Drawing.Point(21, 75);
            this.AllowSilentCHKBOX.Name = "AllowSilentCHKBOX";
            this.AllowSilentCHKBOX.Size = new System.Drawing.Size(183, 17);
            this.AllowSilentCHKBOX.TabIndex = 2;
            this.AllowSilentCHKBOX.Text = "Allow silent synchronization";
            this.AllowSilentCHKBOX.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "When enabled, some operations will be performed silently, only with a tray icon d" +
                "isplayed instead of a dialog.";
            // 
            // categoryItemHeader1
            // 
            this.categoryItemHeader1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemHeader1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.categoryItemHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryItemHeader1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryItemHeader1.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryItemHeader1.HeaderText = "Configure behaviour of Roamie";
            this.categoryItemHeader1.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Image_32x32_Settings;
            this.categoryItemHeader1.Location = new System.Drawing.Point(0, 0);
            this.categoryItemHeader1.MinimumSize = new System.Drawing.Size(300, 40);
            this.categoryItemHeader1.Name = "categoryItemHeader1";
            this.categoryItemHeader1.Size = new System.Drawing.Size(792, 40);
            this.categoryItemHeader1.TabIndex = 0;
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
            this.categoryItemSection1.SectionName = "Automation";
            this.categoryItemSection1.Size = new System.Drawing.Size(765, 20);
            this.categoryItemSection1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Watermark_Sync;
            this.pictureBox1.Location = new System.Drawing.Point(643, 210);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // FullSyncAfterThresholdCHBOX
            // 
            this.FullSyncAfterThresholdCHBOX.AutoSize = true;
            this.FullSyncAfterThresholdCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FullSyncAfterThresholdCHBOX.Location = new System.Drawing.Point(21, 124);
            this.FullSyncAfterThresholdCHBOX.Name = "FullSyncAfterThresholdCHBOX";
            this.FullSyncAfterThresholdCHBOX.Size = new System.Drawing.Size(359, 17);
            this.FullSyncAfterThresholdCHBOX.TabIndex = 2;
            this.FullSyncAfterThresholdCHBOX.Text = "Perform full synchronization when delta count exceeds {0}";
            this.FullSyncAfterThresholdCHBOX.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(534, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "When enabled, Roamie will automatically switch to full synchronization when the c" +
                "ount exceeds the threshold.";
            // 
            // BehaviourOptions
            // 
            this.Controls.Add(this.categoryItemSection1);
            this.Controls.Add(this.categoryItemHeader1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FullSyncAfterThresholdCHBOX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AllowSilentCHKBOX);
            this.Controls.Add(this.pictureBox1);
            this.Name = "BehaviourOptions";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox AllowSilentCHKBOX;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader categoryItemHeader1;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection categoryItemSection1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox FullSyncAfterThresholdCHBOX;
        private System.Windows.Forms.Label label2;
    }
}
