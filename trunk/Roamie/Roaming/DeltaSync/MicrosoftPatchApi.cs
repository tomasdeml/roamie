using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    [SuppressUnmanagedCodeSecurity]
    internal class MicrosoftPatchApi : IDeltaUtility
    {
        #region Native methods

        [DllImport("mspatchc.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CreatePatchFileW(string oldFile, string newFile, string patchFile, ulong flags);

        [DllImport("mspatcha.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool ApplyPatchToFileW(string patchFile, string oldFile, string newFile, ulong flags);

        #endregion

        public void CreatePatch(string oldFilePath, string newFilePath, string outputPatchFilePath)
        {
            // TODO To constant
            bool result = CreatePatchFileW(oldFilePath, newFilePath, outputPatchFilePath, 0x00000001);

            if (!result)
                ThrowPatchApiException();
        }

        public void ApplyPatch(string oldFilePath, string patchFilePath, string outputNewFilePath)
        {
            bool result = ApplyPatchToFileW(patchFilePath, oldFilePath, outputNewFilePath, 0);
            Console.WriteLine(result);

            if (!result)
                ThrowPatchApiException();
        }

        private static void ThrowPatchApiException()
        {
            int winError = Marshal.GetLastWin32Error();

            // TODO
            throw new Exception(winError.ToString());
        }
    }
}
