using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private InteractableElement currentElement;

    // storing reference to the last interactable element that the player is close to
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableElement>())
        {
            currentElement = collision.gameObject.GetComponent<InteractableElement>();
        }
    }

    // clearing references to objects if player gets too far away
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableElement>() == currentElement)
        {
            currentElement = null;
        }
    }

    // detect button presses for interactions
    private void Update()
    {
        // TODO: implement a controller interact button
        if (currentElement != null && Input.GetKeyDown(KeyCode.Space))
        {
            currentElement.Interact(gameObject);
        }
    }
}
