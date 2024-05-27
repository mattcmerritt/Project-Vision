using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : InteractableElement
{
    public override void Interact(GameObject player)
    {
        gameObject.SetActive(false);
    }
}
