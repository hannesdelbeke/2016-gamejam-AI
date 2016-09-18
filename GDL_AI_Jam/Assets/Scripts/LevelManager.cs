using UnityEngine;

class LevelManager : MonoBehaviour
{
    void Start()
    {
        _playersSpawned = new bool[4];
    }

    public void PlayerSpawned(int player)
    {
        _playersSpawned[player] = true;
        _activePlayers++;
    }

    public void PlayerKilled(int player)
    {
        _activePlayers--;
    }

    int _activePlayers = 0;
    bool[] _playersSpawned;
}
