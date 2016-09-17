using UnityEngine;
using System.Collections;

public class TrapDoor : TriggerableTrap
{
    public GameObject[] ElementsToDisable;

    override public void ToggleState(bool active)
    {
        Debug.Log((active ? "Activating" : "Deactivating") + " Trapdoor on " + TriggerButtonID);
        //hide/show elements
        foreach (GameObject GO in ElementsToDisable)
        {
            GO.SetActive(!active);
        }
    }

    override public void ReEnable()
    {
        //cooldown over
    }
}
