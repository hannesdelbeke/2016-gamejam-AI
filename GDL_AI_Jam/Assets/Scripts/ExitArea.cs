using UnityEngine;
using System.Collections;

public class ExitArea : MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Player other = otherCollider.GetComponent<Player>();
        if (other != null)
            GameOver(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            Player other = collision.contacts[0].otherCollider.GetComponent<Player>();
            if (other != null)
                GameOver(other);
        }
    }

    void GameOver(Player player)
    {
        _levelManager = FindObjectOfType<LevelManager>();
        if (_levelManager == null)
            Debug.LogError("A LevelManager Object must exist in the scene, and be set up with Game Over GUI elements");
        _levelManager.ShowGameOver(true);
    }

    LevelManager _levelManager;
}
