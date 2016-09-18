using UnityEngine;
using System.Collections;

public class MovementTrap : TriggerableTrap
{
    public Vector3 MoveOffset;
    public float MovementSpeed;
	public GameObject particles;

    void Start()
    {
		_startPos = transform.localPosition;
    }

    override public void ToggleState(bool active)
    {
        Debug.Log((active ? "Activating" : "Deactivating") + " Sliding Trap on " + TriggerButtonID);
		if (particles)
		particles.SetActive (active);
		StartCoroutine(Slide(active ? MoveOffset : new Vector3(0,0,0)));
    }

    IEnumerator Slide(Vector3 moveOffset)
    {
		Vector3 fromPos = transform.localPosition;
        for (float t = 0; t < 1; t += Time.deltaTime / MovementSpeed)
        {
			transform.localPosition = Vector3.Lerp(fromPos,_startPos +  moveOffset, t);
            yield return null;
        }
		transform.localPosition = _startPos + moveOffset;
    }

    Vector3 _startPos;
}
