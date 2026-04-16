using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TowelController : MonoBehaviour
{
    [Header("Wet State")]
    public bool isWet = false;
    public Renderer towelRenderer;

    [Header("Door Seal")]
    public GameObject doorTowelVisual;   // hidden towel at door gap
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

        // release from hand first
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            var interactor = grabInteractable.firstInteractorSelecting;
            if (interactor != null && grabInteractable.interactionManager != null)
            {
                grabInteractable.interactionManager.SelectExit(interactor, grabInteractable);
            }
        }

        // stop physics
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // show towel at door gap
        if (doorTowelVisual != null)
            doorTowelVisual.SetActive(true);

        // hide grabbed towel
        gameObject.SetActive(false);

        Debug.Log("Door gap sealed with wet towel.");
    }
}