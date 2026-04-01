// VRTriggerZone.cs
using UnityEngine;
using UnityEngine.Events;

public class VRTriggerZone : MonoBehaviour {
    public string playerTag = "Player";
    public StoryState requiredMinState = StoryState.SmellFromDoor;
    public UnityEvent OnPlayerEnter;

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(playerTag)) return;

        if (StoryManager.Instance.currentState < requiredMinState) {
            Debug.Log($"VRTriggerZone blocked — need {requiredMinState}, currently {StoryManager.Instance.currentState}");
            return;
        }

        Debug.Log($"VRTriggerZone fired: {gameObject.name}");
        OnPlayerEnter?.Invoke();
    }
}