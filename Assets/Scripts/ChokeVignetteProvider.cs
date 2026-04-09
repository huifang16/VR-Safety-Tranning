using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class ChokeVignetteProvider : MonoBehaviour, ITunnelingVignetteProvider
{
    public VignetteParameters parameters = new VignetteParameters();

    public VignetteParameters vignetteParameters => parameters;

    public void SetSeverity(float severity)
    {
        // severity: 0 → 1
        parameters.apertureSize = Mathf.Lerp(1f, 0.3f, severity); // tighter vision
        parameters.featheringEffect = Mathf.Lerp(0.1f, 0.6f, severity);

        // Optional: make it feel "dangerous"
        parameters.vignetteColor = Color.Lerp(Color.black, new Color(0.2f, 0f, 0f), severity);

        parameters.easeInTime = 0.2f;
        parameters.easeOutTime = 0.3f;
    }
}