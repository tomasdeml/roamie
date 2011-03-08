using System;
using System.Windows.Forms;
using Virtuoso.Roamie.Configuration;

namespace Virtuoso.Roamie.Forms
{
    internal partial class FirstRunDialog : Form
    {
        private FirstRunDialog()
        {
            InitializeComponent();
        }

        public static void PresentModal()
        {
            using (FirstRunDialog dlg = new FirstRunDialog())
                dlg.ShowDialog();
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            OkBTN.Tag = ((Control)sender).Tag;
            OkBTN.Enabled = true;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            string tag = (string)((Control)sender).Tag;
            var persistencyMode = (ConfigurationPersistencyMode)Enum.Parse(typeof(ConfigurationPersistencyMode), tag);

            ConfigurationManager.Singleton.SetPersistencyMode(persistencyMode);
            Close();
        }
    }
}
