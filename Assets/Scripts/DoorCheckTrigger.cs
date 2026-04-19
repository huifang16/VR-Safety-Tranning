using UnityEngine;

public class DoorCheckTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (StepManager.Instance != null)
                StepManager.Instance.SetStep(StepState.CannotEscape);

            if (StepManager.Instance != null)
                StepManager.Instance.SetStep(StepState.WetTowel);

            if (VFXManager.Instance != null)
                VFXManager.Instance.SetSmokeIntensity("FrontDoorSmoke", 2f);
        }
    }
}