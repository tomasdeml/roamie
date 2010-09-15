using System;

namespace Virtuoso.Roamie
{
    internal static class StringUtility
    {
        public static string FormatExceptionMessage(string message, Exception e)
        {
            return FormatExceptionMessage(message, e, true);
        }

        public static string FormatExceptionMessage(string message, Exception e, bool includeStackTrace)
        {
            return (message + Environment.NewLine + (includeStackTrace ? e.ToString() : e.Message));
        }
    }
}
