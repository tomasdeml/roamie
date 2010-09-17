using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class OneWaySynchronization : ProviderDecorator
    {
        public OneWaySynchronization(DatabaseProvider provider) : base(provider)
        {
        }

        public override void OnSelected()
        {
            Context.State |= RoamingState.MirroringNotSupported;
            Context.State |= RoamingState.DiscardLocalChanges;

            if ((Context.State & RoamingState.WipeLocalDbOnExit) == RoamingState.WipeLocalDbOnExit)
                InformationDialog.PresentModal(Resources.Information_Caption_YourChangesWiilBeLost, Resources.Information_Formatable1_Text_YourChangesWillBeLost, Resources.Image_32x32_Profile);

            base.OnSelected();
        }

        public override void SyncRemoteSite(RoamingProfile profile)
        {
            Provider.NonSyncShutdown();
            Provider.RemoveLocalSiteData();
        }

        /*public override void NonSyncShutdown()
        {
            if ((Context.State & RoamingState.WipeLocalDbOnExit) != RoamingState.WipeLocalDbOnExit ||
                (Context.State & RoamingState.DiscardLocalChanges) != RoamingState.DiscardLocalChanges)
                InformationDialog.PresentModal(Resources.Information_Caption_CannotMirrorChanges, String.Format(Resources.Information_Formatable1_Text_CannotMirrorChanges, Path.GetFileName(Context.ProfilePath)), Resources.Image_32x32_Web);
            
        }*/
    }
}
