using UnityEngine;

public class DoorGapSealer : MonoBehaviour
{
    public Transform snapPoint;
    private bool sealedDone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (sealedDone) return;

        Debug.Log("Door trigger touched: " + other.name);

        TowelPlaceable towelPlaceable = other.GetComponentInParent<TowelPlaceable>();
        TowelState towelState = other.GetComponentInParent<TowelState>();

        if (towelPlaceable == null)
        {
            Debug.Log("No TowelPlaceable found");
            return;
        }

        if (towelState == null)
        {
            Debug.Log("No TowelState found");
            return;
        }

        if (!towelState.isWet)
        {
            Debug.Log("Towel is not wet yet");
            return;
        }

        if (snapPoint == null)
        {
            Debug.Log("SnapPoint is missing");
            return;
        }

        towelPlaceable.PlaceAt(snapPoint);
        sealedDone = true;

        Debug.Log("Wet towel placed. Door gap sealed.");
    }
}