using UnityEngine;
using TMPro;

public class PopUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;
    public float showTime = 10f;

    private Coroutine hideCoroutine;

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (StepManager.Instance != null)
            StepManager.Instance.OnStepTextChanged.AddListener(ShowMessage);
    }

    void OnDestroy()
    {
        if (StepManager.Instance != null)
            StepManager.Instance.OnStepTextChanged.RemoveListener(ShowMessage);
    }

    public void ShowMessage(string message)
    {
        if (panel == null || messageText == null) return;

        panel.SetActive(true);
        messageText.text = message;

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showTime);
        panel.SetActive(false);
    }
}