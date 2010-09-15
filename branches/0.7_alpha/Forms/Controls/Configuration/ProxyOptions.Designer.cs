namespace Virtuoso.Roamie.Forms.Controls.Configuration
{
    partial class ProxyOptions
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
            this.categoryItemSection2 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection();
            this.categoryItemHeader1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader();
            this.LocationGBOX = new System.Windows.Forms.GroupBox();
            this.PortTBOX = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HostTBOX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomProxyRBTN = new System.Windows.Forms.RadioButton();
            this.DefaultProxyRBTN = new System.Windows.Forms.RadioButton();
            this.NoProxyRBTN = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.AuthGBOX = new System.Windows.Forms.GroupBox();
            this.SendCredCHBOX = new System.Windows.Forms.CheckBox();
            this.UseSystemCredCHBOX = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordTBOX = new System.Windows.Forms.TextBox();
            this.UsernameTBOX = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LocationGBOX.SuspendLayout();
            this.AuthGBOX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryItemSection2
            // 
            this.categoryItemSection2.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemSection2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.categoryItemSection2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryItemSection2.ForeColor = System.Drawing.Color.Black;
            this.categoryItemSection2.Location = new System.Drawing.Point(10, 49);
            this.categoryItemSection2.MinimumSize = new System.Drawing.Size(300, 20);
            this.categoryItemSection2.Name = "categoryItemSection2";
            this.categoryItemSection2.SectionName = "Proxy";
            this.categoryItemSection2.Size = new System.Drawing.Size(765, 20);
            this.categoryItemSection2.TabIndex = 1;
            // 
            // categoryItemHeader1
            // 
            this.categoryItemHeader1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemHeader1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.categoryItemHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryItemHeader1.HeaderText = "Configure Roamie to use an HTTP proxy for synchronization";
            this.categoryItemHeader1.Image = global::Virtuoso.Roamie.Properties.Resources.Image_32x32_Web;
            this.categoryItemHeader1.Location = new System.Drawing.Point(0, 0);
            this.categoryItemHeader1.MinimumSize = new System.Drawing.Size(300, 40);
            this.categoryItemHeader1.Name = "categoryItemHeader1";
            this.categoryItemHeader1.Size = new System.Drawing.Size(792, 40);
            this.categoryItemHeader1.TabIndex = 0;
            // 
            // LocationGBOX
            // 
            this.LocationGBOX.Controls.Add(this.PortTBOX);
            this.LocationGBOX.Controls.Add(this.label3);
            this.LocationGBOX.Controls.Add(this.label1);
            this.LocationGBOX.Controls.Add(this.HostTBOX);
            this.LocationGBOX.Enabled = false;
            this.LocationGBOX.Location = new System.Drawing.Point(41, 190);
            this.LocationGBOX.Name = "LocationGBOX";
            this.LocationGBOX.Size = new System.Drawing.Size(382, 60);
            this.LocationGBOX.TabIndex = 7;
            this.LocationGBOX.TabStop = false;
            this.LocationGBOX.Text = "Location";
            // 
            // PortTBOX
            // 
            this.PortTBOX.AllowPromptAsInput = false;
            this.PortTBOX.AsciiOnly = true;
            this.PortTBOX.Culture = new System.Globalization.CultureInfo("");
            this.PortTBOX.HidePromptOnLeave = true;
            this.PortTBOX.HideSelection = false;
            this.PortTBOX.Location = new System.Drawing.Point(304, 22);
            this.PortTBOX.Mask = "00000";
            this.PortTBOX.Name = "PortTBOX";
            this.PortTBOX.Size = new System.Drawing.Size(48, 20);
            this.PortTBOX.TabIndex = 3;
            this.PortTBOX.ValidatingType = typeof(int);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host";
            // 
            // HostTBOX
            // 
            this.HostTBOX.Location = new System.Drawing.Point(52, 22);
            this.HostTBOX.Name = "HostTBOX";
            this.HostTBOX.Size = new System.Drawing.Size(208, 20);
            this.HostTBOX.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(346, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Configures Roamie to use Internet Explorer settings configured proxy.";
            // 
            // CustomProxyRBTN
            // 
            this.CustomProxyRBTN.AutoSize = true;
            this.CustomProxyRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CustomProxyRBTN.Location = new System.Drawing.Point(21, 167);
            this.CustomProxyRBTN.Name = "CustomProxyRBTN";
            this.CustomProxyRBTN.Size = new System.Drawing.Size(127, 17);
            this.CustomProxyRBTN.TabIndex = 6;
            this.CustomProxyRBTN.Text = "Use custom proxy";
            this.CustomProxyRBTN.UseVisualStyleBackColor = true;
            this.CustomProxyRBTN.CheckedChanged += new System.EventHandler(this.CustomProxyRBTN_CheckedChanged);
            // 
            // DefaultProxyRBTN
            // 
            this.DefaultProxyRBTN.AutoSize = true;
            this.DefaultProxyRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DefaultProxyRBTN.Location = new System.Drawing.Point(21, 121);
            this.DefaultProxyRBTN.Name = "DefaultProxyRBTN";
            this.DefaultProxyRBTN.Size = new System.Drawing.Size(125, 17);
            this.DefaultProxyRBTN.TabIndex = 4;
            this.DefaultProxyRBTN.Text = "Use default proxy";
            this.DefaultProxyRBTN.UseVisualStyleBackColor = true;
            // 
            // NoProxyRBTN
            // 
            this.NoProxyRBTN.AutoSize = true;
            this.NoProxyRBTN.Checked = true;
            this.NoProxyRBTN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NoProxyRBTN.Location = new System.Drawing.Point(21, 75);
            this.NoProxyRBTN.Name = "NoProxyRBTN";
            this.NoProxyRBTN.Size = new System.Drawing.Size(121, 17);
            this.NoProxyRBTN.TabIndex = 2;
            this.NoProxyRBTN.TabStop = true;
            this.NoProxyRBTN.Text = "Do not use proxy";
            this.NoProxyRBTN.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Configures Roamie to connect directly to a host.";
            // 
            // AuthGBOX
            // 
            this.AuthGBOX.Controls.Add(this.SendCredCHBOX);
            this.AuthGBOX.Controls.Add(this.UseSystemCredCHBOX);
            this.AuthGBOX.Controls.Add(this.label5);
            this.AuthGBOX.Controls.Add(this.label4);
            this.AuthGBOX.Controls.Add(this.PasswordTBOX);
            this.AuthGBOX.Controls.Add(this.UsernameTBOX);
            this.AuthGBOX.Enabled = false;
            this.AuthGBOX.Location = new System.Drawing.Point(41, 256);
            this.AuthGBOX.Name = "AuthGBOX";
            this.AuthGBOX.Size = new System.Drawing.Size(382, 132);
            this.AuthGBOX.TabIndex = 8;
            this.AuthGBOX.TabStop = false;
            this.AuthGBOX.Text = "Authentication";
            // 
            // SendCredCHBOX
            // 
            this.SendCredCHBOX.AutoSize = true;
            this.SendCredCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SendCredCHBOX.Location = new System.Drawing.Point(20, 19);
            this.SendCredCHBOX.Name = "SendCredCHBOX";
            this.SendCredCHBOX.Size = new System.Drawing.Size(195, 17);
            this.SendCredCHBOX.TabIndex = 0;
            this.SendCredCHBOX.Text = "Proxy requires authentication";
            this.SendCredCHBOX.UseVisualStyleBackColor = true;
            this.SendCredCHBOX.CheckedChanged += new System.EventHandler(this.SendCredCHBOX_CheckedChanged);
            // 
            // UseSystemCredCHBOX
            // 
            this.UseSystemCredCHBOX.AutoSize = true;
            this.UseSystemCredCHBOX.Checked = true;
            this.UseSystemCredCHBOX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseSystemCredCHBOX.Enabled = false;
            this.UseSystemCredCHBOX.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.UseSystemCredCHBOX.Location = new System.Drawing.Point(52, 42);
            this.UseSystemCredCHBOX.Name = "UseSystemCredCHBOX";
            this.UseSystemCredCHBOX.Size = new System.Drawing.Size(158, 17);
            this.UseSystemCredCHBOX.TabIndex = 1;
            this.UseSystemCredCHBOX.Text = "Use system credentials";
            this.UseSystemCredCHBOX.UseVisualStyleBackColor = true;
            this.UseSystemCredCHBOX.CheckedChanged += new System.EventHandler(this.UseSystemCredCHBOX_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(69, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Username";
            // 
            // PasswordTBOX
            // 
            this.PasswordTBOX.Enabled = false;
            this.PasswordTBOX.Location = new System.Drawing.Point(130, 91);
            this.PasswordTBOX.Name = "PasswordTBOX";
            this.PasswordTBOX.Size = new System.Drawing.Size(222, 20);
            this.PasswordTBOX.TabIndex = 5;
            this.PasswordTBOX.UseSystemPasswordChar = true;
            // 
            // UsernameTBOX
            // 
            this.UsernameTBOX.Enabled = false;
            this.UsernameTBOX.Location = new System.Drawing.Point(130, 65);
            this.UsernameTBOX.Name = "UsernameTBOX";
            this.UsernameTBOX.Size = new System.Drawing.Size(222, 20);
            this.UsernameTBOX.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Virtuoso.Roamie.Properties.Resources.Watermark_Sync;
            this.pictureBox1.Location = new System.Drawing.Point(643, 210);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // ProxyOptions
            // 
            this.Controls.Add(this.AuthGBOX);
            this.Controls.Add(this.NoProxyRBTN);
            this.Controls.Add(this.LocationGBOX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CustomProxyRBTN);
            this.Controls.Add(this.DefaultProxyRBTN);
            this.Controls.Add(this.categoryItemHeader1);
            this.Controls.Add(this.categoryItemSection2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ProxyOptions";
            this.LocationGBOX.ResumeLayout(false);
            this.LocationGBOX.PerformLayout();
            this.AuthGBOX.ResumeLayout(false);
            this.AuthGBOX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemSection categoryItemSection2;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader categoryItemHeader1;
        private System.Windows.Forms.GroupBox LocationGBOX;
        private System.Windows.Forms.MaskedTextBox PortTBOX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HostTBOX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton CustomProxyRBTN;
        private System.Windows.Forms.RadioButton DefaultProxyRBTN;
        private System.Windows.Forms.RadioButton NoProxyRBTN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox AuthGBOX;
        private System.Windows.Forms.CheckBox SendCredCHBOX;
        private System.Windows.Forms.CheckBox UseSystemCredCHBOX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordTBOX;
        private System.Windows.Forms.TextBox UsernameTBOX;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
