using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class PlayerDeathSequence : MonoBehaviour
{
    public static PlayerDeathSequence Instance;

    [Header("XR Rig")]
    public Transform cameraOffset;
    public ContinuousMoveProvider moveProvider; 
    public SnapTurnProvider snapTurnProvider;

    [Header("Death Animation")]
    public float tiltAngle    = 80f;
    public float fallDistance = 0.8f;
    public float animDuration = 1.0f;
    public float fadeDuration = 1.0f;
    public float holdBlack    = 0.5f;

    [Header("Vignette")]
    public TunnelingVignetteController vignetteController;
    public ChokeVignetteProvider chokeProvider;

    bool _isDead = false;

    void Awake() => Instance = this;

    public void TriggerDeath(string reason = "You died.")
    {
        if (_isDead) return;
        _isDead = true;
        StartCoroutine(DeathRoutine(reason));
    }

    IEnumerator DeathRoutine(string reason)
    {
        // --- 1. Lock movement immediately ---
        if (moveProvider != null)
            moveProvider.enabled = false;
        if (snapTurnProvider != null)
            snapTurnProvider.enabled = false;

        // --- 2. Tilt and fall ---
        Vector3 startRot = cameraOffset.localEulerAngles;
        Vector3 startPos = cameraOffset.localPosition;
        Vector3 endRot   = startRot + new Vector3(0f, 0f, tiltAngle);
        Vector3 endPos   = startPos - new Vector3(0f, fallDistance, 0f);

        float t = 0f;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            float pct = Mathf.SmoothStep(0f, 1f, t / animDuration);
            cameraOffset.localEulerAngles = Vector3.Lerp(startRot, endRot, pct);
            cameraOffset.localPosition    = Vector3.Lerp(startPos, endPos, pct);
            yield return null;
        }

        // --- 3. Fade to black — set params BEFORE BeginTunnelingVignette ---
        // Zero easeInTime so the controller's own easing snaps to apertureSize=0 immediately
        chokeProvider.parameters.apertureSize     = 0f;
        chokeProvider.parameters.featheringEffect = 0.1f;
        chokeProvider.parameters.vignetteColor    = Color.black;
        chokeProvider.parameters.vignetteColorBlend = Color.black;
        chokeProvider.parameters.easeInTime       = 0f;   // snap, no easing
        chokeProvider.parameters.easeOutTime      = 0f;
        chokeProvider.parameters.easeInTimeLock   = false;
        vignetteController.BeginTunnelingVignette(chokeProvider);

        // Wait for the controller's Update to process it (1 frame) then hold
        yield return null;
        yield return new WaitForSeconds(holdBlack);

        // --- 4. Fire story events ---
        StoryManager.Instance.GoToPlayerDead();
        StoryManager.Instance.OnPlayerDied?.Invoke(reason);
    }

    // Call this from your UI restart button
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}