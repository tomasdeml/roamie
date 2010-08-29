namespace Virtuoso.Miranda.Roamie.Forms
{
    partial class DbTokenMismatchDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbTokenMismatchDialog));
            this.commandButton2 = new Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton();
            this.commandButton3 = new Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DialogPicturePBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialogWatermarkPBOX)).BeginInit();
            this.TopPANEL.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLBL
            // 
            this.TitleLBL.Size = new System.Drawing.Size(160, 13);
            this.TitleLBL.Text = "A different database exists";
            // 
            // DialogWatermarkPBOX
            // 
            this.DialogWatermarkPBOX.Location = new System.Drawing.Point(277, -6);
            // 
            // TopPANEL
            // 
            this.TopPANEL.Controls.Add(this.label1);
            this.TopPANEL.Size = new System.Drawing.Size(447, 73);
            this.TopPANEL.Controls.SetChildIndex(this.TitleLBL, 0);
            this.TopPANEL.Controls.SetChildIndex(this.DialogPicturePBOX, 0);
            this.TopPANEL.Controls.SetChildIndex(this.label1, 0);
            // 
            // commandButton2
            // 
            this.commandButton2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.commandButton2.FlatAppearance.BorderSize = 2;
            this.commandButton2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.commandButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButton2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commandButton2.Image = ((System.Drawing.Image)(resources.GetObject("commandButton2.Image")));
            this.commandButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandButton2.Location = new System.Drawing.Point(69, 132);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(366, 47);
            this.commandButton2.TabIndex = 3;
            this.commandButton2.Text = "  Overwrite the database and continue\r\n";
            this.commandButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.commandButton2.UseVisualStyleBackColor = true;
            this.commandButton2.Click += new System.EventHandler(this.commandButton2_Click);
            // 
            // commandButton3
            // 
            this.commandButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.commandButton3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.commandButton3.FlatAppearance.BorderSize = 2;
            this.commandButton3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.commandButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButton3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commandButton3.Image = ((System.Drawing.Image)(resources.GetObject("commandButton3.Image")));
            this.commandButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandButton3.Location = new System.Drawing.Point(69, 79);
            this.commandButton3.Name = "commandButton3";
            this.commandButton3.Size = new System.Drawing.Size(366, 47);
            this.commandButton3.TabIndex = 3;
            this.commandButton3.Text = "  Disable roaming and preserve the database \r\n  (recommended)\r\n";
            this.commandButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.commandButton3.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(66, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(369, 48);
            this.label1.TabIndex = 4;
            this.label1.Text = "There is already a different database on the remote site.\r\nIf you proceed, its da" +
                "ta will be lost.";
            // 
            // DbTokenMismatchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(447, 195);
            this.ControlBox = false;
            this.Controls.Add(this.commandButton3);
            this.Controls.Add(this.commandButton2);
            this.Name = "DbTokenMismatchDialog";
            this.Text = "Data-loss warning";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.commandButton2, 0);
            this.Controls.SetChildIndex(this.commandButton3, 0);
            this.Controls.SetChildIndex(this.DialogWatermarkPBOX, 0);
            this.Controls.SetChildIndex(this.TopPANEL, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DialogPicturePBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialogWatermarkPBOX)).EndInit();
            this.TopPANEL.ResumeLayout(false);
            this.TopPANEL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton commandButton2;
        private Virtuoso.Miranda.Plugins.Forms.Controls.CommandButton commandButton3;
        private System.Windows.Forms.Label label1;

    }
}
