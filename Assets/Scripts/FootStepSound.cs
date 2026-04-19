using UnityEngine;
using UnityEngine.InputSystem;

public class FootStepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public InputActionProperty moveAction; // from XR input

    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // check if player is moving
        if (input.magnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}