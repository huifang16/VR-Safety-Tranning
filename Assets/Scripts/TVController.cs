using UnityEngine;

public class TVController : MonoBehaviour
{
    public GameObject warningCanvas;
    public AudioSource tvSound;

    private bool isOn = false;

    void Start()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }

    public void ToggleTV()
    {
        isOn = !isOn;

        if (warningCanvas != null)
            warningCanvas.SetActive(isOn);

        if (tvSound != null)
        {
            if (isOn)
                tvSound.Play();
            else
                tvSound.Stop();
        }
    }

    public bool IsOn()
    {
        return isOn;
    }
}