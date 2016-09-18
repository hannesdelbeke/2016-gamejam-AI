using UnityEngine;
using UnityEngine.UI;

class LevelManager : MonoBehaviour
{
    public PlayerSpawner PlayerSpawnerReference;
    public GameObject GameOverGUI;
    public Text GameOverGUIWinnerText;
    public bool TESTMODE = false;

    void Start()
    {
        _playersSpawned = new bool[4];
        GameOverGUI.SetActive(false);
    }

    public void PlayerSpawned(int player)
    {
        _playersSpawned[player] = true;
        _activePlayers++;
    }

    public void PlayerKilled(int player)
    {
        _activePlayers--;
        if (_activePlayers == 0)
        {
            ShowGameOver(false);
        }
    }

    public void ShowGameOver(bool PlayerVictory)
    {
        if (TESTMODE)
            return;
        GameOverGUI.SetActive(true);
        GameOverGUIWinnerText.text = PlayerVictory ? "PLAYERS WIN" : "AI WINS";
        PlayerSpawnerReference.gameObject.SetActive(false); //prevent respawns once everyone dies
    }

    int _activePlayers = 0;
    bool[] _playersSpawned;
}
