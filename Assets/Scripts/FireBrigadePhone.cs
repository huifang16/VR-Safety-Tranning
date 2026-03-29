using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireBrigadePhone : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip dialingSound;        // The "Ringing" sound
    public AudioClip fireBrigadeResponse; // The "911 Operator" voice
    public AudioClip hangUpBeep;          // The "Beep Beep" finish sound

    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;
    private bool hasCalled = false;
    private Coroutine callCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Listen for Grab
        grabInteractable.selectEntered.AddListener(OnPhonePickedUp);
        // Listen for Drop
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
        // If they drop the phone mid-call, stop everything
        if (callCoroutine != null && !hasCalled)
        {
            StopCoroutine(callCoroutine);
            audioSource.Stop();
        }
    }

    IEnumerator MakeCallSequence()
    {
        // 1. Play Dialing/Ringing
        if (dialingSound != null)
        {
            audioSource.clip = dialingSound;
            audioSource.Play();
            yield return new WaitForSeconds(dialingSound.length);
        }

        // 2. Play 911 Operator Response
        if (fireBrigadeResponse != null)
        {
            audioSource.clip = fireBrigadeResponse;
            audioSource.Play();
            yield return new WaitForSeconds(fireBrigadeResponse.length);
        }

        // 3. Play Finish "Beep Beep"
        if (hangUpBeep != null)
        {
            audioSource.clip = hangUpBeep;
            audioSource.Play();
            yield return new WaitForSeconds(hangUpBeep.length);
        }

        hasCalled = true; // Mark call as finished
        OnFireBrigadeCalled();
    }

    void OnFireBrigadeCalled()
    {
        Debug.Log("Fire Brigade Called and Call Disconnected.");
        // Trigger your next game objective here
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
        grabInteractable.selectExited.RemoveListener(OnPhoneDropped);
    }
}