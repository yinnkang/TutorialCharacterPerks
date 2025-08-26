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

namespace TutorialCharacterPerks
{
    public static class TutorialCharacterPerkPatches
    {
        private static DefRepository GetRepo()
        {
            return GameUtl.GameComponent<DefRepository>();
        }
        
        // Tutorial character TacCharacterDef GUIDs from CustomStartingSquad reference
        private static readonly Dictionary<string, string> TutorialCharacterGuids = new Dictionary<string, string>
        {
            { "Sophia", "400f644c-41f2-c534-1b99-34d48400b7f7" }, // PX_Sophia_Tutorial2_TacCharacterDef
            { "Jacob", "2f7a41a8-d68a-3374-1a13-16f18425d7bb" }, // PX_Jacob_Tutorial2_TacCharacterDef
            { "Omar", "8c9986d9-d875-e0e4-8978-578af6eba952" }, // PX_Omar_Tutorial3_TacCharacterDef
            { "Takeshi", "d008b763-7eac-e7f4-e9c4-57eec8bb0c1e" }, // PX_Takeshi_Tutorial3_TacCharacterDef
            { "Irina", "e3c06e40-0543-fa04-5a9d-7ff43410b1e0" } // PX_Irina_Tutorial3_TacCharacterDef
        };

        // Perk name to definition name mapping (from SelectClassTraits)
        private static readonly Dictionary<string, string> DisplayToDefNames = new Dictionary<string, string>
        {
            { "RECKLESS", "Reckless_AbilityDef" },
            { "THIEF", "Thief_AbilityDef" },
            { "STRONGMAN", "Strongman_AbilityDef" },
            { "RESOURCEFUL", "Resourceful_AbilityDef" },
            { "TROOPER", "GoodShot_AbilityDef" },
            { "CLOSE_QUARTERS_SPECIALIST", "CloseQuartersSpecialist_AbilityDef" },
            { "CAUTIOUS", "Cautious_AbilityDef" },
            { "BIOCHEMIST", "BioChemist_AbilityDef" },
            { "HEALER", "Helpful_AbilityDef" },
            { "QUARTERBACK", "Pitcher_AbilityDef" },
            { "SELF_DEFENSE_SPECIALIST", "SelfDefenseSpecialist_AbilityDef" },
            { "SNIPERIST", "Focused_AbilityDef" },
            { "FARSIGHTED", "Brainiac_AbilityDef" },
            { "BOMBARDIER", "Crafty_AbilityDef" }
        };

        public static void ApplyTutorialCharacterPerks()
        {
            try
            {
                var config = TutorialCharacterPerksMain.Main.Config;
                TutorialCharacterPerksMain.Main.Logger.LogInfo($"Starting ApplyTutorialCharacterPerks - Config is {(config == null ? "null" : "available")}");
                
                foreach (var characterPair in TutorialCharacterGuids)
                {
                    string characterKey = characterPair.Key;
                    string guid = characterPair.Value;
                    
                    var repo = GetRepo();
                    TacCharacterDef tacCharDef = (TacCharacterDef)repo.GetDef(guid);
                    if (tacCharDef == null) 
                    {
                        TutorialCharacterPerksMain.Main.Logger.LogWarning($"Could not find TacCharacterDef for {characterKey} (GUID: {guid})");
                        continue;
                    }
                    
                    TutorialCharacterPerksMain.Main.Logger.LogInfo($"Found TacCharacterDef for {characterKey}, scanning config fields...");
                    
                    List<TacticalAbilityDef> newPerks = new List<TacticalAbilityDef>();
                    int fieldsFound = 0;
                    int fieldsEnabled = 0;
                    
                    foreach (FieldInfo field in config.GetType().GetFields())
                    {
                        if (field.FieldType == typeof(bool) && field.Name.StartsWith($"{characterKey}_"))
                        {
                            fieldsFound++;
                            bool isEnabled = (bool)field.GetValue(config);
                            TutorialCharacterPerksMain.Main.Logger.LogInfo($"  Field {field.Name}: {isEnabled}");
                            
                            if (isEnabled)
                            {
                                fieldsEnabled++;
                                string perkName = field.Name.Substring(characterKey.Length + 1); // Remove "CharacterName_" prefix
                                TutorialCharacterPerksMain.Main.Logger.LogInfo($"  Perk name extracted: {perkName}");
                                
                                if (DisplayToDefNames.ContainsKey(perkName))
                                {
                                    string defName = DisplayToDefNames[perkName];
                                    
                                    // Search all defs by name (direct lookup doesn't work reliably)
                                    var allDefs = repo.GetAllDefs<TacticalAbilityDef>();
                                    var perkDef = allDefs.FirstOrDefault(d => d.name == defName);
                                    
                                    if (perkDef != null && !newPerks.Contains(perkDef))
                                    {
                                        newPerks.Add(perkDef);
                                        TutorialCharacterPerksMain.Main.Logger.LogInfo($"  Added perk: {perkDef.name}");
                                    }
                                    else if (perkDef == null)
                                    {
                                        TutorialCharacterPerksMain.Main.Logger.LogWarning($"Could not find perk definition: {defName}");
                                    }
                                }
                                else
                                {
                                    TutorialCharacterPerksMain.Main.Logger.LogWarning($"Unknown perk name: {perkName}");
                                }
                            }
                        }
                    }
                    
                    TutorialCharacterPerksMain.Main.Logger.LogInfo($"For {characterKey}: found {fieldsFound} config fields, {fieldsEnabled} enabled, {newPerks.Count} valid perks");
                    
                    if (newPerks.Count > 0)
                    {
                        // Replace the abilities in the character definition (following Officer Class pattern)
                        tacCharDef.Data.Abilites = newPerks.ToArray();
                        TutorialCharacterPerksMain.Main.Logger.LogInfo($"Applied {newPerks.Count} perks to {characterKey}: {string.Join(", ", newPerks.Select(p => p.name))}");
                    }
                    else
                    {
                        TutorialCharacterPerksMain.Main.Logger.LogInfo($"No perks configured for {characterKey}, leaving default perks");
                    }
                }
            }
            catch (Exception ex)
            {
                TutorialCharacterPerksMain.Main.Logger.LogError($"Error in ApplyTutorialCharacterPerks: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}