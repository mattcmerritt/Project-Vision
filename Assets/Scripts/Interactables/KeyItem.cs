using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Collectible
{
    [SerializeField] private InteractableElement lockedItem;

    public override void InteractBehaviour(GameObject player)
    {
        base.InteractBehaviour(player);
        if (lockedItem != null)
        {
            lockedItem.IsLocked = false;
        }
    }
}
