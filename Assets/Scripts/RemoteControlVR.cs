using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RemoteControlVR : MonoBehaviour
{
    public TVController tv;
    public AudioSource clickSound;
    public Renderer indicatorRenderer;

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    // Store original color
    private Color originalEmission;
    private Material mat;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        if (indicatorRenderer != null)
        {
            mat = indicatorRenderer.material;

            // Save original emission (dark)
            originalEmission = mat.GetColor("_EmissionColor");

            // Ensure it starts dark
            mat.SetColor("_EmissionColor", Color.black);
        }
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.activated.AddListener(OnActivated);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
        grabInteractable.activated.RemoveListener(OnActivated);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }
    void OnActivated(ActivateEventArgs args)
    {
        if (!isHeld) return;

        // Play click sound
        if (clickSound != null)
            clickSound.Play();

        // Toggle TV
        if (tv != null)
            tv.ToggleTV();

        // Flash light
        StartCoroutine(FlashLight());
    }

    IEnumerator FlashLight()
    {
        if (mat != null)
        {
            // Bright glow (you can change color)
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.red * 5f);
        }

        yield return new WaitForSeconds(0.3f);

        if (mat != null)
        {
            // Back to dark
            mat.SetColor("_EmissionColor", Color.black);
        }
    }
}