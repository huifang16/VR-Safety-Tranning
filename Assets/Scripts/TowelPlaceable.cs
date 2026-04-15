using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TowelPlaceable : MonoBehaviour
{
    public bool isPlaced = false;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    public void PlaceAt(Transform snapPoint)
    {
        if (isPlaced) return;

        isPlaced = true;

        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (grabInteractable != null)
            grabInteractable.enabled = false;

        Debug.Log("Towel placed at door gap.");
    }
}