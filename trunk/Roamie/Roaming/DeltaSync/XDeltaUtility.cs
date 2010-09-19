using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    internal class XDeltaUtility : IDeltaUtility
    {
        public void CreatePatch(string oldFilePath, string newFilePath, string outputPatchFilePath)
        {
            Process proc = CreateProcess();
            proc.StartInfo.Arguments = String.Format("-e -0 -s \"{0}\" \"{1}\" \"{2}\"", oldFilePath, newFilePath, outputPatchFilePath);
            
            proc.Start();
            proc.WaitForExit();

            if (proc.ExitCode != 0)
                throw new Exception("Patch generation failed."); // TODO
        }

        public void ApplyPatch(string oldFilePath, string patchFilePath, string outputNewFilePath)
        {
            Process proc = CreateProcess();
            proc.StartInfo.Arguments = String.Format("-d -s \"{0}\" \"{1}\" \"{2}\"", oldFilePath, patchFilePath, outputNewFilePath);

            proc.Start();
            proc.WaitForExit();

            if (proc.ExitCode != 0)
                throw new Exception("Patch generation failed."); // TODO
        }

        private static Process CreateProcess()
        {
            string xDeltaExePath = Path.Combine(MirandaEnvironment.MirandaFolderPath, "xdelta.exe");

            if (!File.Exists(xDeltaExePath))
                throw new FileNotFoundException("xdelta.exe not found in your Miranda directory."); // TODO Resx

            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(xDeltaExePath);
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.ErrorDialog = false;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            return proc;
        }
    }
}
