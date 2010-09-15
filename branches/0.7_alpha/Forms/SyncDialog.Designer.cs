namespace Virtuoso.Roamie.Forms
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
            this.LogTBOX = new System.Windows.Forms.TextBox();
            this.SilentStatusIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TryAgainBTN = new System.Windows.Forms.Button();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressPBAR
            // 
            this.ProgressPBAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressPBAR.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ProgressPBAR.Location = new System.Drawing.Point(12, 154);
            this.ProgressPBAR.MarqueeAnimationSpeed = 50;
            this.ProgressPBAR.Name = "ProgressPBAR";
            this.ProgressPBAR.Size = new System.Drawing.Size(366, 18);
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
            // LogTBOX
            // 
            this.LogTBOX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTBOX.BackColor = System.Drawing.Color.White;
            this.LogTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogTBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LogTBOX.Location = new System.Drawing.Point(12, 52);
            this.LogTBOX.Multiline = true;
            this.LogTBOX.Name = "LogTBOX";
            this.LogTBOX.ReadOnly = true;
            this.LogTBOX.Size = new System.Drawing.Size(366, 96);
            this.LogTBOX.TabIndex = 6;
            this.LogTBOX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SyncDialog_KeyDown);
            // 
            // SilentStatusIcon
            // 
            this.SilentStatusIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SilentStatusIcon.Icon")));
            this.SilentStatusIcon.Text = "Synchronizing Miranda database...";
            this.SilentStatusIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SilentStatusIcon_MouseClick);
            // 
            // TryAgainBTN
            // 
            this.TryAgainBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TryAgainBTN.Location = new System.Drawing.Point(298, 7);
            this.TryAgainBTN.Name = "TryAgainBTN";
            this.TryAgainBTN.Size = new System.Drawing.Size(80, 30);
            this.TryAgainBTN.TabIndex = 7;
            this.TryAgainBTN.Text = "Try again";
            this.TryAgainBTN.UseVisualStyleBackColor = true;
            this.TryAgainBTN.Visible = false;
            this.TryAgainBTN.Click += new System.EventHandler(this.TryAgainBTN_Click);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gradientPanel1.BackgroundImage")));
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.TryAgainBTN);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(390, 45);
            this.gradientPanel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Synchronizing...";
            // 
            // SyncDialog
            // 
            this.AcceptButton = this.TryAgainBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(390, 184);
            this.ControlBox = false;
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.LogTBOX);
            this.Controls.Add(this.ProgressPBAR);
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
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressPBAR;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.TextBox LogTBOX;
        private System.Windows.Forms.NotifyIcon SilentStatusIcon;
        private System.Windows.Forms.Button TryAgainBTN;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label3;
    }
}