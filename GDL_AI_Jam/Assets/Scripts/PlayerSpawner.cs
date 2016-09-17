using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    public Text[] SpawnLabels;
    public Player PlayerPrefab;
    public bool CanRespawn = false;

    void Start()
    {
        _players = new Player[4];
        for (int i = 1; i < 4; i++)
        {
            ShowSpawn(i);
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
                if (Input.GetAxisRaw("A_" + i) > 0.5)
                {
                    SpawnPlayer(i);
                    ClearMessage(i);
                }
            }
        }
    }

    void SpawnPlayer(int i)
    {
        GameObject playerGO = (GameObject)Instantiate(PlayerPrefab.gameObject, this.transform.position, Quaternion.identity);
        _players[i] = playerGO.GetComponent<Player>();
        _players[i].ControllerNumber = i;
    }

    public void PlayerKilled(Player player)
    {
        for (int i = 1; i < 4; i++)
            if (_players[i] == player)
            {
                if (CanRespawn)
                {
                    ShowReSpawn(i);
                    _players[i] = null;
                    Destroy(player.gameObject);
                }
                else
                    player.CanMove = false;
            }
    }

    void ShowSpawn(int i)
    {
        SpawnLabels[i].text = i + ": Press A to enter level";
    }

    void ShowReSpawn(int i)
    {
        SpawnLabels[i].text = i + ": Press A to respawn";
    }

    void ClearMessage(int i)
    {
        SpawnLabels[i].text = "";
    }

    Player[] _players;
}
