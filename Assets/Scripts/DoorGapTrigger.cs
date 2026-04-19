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
            Debug.Log("Towel is dry. Wet it first.");
            return;
        }

        if (StepManager.Instance != null)
            StepManager.Instance.SetStep(StepState.CallHelp);

        if (VFXManager.Instance != null)
            VFXManager.Instance.SetSmokeIntensity("FrontDoorSmoke", 0.2f);

        towel.SealDoor();
        triggered = true;

        Debug.Log("Door gap sealed. Next step: Call for help.");
    }
}