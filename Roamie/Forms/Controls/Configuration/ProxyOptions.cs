using System;
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;
using System.Net;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Forms.Controls.Configuration
{
    internal sealed partial class ProxyOptions : CategoryItemControl
    {
        #region .ctors

        public ProxyOptions()
        {
            InitializeComponent();
        }

        #endregion

        #region Overrides

        protected override bool OnShow(bool firstTime)
        {
            if (!firstTime)
                return false;

            RoamingConfiguration config = RoamiePlugin.Singleton.RoamingContext.Configuration;

            if (config.UseProxy)
            {
                if (config.Proxy == null)
                    DefaultProxyRBTN.Checked = true;
                else
                {
                    WebProxy proxy = config.Proxy;
                    CustomProxyRBTN.Checked = true;

                    HostTBOX.Text = proxy.Address.GetComponents(UriComponents.Host, UriFormat.Unescaped);
                    PortTBOX.Text = proxy.Address.GetComponents(UriComponents.Port, UriFormat.Unescaped);

                    if (config.AuthenticateToProxy)
                    {
                        SendCredCHBOX.Checked = true;

                        if (proxy.UseDefaultCredentials)
                            UseSystemCredCHBOX.Checked = true;
                        else
                        {
                            UseSystemCredCHBOX.Checked = false;

                            NetworkCredential credential = (NetworkCredential)proxy.Credentials;
                            UsernameTBOX.Text = credential.UserName;
                            PasswordTBOX.Text = credential.Password;
                        }
                    }
                    else
                        SendCredCHBOX.Checked = false;
                }
            }
            else
                NoProxyRBTN.Checked = true;

            AttachControlDirtiers();
            return false;
        }

        private void AttachControlDirtiers()
        {
            NoProxyRBTN.CheckedChanged += SetControlDirtyHandler;
            DefaultProxyRBTN.CheckedChanged += SetControlDirtyHandler;
            CustomProxyRBTN.CheckedChanged += SetControlDirtyHandler;

            HostTBOX.TextChanged += SetControlDirtyHandler;
            PortTBOX.TextChanged += SetControlDirtyHandler;

            SendCredCHBOX.CheckedChanged += SetControlDirtyHandler;
            UseSystemCredCHBOX.CheckedChanged += SetControlDirtyHandler;

            UsernameTBOX.TextChanged += SetControlDirtyHandler;
            PasswordTBOX.TextChanged += SetControlDirtyHandler;
        }

        protected override bool OnHide()
        {
            Uri host;

            if (!CustomProxyRBTN.Checked || Uri.TryCreate("http://" + HostTBOX.Text, UriKind.RelativeOrAbsolute, out host))
                return false;
            else
            {
                MessageBox.Show(Resources.MsgBox_Text_InvalidHost, Resources.MsgBox_Title_InvalidHost, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                HostTBOX.Focus();

                return true;
            }
        }

        protected override void Save()
        {
            RoamingConfiguration config = RoamiePlugin.Singleton.RoamingContext.Configuration;

            if (NoProxyRBTN.Checked)
            {
                config.UseProxy = false;
                config.AuthenticateToProxy = false;

                config.Proxy = null;
            }
            else if (DefaultProxyRBTN.Checked)
            {
                config.UseProxy = true;
                config.AuthenticateToProxy = false;

                config.Proxy = null;
            }
            else
            {
                config.UseProxy = true;
                config.AuthenticateToProxy = SendCredCHBOX.Checked;

                WebProxy proxy = null;

                if (String.IsNullOrEmpty(PortTBOX.Text))
                    proxy = new WebProxy(HostTBOX.Text);
                else
                    proxy = new WebProxy(HostTBOX.Text, int.Parse(PortTBOX.Text));

                if (config.AuthenticateToProxy)
                {
                    if (UseSystemCredCHBOX.Checked)
                        proxy.UseDefaultCredentials = true;
                    else
                        proxy.Credentials = new NetworkCredential(UsernameTBOX.Text, PasswordTBOX.Text);
                }

                config.Proxy = proxy;
            }

            RoamiePlugin.Singleton.RoamingContext.InitializeProxySettings();
        }

        #endregion

        #region UI Handlers

        private void CustomProxyRBTN_CheckedChanged(object sender, EventArgs e)
        {
            LocationGBOX.Enabled = AuthGBOX.Enabled = CustomProxyRBTN.Checked;
        }

        private void UseSystemCredCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            UsernameTBOX.Enabled = PasswordTBOX.Enabled = !UseSystemCredCHBOX.Checked;
        }

        private void SendCredCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            UseSystemCredCHBOX.Enabled = SendCredCHBOX.Checked;
        }

        #endregion
    }
}

