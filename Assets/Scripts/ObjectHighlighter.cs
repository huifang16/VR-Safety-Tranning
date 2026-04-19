using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color highlightColor = Color.yellow;
    public float intensity = 2f;

    private Material mat;

    void Start()
    {
        if (targetRenderer != null)
        {
            mat = targetRenderer.material;
            SetHighlight(false);
        }
    }

    public void SetHighlight(bool active)
    {
        if (mat == null) return;

        mat.EnableKeyword("_EMISSION");

        if (active)
            mat.SetColor("_EmissionColor", highlightColor * intensity);
        else
            mat.SetColor("_EmissionColor", Color.black);
    }
}