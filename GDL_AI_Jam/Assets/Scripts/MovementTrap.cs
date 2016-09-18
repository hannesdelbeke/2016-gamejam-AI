using UnityEngine;
using System.Collections;

public class MovementTrap : TriggerableTrap
{
    public Vector3 MoveOffset;
    public float MovementSpeed;

    void Start()
    {
        _startPos = transform.position;
    }

    override public void ToggleState(bool active)
    {
        Debug.Log((active ? "Activating" : "Deactivating") + " Sliding Trap on " + TriggerButtonID);
        
        StartCoroutine(Slide(active ? MoveOffset : -MoveOffset));
    }

    IEnumerator Slide(Vector3 moveOffset)
    {
        Vector3 fromPos = transform.position;
        for (float t = 0; t < 1; t += Time.deltaTime / MovementSpeed)
        {
            transform.position = Vector3.Lerp(fromPos, moveOffset, t);
            yield return null;
        }
    }


    Vector3 _startPos;
}
