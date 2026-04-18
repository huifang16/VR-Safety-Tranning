using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuUI;
    private int totalFires;
    private int firesExtinguished = 0;

    void Start()
    {
        totalFires = FindObjectsOfType<Fire>().Length;
        menuUI.SetActive(false);
    }

    public void FireExtinguished()
    {
        firesExtinguished++;

        if (firesExtinguished >= totalFires)
        {
            ShowMenu();
        }
    }

    void ShowMenu()
    {
        menuUI.SetActive(true);
    }

    // Button Functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}