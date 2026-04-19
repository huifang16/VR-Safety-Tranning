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
    public AudioClip grabSound;

    [Header("UI")]
    public CompleteUIManager completeUIManager;

    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    private Coroutine callCoroutine;
    private bool callFinished = false;
    private bool uiShown = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnPhonePickedUp);
    }

    void OnPhonePickedUp(SelectEnterEventArgs args)
    {
        if (grabSound != null)
            audioSource.PlayOneShot(grabSound);

        if (!callFinished && callCoroutine == null)
        {
            callCoroutine = StartCoroutine(MakeCallSequence());
        }
    }

    IEnumerator MakeCallSequence()
    {
        if (dialingSound != null)
        {
            audioSource.clip = dialingSound;
            audioSource.Play();
            yield return new WaitForSeconds(dialingSound.length);
        }

        if (fireBrigadeResponse != null)
        {
            audioSource.clip = fireBrigadeResponse;
            audioSource.Play();
            yield return new WaitForSeconds(fireBrigadeResponse.length);
        }

        if (playerResponse != null)
        {
            audioSource.clip = playerResponse;
            audioSource.Play();
            yield return new WaitForSeconds(playerResponse.length);
        }

        if (hangUpBeep != null)
        {
            audioSource.clip = hangUpBeep;
            audioSource.Play();
            yield return new WaitForSeconds(hangUpBeep.length);
        }

        callFinished = true;
        callCoroutine = null;

        Debug.Log("Call finished.");

        ShowCompleteUI();
    }

    void ShowCompleteUI()
    {
        if (uiShown) return;

        uiShown = true;

        if (completeUIManager != null)
            completeUIManager.ShowCompleteUI();

        Debug.Log("Complete UI shown after call finished.");
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
        }
    }
}