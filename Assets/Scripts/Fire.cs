using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public List<string> allowedExtinguisherTypes;
    public float health = 5f;

    public ParticleSystem fireParticles;

    public void ApplyExtinguish(string extinguisherType, float amount)
    {
        if (allowedExtinguisherTypes.Contains(extinguisherType))
        {
            health -= amount;

            if (health <= 0)
            {
                Extinguish();
            }
        }
        else
        {
            Debug.Log("Wrong extinguisher!");
        }
    }

    void Extinguish()
    {
        fireParticles.Stop();
        gameObject.SetActive(false);
    }
}