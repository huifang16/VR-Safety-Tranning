using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WetCloth : MonoBehaviour
{
    public bool isWet = false;
    public Material dryMaterial;
    public Material wetMaterial;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // when cloth is put in the tub filled with water 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterSource"))
        {
            MakeWet();
        }
    }

    void MakeWet()
    {
        isWet = true;
        meshRenderer.material = wetMaterial; 
        Debug.Log("Cloth is wet！");
    }
}