using PhoenixPoint.Modding;

namespace TutorialCharacterPerks
{
    public class TutorialCharacterPerksConfig : ModConfig
    {
        // ─────────────────────────── Global Options ───────────────────────────
        [ConfigField(text: "Debug Logging")]
        public bool DebugLogging = false;
        
        [ConfigField(text: "Randomize Perk Levels")]
        public bool RandomizePerkLevels = true;
        
        [ConfigField(text: "Allowed Perk Levels (comma-separated 2-7)")]
        public string AllowedPerkLevelsStr = "2,3,4,5,6,7";

        // ───────────────────────────── Sophia Brown ────────────────────────────────
        [ConfigField(text: "Sophia: Reckless")] public bool Sophia_RECKLESS = true;
        [ConfigField(text: "Sophia: Thief")] public bool Sophia_THIEF = true;
        [ConfigField(text: "Sophia: Strongman")] public bool Sophia_STRONGMAN = false;
        [ConfigField(text: "Sophia: Resourceful")] public bool Sophia_RESOURCEFUL = false;
        [ConfigField(text: "Sophia: Trooper")] public bool Sophia_TROOPER = true;
        [ConfigField(text: "Sophia: Close Quarter Specialist")] public bool Sophia_CLOSE_QUARTERS_SPECIALIST = false;
        [ConfigField(text: "Sophia: Cautious")] public bool Sophia_CAUTIOUS = false;
        [ConfigField(text: "Sophia: Biochemist")] public bool Sophia_BIOCHEMIST = false;
        [ConfigField(text: "Sophia: Healer")] public bool Sophia_HEALER = false;
        [ConfigField(text: "Sophia: Quarterback")] public bool Sophia_QUARTERBACK = false;
        [ConfigField(text: "Sophia: Self Defense Specialist")] public bool Sophia_SELF_DEFENSE_SPECIALIST = false;
        [ConfigField(text: "Sophia: Sniperist")] public bool Sophia_SNIPERIST = false;
        [ConfigField(text: "Sophia: Farsighted")] public bool Sophia_FARSIGHTED = false;
        [ConfigField(text: "Sophia: Bombardier")] public bool Sophia_BOMBARDIER = false;

        // ───────────────────────────── Jacob Eber ────────────────────────────────
        [ConfigField(text: "Jacob: Reckless")] public bool Jacob_RECKLESS = true;
        [ConfigField(text: "Jacob: Thief")] public bool Jacob_THIEF = false;
        [ConfigField(text: "Jacob: Strongman")] public bool Jacob_STRONGMAN = false;
        [ConfigField(text: "Jacob: Resourceful")] public bool Jacob_RESOURCEFUL = false;
        [ConfigField(text: "Jacob: Trooper")] public bool Jacob_TROOPER = true;
        [ConfigField(text: "Jacob: Close Quarter Specialist")] public bool Jacob_CLOSE_QUARTERS_SPECIALIST = false;
        [ConfigField(text: "Jacob: Cautious")] public bool Jacob_CAUTIOUS = false;
        [ConfigField(text: "Jacob: Biochemist")] public bool Jacob_BIOCHEMIST = false;
        [ConfigField(text: "Jacob: Healer")] public bool Jacob_HEALER = false;
        [ConfigField(text: "Jacob: Quarterback")] public bool Jacob_QUARTERBACK = false;
        [ConfigField(text: "Jacob: Self Defense Specialist")] public bool Jacob_SELF_DEFENSE_SPECIALIST = false;
        [ConfigField(text: "Jacob: Sniperist")] public bool Jacob_SNIPERIST = true;
        [ConfigField(text: "Jacob: Farsighted")] public bool Jacob_FARSIGHTED = false;
        [ConfigField(text: "Jacob: Bombardier")] public bool Jacob_BOMBARDIER = false;

        // ───────────────────────────── Omar Ashour ────────────────────────────────
        [ConfigField(text: "Omar: Reckless")] public bool Omar_RECKLESS = true;
        [ConfigField(text: "Omar: Thief")] public bool Omar_THIEF = false;
        [ConfigField(text: "Omar: Strongman")] public bool Omar_STRONGMAN = true;
        [ConfigField(text: "Omar: Resourceful")] public bool Omar_RESOURCEFUL = false;
        [ConfigField(text: "Omar: Trooper")] public bool Omar_TROOPER = false;
        [ConfigField(text: "Omar: Close Quarter Specialist")] public bool Omar_CLOSE_QUARTERS_SPECIALIST = false;
        [ConfigField(text: "Omar: Cautious")] public bool Omar_CAUTIOUS = true;
        [ConfigField(text: "Omar: Biochemist")] public bool Omar_BIOCHEMIST = false;
        [ConfigField(text: "Omar: Healer")] public bool Omar_HEALER = false;
        [ConfigField(text: "Omar: Quarterback")] public bool Omar_QUARTERBACK = false;
        [ConfigField(text: "Omar: Self Defense Specialist")] public bool Omar_SELF_DEFENSE_SPECIALIST = false;
        [ConfigField(text: "Omar: Sniperist")] public bool Omar_SNIPERIST = false;
        [ConfigField(text: "Omar: Farsighted")] public bool Omar_FARSIGHTED = false;
        [ConfigField(text: "Omar: Bombardier")] public bool Omar_BOMBARDIER = false;

        // ───────────────────────────── Takeshi Sato ────────────────────────────────
        [ConfigField(text: "Takeshi: Reckless")] public bool Takeshi_RECKLESS = true;
        [ConfigField(text: "Takeshi: Thief")] public bool Takeshi_THIEF = true;
        [ConfigField(text: "Takeshi: Strongman")] public bool Takeshi_STRONGMAN = false;
        [ConfigField(text: "Takeshi: Resourceful")] public bool Takeshi_RESOURCEFUL = false;
        [ConfigField(text: "Takeshi: Trooper")] public bool Takeshi_TROOPER = false;
        [ConfigField(text: "Takeshi: Close Quarter Specialist")] public bool Takeshi_CLOSE_QUARTERS_SPECIALIST = true;
        [ConfigField(text: "Takeshi: Cautious")] public bool Takeshi_CAUTIOUS = false;
        [ConfigField(text: "Takeshi: Biochemist")] public bool Takeshi_BIOCHEMIST = false;
        [ConfigField(text: "Takeshi: Healer")] public bool Takeshi_HEALER = false;
        [ConfigField(text: "Takeshi: Quarterback")] public bool Takeshi_QUARTERBACK = false;
        [ConfigField(text: "Takeshi: Self Defense Specialist")] public bool Takeshi_SELF_DEFENSE_SPECIALIST = false;
        [ConfigField(text: "Takeshi: Sniperist")] public bool Takeshi_SNIPERIST = false;
        [ConfigField(text: "Takeshi: Farsighted")] public bool Takeshi_FARSIGHTED = false;
        [ConfigField(text: "Takeshi: Bombardier")] public bool Takeshi_BOMBARDIER = false;

        // ───────────────────────────── Irina Sokolova ────────────────────────────────
        [ConfigField(text: "Irina: Reckless")] public bool Irina_RECKLESS = true;
        [ConfigField(text: "Irina: Thief")] public bool Irina_THIEF = true;
        [ConfigField(text: "Irina: Strongman")] public bool Irina_STRONGMAN = false;
        [ConfigField(text: "Irina: Resourceful")] public bool Irina_RESOURCEFUL = false;
        [ConfigField(text: "Irina: Trooper")] public bool Irina_TROOPER = false;
        [ConfigField(text: "Irina: Close Quarter Specialist")] public bool Irina_CLOSE_QUARTERS_SPECIALIST = false;
        [ConfigField(text: "Irina: Cautious")] public bool Irina_CAUTIOUS = false;
        [ConfigField(text: "Irina: Biochemist")] public bool Irina_BIOCHEMIST = false;
        [ConfigField(text: "Irina: Healer")] public bool Irina_HEALER = false;
        [ConfigField(text: "Irina: Quarterback")] public bool Irina_QUARTERBACK = false;
        [ConfigField(text: "Irina: Self Defense Specialist")] public bool Irina_SELF_DEFENSE_SPECIALIST = false;
        [ConfigField(text: "Irina: Sniperist")] public bool Irina_SNIPERIST = true;
        [ConfigField(text: "Irina: Farsighted")] public bool Irina_FARSIGHTED = false;
        [ConfigField(text: "Irina: Bombardier")] public bool Irina_BOMBARDIER = false;
    }
}