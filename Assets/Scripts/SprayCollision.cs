using UnityEngine;

public class SprayCollision : MonoBehaviour
{
    public FireExtinguisher extinguisher;
    public float extinguishPower = 1f;

    void OnTriggerStay(Collider other)
    {
        if (!extinguisher.isSpraying) return;

        Fire fire = other.GetComponent<Fire>();

        if (fire != null)
        {
            fire.ApplyExtinguish(extinguisher.extinguisherType, Time.deltaTime * extinguishPower);
        }
    }
}