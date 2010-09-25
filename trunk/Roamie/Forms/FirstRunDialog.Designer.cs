namespace Virtuoso.Roamie.Forms
{
    partial class FirstRunDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstRunDialog));
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NewPBOX = new System.Windows.Forms.PictureBox();
            this.LocalPBOX = new System.Windows.Forms.PictureBox();
            this.DownloadPBOX = new System.Windows.Forms.PictureBox();
            this.MyFlashDriveRBTN = new System.Windows.Forms.RadioButton();
            this.MyComputerRBTN = new System.Windows.Forms.RadioButton();
            this.PublicComputerRBTN = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OkBTN = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPBOX)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = global::Virtuoso.Roamie.Properties.Resources.Header_Golden;
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(432, 84);
            this.gradientPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(108, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Before you start...";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(21, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(108, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please take a moment to optimize Roamie and it\'s privacy settings for your needs." +
                "";
            // 
            // NewPBOX
            // 
            this.NewPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_FlashDrive;
            this.NewPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NewPBOX.Location = new System.Drawing.Point(25, 257);
            this.NewPBOX.Name = "NewPBOX";
            this.NewPBOX.Size = new System.Drawing.Size(32, 32);
            this.NewPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.NewPBOX.TabIndex = 21;
            this.NewPBOX.TabStop = false;
            // 
            // LocalPBOX
            // 
            this.LocalPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Home;
            this.LocalPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LocalPBOX.Location = new System.Drawing.Point(25, 182);
            this.LocalPBOX.Name = "LocalPBOX";
            this.LocalPBOX.Size = new System.Drawing.Size(32, 32);
            this.LocalPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LocalPBOX.TabIndex = 22;
            this.LocalPBOX.TabStop = false;
            // 
            // DownloadPBOX
            // 
            this.DownloadPBOX.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Users;
            this.DownloadPBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DownloadPBOX.Location = new System.Drawing.Point(25, 120);
            this.DownloadPBOX.Name = "DownloadPBOX";
            this.DownloadPBOX.Size = new System.Drawing.Size(32, 32);
            this.DownloadPBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DownloadPBOX.TabIndex = 23;
            this.DownloadPBOX.TabStop = false;
            // 
            // MyFlashDriveRBTN
            // 
            this.MyFlashDriveRBTN.AutoSize = true;
            this.MyFlashDriveRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.MyFlashDriveRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MyFlashDriveRBTN.Location = new System.Drawing.Point(68, 263);
            this.MyFlashDriveRBTN.Name = "MyFlashDriveRBTN";
            this.MyFlashDriveRBTN.Size = new System.Drawing.Size(239, 17);
            this.MyFlashDriveRBTN.TabIndex = 6;
            this.MyFlashDriveRBTN.Tag = "MirandaInstallation";
            this.MyFlashDriveRBTN.Text = "I run Miranda from my USB flash drive";
            this.MyFlashDriveRBTN.UseVisualStyleBackColor = true;
            this.MyFlashDriveRBTN.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // MyComputerRBTN
            // 
            this.MyComputerRBTN.AutoSize = true;
            this.MyComputerRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.MyComputerRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MyComputerRBTN.Location = new System.Drawing.Point(68, 189);
            this.MyComputerRBTN.Name = "MyComputerRBTN";
            this.MyComputerRBTN.Size = new System.Drawing.Size(126, 17);
            this.MyComputerRBTN.TabIndex = 4;
            this.MyComputerRBTN.Tag = "WindowsAccount";
            this.MyComputerRBTN.Text = "My own computer";
            this.MyComputerRBTN.UseVisualStyleBackColor = true;
            this.MyComputerRBTN.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // PublicComputerRBTN
            // 
            this.PublicComputerRBTN.AutoSize = true;
            this.PublicComputerRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.PublicComputerRBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PublicComputerRBTN.Location = new System.Drawing.Point(68, 126);
            this.PublicComputerRBTN.Name = "PublicComputerRBTN";
            this.PublicComputerRBTN.Size = new System.Drawing.Size(173, 17);
            this.PublicComputerRBTN.TabIndex = 2;
            this.PublicComputerRBTN.Tag = "Transient";
            this.PublicComputerRBTN.Text = "Public or shared computer";
            this.PublicComputerRBTN.UseVisualStyleBackColor = true;
            this.PublicComputerRBTN.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Which privacy status applies to this computer?";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(84, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(334, 35);
            this.label4.TabIndex = 3;
            this.label4.Text = "If this is not your computer or you share it with others. Roamie will not store a" +
                "ny personal data, passwords and settings.";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(84, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(334, 49);
            this.label5.TabIndex = 5;
            this.label5.Text = "If this is your own computer and it is trustworthy. Roamie will store encrypted p" +
                "asswords and settings on the computer for your convenience.";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(84, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(334, 50);
            this.label6.TabIndex = 7;
            this.label6.Text = "If this is either public or your own computer but you run Miranda from a flash dr" +
                "ive. Roamie will store encrypted passwords and settings on the flash drive for y" +
                "our convenience.";
            // 
            // OkBTN
            // 
            this.OkBTN.Enabled = false;
            this.OkBTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OkBTN.Location = new System.Drawing.Point(12, 342);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 8;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.label7.Location = new System.Drawing.Point(146, 347);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(274, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "You can change these settings later in Roamie\'s options.";
            // 
            // FirstRunDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(432, 377);
            this.ControlBox = false;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NewPBOX);
            this.Controls.Add(this.LocalPBOX);
            this.Controls.Add(this.DownloadPBOX);
            this.Controls.Add(this.MyFlashDriveRBTN);
            this.Controls.Add(this.MyComputerRBTN);
            this.Controls.Add(this.PublicComputerRBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirstRunDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Roamie";
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPBOX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox NewPBOX;
        private System.Windows.Forms.PictureBox LocalPBOX;
        private System.Windows.Forms.PictureBox DownloadPBOX;
        private System.Windows.Forms.RadioButton MyFlashDriveRBTN;
        private System.Windows.Forms.RadioButton MyComputerRBTN;
        private System.Windows.Forms.RadioButton PublicComputerRBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Label label7;
    }
}