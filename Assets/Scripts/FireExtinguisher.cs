using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class FireExtinguisher : MonoBehaviour
{
    public string extinguisherType;
    public ParticleSystem sprayParticles;

    public XRNode controllerNode = XRNode.RightHand;
    private InputDevice device;

    private bool isHeld = false;
    public bool isSpraying = false;

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(controllerNode);
        sprayParticles.Stop();
    }

    void Update()
    {
        if (!isHeld) return;

        if (!device.isValid)
            device = InputDevices.GetDeviceAtXRNode(controllerNode);

        bool triggerPressed = false;
        bool mousePressed = Input.GetMouseButton(0); // Left click

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool vrTrigger))
        {
            triggerPressed = vrTrigger;
        }


        bool isPressing = triggerPressed || mousePressed;

        if (isPressing)
            StartSpray();
        else
            StopSpray();
    }

    public void StartSpray()
    {
        if (isSpraying) return;

        isSpraying = true;
        sprayParticles.Play();
    }

    public void StopSpray()
    {
        if (!isSpraying) return;

        isSpraying = false;
        sprayParticles.Stop();
    }

    public void OnGrab()
    {
        isHeld = true;
    }

    public void OnRelease()
    {
        isHeld = false;
        StopSpray();
    }
}