using UnityEngine;

public class TemporaryUI : MonoBehaviour
{
    public GameObject uiObject;
    public float displayTime = 3f;

    void Start()
    {
        uiObject.SetActive(true);
        Invoke("HideUI", displayTime);
    }

    void HideUI()
    {
        uiObject.SetActive(false);
    }
}