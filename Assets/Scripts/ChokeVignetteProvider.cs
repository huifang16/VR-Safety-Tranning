using UnityEditor.ShaderGraph;
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
    
    public void ResetToRedNormal()
    {
        parameters.apertureSize = 0.7f;
        parameters.featheringEffect = 0.2f;
        parameters.vignetteColor = new Color32(80, 0, 0, 255);
        parameters.vignetteColorBlend = new Color32(80, 0, 0, 255);
        parameters.easeInTime = 0.3f;
        parameters.easeOutTime = 0.3f;
        parameters.easeOutDelayTime = 0f;
        parameters.easeInTimeLock = false;
        parameters.apertureVerticalPosition = 0f;
    }
}