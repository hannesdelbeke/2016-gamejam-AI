using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public int KeyType = 1;

    void Start()
    {
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Player other = otherCollider.GetComponent<Player>();
        if (other != null)
            Open(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i=0; i<collision.contacts.Length; i++)
        {
            Player other = collision.contacts[0].otherCollider.GetComponent<Player>();
            if (other != null)
                Open(other);
        }
    }

    void Open(Player player)
    {
        if (player.KeysHeld.ContainsKey(KeyType))
            gameObject.SetActive(false);
    }
}
