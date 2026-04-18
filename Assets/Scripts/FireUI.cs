using TMPro;
using UnityEngine;

public class FireUI : MonoBehaviour
{
    public Fire fire; // reference to Fire script
    public TextMeshProUGUI textUI;
    public Transform playerCamera;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        transform.LookAt(playerCamera);
        transform.Rotate(0, 180, 0);
    }

    void UpdateText()
    {
        if (fire.allowedExtinguisherTypes.Count > 0)
        {
            textUI.text = "Use: " + fire.allowedExtinguisherTypes[0];
        }
    }
}