namespace Virtuoso.Miranda.Roamie.Forms
{
    partial class SyncDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncDialog));
            this.ProgressPBAR = new System.Windows.Forms.ProgressBar();
            this.Worker = new System.ComponentModel.BackgroundWorker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.LogTBOX = new System.Windows.Forms.TextBox();
            this.SilentStatusIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.TryAgainBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressPBAR
            // 
            this.ProgressPBAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressPBAR.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ProgressPBAR.Location = new System.Drawing.Point(12, 45);
            this.ProgressPBAR.MarqueeAnimationSpeed = 50;
            this.ProgressPBAR.Name = "ProgressPBAR";
            this.ProgressPBAR.Size = new System.Drawing.Size(456, 16);
            this.ProgressPBAR.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressPBAR.TabIndex = 1;
            // 
            // Worker
            // 
            this.Worker.WorkerReportsProgress = true;
            this.Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_RunWorkerCompleted);
            this.Worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::Virtuoso.Miranda.Roamie.Properties.Resources.Watermark_Play_256x256;
            this.pictureBox2.Location = new System.Drawing.Point(225, -45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // LogTBOX
            // 
            this.LogTBOX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogTBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LogTBOX.Location = new System.Drawing.Point(12, 67);
            this.LogTBOX.Multiline = true;
            this.LogTBOX.Name = "LogTBOX";
            this.LogTBOX.ReadOnly = true;
            this.LogTBOX.Size = new System.Drawing.Size(258, 130);
            this.LogTBOX.TabIndex = 6;
            this.LogTBOX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SyncDialog_KeyDown);
            // 
            // SilentStatusIcon
            // 
            this.SilentStatusIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SilentStatusIcon.Icon")));
            this.SilentStatusIcon.Text = "Synchronizing Miranda database...";
            this.SilentStatusIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SilentStatusIcon_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Synchronizing...";
            // 
            // TryAgainBTN
            // 
            this.TryAgainBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TryAgainBTN.Location = new System.Drawing.Point(388, 9);
            this.TryAgainBTN.Name = "TryAgainBTN";
            this.TryAgainBTN.Size = new System.Drawing.Size(80, 30);
            this.TryAgainBTN.TabIndex = 7;
            this.TryAgainBTN.Text = "Try again";
            this.TryAgainBTN.UseVisualStyleBackColor = true;
            this.TryAgainBTN.Visible = false;
            this.TryAgainBTN.Click += new System.EventHandler(this.TryAgainBTN_Click);
            // 
            // SyncDialog
            // 
            this.AcceptButton = this.TryAgainBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 209);
            this.ControlBox = false;
            this.Controls.Add(this.TryAgainBTN);
            this.Controls.Add(this.LogTBOX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProgressPBAR);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profile synchronization";
            this.Shown += new System.EventHandler(this.SyncDialog_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SyncDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressPBAR;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox LogTBOX;
        private System.Windows.Forms.NotifyIcon SilentStatusIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TryAgainBTN;
    }
}