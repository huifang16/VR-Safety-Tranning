using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingMenu;

    public AudioSource audioSource;
    public AudioClip menuSound;
    public AudioClip buttonClickSound;

    void Start()
    {
        if (startMenu != null)
            startMenu.SetActive(true);

        if (settingMenu != null)
            settingMenu.SetActive(false);

        if (audioSource != null && menuSound != null)
        {
            audioSource.clip = menuSound;
            audioSource.Play();
        }
    }

    void PlayButtonClick()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void StartGame()
    {
        PlayButtonClick();

        if (audioSource != null)
            audioSource.Stop();

        Invoke(nameof(LoadGameScene), 0.2f);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("ApartmentEscape");
    }

    public void OpenSettings()
    {
        PlayButtonClick();

        if (startMenu != null)
            startMenu.SetActive(false);

        if (settingMenu != null)
            settingMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        PlayButtonClick();

        if (settingMenu != null)
            settingMenu.SetActive(false);

        if (startMenu != null)
            startMenu.SetActive(true);
    }

    public void QuitGame()
    {
        PlayButtonClick();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}