using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public string extinguisherType;
    public ParticleSystem sprayParticles;

    public bool isSpraying = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSpray();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopSpray();
        }
    }

    public void StartSpray()
    {
        isSpraying = true;
        sprayParticles.Play();
    }

    public void StopSpray()
    {
        isSpraying = false;
        sprayParticles.Stop();
    }
}
