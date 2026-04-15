using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMenuManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public Transform vrCamera;
    public float distance = 1.5f;

    void Start()
    {
        ShowMainMenu();
    }

    void PositionMenu(GameObject menu)
    {
        Vector3 forward = vrCamera.forward;
        forward.y = 0f;
        forward.Normalize();

        menu.transform.position = vrCamera.position + forward * distance;

        menu.transform.LookAt(new Vector3(
            vrCamera.position.x,
            menu.transform.position.y,
            vrCamera.position.z
        ));

        menu.transform.Rotate(0f, 180f, 0f);
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        PositionMenu(mainMenuCanvas);
    }

    public void ShowSettings()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        PositionMenu(settingsCanvas);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}