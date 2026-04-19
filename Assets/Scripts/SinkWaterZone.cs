using UnityEngine;

public class SinkWaterZone : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource waterAudio;
    public AudioClip waterLoop;
    public AudioClip splashSound;

    private void Start()
    {
        if (waterAudio != null)
        {
            waterAudio.playOnAwake = false;
            waterAudio.loop = true;
            waterAudio.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered water zone");

        TowelController towel = other.GetComponentInParent<TowelController>();

        if (towel != null)
        {
            if (!towel.isWet)
            {
                towel.SetWet();
                Debug.Log("Sink touched towel");
            }

            if (waterAudio != null)
            {
                if (splashSound != null)
                {
                    waterAudio.PlayOneShot(splashSound);
                }

                if (waterLoop != null)
                {
                    waterAudio.clip = waterLoop;
                    waterAudio.loop = true;

                    if (!waterAudio.isPlaying)
                    {
                        waterAudio.Play();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TowelController towel = other.GetComponentInParent<TowelController>();

        if (towel != null)
        {
            if (waterAudio != null && waterAudio.isPlaying)
            {
                waterAudio.Stop();
            }

            Debug.Log("Towel left water zone");
        }
    }
}