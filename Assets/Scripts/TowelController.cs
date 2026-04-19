using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TowelController : MonoBehaviour
{

    public bool isWet = false;
    public Renderer towelRenderer;
    public AudioSource audioSource;
    public AudioClip grabSound;
    public GameObject doorTowelVisual;
    private bool isSealed = false;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (doorTowelVisual != null)
            doorTowelVisual.SetActive(false);
    }
    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }
    void OnGrab(SelectEnterEventArgs args)
    {
        if (audioSource != null && grabSound != null)
        {
            audioSource.PlayOneShot(grabSound);
        }
    }

    public void SetWet()
    {
        if (isWet) return;

        isWet = true;

        if (towelRenderer != null)
        {
            Material mat = towelRenderer.material;
            Color wetColor = new Color(0.25f, 0.25f, 0.3f);

            mat.color = wetColor;

            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", wetColor);

            if (mat.HasProperty("_Smoothness"))
                mat.SetFloat("_Smoothness", 0.1f);
        }

        if (StepManager.Instance != null)
            StepManager.Instance.SetStep(StepState.SealDoorGap);

        Debug.Log("Towel is now wet.");
    }

    public void SealDoor()
    {
        if (isSealed) return;

        if (!isWet)
        {
            Debug.Log("Towel is not wet, cannot seal door.");
            return;
        }

        isSealed = true;

        if (grabInteractable != null && grabInteractable.isSelected)
        {
            var interactor = grabInteractable.firstInteractorSelecting;
            if (interactor != null && grabInteractable.interactionManager != null)
            {
                grabInteractable.interactionManager.SelectExit(interactor, grabInteractable);
            }
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (doorTowelVisual != null)
            doorTowelVisual.SetActive(true);

        gameObject.SetActive(false);

        Debug.Log("Door gap sealed with wet towel.");
    }
}