using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableElement
{
    // details for placing the player at the door endpoint
    [SerializeField] private Door connectedDoor;
    [SerializeField] private Vector3 targetLocation;

    public override void InteractBehaviour(GameObject player)
    {
        player.transform.position = connectedDoor.targetLocation;
    }
}
