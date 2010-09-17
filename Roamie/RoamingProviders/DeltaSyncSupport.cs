using System;
using System.Collections.Generic;
using System.Text;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class DeltaSyncSupport : ProviderDecorator
    {
        public DeltaSyncSupport(DatabaseProvider provider) : base(provider)
        {
        }

        // if (Context.IsInState(RoamingState.DeltaIncompatibleChangeOccured) || Context.IsInState(RoamingState.PreferFullSync))
    }
}
