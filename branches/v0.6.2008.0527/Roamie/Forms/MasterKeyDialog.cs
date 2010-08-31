using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Virtuoso.Miranda.Roamie.Forms
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
