using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public List<string> allowedExtinguisherTypes;
    public float health = 5f;

    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private ParticleSystem[] fireParticles; // ALL particle systems

    private bool hasPlayedCorrectSound = false;

    private bool hasReported = false;

    void Awake()
    {
        fireParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void ApplyExtinguish(string extinguisherType, float amount)
    {
        if (allowedExtinguisherTypes.Contains(extinguisherType))
        {
            if (!hasPlayedCorrectSound && audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
                hasPlayedCorrectSound = true;
            }

            health -= amount;

            if (health <= 0)
            {
                Extinguish();
            }
        }
        else
        {
            if (audioSource != null && wrongSound != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(wrongSound);
            }
            Debug.Log("Wrong extinguisher: " + extinguisherType);
        }
    }

    void Extinguish()
    {
        foreach (ParticleSystem ps in fireParticles)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        if (hasReported) return;
        hasReported = true;

        if (FireGameManager.Instance != null)
        {
            FireGameManager.Instance.FirePutOut();
        }
    }
}