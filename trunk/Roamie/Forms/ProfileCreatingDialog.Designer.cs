using Virtuoso.Roamie.Forms.Controls;

namespace Virtuoso.Roamie.Forms
{
    partial class ProfileCreatingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileCreatingDialog));
            this.SuspendLayout();
            // 
            // ProfileEditor
            // 
            this.ProfileEditor.Mode = ProfileEditor.EditingMode.CreateProfile;
            this.ProfileEditor.Cancel += new System.EventHandler(this.ProfileEditor_Cancel);
            this.ProfileEditor.Save += new System.EventHandler<ProfileEditor.SaveEventArgs>(this.ProfileEditor_Save);
            // 
            // ProfileCreatingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(395, 345);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileCreatingDialog";
            this.Text = "Create a profile...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
