using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, GetComponent<Rigidbody2D>().velocity.y);
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
        if(context.started)
        {
            // Debug.Log("started");
            Debug.Log("interact pressed");
        }
        else if(context.performed)
        {
            // Debug.Log("performed");
            
        }
        else if(context.canceled)
        {
            // Debug.Log("canceled");
        }
    }

    public int GetIndex()
    {
        return index;
    }
}
