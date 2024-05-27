using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : InteractableElement
{
    [SerializeField] private GameObject revealInterface;

    public override void InteractBehaviour(GameObject player)
    {
        interactUI.SetActive(false);
        revealInterface.SetActive(true);
        player.GetComponent<PlayerMovement>().Hidden = true;
        player.GetComponent<PlayerMovement>().MovementLocked = true;
        player.GetComponent<Collider2D>().isTrigger = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void Reveal(GameObject player)
    {
        revealInterface.SetActive(false);
        interactUI.SetActive(true);
        player.GetComponent<PlayerMovement>().Hidden = false;
        player.GetComponent<PlayerMovement>().MovementLocked = false;
        player.GetComponent<Collider2D>().isTrigger = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
