using UnityEngine;

public class SinkWaterZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TowelState towel = other.GetComponentInParent<TowelState>();

        if (towel != null && !towel.isWet)
        {
            towel.SetWet();
            Debug.Log("Sink touched towel");
        }
    }
}