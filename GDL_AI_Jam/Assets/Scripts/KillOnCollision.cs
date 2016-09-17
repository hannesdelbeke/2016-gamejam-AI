using UnityEngine;
using System.Collections;

public class KillOnCollision : MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Player other = otherCollider.GetComponent<Player>();
        if (other != null)
            KillPlayer(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i=0; i<collision.contacts.Length; i++)
        {
            Player other = collision.contacts[0].otherCollider.GetComponent<Player>();
            if (other != null)
                KillPlayer(other);
        }
    }

    void KillPlayer(Player player)
    {
        player.Kill();
    }
}
