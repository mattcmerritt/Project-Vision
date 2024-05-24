using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;

    // interaction details
    private InteractableElement currentElement;
    private bool movementLocked;
    public bool MovementLocked
    {
        get { return movementLocked; }
        set { movementLocked = value; }
    }

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

    private void FixedUpdate()
    {
        if (!movementLocked)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Debug.Log("started");
        }
        else if(context.performed)
        {
            // Debug.Log("performed");
            direction = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            // Debug.Log("canceled");
            direction = Vector2.zero;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (currentElement == null) return;

        // handling interact presses in an interact popup
        if (movementLocked)
        {
            if (currentElement is LockedDoor)
            {
                if (context.started)
                {
                    LockedDoor lockedDoor = (LockedDoor)currentElement;
                    lockedDoor.AttemptUnlock(gameObject);
                }
            }
        }
        // handling interact presses outside of popups
        else
        {
            if (context.started)
            {
                // Debug.Log("started");
                Debug.Log("interact pressed");
                currentElement.Interact(gameObject);
            }
            else if (context.performed)
            {
                // Debug.Log("performed");

            }
            else if (context.canceled)
            {
                // Debug.Log("canceled");
            }
        } 
    }

    public int GetIndex()
    {
        return index;
    }
}
