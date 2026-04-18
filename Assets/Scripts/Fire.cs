using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public List<string> allowedExtinguisherTypes;
    public float health = 5f;

    private ParticleSystem[] fireParticles; // ALL particle systems

    void Awake()
    {
        // Get all particle systems in this object + children
        fireParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void ApplyExtinguish(string extinguisherType, float amount)
    {
        if (allowedExtinguisherTypes.Contains(extinguisherType))
        {
            health -= amount;
            Debug.Log("Correct extinguisher. Health: " + health);

            if (health <= 0)
            {
                Extinguish();
            }
        }
        else
        {
            Debug.Log("Wrong extinguisher: " + extinguisherType);
        }
    }

    void Extinguish()
    {
        Debug.Log("Fire extinguished!");

        
        foreach (ParticleSystem ps in fireParticles)
        {
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().FireExtinguished();
    }
}