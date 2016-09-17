using UnityEngine;
using System.Collections;

public class Keycard : MonoBehaviour
{
    public int KeyType = 1;

    void Start()
    {
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Player other = otherCollider.GetComponent<Player>();
        if (other != null)
            GiveKey(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i=0; i<collision.contacts.Length; i++)
        {
            Player other = collision.contacts[0].otherCollider.GetComponent<Player>();
            if (other != null)
                GiveKey(other);
        }
    }

    void GiveKey(Player player)
    {
        if (!player.KeysHeld.ContainsKey(KeyType))
            player.GiveKey(KeyType, this);
        gameObject.SetActive(false);
    }
}
