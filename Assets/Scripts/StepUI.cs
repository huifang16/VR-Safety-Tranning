using UnityEngine;
using TMPro;

public class StepUI : MonoBehaviour
{
    public TextMeshProUGUI stepText;

    public void SetStep(string message)
    {
        if (stepText != null)
            stepText.text = message;
    }
}