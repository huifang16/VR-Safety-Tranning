using UnityEngine;

public class Water : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem waterFlow;    // Drag your Spout Particles here

    [Header("Settings")]
    public float offRotation = 0f;      // Handle rotation when OFF
    public float onRotation = 45f;      // Handle rotation when ON

    public bool isFlowing = false;

    // This is the function you will select in the "Select Entered" dropdown
    public void ToggleWater()
    {
        Debug.Log("Interaction Triggered!");
        isFlowing = !isFlowing;

        if (isFlowing)
        {
            if (waterFlow != null) waterFlow.Play();
            // Rotates the handle to look "Open"
            transform.localRotation = Quaternion.Euler(0, 0, onRotation);
        }
        else
        {
            if (waterFlow != null) waterFlow.Stop();
            // Rotates the handle back to "Closed"
            transform.localRotation = Quaternion.Euler(0, 0, offRotation);
        }
    }

    void Update()
    {

    }
}
