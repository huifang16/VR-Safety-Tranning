using UnityEngine;

public class DoorGapTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        TowelController towel = other.GetComponentInParent<TowelController>();
        if (towel == null)
        {
            Debug.Log("No TowelController found.");
            return;
        }

        if (!towel.isWet)
        {
            Debug.Log("Towel is dry.");
            return;
        }

        towel.SealDoor();
        triggered = true;
    }
}