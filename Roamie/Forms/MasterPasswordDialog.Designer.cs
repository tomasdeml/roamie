namespace Virtuoso.Roamie.Forms
{
    partial class MasterPasswordDialog
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
            this.OkBTN = new System.Windows.Forms.Button();
            this.PasswordTBOX = new System.Windows.Forms.TextBox();
            this.gradientPanel1 = new Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.HintLABEL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Password2LABEL = new System.Windows.Forms.Label();
            this.Password2TBOX = new System.Windows.Forms.TextBox();
            this.PasswordHintLBTN = new System.Windows.Forms.LinkLabel();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // OkBTN
            // 
            this.OkBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkBTN.Enabled = false;
            this.OkBTN.Location = new System.Drawing.Point(12, 195);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 5;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // PasswordTBOX
            // 
            this.PasswordTBOX.Location = new System.Drawing.Point(12, 114);
            this.PasswordTBOX.Name = "PasswordTBOX";
            this.PasswordTBOX.Size = new System.Drawing.Size(332, 20);
            this.PasswordTBOX.TabIndex = 2;
            this.PasswordTBOX.UseSystemPasswordChar = true;
            this.PasswordTBOX.TextChanged += new System.EventHandler(this.PasswordTBOX_TextChanged);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = global::Virtuoso.Roamie.Properties.Resources.Header_Golden;
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.HintLABEL);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(359, 84);
            this.gradientPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(108, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter your passphrase";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Virtuoso.Roamie.Properties.Resources.Image_48x48_Lock;
            this.pictureBox2.Location = new System.Drawing.Point(21, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // HintLABEL
            // 
            this.HintLABEL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HintLABEL.BackColor = System.Drawing.Color.Transparent;
            this.HintLABEL.ForeColor = System.Drawing.Color.Black;
            this.HintLABEL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HintLABEL.Location = new System.Drawing.Point(108, 37);
            this.HintLABEL.Name = "HintLABEL";
            this.HintLABEL.Size = new System.Drawing.Size(248, 35);
            this.HintLABEL.TabIndex = 1;
            this.HintLABEL.Text = "(TBL)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Passphrase";
            // 
            // Password2LABEL
            // 
            this.Password2LABEL.AutoSize = true;
            this.Password2LABEL.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Password2LABEL.Location = new System.Drawing.Point(9, 144);
            this.Password2LABEL.Name = "Password2LABEL";
            this.Password2LABEL.Size = new System.Drawing.Size(141, 13);
            this.Password2LABEL.TabIndex = 3;
            this.Password2LABEL.Text = "Confirm the passphrase";
            // 
            // Password2TBOX
            // 
            this.Password2TBOX.Location = new System.Drawing.Point(12, 160);
            this.Password2TBOX.Name = "Password2TBOX";
            this.Password2TBOX.Size = new System.Drawing.Size(332, 20);
            this.Password2TBOX.TabIndex = 4;
            this.Password2TBOX.UseSystemPasswordChar = true;
            // 
            // PasswordHintLBTN
            // 
            this.PasswordHintLBTN.AutoSize = true;
            this.PasswordHintLBTN.Location = new System.Drawing.Point(177, 200);
            this.PasswordHintLBTN.Name = "PasswordHintLBTN";
            this.PasswordHintLBTN.Size = new System.Drawing.Size(167, 13);
            this.PasswordHintLBTN.TabIndex = 6;
            this.PasswordHintLBTN.TabStop = true;
            this.PasswordHintLBTN.Text = "How to create a good passphrase";
            this.PasswordHintLBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PasswordHintLBTN_LinkClicked);
            // 
            // MasterPasswordDialog
            // 
            this.AcceptButton = this.OkBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 231);
            this.ControlBox = false;
            this.Controls.Add(this.PasswordHintLBTN);
            this.Controls.Add(this.Password2TBOX);
            this.Controls.Add(this.PasswordTBOX);
            this.Controls.Add(this.Password2LABEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.OkBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterPasswordDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roamie settings passphrase";
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.TextBox PasswordTBOX;
        private Virtuoso.Miranda.Plugins.Forms.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label HintLABEL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Password2LABEL;
        private System.Windows.Forms.TextBox Password2TBOX;
        private System.Windows.Forms.LinkLabel PasswordHintLBTN;
    }
}