using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace HideoutTroopCards
{
    internal static class Log
    {
        /// <summary>Message-log line, only when "Debug messages" is on. Debug text isn't translated.</summary>
        public static void Debug(string text)
        {
            if (!HtcSettings.DebugMessages) return;
            InformationManager.DisplayMessage(new InformationMessage("[HideoutTroopCards] " + text, Colors.Gray));
        }

        public static void Error(string where, Exception e)
        {
            InformationManager.DisplayMessage(new InformationMessage(
                new TextObject("{=htc_err_generic}[HideoutTroopCards] error ({WHERE}): {ERROR}")
                    .SetTextVariable("WHERE", where)
                    .SetTextVariable("ERROR", e.Message)
                    .ToString(),
                Colors.Red));
        }

        public static void Info(TextObject text, Color color)
        {
            InformationManager.DisplayMessage(new InformationMessage(text.ToString(), color));
        }
    }
}
