using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class PlayerChokeManager : MonoBehaviour
{
    public static PlayerChokeManager Instance;

    [Header("References")]
    [SerializeField] private Transform head; // XR camera
    [SerializeField] private ContinuousMoveProvider moveProvider;
    [SerializeField] private MonoBehaviour tunnelingVignetteController; // drag your vignette controller here

    [Header("Crouch Check")]
    [SerializeField] private float crouchHeightThreshold = 1.20f; // tune for your rig

    [Header("Choke Tuning")]
    [SerializeField] private float timeToStartDamage = 2f;
    [SerializeField] private float timeToDeath = 12f;
    [SerializeField] private float minMoveMultiplier = 0.45f;
    [SerializeField] private float severityToFullBlur = 1f;
    [SerializeField] private TunnelingVignetteController vignetteController;
    [SerializeField] private ChokeVignetteProvider chokeProvider;

    private float _baseMoveSpeed;
    private bool _active;
    private bool _doorPenalty;
    private float _exposure;
    private float _severity;

    private void Awake()
    {
        Instance = this;

        if (moveProvider != null)
            _baseMoveSpeed = moveProvider.moveSpeed;
    }

    private void Update()
    {
        if (!_active)
            return;

        bool isLowEnough = IsPlayerCrouching();
        bool unsafeState = !isLowEnough || _doorPenalty;

        if (unsafeState)
            _exposure += Time.deltaTime;
        else
            _exposure = Mathf.Max(0f, _exposure - Time.deltaTime * 1.5f);

        _severity = Mathf.InverseLerp(timeToStartDamage, timeToDeath, _exposure);
        _severity = Mathf.Clamp01(_severity);

        ApplyMovementPenalty(_severity, _doorPenalty);
        ApplyVisionPenalty(_severity, _doorPenalty);

        if (_exposure >= timeToDeath)
        {
            StoryManager.Instance?.GoToPlayerDead();
        }
        
        chokeProvider.SetSeverity(_severity);

        if (_severity > 0.01f)
        {
            vignetteController.BeginTunnelingVignette(chokeProvider);
        }
        else
        {
            vignetteController.EndTunnelingVignette(chokeProvider);
        }
    }

    public void BeginCrawlHallway(bool doorWasClosed)
    {
        _active = true;
        _doorPenalty = !doorWasClosed;
        _exposure = 0f;
        _severity = 0f;
    }

    public void StopChoking()
    {
        _active = false;
        _doorPenalty = false;
        _exposure = 0f;
        _severity = 0f;

        if (moveProvider != null)
            moveProvider.moveSpeed = _baseMoveSpeed;

        ApplyVisionPenalty(0f, false);
    }

    public void SetDoorClosedBehind(bool closed)
    {
        _doorPenalty = !closed;
    }

    private bool IsPlayerCrouching()
    {
        if (head == null)
            return true;

        // Works well if the camera is a child of the XR Origin.
        return head.localPosition.y <= crouchHeightThreshold;
    }

    private void ApplyMovementPenalty(float severity, bool doorPenalty)
    {
        if (moveProvider == null)
            return;

        float extraPenalty = doorPenalty ? 0.25f : 0f;
        float multiplier = Mathf.Lerp(1f, minMoveMultiplier, Mathf.Clamp01(severity + extraPenalty));
        moveProvider.moveSpeed = _baseMoveSpeed * multiplier;
    }

    private void ApplyVisionPenalty(float severity, bool doorPenalty)
    {
        float finalSeverity = Mathf.Clamp01(severity + (doorPenalty ? 0.25f : 0f));

        // Replace these with the actual API of your tunneling vignette controller.
        // Example idea:
        // tunnelingVignetteController.SetIntensity(finalSeverity);
        // tunnelingVignetteController.SetBlur(finalSeverity);

        Debug.Log($"Choke severity: {finalSeverity:0.00}");
    }
}
