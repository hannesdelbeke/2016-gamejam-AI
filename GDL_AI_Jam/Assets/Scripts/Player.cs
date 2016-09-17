using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool CanMove;
    public int ControllerNumber;

    void Start()
    {
        CanMove = true;
        _rb = GetComponent<Rigidbody>();

        //add random jitter so they don't all spawn on top of one another
        this.transform.position = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
    }

    void Update()
    {
        if (CanMove)
        {
            float xInp = Input.GetAxis(_xAxisName + ControllerNumber);
            float yInp = Input.GetAxis(_yAxisName + ControllerNumber);
            Vector3 dir = new Vector3(xInp, 0, -yInp);
            _rb.AddForce(dir * 50);
        }
    }

    public void Kill()
    {
        PlayerSpawner spawner = FindObjectOfType<PlayerSpawner>();
        spawner.PlayerKilled(this);
    }

    string _xAxisName = "L_XAxis_";
    string _yAxisName = "L_YAxis_";
    Rigidbody _rb;
}
