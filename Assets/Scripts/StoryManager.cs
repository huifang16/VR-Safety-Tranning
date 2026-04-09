using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;

    public StoryState currentState;
    public bool isDoorClosed = true;

    public UnityEvent<StoryState> OnStateChanged;
    public UnityEvent<string>     OnUIPrompt;
    public UnityEvent<string>     OnPlayerDied;


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
        currentState = state;
        OnStateChanged?.Invoke(state);
        HandleState(state);
    }
    
    void HandleState(StoryState state)
    {
        switch (state)
        {
            case StoryState.LivingRoom:
                OnUIPrompt?.Invoke("Somethign smells like it's burning...\nCheck the kitchen.");
                break;
            // case StoryState.CheckKitchen:
            //     OnUIPrompt?.Invoke("Nothing is cooking here.\nThe smell isn't coming from the kitchen.");
            //     break;
            case StoryState.SmellFromDoor:
                VFXManager.Instance.ShowSmoke("FrontDoor");
                OnUIPrompt?.Invoke("Wait - the smoke is not coming from the kitchen, the smoke is coming from front door!");
                break;
            case StoryState.TouchTest:
                OnUIPrompt?.Invoke("Do a touch test.\nCheck if the door handle feels hot.");
                break;
            // case StoryState.OpenDoor:
            //     OnUIPrompt?.Invoke("The hallway is hazy with smoke.\nYou can see the green \"Keluar\" sign ahead");
            //     break;
             case StoryState.InvestigateNeighbour:
                VFXManager.Instance.DisableSmoke("FrontDoor");
                VFXManager.Instance.ShowSmoke("NeighbourRoom");
                VFXManager.Instance.ShowSmoke("HallwayLeft");
                VFXManager.Instance.ShowSmoke("HallwayRight");
                VFXManager.Instance.SetSmokeIntensity("HallwayLeft", 0.3f);
                VFXManager.Instance.SetSmokeIntensity("HallwayRight", 0.3f);
                OnUIPrompt?.Invoke("Smoke is coming from your neighbour's door.\nDo you investigate?");
                break;
            case StoryState.NeighbourDead:
                VFXManager.Instance.ShowFire("NeighbourRoom");
                VFXManager.Instance.SetSmokeIntensity("HallwayLeft", 0.7f);
                VFXManager.Instance.SetSmokeIntensity("HallwayRight", 0.9f);
                OnUIPrompt?.Invoke("The fire is far too big to fight.\nRun — now!");
                break;
            case StoryState.CrawlHallway:
                VFXManager.Instance.SetSmokeIntensity("HallwayLeft", isDoorClosed ? 0.7f:1.0f);
                VFXManager.Instance.SetSmokeIntensity("HallwayRight", isDoorClosed ? 0.9f:1.3f);
                OnUIPrompt?.Invoke("The smoke is thick and choking.\nStay low — crawl to the stairwell.");
                
                PlayerChokeManager.Instance?.BeginCrawlHallway(isDoorClosed);
                break;
            case StoryState.Stairwell1:
                OnUIPrompt?.Invoke("Smoke is small.\nMaybe we can go through?");
                break;
            case StoryState.Stairwell1Dead:
                VFXManager.Instance.ShowSmoke("Stairwell1");
                VFXManager.Instance.SetSmokeIntensity("Stairwell1", 0.3f);
                OnUIPrompt?.Invoke("Heavy smoke and heat below.\nSeek an alternative route?");
                break;
            case StoryState.Backtrack:
                VFXManager.Instance.SetSmokeIntensity("Stairwell1", 1.0f);
                OnUIPrompt?.Invoke("Smoke is too heavy. Find the other stairwell!!");
                break;
            case StoryState.Stairwell2:
                OnUIPrompt?.Invoke("This stairwell seems clear... keep moving.");
                VFXManager.Instance.ShowSmoke("Stairwell2");
                VFXManager.Instance.StartSmokeRamp("Stairwell2");
                break;
            case StoryState.SafeZone:
                OnUIPrompt?.Invoke("You successfully escaped!");
                // VFXManager.Instance.ShowOutdoorChaos();
                break;
            case StoryState.PlayerDead:
                // UIManager handles death screen
                break;
        }
        Debug.Log("Story State Change: " + state);
    }

    

    // Inspector-callable wrappers
    public void GoToLivingRoom()           => SetState(StoryState.LivingRoom);
    public void GoToCheckKitchen()         => SetState(StoryState.CheckKitchen);
    public void GoToSmellFromDoor()        => SetState(StoryState.SmellFromDoor);
    public void GoToTouchTest()            => SetState(StoryState.TouchTest);
    public void GoToOpenDoor()             => SetState(StoryState.OpenDoor);
    public void GoToInvestigateNeighbour() => SetState(StoryState.InvestigateNeighbour);
    public void GoToNeighbourDead()        => SetState(StoryState.NeighbourDead);
    public void GoToCrawlHallway()         => SetState(StoryState.CrawlHallway);
    public void GoToStairwell1()           => SetState(StoryState.Stairwell1);
    public void GoToStairwell1Dead()       => SetState(StoryState.Stairwell1Dead);
    public void GoToBacktrack()            => SetState(StoryState.Backtrack);
    public void GoToStairwell2()           => SetState(StoryState.Stairwell2);
    public void GoToSafeZone()             => SetState(StoryState.SafeZone);
    public void GoToPlayerDead()           => SetState(StoryState.PlayerDead);
}
