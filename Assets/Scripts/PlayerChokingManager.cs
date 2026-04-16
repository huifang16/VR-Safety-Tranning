using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class PlayerChokeManager : MonoBehaviour
{
    public static PlayerChokeManager Instance;

    [Header("References")]
    [SerializeField] private Transform head; // XR camera
    [SerializeField] private ContinuousMoveProvider moveProvider;

    [Header("Crouch Check")]
    [SerializeField] private float crouchHeightThreshold = 1.2f; // tune for your rig

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
    private bool _extremeSmoke = false;

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
        {
            float multiplier = 1.0f;
            if (_extremeSmoke) multiplier *= 2.0f;
            if (_doorPenalty) multiplier *= 1.3f;
            _exposure += Time.deltaTime * multiplier;
        }
        else
        {
            float recoveryRate = _extremeSmoke ? 1.5f : 2.5f;
            _exposure = Mathf.Max(0f, _exposure - Time.deltaTime * recoveryRate);
        }

        _severity = Mathf.Clamp01(Mathf.InverseLerp(timeToStartDamage, timeToDeath, _exposure));

        float finalSeverity = Mathf.Clamp01(_severity + (_doorPenalty ? 0.2f : 0f) + (_extremeSmoke ? 0.3f : 0f));

        chokeProvider.SetSeverity(finalSeverity);

        if (vignetteController != null)
        {
            if (finalSeverity > 0.05f)
                vignetteController.BeginTunnelingVignette(chokeProvider);
            else
                vignetteController.EndTunnelingVignette(chokeProvider);
        }

        if (_exposure >= timeToDeath)
        {
            PlayerDeathSequence.Instance.TriggerDeath("You were overcome by smoke.");            
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
        _extremeSmoke = false;

        if (moveProvider != null)
            moveProvider.moveSpeed = _baseMoveSpeed;

        if (chokeProvider != null)
            chokeProvider.SetSeverity(0f);

        if (vignetteController != null && chokeProvider != null)
            vignetteController.EndTunnelingVignette(chokeProvider);
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

    public void BeginExtremeSmoke()
    {
        _extremeSmoke = true;
    }

    public void EndExtremeSmoke()
    {
        _extremeSmoke = false;    
        if (chokeProvider != null)
            chokeProvider.ResetToRedNormal();

        if (vignetteController != null && chokeProvider != null)
            vignetteController.EndTunnelingVignette(chokeProvider); 
    }
}
