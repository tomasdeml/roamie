/***********************************************************************************\
 * Virtuoso.Miranda.Roamie (Roamie)                                                *
 * A Miranda plugin providing a remote database synchronization features.          *
 * Copyright (C) 2006-2007 Virtuoso                                                *
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
using System.Collections.Generic;
using System.Text;
using Virtuoso.Hyphen.Mini;
using Virtuoso.Miranda.Plugins;
using Virtuoso.Miranda.Roamie;
using Virtuoso.Hyphen.Mini.Custom;
using Virtuoso.Miranda.Plugins.Native;
using System.Reflection;
using System.IO;
using Virtuoso.Miranda.Roamie.Forms;
using Virtuoso.Miranda.Roamie.Roaming;
using System.Diagnostics;
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Collections;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Roamie.Native;
using System.Threading;
using Virtuoso.Miranda.Plugins.ThirdParty.Updater;
using Virtuoso.Miranda.Plugins.Forms.Controls;
using Virtuoso.Miranda.Roamie.Forms.Controls.Configuration;
using System.Drawing;
using Virtuoso.Miranda.Roamie.Roaming.DeltaSync;
using Virtuoso.Miranda.Roamie.Roaming.Providers;
using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;
using Virtuoso.Miranda.Plugins.Configuration.Forms;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Miranda.Roamie.Forms.Controls;

[assembly: ExposingPlugin(typeof(RoamiePlugin))]

namespace Virtuoso.Miranda.Roamie
{
    [LoaderOptions("0.8.6.1921")]
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
            get { return "Database roaming proxy."; }
        }

        public override bool HasOptions
        {
            get { return true; }
        }

        public override Uri HomePage
        {
            get { return new Uri("http://virtuosity.aspweb.cz/Miranda/Plugins/Roamie"); }
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

        protected override void AfterMenuItemsPopulation(MenuItemDeclarationCollection items)
        {
            MenuItemDeclarationAttribute item = items.Find("RoamingSettings");

            if (item.Text == "-")
                item.Text = Resources.Text_UI_MenuItem_RoamingOverview;           
        }

        #endregion

        #region Configuration Impl

        private PluginConfiguration[] configuration;
        public PluginConfiguration[] Configuration
        {
            get { return configuration ?? (configuration = new PluginConfiguration[] { roamingContext.Configuration }); }
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
            roamingContext.Configuration = PluginConfiguration.Load<RoamingConfiguration>();
        }

        public void ResetConfiguration()
        {
            roamingContext.Configuration = PluginConfiguration.GetDefaultConfiguration<RoamingConfiguration>();
        }

        #endregion

        #region Fields

        public const string TraceCategory = "Roamie";

        private RoamingContext roamingContext;
        public RoamingContext RoamingContext
        {
            get { return roamingContext; }
        }

        private static RoamiePlugin singleton;
        public static RoamiePlugin Singleton
        {
            get { return singleton; }
        }

        private ExternalDatabaseDriver DatabaseDriver;

        public static TraceSwitch TraceSwitch;

        private static readonly Uri UpdateUrl = new Uri("http://testplace.aspweb.cz/Apps/Roamie/Roamie_update.zip"),
            VersionUrl = new Uri("http://testplace.aspweb.cz/Apps/Roamie/Roamie_update_version.txt");

        #endregion

        #region .ctors

        private RoamiePlugin()
        {
            InitLogger();

            if (singleton == null)
                singleton = this;
            else
            {
                Trace.WriteLineIf(TraceSwitch.TraceError, "Roamie already initialized, internal error.", TraceCategory);
                throw new InvalidOperationException("Internal error.");
            }
        }

        private void InitLogger()
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
                string message = String.Format(Resources.MsgBox_Text_Formatable1_UnableToLoadDbDriver, e.ToString());

                Trace.WriteLineIf(TraceSwitch.TraceError, GlobalEvents.FormatExceptionMessage(message, e), TraceCategory);
                MessageBox.Show(message, Resources.MsgBox_Title_UnableToLoadDbDriver, MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }

            base.AfterPluginInitialization();
        }

        #endregion

        #region Menu item handlers

        [MenuItemDeclaration("-", typeof(LanguagePackStringResolver), Tag = "RoamingSettings", IsContactMenuItem = false, HasIcon = true, UseEmbeddedIcon = true, IconID = "Virtuoso.Miranda.Roamie.Resources.MenuItems.RoamingSettings.ico")]
        private int MenuItem_RoamingSettings(UIntPtr wParam, IntPtr lParam)
        {
            RoamingOverviewDialog.GetSingleton<RoamingOverviewDialog>(true).ShowSingleton(false);
            return 0;
        }
        
        #endregion

        #region Helpers

        private void RegisterForUpdates(object sender, EventArgs e)
        {
            Context.ModulesLoaded -= RegisterForUpdates;

            if (UpdaterPlugin.IsUpdateSupported())
            {
                Update update = new Update(this, UpdateUrl, VersionUrl, " ");
                UpdaterPlugin.RegisterForUpdate(update);
            }
        }

        private bool CanSync
        {
            get
            {
                return roamingContext.ActiveProfile != null &&
                       !roamingContext.IsInState(RoamingState.RoamingDisabled) &&
                       !roamingContext.IsInState(RoamingState.SyncErrorOccured);
            }
        }

        #endregion

        #region Proxy Impl

        #region Roaming configuration

        private int ConfigureRoaming(string profile, bool firstTime)
        {
            try
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, String.Format("'Intialize' method invoked: profile = '{0}', firstTime = '{1}'.", profile, firstTime.ToString()), TraceCategory);

                if (RoamingContext == null)
                {
                    Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Initializing context...", TraceCategory);
                    roamingContext = new RoamingContext(profile);

                    Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Presenting startup dialog...", TraceCategory);
                    StartupDialog.PresentModal(firstTime);

                    if (RoamingContext.State == RoamingState.RoamingDisabled)
                    {
                        Trace.WriteLineIf(TraceSwitch.TraceWarning, "Invalid startup state detected, aborting.", TraceCategory);
                        return (int)CallbackResult.Failure;
                    }
                }

                return (int)CallbackResult.Success;
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(TraceSwitch.TraceError, GlobalEvents.FormatExceptionMessage("'Initialize' method failed.", e), TraceCategory);
                return (int)CallbackResult.Failure;
            }
        }

        #endregion

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
                return (int)CallbackResult.Failure;

            int result = DatabaseDriver.DatabaseLink.Init(RoamingContext.ProfilePath, link);

            if (result == (int)CallbackResult.Success)
            {
                RoamingOrchestration.Performance.LoadDeltaEngine();
                Context.ModulesLoaded += RegisterForUpdates;
            }

            return result;
        }

        protected override int MakeDatabaseThunk(string profile, ref int error)
        {
            int retValue = (int)CallbackResult.Failure;
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'MakeDatabase' API invoked.", TraceCategory);

            if ((error = ConfigureRoaming(profile, true)) == 0)
            {
                if ((roamingContext.State & RoamingState.CreateNewDb) == RoamingState.CreateNewDb)
                {
                    Trace.WriteLineIf(TraceSwitch.TraceInfo, "Creating a database...", TraceCategory);
                    retValue = DatabaseDriver.DatabaseLink.MakeDatabase(roamingContext.ProfilePath, ref error);
                }
                else
                    retValue = (int)CallbackResult.Success;
            }

            return retValue;
        }

        protected override int GrokHeaderThunk(string profile, ref int error)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'GrokHeader' API invoked.", TraceCategory);

            if ((error = ConfigureRoaming(profile, false)) != 0)
                return (int)CallbackResult.Failure;

            return DatabaseDriver.DatabaseLink.GrokHeader(roamingContext.ProfilePath, ref error);
        }

        protected override int UnloadThunk(int wasLoaded)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "'Unload' API invoked.", TraceCategory);

            try
            {
                if (wasLoaded != 0)
                {
                    RoamingOrchestration.Finale.FinalizeDeltaEngine();
                    UnloadDriver(wasLoaded);

                    if (RoamingContext != null)
                    {
                        Trace.WriteLineIf(TraceSwitch.TraceInfo, "Unloading Roamie...", TraceCategory);
                        RoamingContext.Configuration.Save();

                        if (CanSync)
                        {
                            Trace.WriteLineIf(TraceSwitch.TraceInfo, "Active roaming profile detected, no Sync Error detected, roaming state is defined => checking Sandbox mode...", TraceCategory);
                            RoamingOrchestration.Finale.PerformRemoteSync();
                        }
                        else
                            Trace.WriteLineIf(TraceSwitch.TraceWarning, "No active roaming profile detected or undefined roaming state or sync error occured => no synchronization can be performed.", TraceCategory);
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(TraceSwitch.TraceError, GlobalEvents.FormatExceptionMessage("Unable to unload Roamie.", e), TraceCategory);
            }
            finally
            {
                Trace.WriteLineIf(TraceSwitch.TraceInfo, "Unloaded.", TraceCategory);
                Trace.WriteLine("===== ROAMIE LOG END ======");
                Trace.Close();
            }

            return (int)CallbackResult.Success;
        }

        private void UnloadDriver(int wasLoaded)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Unloading external database driver...", TraceCategory);

            int result = DatabaseDriver.DatabaseLink.Unload(wasLoaded);
            Debug.Assert(result == 0);

            DatabaseDriver.Close();
        }

        #endregion        

        #endregion
    }
}
