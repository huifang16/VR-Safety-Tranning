using UnityEngine;

public class SprayCollision : MonoBehaviour
{
    public FireExtinguisher extinguisher;
    public float extinguishPower = 0.5f;

    void OnParticleCollision(GameObject other)
    {
        if (!extinguisher.isSpraying) return;

        Fire fire = other.GetComponentInParent<Fire>();

        if (fire != null)
        {
            fire.ApplyExtinguish(
                extinguisher.extinguisherType,
                extinguishPower * Time.deltaTime
            );
        }

        Debug.Log("Particle hit: " + other.name);
    }
}