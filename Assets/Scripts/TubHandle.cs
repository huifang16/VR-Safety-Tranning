using UnityEngine;

public class TubHandle : MonoBehaviour
{
    public HingeJoint handleHinge;
    public ParticleSystem waterParticles;
    public float threshold = 10f; // Degrees rotated before water starts
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current angle of the hinge
        float currentAngle = handleHinge.angle;

        if (currentAngle > threshold)
        {
            if (!waterParticles.isEmitting) waterParticles.Play();

            // Optional: Adjust water flow intensity based on how far it's turned
            var emission = waterParticles.emission;
            emission.rateOverTime = currentAngle * 2f;
        }
        else
        {
            waterParticles.Stop();
        }
    }
}
