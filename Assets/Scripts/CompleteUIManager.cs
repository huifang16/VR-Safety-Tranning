using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteUIManager : MonoBehaviour
{
    public GameObject completePanel;
    public string nextSceneName = "NextScene";

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip completeSound;
    public AudioClip buttonClickSound;

    void Start()
    {
        if (completePanel != null)
            completePanel.SetActive(false);
    }

    public void ShowCompleteUI()
    {
        if (completePanel != null)
            completePanel.SetActive(true);

        if (audioSource != null && completeSound != null)
        {
            audioSource.PlayOneShot(completeSound);
        }
    }

    public void LoadNextScene()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }

        // small delay so sound can be heard
        Invoke(nameof(LoadScene), 0.3f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fire Extinguish");
    }
}