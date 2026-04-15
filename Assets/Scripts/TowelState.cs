using UnityEngine;

public class TowelState : MonoBehaviour
{
    public bool isWet = false;

    public Renderer towelRenderer;

    void Start()
    {
        // Ensure towel starts dry 
        isWet = false;
    }

    public void SetWet()
    {
        if (isWet) return;

        isWet = true;

        if (towelRenderer != null)
        {
            Material mat = towelRenderer.material;

            Color wetColor = new Color(0.25f, 0.25f, 0.3f);

            mat.color = wetColor;

            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", wetColor);

            if (mat.HasProperty("_Smoothness"))
                mat.SetFloat("_Smoothness", 0.1f);
        }

        Debug.Log("Towel is now wet");
    }
}