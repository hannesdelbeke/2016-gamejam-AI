using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    public Text SpawnMessageLabel;
    public Player PlayerPrefab;
    public bool CanRespawn = false;

    void Start()
    {
        _players = new Player[4];
        
        _levelManager = FindObjectOfType<LevelManager>();
        if (_levelManager == null)
            Debug.LogError("A LevelManager Object must exist in the scene, and be set up with Game Over GUI elements");

        for (int i = 1; i < 4; i++)
        {
            UpdateLabels();
            _players[i] = null;
        }
    }

    void Update()
    {
        //check for absent players pushing A
        for (int i = 1; i < 4; i++)
        {
            if (_players[i] == null)
            {
                if (Input.GetAxisRaw("A_" + (i+1)) > 0.5)
                {
                    SpawnPlayer(i);
                    UpdateLabels();
                }
            }
        }
    }

    void SpawnPlayer(int i)
    {
        GameObject playerGO = (GameObject)Instantiate(PlayerPrefab.gameObject, transform.position, Quaternion.identity);
        _players[i] = playerGO.GetComponent<Player>();
        _players[i].ControllerNumber = i + 1;
        _players[i].Spawner = this;
        _levelManager.PlayerSpawned(i);
    }

    public void PlayerKilled(Player player)
    {
        for (int i = 1; i < 4; i++)
            if (_players[i] == player)
            {
                _levelManager.PlayerKilled(i);
                if (CanRespawn)
                {
                    UpdateLabels();
                    _players[i] = null;
                    Destroy(player.gameObject);
                }
                else
                {
                    player.CanMove = false;
                    player.gameObject.SetActive(false);
                }
            }
    }

    void UpdateLabels()
    {
        string msg = "";
        for (int i = 1; i < 4; i++)
        {
            if (_players[i] == null)
            {
                msg += "Player " + (i+1) + ": Press A to to enter level\n";
            }
        }
        if (SpawnMessageLabel == null)
            Debug.LogError("No GUI Label for spawn messages, please hook one up to the Player Spawner");
        else 
            SpawnMessageLabel.text = msg;
    }

    Player[] _players;
    LevelManager _levelManager;
}
