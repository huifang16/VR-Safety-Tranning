using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private bool isOpen = true;

    public void ToggleDoor()
    {
        isOpen = !isOpen;

        // If the door is closed, "doorClosedBehind" should be true.
        if (StoryManager.Instance != null)
        {
            StoryManager.Instance.isDoorClosed= !isOpen;
            PlayerChokeManager.Instance?.SetDoorClosedBehind(!isOpen);
        }

        Debug.Log($"Door is now {(isOpen ? "Open" : "Closed")}");
    }
}