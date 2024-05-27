using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InteractableElement : MonoBehaviour
{
    // control UI
    [SerializeField] protected GameObject interactUI;

    // whether or not other items / interactions block this one (i.e. keys)
    [SerializeField] private bool isLocked;
    public bool IsLocked 
    {
        get { return isLocked; } 
        set { isLocked = value; } 
    }

    // used by enemies to start pathing to waypoints
    public Action triggerAction;

    public void Interact(GameObject player)
    {
        if (IsLocked)
        {
            Debug.Log("Could not use, locked");
            return;
        }
        else
        {
            InteractBehaviour(player);
            triggerAction?.Invoke();
        }
    }

    public abstract void InteractBehaviour(GameObject player);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            interactUI.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            interactUI.SetActive(false);
        }
    }
}
