/***********************************************************************************\
 * Virtuoso.Miranda.Roamie (Roamie)                                                *
 * A Miranda plugin providing a remote database synchronization features.          *
 * Copyright (C) 2006-2010 virtuoso                                                *
 *                    deml.tomas@seznam.cz                                         *
 *                                                                                 *
 * This program is free software; you can redistribute it and/or                   *
 * modify it under the terms of the GNU General Public License                     *
 * as published by the Free Software Foundation; either version 2                  *
 * of the License, or (at your option) any later version.                          *
 *                                                                                 *
 * This program is distributed in the hope that it will be useful,                 *
 * but WITHOUT ANY WARRANTY; without even the implied warranty of                  *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                   *
 * GNU General Public License for more details.                                    *
 *                                                                                 *
 * You should have received a copy of the GNU General Public License               *
 * along with this program; if not, write to the Free Software                     *
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA. *
\***********************************************************************************/

using System;
using Virtuoso.Miranda.Plugins;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Hyphen.Mini.Custom;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Collections;
using Virtuoso.Miranda.Plugins.ThirdParty.Updater;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Roamie;
using Virtuoso.Roamie.Forms;
using Virtuoso.Roamie.Forms.Controls;
using Virtuoso.Roamie.Forms.Controls.Configuration;
using Virtuoso.Roamie.Native;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Properties;

[assembly: ExposingPlugin(typeof(RoamiePlugin))]

namespace Virtuoso.Roamie
{
    [LoaderOptions("0.8.3000.909")]
    internal sealed class RoamiePlugin : DatabaseDriver, IConfigurablePlugin
    {
        #region Plugin infos

        public override string AuthorEmail
        {
            get { return "deml.tomas@seznam.cz"; }
        }

        public override string Copyright
        {
            get { return "virtuoso"; }
        }

        public override int ReplacesDefaultModule
        {
            get { throw new NotImplementedException(); }
        }

        public override string Author
        {
            get { return "virtuoso"; }
        }

        public override string Description
        {
            get { return "Profile synchronization plugin."; }
        }

        public override bool HasOptions
        {
            get { return true; }
        }

        public override Uri HomePage
        {
            get { return new Uri("http://virtuosity.aspone.cz/MirandaDev/Roamie"); }
        }

        public override string Name
        {
            get { return "Roamie"; }
        }

        public override Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public override Guid UUID
        {
            get { throw new NotImplementedException(); }
        }

        public override Guid[] PluginInterfaces
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Fields

        public const string TraceCategory = "Roamie";
        public static TraceSwitch TraceSwitch;

        private ExternalDatabaseDriver DatabaseDriver;

        private static readonly Uri UpdateUrl = new Uri("http://testplace.aspweb.cz/Apps/Roamie/Roamie_update.zip"),
            VersionUrl = new Uri("http://testplace.aspweb.cz/Apps/Roamie/Roamie_update_version.txt");

        #endregion

        #region Properties

        public RoamingContext RoamingContext { get; private set; }

        public static RoamiePlugin Singleton { get; private set; }

        private bool CanSync
        {
            get
            {
                return RoamingContext.ActiveProfile != null &&
                       !RoamingContext.IsInState(RoamingState.Disabled) &&
                       !RoamingContext.IsInState(RoamingState.SyncErrorOccured);
            }
        }

        #endregion

        #region .ctors

        private RoamiePlugin()
        {
            InitLogger();

            if (Singleton == null)
                Singleton = this;
            else
            {
                Trace.WriteLineIf(TraceSwitch.TraceError, "Roamie already initialized, internal error.", TraceCategory);
                throw new InvalidOperationException("Internal error.");
            }
        }

        private static void InitLogger()
        {
            TraceSwitch = new TraceSwitch("RoamieTracing", "Roaming Tracing", "Warning");

            try
            {
                TextWriterTraceListener writer = new TextWriterTraceListener();

                writer.Writer = new StreamWriter(Application.StartupPath + @"\Roamie.log", true);
                writer.TraceOutputOptions = TraceOptions.Timestamp | TraceOptions.DateTime | TraceOptions.Callstack;
                writer.WriteLine("===== ROAMIE LOG BEGIN =====");

                Trace.Listeners.Add(writer);
                Trace.AutoFlush = true;
            }
            catch { }
        }

        protected override void AfterModuleInitialization()
        {
            try
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Loading external database driver...", TraceCategory);
                DatabaseDriver = ExternalDatabaseDriver.Load(Module.IsPostV07Build20Api);
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "External database driver loaded.", TraceCategory);
            }
            catch (Exception e)
            {
                string message = String.Format(Resources.MsgBox_Text_Formatable1_UnableToLoadDbDriver, e);

                Trace.WriteLineIf(TraceSwitch.TraceError, StringUtility.FormatExceptionMessage(message, e), TraceCategory);
                MessageBox.Show(message, Resources.MsgBox_Title_UnableToLoadDbDriver, MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }

            AfterPluginInitialization();
        }

        #endregion

        #region Configuration Impl

        // TODO Refactor

        private PluginConfiguration[] configuration;
        public PluginConfiguration[] Configuration
        {
            get { return configuration ?? (configuration = new PluginConfiguration[] { RoamingContext.Configuration }); }
        }

        public void PopulateConfiguration(CategoryCollection categories)
        {
            PopulateProfilesCategory(categories);
            PopulateGeneralCategory(categories);
        }

        private static void PopulateProfilesCategory(CategoryCollection categories)
        {
            Category profCategory = new Category(Resources.Config_RoamingProfiles, Resources.Config_RoamingProfiles_Description);
            categories.Add(profCategory);

            CategoryItem item = new CategoryItem(Resources.Config_RoamingProfiles_NewProfile, Resources.Config_RoamingProfiles_NewProfile_Description, delegate
            {
                if (ProfileCreatingDialog.PresentModal() != null)
                    MessageBox.Show(Resources.MsgBox_Text_ProfileCreated, Resources.MsgBox_Title_Completed, MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            item.Image = Resources.Image_64x67_Add;
            profCategory.Items.Add(item);

            item = new CategoryItem(Resources.Config_RoamingProfiles_Manage, Resources.Config_RoamingProfiles_Manage_Description, typeof(ProfileManagement));
            item.Image = Resources.Image_64x67_Profile;
            profCategory.Items.Add(item);

            item = new CategoryItem(Resources.Config_RoamingProfiles_DefaultProfile, Resources.Config_RoamingProfiles_DefaultProfile_Description, typeof(AutoProfileOptions));
            item.Image = Resources.Image_64x67_Favourite;
            item.IsExpertOption = true;
            profCategory.Items.Add(item);
        }

        private static void PopulateGeneralCategory(CategoryCollection categories)
        {
            Category genCategory = new Category(Resources.Config_General, Resources.Config_General_Behaviour_Description);
            categories.Add(genCategory);

            CategoryItem item = new CategoryItem(Resources.Config_General_About, Resources.Config_General_About_Description, typeof(AboutInformation));
            item.Image = Resources.Image_64x67_Information;
            genCategory.Items.Add(item);

            item = new CategoryItem(Resources.Config_General_Behaviour, Resources.Config_General_Behaviour_Description, typeof(BehaviourOptions));
            item.Image = Resources.Image_64x67_Settings;
            genCategory.Items.Add(item);

            item = new CategoryItem(Resources.Config_General_Proxy, Resources.Config_General_Proxy_Description, typeof(ProxyOptions));
            item.Image = Resources.Image_64x67_Web;
            genCategory.Items.Add(item);
        }

        public void ReloadConfiguration()
        {
            RoamingContext.Configuration = PluginConfiguration.Load<RoamingConfiguration>();
        }

        public void ResetConfiguration()
        {
            RoamingContext.Configuration = PluginConfiguration.GetDefaultConfiguration<RoamingConfiguration>();
        }

        #endregion

        #region Menu item handlers

        protected override void AfterMenuItemsPopulation(MenuItemDeclarationCollection items)
        {
            MenuItemDeclarationAttribute item = items.Find("RoamingSettings");

            if (item.Text == "-")
                item.Text = Resources.Text_UI_MenuItem_RoamingOverview;
        }

        [MenuItemDeclaration("-", typeof(LanguagePackStringResolver), Tag = "RoamingSettings", IsContactMenuItem = false, HasIcon = true, UseEmbeddedIcon = true, IconID = "Virtuoso.Miranda.Roamie.Resources.MenuItems.RoamingSettings.ico")]
        private int MenuItem_RoamingSettings(UIntPtr wParam, IntPtr lParam)
        {
            SingletonDialog.GetSingleton<RoamingOverviewDialog>(true).ShowSingleton(false);
            return 0;
        }
        
        #endregion

        #region Helpers

        private void RegisterForUpdates(object sender, EventArgs e)
        {
            Context.ModulesLoaded -= RegisterForUpdates;

            if (!UpdaterPlugin.IsUpdateSupported())
                return;

            Update update = new Update(this, UpdateUrl, VersionUrl, " ");
            UpdaterPlugin.RegisterForUpdate(update);
        }

        #endregion

        #region Proxy Impl

        #region Plain thunks

        protected override IntPtr MirandaPluginInfo(uint version, bool ex)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, String.Format("'MirandaPluginInfo' export invoked: version = '{0}'", version.ToString()), TraceCategory);
            return DatabaseDriver.MirandaPluginInfo(version);
        }

        protected override IntPtr MirandaPluginInterfaces()
        {
            return DatabaseDriver.MirandaPluginInterfaces();
        }

        protected override int GetCapabilityThunk(int flags)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'GetCapability' api method invoked: flags = '" + flags.ToString("g") + "'", TraceCategory);
            return DatabaseDriver.DatabaseLink.GetCapability(flags);
        }

        protected override int GetFriendlyNameThunk(IntPtr buffer, int size, int shortName)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'GetFriendlyName' api method invoked.", TraceCategory);
            return DatabaseDriver.DatabaseLink.GetFriendlyName(buffer, size, shortName);
        }

        #endregion

        #region Major thunks

        protected override int InitThunk(string profile, IntPtr link)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'Init' API invoked.", TraceCategory);

            if (ConfigureRoaming(profile, false) != 0)
                return CallbackResult.Failure;

            int result = DatabaseDriver.DatabaseLink.Init(RoamingContext.ProfilePath, link);

            if (result == CallbackResult.Success)
                Context.ModulesLoaded += RegisterForUpdates;

            return result;
        }

        protected override int MakeDatabaseThunk(string profile, ref int error)
        {
            int retValue = CallbackResult.Failure;
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'MakeDatabase' API invoked.", TraceCategory);

            if ((error = ConfigureRoaming(profile, true)) == CallbackResult.Success)
            {
                if ((RoamingContext.State & RoamingState.NewProfileCreated) == RoamingState.NewProfileCreated)
                {
                    Trace.WriteLineIf(TraceSwitch.TraceInfo, "Creating a database...", TraceCategory);
                    retValue = DatabaseDriver.DatabaseLink.MakeDatabase(RoamingContext.ProfilePath, ref error);
                }
                else
                    retValue = CallbackResult.Success;
            }

            return retValue;
        }

        protected override int GrokHeaderThunk(string profile, ref int error)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'GrokHeader' API invoked.", TraceCategory);

            if ((error = ConfigureRoaming(profile, false)) != CallbackResult.Success)
                return CallbackResult.Failure;

            return DatabaseDriver.DatabaseLink.GrokHeader(RoamingContext.ProfilePath, ref error);
        }

        protected override int UnloadThunk(int wasLoaded)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'Unload' API invoked.", TraceCategory);

            try
            {
                if (wasLoaded != 0)
                {
                    UnloadDriver(wasLoaded);

                    if (RoamingContext != null)
                    {
                        Trace.WriteLineIf(TraceSwitch.TraceInfo, "Unloading Roamie...", TraceCategory);
                        RoamingContext.Configuration.Save();

                        if (CanSync)
                        {
                            Trace.WriteLineIf(TraceSwitch.TraceInfo, "Active roaming profile detected, no Sync Error detected, roaming state is defined => checking Sandbox mode...", TraceCategory);
                            RoamingOrchestration.SyncRemoteSite();
                        }
                        else
                            Trace.WriteLineIf(TraceSwitch.TraceWarning, "No active roaming profile detected or undefined roaming state or sync error occured => no synchronization can be performed.", TraceCategory);
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(TraceSwitch.TraceError, StringUtility.FormatExceptionMessage("Unable to unload Roamie.", e), TraceCategory);
            }
            finally
            {
                Trace.WriteLineIf(TraceSwitch.TraceInfo, "Unloaded.", TraceCategory);
                Trace.WriteLine("===== ROAMIE LOG END ======");
                Trace.Close();
            }

            return CallbackResult.Success;
        }

        private void UnloadDriver(int wasLoaded)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Unloading external database driver...", TraceCategory);

            int result = DatabaseDriver.DatabaseLink.Unload(wasLoaded);
            Debug.Assert(result == 0);

            DatabaseDriver.Close();
        }

        #endregion        

        #region Roaming configuration

        private int ConfigureRoaming(string profile, bool firstTime)
        {
            try
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, String.Format("'Intialize' method invoked: profile = '{0}', firstTime = '{1}'.", profile, firstTime.ToString()), TraceCategory);

                if (RoamingContext == null)
                {
                    Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Initializing context...", TraceCategory);
                    RoamingContext = new RoamingContext(profile);

                    Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Presenting startup dialog...", TraceCategory);
                    StartupDialog.PresentModal(firstTime);

                    if (RoamingContext.State == RoamingState.Disabled)
                    {
                        Trace.WriteLineIf(TraceSwitch.TraceWarning, "Invalid startup state detected, aborting.", TraceCategory);
                        return CallbackResult.Failure;
                    }
                }

                return CallbackResult.Success;
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(TraceSwitch.TraceError, StringUtility.FormatExceptionMessage("'Initialize' method failed.", e), TraceCategory);
                return CallbackResult.Failure;
            }
        }

        #endregion

        #endregion
    }
}
