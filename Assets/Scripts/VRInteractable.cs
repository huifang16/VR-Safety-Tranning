using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class VRInteractable : MonoBehaviour {
    public UnityEvent OnInteract;

    private XRSimpleInteractable _xrInteractable;

    void Start() {
        _xrInteractable = GetComponent<XRSimpleInteractable>();
        _xrInteractable.selectEntered.AddListener(OnSelected);
    }

    void OnDestroy() {
        _xrInteractable.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args) {
        Debug.Log($"VRInteractable triggered on {gameObject.name}");
        OnInteract?.Invoke();
    }
}