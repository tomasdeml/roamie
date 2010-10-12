using System;
using System.Drawing;
using System.Windows.Forms;
using Virtuoso.Roamie.Properties;
using System.Diagnostics;

namespace Virtuoso.Roamie.Forms
{
    internal partial class MasterPasswordDialog : Form
    {
        #region Fields

        private const string PasswordGuideUrl = "http://www.microsoft.com/protect/fraud/passwords/create.aspx";
        private readonly bool Decrypting; 

        #endregion

        #region .ctors

        private MasterPasswordDialog(bool decrypting)
        {
            InitializeComponent();
            Decrypting = decrypting;

            if (decrypting)
            {
                Size = new Size(Width, 205);
                Password2TBOX.Visible = Password2LABEL.Visible = false;
                HintLABEL.Text = Resources.Text_UI_MasterPasswordDecrypting;
            }
            else
            {
                HintLABEL.Text = Resources.Text_UI_MasterPasswordEncrypting;
            }
        }

        public static string Prompt(bool decrypting)
        {
            using (MasterPasswordDialog dlg = new MasterPasswordDialog(decrypting))
            {
                dlg.ShowDialog();
                return dlg.PasswordTBOX.Text;
            }
        }

        #endregion

        #region UI Handlers

        private void PasswordHintLBTN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(PasswordGuideUrl) { UseShellExecute = true });
        }

        private void PasswordTBOX_TextChanged(object sender, EventArgs e)
        {
            OkBTN.Enabled = (PasswordTBOX.Text.Length > 0);
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            if (!Decrypting && !PasswordTBOX.Text.Equals(Password2TBOX.Text))
            {
                MessageBox.Show(Resources.MsgBox_Text_PassphrasesDoNotMatch, Resources.MsgBox_Title_Error,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }

        #endregion
    }
}
