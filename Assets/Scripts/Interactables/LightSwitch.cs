using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : InteractableElement
{
    // the rooms on this switch
    [SerializeField] private Room[] rooms;

    // the current state of the switch (true is on, false is off)
    [SerializeField] private bool switchState;

    public override void InteractBehaviour(GameObject player)
    {
        switchState = !switchState;
        foreach (Room room in rooms)
        {
            room.LightsOn = switchState;
        }
    }
}
