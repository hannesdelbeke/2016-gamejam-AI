using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    public Text SpawnMessageLabel;
    public Player PlayerPrefab;
    public bool CanRespawn = false;
    public float TimeToRespawn = 5f;

    void Start()
    {
        _players = new Player[4];
        _respawnTimers = new float[4];
        
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
            if (_players[i] == null && _respawnTimers[i] <= 0)
            {
                if (Input.GetAxisRaw("A_" + (i+1)) > 0.5)
                {
                    SpawnPlayer(i);
                    UpdateLabels();
                }
            }
        }

        bool IsAnyoneRespawning = false;
        for (int i = 1; i < 4; i++)
            if (_respawnTimers[i] > 0)
            {
                _respawnTimers[i] -= Time.deltaTime;
                IsAnyoneRespawning = true;
            }
        if (IsAnyoneRespawning)
            UpdateLabels();
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
                    _respawnTimers[i] = TimeToRespawn;
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
            if (_players[i] == null && _respawnTimers[i] <= 0)
            {
                msg += "Player " + (i+1) + ": Press A to to enter level\n";
            }
            else if (_players[i] == null && _respawnTimers[i] > 0)
            {
                msg += "Player " + (i + 1) + ": Respawn in " + Mathf.CeilToInt(_respawnTimers[i]) + " seconds\n";
            }
        }
        if (SpawnMessageLabel == null)
            Debug.LogError("No GUI Label for spawn messages, please hook one up to the Player Spawner");
        else 
            SpawnMessageLabel.text = msg;
    }

    Player[] _players;
    LevelManager _levelManager;
    float[] _respawnTimers;
}
