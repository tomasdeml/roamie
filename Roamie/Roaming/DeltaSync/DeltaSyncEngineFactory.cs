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
            if (Engine == null)
                Engine = new MicrosoftPatchDeltaSyncEngine();

            return Engine;
        }
    }
}
