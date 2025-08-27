using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Geoscape.Entities;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Geoscape.Core;
using PhoenixPoint.Geoscape.Levels;
using HarmonyLib;

namespace TutorialCharacterPerks
{
    public static class TutorialCharacterPerkPatches
    {
        private static DefRepository GetRepo()
        {
            return GameUtl.GameComponent<DefRepository>();
        }
        
        // Store original abilities for restoration
        private static readonly Dictionary<string, TacticalAbilityDef[]> OriginalCharacterAbilities = new Dictionary<string, TacticalAbilityDef[]>();
        
        // Tutorial character template names (from TacCharacterDef.Data.Name)
        private static readonly HashSet<string> TutorialCharacterTemplateNames = new HashSet<string>
        {
            "KEY_TUT_ASSAULT_FEMALE",
            "KEY_TUT_ASSAULT_MALE",
            "KEY_TUT_HEAVY_MALE",
            "KEY_TUT_ASSAULT3_MALE",
            "KEY_TUT_SNIPER_FEMALE"
        };
        
        // Mapping from template names to character names for config lookup
        private static readonly Dictionary<string, string> TemplateNameToCharacterKey = new Dictionary<string, string>
        {
            { "KEY_TUT_ASSAULT_FEMALE", "Sophia" },
            { "KEY_TUT_ASSAULT_MALE", "Jacob" },
            { "KEY_TUT_HEAVY_MALE", "Omar" },
            { "KEY_TUT_ASSAULT3_MALE", "Takeshi" },
            { "KEY_TUT_SNIPER_FEMALE", "Irina" }
        };

        // Perk enum to definition name mapping
        private static readonly Dictionary<TutorialCharacterPerksConfig.PerkType, string> PerkToDefNames = new Dictionary<TutorialCharacterPerksConfig.PerkType, string>
        {
            { TutorialCharacterPerksConfig.PerkType.Reckless, "Reckless_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Thief, "Thief_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Strongman, "Strongman_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Resourceful, "Resourceful_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Trooper, "GoodShot_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.CloseQuarterSpecialist, "CloseQuartersSpecialist_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Cautious, "Cautious_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Biochemist, "BioChemist_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Healer, "Helpful_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Quarterback, "Pitcher_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.SelfDefenseSpecialist, "SelfDefenseSpecialist_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Sniperist, "Focused_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Farsighted, "Brainiac_AbilityDef" },
            { TutorialCharacterPerksConfig.PerkType.Bombardier, "Crafty_AbilityDef" }
        };

        /// <summary>
        /// Patch character generation to assign specific personal abilities to tutorial characters
        /// Following TFTV's approach exactly
        /// </summary>
        [HarmonyPatch(typeof(FactionCharacterGenerator), "GenerateUnit", new Type[] { typeof(GeoFaction), typeof(TacCharacterDef) })]
        public static class GenerateUnit_TutorialCharacterPatch
        {
            private static void Postfix(ref GeoUnitDescriptor __result, GeoFaction faction, TacCharacterDef template)
            {
                try
                {
                    TutorialCharacterPerksMain.LogDebug($"GenerateUnit patch called for template: {template?.Data?.Name ?? "NULL"}");
                    
                    if (TutorialCharacterPerksMain.Main?.Config == null || __result?.Progression == null)
                    {
                        TutorialCharacterPerksMain.LogDebug($"Early return: Main={TutorialCharacterPerksMain.Main != null}, Config={TutorialCharacterPerksMain.Main?.Config != null}, Progression={__result?.Progression != null}");
                        return;
                    }
                        
                    var config = TutorialCharacterPerksMain.Main.Config;
                    
                    // Check if this is a tutorial character by template name
                    string templateName = template.Data.Name;
                    if (!TemplateNameToCharacterKey.TryGetValue(templateName, out string characterKey))
                    {
                        TutorialCharacterPerksMain.LogDebug($"Not a tutorial character: {templateName}");
                        return;
                    }
                    
                    TutorialCharacterPerksMain.LogDebug($"Processing tutorial character generation: {characterKey} ({templateName})");
                    
                    // Get configured perks for this character
                    var configuredPerks = config.GetCharacterPerks(characterKey);
                    TutorialCharacterPerksMain.LogDebug($"Found {configuredPerks.Count} configured perks for {characterKey}: {string.Join(", ", configuredPerks)}");
                    
                    if (configuredPerks.Count == 0)
                    {
                        TutorialCharacterPerksMain.LogDebug($"No perks configured for {characterKey}, using default generation");
                        return;
                    }
                    
                    var repo = GetRepo();
                    var allAbilityDefs = repo.GetAllDefs<TacticalAbilityDef>();
                    
                    // Clear existing personal abilities and assign our configured ones
                    // This follows TFTV's approach exactly: __result.Progression.PersonalAbilities[i] = tacticalAbilityDef;
                    __result.Progression.PersonalAbilities.Clear();
                    
                    // Create a randomized list of levels 1-7 (indices 0-6)
                    var availableLevels = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
                    var random = new System.Random();
                    
                    // Shuffle the levels using Fisher-Yates algorithm
                    for (int i = availableLevels.Count - 1; i > 0; i--)
                    {
                        int j = random.Next(i + 1);
                        (availableLevels[i], availableLevels[j]) = (availableLevels[j], availableLevels[i]);
                    }
                    
                    for (int i = 0; i < configuredPerks.Count && i < 7; i++) // Max 7 personal abilities (0-6)
                    {
                        var perkType = configuredPerks[i];
                        int levelIndex = availableLevels[i]; // Use randomized level
                        
                        if (PerkToDefNames.TryGetValue(perkType, out string defName))
                        {
                            var perkDef = allAbilityDefs.FirstOrDefault(d => d.name == defName);
                            if (perkDef != null)
                            {
                                __result.Progression.PersonalAbilities[levelIndex] = perkDef;
                                TutorialCharacterPerksMain.LogDebug($"  Set personal ability level {levelIndex + 1}: {perkDef.name} for {perkType}");
                            }
                            else
                            {
                                TutorialCharacterPerksMain.LogWarning($"Could not find perk definition: {defName} for {perkType}");
                            }
                        }
                        else
                        {
                            TutorialCharacterPerksMain.LogWarning($"Unknown perk type: {perkType}");
                        }
                    }
                    
                    TutorialCharacterPerksMain.LogDebug($"Applied {__result.Progression.PersonalAbilities.Count} personal abilities to {characterKey}");
                }
                catch (Exception ex)
                {
                    TutorialCharacterPerksMain.LogError($"Error in GenerateUnit_TutorialCharacterPatch: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }
        
        /// <summary>
        /// Apply Harmony patches for tutorial character perks
        /// </summary>
        public static void ApplyTutorialCharacterPerks()
        {
            try
            {
                var harmony = new Harmony("TutorialCharacterPerks");
                harmony.PatchAll();
                TutorialCharacterPerksMain.Main.Logger.LogInfo("Applied Harmony patches for tutorial character personal abilities");
            }
            catch (Exception ex)
            {
                TutorialCharacterPerksMain.Main.Logger.LogError($"Error applying Harmony patches: {ex.Message}\n{ex.StackTrace}");
            }
        }
        
        /// <summary>
        /// Restore original character abilities
        /// </summary>
        public static void RestoreOriginalAbilities()
        {
            try
            {
                var repo = GetRepo();
                
                foreach (var kvp in TemplateNameToCharacterKey)
                {
                    string templateName = kvp.Key;
                    string characterKey = kvp.Value;
                    
                    if (!OriginalCharacterAbilities.ContainsKey(characterKey))
                        continue;
                        
                    var tacCharDef = repo.GetAllDefs<TacCharacterDef>()
                        .FirstOrDefault(t => t.Data.Name == templateName);
                        
                    if (tacCharDef != null)
                    {
                        tacCharDef.Data.Abilites = OriginalCharacterAbilities[characterKey];
                        TutorialCharacterPerksMain.LogDebug($"Restored original abilities for {characterKey}");
                    }
                }
            }
            catch (Exception ex)
            {
                TutorialCharacterPerksMain.LogError($"Error restoring abilities: {ex.Message}");
            }
        }
    }
}