using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float MoveForce = 50f;
    public float RotationSpeed = 5f;
    public bool CanMove;
    public int ControllerNumber;
    public Dictionary<int, Keycard> KeysHeld;

    public PlayerSpawner Spawner { get; set; }

    void Start()
    {
        KeysHeld = new Dictionary<int,Keycard>();
        CanMove = true;
        _rb = GetComponent<Rigidbody>();

        //add random jitter so they don't all spawn on top of one another
        this.transform.position += new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            float xInp = Input.GetAxis(_xAxisName + ControllerNumber);
            float yInp = Input.GetAxis(_yAxisName + ControllerNumber);
            Vector3 dir = new Vector3(xInp, 0, -yInp);
            _rb.AddForce(dir * MoveForce);

            //rotate him to face the direction of travel
            Vector3 velocity = _rb.velocity;
            if (dir.sqrMagnitude > 0.1) //(velocity.sqrMagnitude > 0.01)
            {
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed);
                transform.rotation = newRotation;
            }
        }
    }

    public void Kill()
    {
        Spawner.PlayerKilled(this);
        foreach (Keycard key in KeysHeld.Values)
            key.gameObject.SetActive(true);
    }

    public void GiveKey(int keyType, Keycard keyObject)
    {
        KeysHeld.Add(keyType, keyObject);
    }

    string _xAxisName = "L_XAxis_";
    string _yAxisName = "L_YAxis_";
    Rigidbody _rb;
}
