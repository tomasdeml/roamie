namespace Virtuoso.Roamie.Forms
{
    partial class MasterKeyDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // OkBTN
            // 
            this.OkBTN.Enabled = false;
            this.OkBTN.Location = new System.Drawing.Point(21, 128);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 2;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // PasswordTBOX
            // 
            this.PasswordTBOX.Location = new System.Drawing.Point(21, 90);
            this.PasswordTBOX.Name = "PasswordTBOX";
            this.PasswordTBOX.Size = new System.Drawing.Size(233, 21);
            this.PasswordTBOX.TabIndex = 3;
            this.PasswordTBOX.UseSystemPasswordChar = true;
            this.PasswordTBOX.TextChanged += new System.EventHandler(this.PasswordTBOX_TextChanged);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackgroundImage = global::Virtuoso.Roamie.Properties.Resources.Header_Golden;
            this.gradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.label2);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Rotation = -90F;
            this.gradientPanel1.Size = new System.Drawing.Size(394, 84);
            this.gradientPanel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(108, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter your master key";
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
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(108, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "TBS";
            // 
            // MasterKeyDialog
            // 
            this.AcceptButton = this.OkBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 160);
            this.ControlBox = false;
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.PasswordTBOX);
            this.Controls.Add(this.OkBTN);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterKeyDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master key required";
            this.TopMost = true;
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
        private System.Windows.Forms.Label label2;
    }
}