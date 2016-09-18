using UnityEngine;
using System.Collections;

public class MovementTrapSpike : TriggerableTrap
{
	public float MoveOffset;
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
		StartCoroutine(Slide(active ? MoveOffset : 0 ));
    }

    IEnumerator Slide(float moveOffset)
    {
		Vector3 fromPos = transform.localPosition;
        for (float t = 0; t < 1; t += Time.deltaTime / MovementSpeed)
        {
			transform.localPosition = Vector3.Lerp(fromPos,_startPos + transform.up*moveOffset, t);
            yield return null;
        }
		//if(moveOffset == 0 )
		//transform.localPosition = _startPos;
    }

    Vector3 _startPos;
}
