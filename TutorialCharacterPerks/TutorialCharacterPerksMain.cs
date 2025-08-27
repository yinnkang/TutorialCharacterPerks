using Base.Defs;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using System;

namespace TutorialCharacterPerks
{
    public class TutorialCharacterPerksMain : ModMain
    {
        internal static TutorialCharacterPerksMain Main;

        public new TutorialCharacterPerksConfig Config => (TutorialCharacterPerksConfig)base.Config;

        public override void OnModEnabled()
        {
            Main = this;

            try
            {
                TutorialCharacterPerkPatches.ApplyTutorialCharacterPerks();
                Logger.LogInfo("[TutorialCharacterPerks] Enabled and character perks applied.");
            }
            catch (Exception e)
            {
                Logger.LogError("[TutorialCharacterPerks] Failed to apply character perks: " + e);
            }
        }

        public override void OnConfigChanged()
        {
            try
            {
                TutorialCharacterPerkPatches.ApplyTutorialCharacterPerks();
                Logger.LogInfo("[TutorialCharacterPerks] Config changed, character perks reapplied.");
            }
            catch (Exception e)
            {
                Logger.LogError("[TutorialCharacterPerks] Failed to apply character perks after config change: " + e);
            }
        }

        public override void OnModDisabled()
        {
            TutorialCharacterPerkPatches.RestoreOriginalAbilities();
            Logger.LogInfo("[TutorialCharacterPerks] Disabled and restored original abilities.");
            Main = null;
        }

        // Static logging helpers
        public static void LogDebug(string message)
        {
            if (Main != null && Main.Config != null && Main.Config.DebugLogging)
                Main.Logger.LogInfo("[TutorialCharacterPerks][DEBUG] " + message);
        }

        public static void LogWarning(string message)
        {
            if (Main != null)
                Main.Logger.LogWarning("[TutorialCharacterPerks] " + message);
        }

        public static void LogError(string message)
        {
            if (Main != null)
                Main.Logger.LogError("[TutorialCharacterPerks] " + message);
        }
    }
}