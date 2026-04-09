using UnityEngine;
using TMPro;

public class WristUIController : MonoBehaviour
{
    [Header("References")]
    public Canvas wristCanvas;
    public TextMeshProUGUI promptText;
    public Transform wristAnchor; // the WristUIAnchor empty GameObject

    [Header("Settings")]
    public float showAngleThreshold = 10f; // degrees above horizontal to show UI
    public float fadeSpeed = 2f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = wristCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = wristCanvas.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        wristCanvas.gameObject.SetActive(true);

        // Subscribe to story prompts
        StoryManager.Instance.OnUIPrompt.AddListener(SetPrompt);
    }

    void OnDestroy()
    {
        if (StoryManager.Instance != null)
            StoryManager.Instance.OnUIPrompt.RemoveListener(SetPrompt);
    }

    void Update()
    {
        bool raised = IsWristRaised();
        float targetAlpha = raised ? 1f : 0f;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        // Always face the player camera
        if (Camera.main != null)
        {
            wristAnchor.LookAt(Camera.main.transform);
            wristAnchor.Rotate(0, 180f, 0); // flip so text faces player
        }
    }

    bool IsWristRaised()
    {
        // Check if the palm is tilted upward toward the player's face
        // transform here is the hand controller
        float angle = Vector3.Angle(transform.up, Vector3.up);
        return angle < showAngleThreshold;
    }

    public void SetPrompt(string message)
    {
        promptText.text = message;
    }
}