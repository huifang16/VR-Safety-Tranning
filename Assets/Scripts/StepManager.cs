using UnityEngine;

public class StepManager : MonoBehaviour
{
    public StepUI stepUI;

    public ObjectHighlighter tv;
    public ObjectHighlighter towel;
    public ObjectHighlighter sink;
    public ObjectHighlighter door;

    void Start()
    {
        ShowTVStep();
    }

    void TurnOffAll()
    {
        tv.SetHighlight(false);
        towel.SetHighlight(false);
        sink.SetHighlight(false);
        door.SetHighlight(false);
    }

    public void ShowTVStep()
    {
        TurnOffAll();
        stepUI.SetStep("Turn on the TV");
        tv.SetHighlight(true);
    }

    public void ShowTowelStep()
    {
        TurnOffAll();
        stepUI.SetStep("Find a towel");
        towel.SetHighlight(true);
    }

    public void ShowSinkStep()
    {
        TurnOffAll();
        stepUI.SetStep("Wet the towel");
        sink.SetHighlight(true);
    }

    public void ShowDoorStep()
    {
        TurnOffAll();
        stepUI.SetStep("Seal the door gap");
        door.SetHighlight(true);
    }

    public void FinishStep()
    {
        TurnOffAll();
        stepUI.SetStep("Wait for rescue");
    }
}