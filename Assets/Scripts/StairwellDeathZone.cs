using UnityEngine;

public class StairwellDeathZone : MonoBehaviour
{
    public float timeToDeath = 5f;

    private float _timer = 0f;
    private bool _playerInside = false;

    void Update()
    {
        if (!_playerInside)
            return;

        _timer += Time.deltaTime;

        if (_timer >= timeToDeath)
        {
            StoryManager.Instance?.GoToPlayerDead();
            PlayerDeathSequence.Instance?.TriggerDeath("You where overcome by smoke");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("player enter stairwell death zone");
        _playerInside = true;
        _timer = 0f;

        // Increase choke severity immediately
        PlayerChokeManager.Instance?.BeginExtremeSmoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("player exit stairwell death zone");
        _playerInside = false;

        var choke = PlayerChokeManager.Instance;

        if (choke != null)
        {
            choke.EndExtremeSmoke();
            choke.StopChoking();
        }
    }
}