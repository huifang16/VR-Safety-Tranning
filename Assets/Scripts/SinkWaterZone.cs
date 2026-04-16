using UnityEngine;

public class SinkWaterZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TowelController towel = other.GetComponentInParent<TowelController>();

        if (towel != null && !towel.isWet)
        {
            towel.SetWet();
            Debug.Log("Sink touched towel");
        }
    }
}