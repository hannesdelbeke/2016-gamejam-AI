using UnityEngine;
using System.Collections;

public class TriggerableTrap : MonoBehaviour
{
    [Tooltip("A, B, X, Y, LB or RB")]
    public string TriggerButtonID;

    public float CooldownSeconds = 4;
    public float ActivationTime = 2;

    void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0)
                ReEnable();
        }

        if (_active > 0)
        {
            _active -= Time.deltaTime;
            if (_active <= 0)
            {
                ToggleState(false);
            }
        }
        else if (_cooldown <= 0)
        {
            //poll controller
            if (Input.GetAxisRaw(TriggerButtonID + "_1") > 0.5)
            {
                _active = ActivationTime;
                _cooldown = CooldownSeconds;
                ToggleState(true);
            }
        }
    }

    public virtual void ToggleState(bool active)
    {
    }

    public virtual void ReEnable()
    {
    }

    protected float _cooldown = 0;
    protected float _active = 0;
}
