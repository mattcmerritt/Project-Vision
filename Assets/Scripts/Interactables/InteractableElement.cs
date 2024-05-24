using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableElement : MonoBehaviour
{
    [SerializeField] protected GameObject interactUI;

    public abstract void Interact(GameObject player);

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
