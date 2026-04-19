public enum StepState
{
    StartAtHome,     // Player starts in house

    TVWarning,       // Turn on TV / see warning
    SmokeAtDoor,     // Smoke appears at door
    CannotEscape,    // Realize cannot go outside

    WetTowel,        // Go wet towel
    SealDoorGap,     // Block door gap

    CallHelp,        // Call emergency
}