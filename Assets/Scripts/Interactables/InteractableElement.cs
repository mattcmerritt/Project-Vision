using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableElement : MonoBehaviour
{
    [SerializeField] protected GameObject interactUI;

    [SerializeField] private bool isLocked;
    public bool IsLocked 
    {
        get { return isLocked; } 
        set { isLocked = value; } 
    }

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
