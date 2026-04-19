using UnityEngine;

public class FireGameManager : MonoBehaviour
{
    public static FireGameManager Instance;

    [Header("UI")]
    public GameObject successPanel;

    [Header("Settings")]
    public int firesToWin = 3;

    private int firesExtinguished = 0;

    void Awake()
    {
        Instance = this;
        successPanel.SetActive(false);
    }

    public void FirePutOut()
    {
        firesExtinguished++;

        Debug.Log("Fires extinguished: " + firesExtinguished);

        if (firesExtinguished >= firesToWin)
        {
            successPanel.SetActive(true);
        }
    }
}