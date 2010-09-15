using Virtuoso.Roamie.Forms.Controls;

namespace Virtuoso.Roamie.Forms
{
    partial class ProfileEditingDialog
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
            this.SuspendLayout();
            // 
            // ProfileEditor
            // 
            this.ProfileEditor.Cancel += new System.EventHandler(this.ProfileEditor_Cancel);
            this.ProfileEditor.Save += new System.EventHandler<ProfileEditor.SaveEventArgs>(this.ProfileEditor_Save);
            // 
            // ProfileEditingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(395, 345);
            this.Name = "ProfileEditingDialog";
            this.Text = "Edit profile...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
