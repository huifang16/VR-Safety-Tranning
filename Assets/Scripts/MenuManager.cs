using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject startMenu;
    public GameObject levelMenu;
    public GameObject settingMenu;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip menuSound;
    public AudioClip buttonClickSound;

    void Start()
    {
        ShowStartMenu();

        if (audioSource != null && menuSound != null)
        {
            audioSource.clip = menuSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void PlayClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void ShowStartMenu()
    {
        if (startMenu != null) startMenu.SetActive(true);
        if (levelMenu != null) levelMenu.SetActive(false);
        if (settingMenu != null) settingMenu.SetActive(false);
    }

    public void OpenLevelMenu()
    {
        PlayClickSound();

        if (startMenu != null) startMenu.SetActive(false);
        if (levelMenu != null) levelMenu.SetActive(true);
        if (settingMenu != null) settingMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        PlayClickSound();

        if (startMenu != null) startMenu.SetActive(false);
        if (levelMenu != null) levelMenu.SetActive(false);
        if (settingMenu != null) settingMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        PlayClickSound();
        ShowStartMenu();
    }

    public void LoadStayHomeScene()
    {
        PlayClickSound();
        SceneManager.LoadScene("ApartmentStay");
    }

    public void LoadEscapeScene()
    {
        PlayClickSound();
        SceneManager.LoadScene("ApartmentEscape");
    }

    public void LoadFireExtinguishScene()
    {
        PlayClickSound();
        SceneManager.LoadScene("Fire Extinguish");
    }

    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }
}