using System;
using System.Windows.Forms;

namespace Virtuoso.Roamie.Forms
{
    public partial class MasterKeyDialog : Form
    {
        protected MasterKeyDialog()
        {
            InitializeComponent();
        }

        public static string Prompt(bool decrypting)
        {
            using (MasterKeyDialog dlg = new MasterKeyDialog())
            {
                dlg.PasswordTBOX.UseSystemPasswordChar = decrypting;
                dlg.ShowDialog();

                return dlg.PasswordTBOX.Text;
            }
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PasswordTBOX_TextChanged(object sender, EventArgs e)
        {
            OkBTN.Enabled = PasswordTBOX.Text.Length > 0;
        }
    }
}
