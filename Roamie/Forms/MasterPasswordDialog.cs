using System;
using System.Windows.Forms;

namespace Virtuoso.Roamie.Forms
{
    internal partial class MasterPasswordDialog : Form
    {
        protected MasterPasswordDialog()
        {
            InitializeComponent();
        }

        public static string Prompt(bool settingPassword)
        {
            using (MasterPasswordDialog dlg = new MasterPasswordDialog())
            {
                dlg.PasswordTBOX.UseSystemPasswordChar = !settingPassword;
                dlg.ShowDialog();

                return dlg.PasswordTBOX.Text;
            }
        }

        private void PasswordTBOX_TextChanged(object sender, EventArgs e)
        {
            OkBTN.Enabled = PasswordTBOX.Text.Length > 0;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
