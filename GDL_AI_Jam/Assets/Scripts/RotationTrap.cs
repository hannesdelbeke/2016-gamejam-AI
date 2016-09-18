using UnityEngine;
using System.Collections;

public class RotationTrap : TriggerableTrap
{
    public Vector3 RotateAngle;
    public float RotationSpeed;

    void Start()
    {
        _startAngle = transform.eulerAngles;
    }

    override public void ToggleState(bool active)
    {
        Debug.Log((active ? "Activating" : "Deactivating") + " Rotational Trap on " + TriggerButtonID);
        
        StartCoroutine(Swivel(active ? RotateAngle : -RotateAngle));
    }

    IEnumerator Swivel(Vector3 rotateTo)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + rotateTo);
        Debug.LogFormat("From: {0} To: {1}", fromAngle, toAngle);
        for (float t = 0; t < 1; t += Time.deltaTime / RotationSpeed)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }


    Vector3 _startAngle;
}
