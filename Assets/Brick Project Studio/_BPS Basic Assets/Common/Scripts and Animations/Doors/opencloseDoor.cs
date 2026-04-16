using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace SojaExiles
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    [RequireComponent(typeof(AudioSource))]
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public float interactDistance = 5f;

        [Header("Door Sound")]
        public AudioSource doorAudioSource;
        public AudioClip openSound;
        public AudioClip closeSound;

        private XRSimpleInteractable interactable;

        void Start()
        {
            open = false;

            interactable = GetComponent<XRSimpleInteractable>();
            interactable.selectEntered.AddListener(OnTriggerPressed);

            if (doorAudioSource == null)
                doorAudioSource = GetComponent<AudioSource>();
        }

        void OnDestroy()
        {
            if (interactable != null)
                interactable.selectEntered.RemoveListener(OnTriggerPressed);
        }

        private void OnTriggerPressed(SelectEnterEventArgs args)
        {
            float dist = Vector3.Distance(args.interactorObject.transform.position, transform.position);
            if (dist > interactDistance) return;

            if (!open)
                StartCoroutine(Opening());
            else
                StartCoroutine(Closing());
        }

        IEnumerator Opening()
        {
            Debug.Log("Opening door");
            openandclose.Play("Opening");

            if (doorAudioSource != null && openSound != null)
                doorAudioSource.PlayOneShot(openSound);

            open = true;
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator Closing()
        {
            Debug.Log("Closing door");
            openandclose.Play("Closing");

            if (doorAudioSource != null && closeSound != null)
                doorAudioSource.PlayOneShot(closeSound);

            open = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}