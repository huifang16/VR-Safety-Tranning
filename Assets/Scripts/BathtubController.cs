using UnityEngine;
public class WaterController : MonoBehaviour
{
    public ParticleSystem waterFlow;    // Assign your spout particles
    public GameObject tubWater;         // Assign the water plane in the tub
    public float fillSpeed = 0.1f;      // How fast the water rises
    public float maxHeight = 1.0f;      // The Y-position when the tub is full

    private bool isFlowing = false;

    // This method will be called by the VR Controller
    public void ToggleWater()
    {
        isFlowing = !isFlowing;

        if (isFlowing)
            waterFlow.Play();
        else
            waterFlow.Stop();
    }

    void Update()
    {
        if (isFlowing && tubWater.transform.localPosition.y < maxHeight)
        {
            // Move the water plane upward over time
            tubWater.transform.Translate(Vector3.up * fillSpeed * Time.deltaTime);
        }
    }
}
[RequireComponent(typeof(AudioSource))]
public class BathtubController : MonoBehaviour
{
    public ParticleSystem waterParticles; 
    public GameObject waterSurface;    
    public AudioSource waterSound;       

    public float fillSpeed = 0.05f;       // water speed 
    public float maxWaterHeight = 0.8f;   

    private bool isWaterOn = false;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = waterSurface.transform.localScale;
        // initial water level
        waterSurface.transform.localScale = new Vector3(initialScale.x, 0, initialScale.z);
        waterSurface.SetActive(false);
    }

    // when click
    public void ToggleWater()
    {
        isWaterOn = !isWaterOn;

        if (isWaterOn)
        {
            waterParticles.Play();
            waterSound.Play();
            waterSurface.SetActive(true);
        }
        else
        {
            waterParticles.Stop();
            waterSound.Stop();
        }
    }

    void Update()
    {
        if (isWaterOn && waterSurface.transform.localScale.y < maxWaterHeight)
        {
            float newY = waterSurface.transform.localScale.y + (fillSpeed * Time.deltaTime);
            waterSurface.transform.localScale = new Vector3(initialScale.x, newY, initialScale.z);
        }
    }
}