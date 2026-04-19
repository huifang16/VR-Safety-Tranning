using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class StepManager : MonoBehaviour
{
    public static StepManager Instance;

    [Header("Current Step")]
    public StepState currentStep;

    [Header("UI Event")]
    public UnityEvent<string> OnStepTextChanged = new UnityEvent<string>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetStep(StepState.StartAtHome);
    }

    public void SetStep(StepState newStep)
    {
        currentStep = newStep;

        string message = GetCurrentStepText();

        Debug.Log("Step Changed: " + newStep + " | " + message);

        OnStepTextChanged.Invoke(message);
    }

    public string GetCurrentStepText()
    {
        switch (currentStep)
        {
            case StepState.StartAtHome:
                return "Turn on the TV.";

            case StepState.TVWarning:
                return "Emergency alert detected.";

            case StepState.SmokeAtDoor:
                return "Smoke is coming in. " +
                    "Check the door.";

            case StepState.CannotEscape:
                return "Escape is not safe. " +
                    "Stay inside.";

            case StepState.WetTowel:
                return "Go to the bathroom and wet a towel.";

            case StepState.SealDoorGap:
                return "Use the wet towel to block the door gap.";

            case StepState.CallHelp:
                return "Go to Bedroom. Call emergency services.";

            default:
                return "";
        }
    }


}