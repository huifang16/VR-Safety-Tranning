using UnityEngine;

public class FaucetController : MonoBehaviour
{
    public ParticleSystem waterParticles;
    public float thresholdAngle = 45f; // Point where water starts

    void Update()
    {
        // Check the local rotation of the handle
        float currentAngle = transform.localEulerAngles.y;

        // Handle Euler wrapping (360 back to 0) if necessary
        if (currentAngle > 180) currentAngle -= 360;

        if (currentAngle > thresholdAngle)
        {
            if (!waterParticles.isPlaying) waterParticles.Play();
        }
        else
        {
            if (waterParticles.isPlaying) waterParticles.Stop();
        }
    }
}