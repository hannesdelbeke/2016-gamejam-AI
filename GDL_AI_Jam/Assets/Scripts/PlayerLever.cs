using UnityEngine;
using System.Collections.Generic;

public class PlayerLever : MonoBehaviour
{
    public int LeverID;
    public GameObject[] ToggleVisibility;

    void Start()
    {
        Door[] doors = FindObjectsOfType<Door>();
        _matchingDoors = new List<Door>();
        foreach (Door door in doors)
        {
            if (door.LeverType == LeverID)
            {
                _matchingDoors.Add(door);
            }
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Player other = otherCollider.GetComponent<Player>();
        if (other != null)
            OpenDoors(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            Player other = collision.contacts[0].otherCollider.GetComponent<Player>();
            if (other != null)
                OpenDoors(other);
        }
    }

    void OpenDoors(Player player)
    {
        foreach (Door door in _matchingDoors)
            door.Open(LeverID);
        if (!_triggered)
        {
            foreach (GameObject GO in ToggleVisibility)
            {
                GO.SetActive(!GO.activeSelf);
            }
            _triggered = true;
        }
    }

    bool _triggered = false;
    List<Door> _matchingDoors;
}
