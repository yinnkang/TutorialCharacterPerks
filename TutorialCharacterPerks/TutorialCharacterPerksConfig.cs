using PhoenixPoint.Modding;
using System.Collections.Generic;

namespace TutorialCharacterPerks
{
    public class TutorialCharacterPerksConfig : ModConfig
    {
        public enum PerkType
        {
            None,
            Reckless,
            Thief,
            Strongman,
            Resourceful,
            Trooper,
            CloseQuarterSpecialist,
            Cautious,
            Biochemist,
            Healer,
            Quarterback,
            SelfDefenseSpecialist,
            Sniperist,
            Farsighted,
            Bombardier
        }
        // ─────────────────────────── Global Options ───────────────────────────
        [ConfigField(text: "Debug Logging")]
        public bool DebugLogging = false;
        
        [ConfigField(text: "Randomize Perk Levels")]
        public bool RandomizePerkLevels = true;
        
        [ConfigField(text: "Allowed Perk Levels (comma-separated 2-7)")]
        public string AllowedPerkLevelsStr = "2,3,4,5,6,7";

        // ───────────────────────────── Sophia Brown ────────────────────────────────
        [ConfigField(text: "Sophia: Perk 1")]
        public PerkType SophiaPerk1 = PerkType.Reckless;
        
        [ConfigField(text: "Sophia: Perk 2")]
        public PerkType SophiaPerk2 = PerkType.Thief;
        
        [ConfigField(text: "Sophia: Perk 3")]
        public PerkType SophiaPerk3 = PerkType.Trooper;
        
        [ConfigField(text: "Sophia: Perk 4")]
        public PerkType SophiaPerk4 = PerkType.None;
        
        [ConfigField(text: "Sophia: Perk 5")]
        public PerkType SophiaPerk5 = PerkType.None;

        // ───────────────────────────── Jacob Eber ────────────────────────────────
        [ConfigField(text: "Jacob: Perk 1")]
        public PerkType JacobPerk1 = PerkType.Reckless;
        
        [ConfigField(text: "Jacob: Perk 2")]
        public PerkType JacobPerk2 = PerkType.Trooper;
        
        [ConfigField(text: "Jacob: Perk 3")]
        public PerkType JacobPerk3 = PerkType.Sniperist;
        
        [ConfigField(text: "Jacob: Perk 4")]
        public PerkType JacobPerk4 = PerkType.None;
        
        [ConfigField(text: "Jacob: Perk 5")]
        public PerkType JacobPerk5 = PerkType.None;

        // ───────────────────────────── Omar Ashour ────────────────────────────────
        [ConfigField(text: "Omar: Perk 1")]
        public PerkType OmarPerk1 = PerkType.Reckless;
        
        [ConfigField(text: "Omar: Perk 2")]
        public PerkType OmarPerk2 = PerkType.Strongman;
        
        [ConfigField(text: "Omar: Perk 3")]
        public PerkType OmarPerk3 = PerkType.Cautious;
        
        [ConfigField(text: "Omar: Perk 4")]
        public PerkType OmarPerk4 = PerkType.None;
        
        [ConfigField(text: "Omar: Perk 5")]
        public PerkType OmarPerk5 = PerkType.None;

        // ───────────────────────────── Takeshi Sato ────────────────────────────────
        [ConfigField(text: "Takeshi: Perk 1")]
        public PerkType TakeshiPerk1 = PerkType.Reckless;
        
        [ConfigField(text: "Takeshi: Perk 2")]
        public PerkType TakeshiPerk2 = PerkType.Thief;
        
        [ConfigField(text: "Takeshi: Perk 3")]
        public PerkType TakeshiPerk3 = PerkType.CloseQuarterSpecialist;
        
        [ConfigField(text: "Takeshi: Perk 4")]
        public PerkType TakeshiPerk4 = PerkType.None;
        
        [ConfigField(text: "Takeshi: Perk 5")]
        public PerkType TakeshiPerk5 = PerkType.None;

        // ───────────────────────────── Irina Sokolova ────────────────────────────────
        [ConfigField(text: "Irina: Perk 1")]
        public PerkType IrinaPerk1 = PerkType.Reckless;
        
        [ConfigField(text: "Irina: Perk 2")]
        public PerkType IrinaPerk2 = PerkType.Thief;
        
        [ConfigField(text: "Irina: Perk 3")]
        public PerkType IrinaPerk3 = PerkType.Sniperist;
        
        [ConfigField(text: "Irina: Perk 4")]
        public PerkType IrinaPerk4 = PerkType.None;
        
        [ConfigField(text: "Irina: Perk 5")]
        public PerkType IrinaPerk5 = PerkType.None;
        
        // ─────────────────────────── Helper Methods ───────────────────────────
        public List<PerkType> GetCharacterPerks(string characterName)
        {
            var perks = new List<PerkType>();
            
            switch (characterName.ToLower())
            {
                case "sophia":
                    if (SophiaPerk1 != PerkType.None) perks.Add(SophiaPerk1);
                    if (SophiaPerk2 != PerkType.None) perks.Add(SophiaPerk2);
                    if (SophiaPerk3 != PerkType.None) perks.Add(SophiaPerk3);
                    if (SophiaPerk4 != PerkType.None) perks.Add(SophiaPerk4);
                    if (SophiaPerk5 != PerkType.None) perks.Add(SophiaPerk5);
                    break;
                case "jacob":
                    if (JacobPerk1 != PerkType.None) perks.Add(JacobPerk1);
                    if (JacobPerk2 != PerkType.None) perks.Add(JacobPerk2);
                    if (JacobPerk3 != PerkType.None) perks.Add(JacobPerk3);
                    if (JacobPerk4 != PerkType.None) perks.Add(JacobPerk4);
                    if (JacobPerk5 != PerkType.None) perks.Add(JacobPerk5);
                    break;
                case "omar":
                    if (OmarPerk1 != PerkType.None) perks.Add(OmarPerk1);
                    if (OmarPerk2 != PerkType.None) perks.Add(OmarPerk2);
                    if (OmarPerk3 != PerkType.None) perks.Add(OmarPerk3);
                    if (OmarPerk4 != PerkType.None) perks.Add(OmarPerk4);
                    if (OmarPerk5 != PerkType.None) perks.Add(OmarPerk5);
                    break;
                case "takeshi":
                    if (TakeshiPerk1 != PerkType.None) perks.Add(TakeshiPerk1);
                    if (TakeshiPerk2 != PerkType.None) perks.Add(TakeshiPerk2);
                    if (TakeshiPerk3 != PerkType.None) perks.Add(TakeshiPerk3);
                    if (TakeshiPerk4 != PerkType.None) perks.Add(TakeshiPerk4);
                    if (TakeshiPerk5 != PerkType.None) perks.Add(TakeshiPerk5);
                    break;
                case "irina":
                    if (IrinaPerk1 != PerkType.None) perks.Add(IrinaPerk1);
                    if (IrinaPerk2 != PerkType.None) perks.Add(IrinaPerk2);
                    if (IrinaPerk3 != PerkType.None) perks.Add(IrinaPerk3);
                    if (IrinaPerk4 != PerkType.None) perks.Add(IrinaPerk4);
                    if (IrinaPerk5 != PerkType.None) perks.Add(IrinaPerk5);
                    break;
            }
            
            return perks;
        }
    }
}