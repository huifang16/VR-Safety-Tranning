using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace SojaExiles
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public float interactDistance = 5f;

        private XRSimpleInteractable interactable;

        void Start()
        {
            open = false;

            interactable = GetComponent<XRSimpleInteractable>();
            interactable.selectEntered.AddListener(OnTriggerPressed);
        }

        void OnDestroy()
        {
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
            open = true;
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator Closing()
        {
            Debug.Log("Closing door");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}