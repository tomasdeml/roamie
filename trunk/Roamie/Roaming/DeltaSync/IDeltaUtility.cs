using System;
using System.Collections.Generic;
using System.Text;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    internal interface IDeltaUtility
    {
        void CreatePatch(string oldFilePath, string newFilePath, string outputPatchFilePath);

        void ApplyPatch(string oldFilePath, string patchFilePath, string outputNewFilePath);
    }
}
