using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteUI : MonoBehaviour
{
    public GameObject completePanel;
    public string nextSceneName = "NextScene";

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip completeSound;
    public AudioClip buttonClickSound;

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
        SceneManager.LoadScene("MainMenu");
    }
}