using System.Runtime.CompilerServices;
namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    internal static class DeltaSyncEngineFactory
    {
        #region Fields

        private static IDeltaSyncEngine Engine; 

        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDeltaSyncEngine GetEngine()
        {
            return Engine ?? (Engine = new MicrosoftPatchDeltaSyncEngine());
        }
    }
}
