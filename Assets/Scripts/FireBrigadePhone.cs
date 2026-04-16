using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireBrigadePhone : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip dialingSound;
    public AudioClip fireBrigadeResponse;
    public AudioClip playerResponse;
    public AudioClip hangUpBeep;

    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;
    private bool hasCalled = false;
    private Coroutine callCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnPhonePickedUp);
        grabInteractable.selectExited.AddListener(OnPhoneDropped);
    }

    void OnPhonePickedUp(SelectEnterEventArgs args)
    {
        if (!hasCalled)
        {
            callCoroutine = StartCoroutine(MakeCallSequence());
        }
    }

    void OnPhoneDropped(SelectExitEventArgs args)
    {
        if (callCoroutine != null && !hasCalled)
        {
            StopCoroutine(callCoroutine);
            audioSource.Stop();
        }
    }

    IEnumerator MakeCallSequence()
    {
        // 1. Dialing / ringing
        if (dialingSound != null)
        {
            audioSource.clip = dialingSound;
            audioSource.Play();
            yield return new WaitForSeconds(dialingSound.length);
        }

        // 2. Fire brigade / operator answer
        if (fireBrigadeResponse != null)
        {
            audioSource.clip = fireBrigadeResponse;
            audioSource.Play();
            yield return new WaitForSeconds(fireBrigadeResponse.length);
        }

        // 3. Player response
        if (playerResponse != null)
        {
            audioSource.clip = playerResponse;
            audioSource.Play();
            yield return new WaitForSeconds(playerResponse.length);
        }

        // 4. Hang up beep
        if (hangUpBeep != null)
        {
            audioSource.clip = hangUpBeep;
            audioSource.Play();
            yield return new WaitForSeconds(hangUpBeep.length);
        }

        hasCalled = true;
        OnFireBrigadeCalled();
    }

    void OnFireBrigadeCalled()
    {
        Debug.Log("Fire Brigade Called and Call Disconnected.");
        // Trigger next objective here
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
            grabInteractable.selectExited.RemoveListener(OnPhoneDropped);
        }
    }
}