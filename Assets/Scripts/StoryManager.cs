using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    

    public UnityEvent<string> OnUIPrompt;


    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        SetState(StoryState.LivingRoom);
    }
    
    public void SetState(StoryState state)
    {
        HandleState(state);
        
    }
    
    void HandleState(StoryState state)
    {
        switch (state)
        {
            case StoryState.LivingRoom:
                OnUIPrompt?.Invoke("Somethign smells like it's burning...\nCheck the kitchen.");
                break;
            case StoryState.CheckKitchen:
                OnUIPrompt?.Invoke("Nothing is cooking here.\nThe smell isn't coming from the kitchen.");
                break;
            case StoryState.SmellFromDoor:
                VFXManager.Instance.ShowSmoke("FrontDoor");
                OnUIPrompt?.Invoke("Wait - smoke is seeping through the gap at the bottom of the door!");
                break;
            case StoryState.TouchTest:
                OnUIPrompt?.Invoke("Do a touch test.\nCheck if the door handle feels hot.");
                break;
            case StoryState.OpenDoor:
                VFXManager.Instance.ShowSmoke("Hallway");
                OnUIPrompt?.Invoke("The hallway is hazy with smoke.\nYou can see the green \"Keluar\" sign ahead");
                break;
             case StoryState.InvestigateNeighbour:
                OnUIPrompt?.Invoke("Smoke is coming from your neighbour's door.\nDo you investigate?");
                break;
            case StoryState.NeighbourDead:
                VFXManager.Instance.ShowFire("NeighbourRoom");
                OnUIPrompt?.Invoke("The fire is far too big to fight.\nRun — now!");
                break;
            case StoryState.CrawlHallway:
                VFXManager.Instance.SetSmokeIntensity("Hallway", 0.9f);
                OnUIPrompt?.Invoke("The smoke is thick and choking.\nStay low — crawl to the stairwell.");
                break;
            case StoryState.Stairwell1:
                OnUIPrompt?.Invoke("Smoke is small.\nMaybe we can go through?");
                break;
            case StoryState.Stairwell1Dead:
                VFXManager.Instance.SetSmokeIntensity("Stairwell1", 1.5f);
                OnUIPrompt?.Invoke("Heavy smoke and heat below.\nSeek an alternative route?");
                break;
            case StoryState.Backtrack:
                OnUIPrompt?.Invoke("Find the other stairwell.");
                break;
            case StoryState.Stairwell2:
                OnUIPrompt?.Invoke("This stairwell seems clear... keep moving.");
                VFXManager.Instance.StartSmokeRamp("Stairwell2");
                break;
            case StoryState.SafeZone:
                OnUIPrompt?.Invoke("You successfully escaped!");
                VFXManager.Instance.ShowOutdoorChaos();
                break;
            case StoryState.PlayerDead:
                // UIManager handles death screen
                break;
        }
    }

}
