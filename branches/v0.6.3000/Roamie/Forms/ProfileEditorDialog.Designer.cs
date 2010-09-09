using Virtuoso.Miranda.Roamie.Forms.Controls;
namespace Virtuoso.Miranda.Roamie.Forms
{
    partial class ProfileEditorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileEditorDialog));
            this.categoryItemHeader1 = new Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader();
            this.ProfileEditor = new Virtuoso.Miranda.Roamie.Forms.Controls.ProfileEditor();
            this.SuspendLayout();
            // 
            // categoryItemHeader1
            // 
            this.categoryItemHeader1.BackColor = System.Drawing.Color.Transparent;
            this.categoryItemHeader1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.categoryItemHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryItemHeader1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryItemHeader1.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryItemHeader1.HeaderText = "";
            this.categoryItemHeader1.Image = ((System.Drawing.Image)(resources.GetObject("categoryItemHeader1.Image")));
            this.categoryItemHeader1.Location = new System.Drawing.Point(0, 0);
            this.categoryItemHeader1.MinimumSize = new System.Drawing.Size(300, 40);
            this.categoryItemHeader1.Name = "categoryItemHeader1";
            this.categoryItemHeader1.Size = new System.Drawing.Size(400, 40);
            this.categoryItemHeader1.TabIndex = 1;
            // 
            // ProfileEditor
            // 
            this.ProfileEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfileEditor.AutoSize = true;
            this.ProfileEditor.BackColor = System.Drawing.Color.Transparent;
            this.ProfileEditor.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ProfileEditor.Location = new System.Drawing.Point(0, 46);
            this.ProfileEditor.MinimumSize = new System.Drawing.Size(402, 305);
            this.ProfileEditor.Mode = Virtuoso.Miranda.Roamie.Forms.Controls.ProfileEditor.EditingMode.EditProfile;
            this.ProfileEditor.Name = "ProfileEditor";
            this.ProfileEditor.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.ProfileEditor.Size = new System.Drawing.Size(405, 305);
            this.ProfileEditor.TabIndex = 0;
            this.ProfileEditor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProfileEditor_KeyUp);
            // 
            // ProfileEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 351);
            this.Controls.Add(this.categoryItemHeader1);
            this.Controls.Add(this.ProfileEditor);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileEditorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profile editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected ProfileEditor ProfileEditor;
        private Virtuoso.Miranda.Plugins.Configuration.Forms.Controls.CategoryItemHeader categoryItemHeader1;

    }
}